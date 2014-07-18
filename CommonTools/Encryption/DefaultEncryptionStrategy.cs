#region - Using Statements -

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace CommonTools.Encryption
{
    internal sealed class DefaultEncryptionStrategy : EncryptionStrategyBase
    {

        #region - Constants -

        private const int BLOCK_BIT_SIZE = 128;
        private const int KEY_BIT_SIZE = 256;

        private const int SALT_BIT_SIZE = 64;
        private const int ITERATIONS = 10000;
        private const int MIN_PASSWORD_LENGTH = 6;

        private const int BITS_PER_BYTE = 8;

        #endregion

        #region - Constructor -

        public DefaultEncryptionStrategy()
            : base()
        {
        }

        #endregion

        #region - Public Methods -

        public override byte[] Encrypt(byte[] input, string password, string nonSecretKey)
        {
            if (string.IsNullOrWhiteSpace(password) || password.Length < MIN_PASSWORD_LENGTH)
            {
                throw new ArgumentException(string.Format("Must have a password of at least {0} characters.", MIN_PASSWORD_LENGTH), "password");
            }

            if (input == null || input.Length <= 0)
            {
                throw new ArgumentException("Input message is required.", "input");
            }

            byte[] nonSecretPayload = GetNonSecretPayload(nonSecretKey);
            byte[] payload = new byte[((SALT_BIT_SIZE / BITS_PER_BYTE) * 2) + nonSecretPayload.Length];

            Array.Copy(nonSecretPayload, payload, nonSecretPayload.Length);
            int payloadIndex = nonSecretPayload.Length;

            byte[] cryptKey;
            byte[] authKey;

            using (Rfc2898DeriveBytes generator = new Rfc2898DeriveBytes(password, SALT_BIT_SIZE / BITS_PER_BYTE, ITERATIONS))
            {
                byte[] salt = generator.Salt;
                cryptKey = generator.GetBytes(KEY_BIT_SIZE / BITS_PER_BYTE);

                Array.Copy(salt, 0, payload, payloadIndex, salt.Length);
                payloadIndex += salt.Length;
            }

            using (Rfc2898DeriveBytes generator = new Rfc2898DeriveBytes(password, SALT_BIT_SIZE / BITS_PER_BYTE, ITERATIONS))
            {
                byte[] salt = generator.Salt;
                authKey = generator.GetBytes(KEY_BIT_SIZE / BITS_PER_BYTE);

                Array.Copy(salt, 0, payload, payloadIndex, salt.Length);
            }

            return Encrypt(input, cryptKey, authKey, payload);
        }

        public override byte[] Decrypt(byte[] input, string password, string nonSecretKey)
        {
            if (string.IsNullOrWhiteSpace(password) || password.Length < MIN_PASSWORD_LENGTH)
            {
                throw new ArgumentException(string.Format("Must have a password of at least {0} characters.", MIN_PASSWORD_LENGTH), "password");
            }

            if (input == null || input.Length <= 0)
            {
                throw new ArgumentException("Encrypted message is required.", "input");
            }

            byte[] cryptSalt = new byte[SALT_BIT_SIZE / BITS_PER_BYTE];
            byte[] authSalt = new byte[SALT_BIT_SIZE / BITS_PER_BYTE];
            byte[] nonSecretPayload = GetNonSecretPayload(nonSecretKey);

            Array.Copy(input, nonSecretPayload.Length, cryptSalt, 0, cryptSalt.Length);
            Array.Copy(input, nonSecretPayload.Length + cryptSalt.Length, authSalt, 0, authSalt.Length);

            byte[] cryptKey;
            byte[] authKey;

            using (Rfc2898DeriveBytes generator = new Rfc2898DeriveBytes(password, cryptSalt, ITERATIONS))
            {
                cryptKey = generator.GetBytes(KEY_BIT_SIZE / BITS_PER_BYTE);
            }

            using (Rfc2898DeriveBytes generator = new Rfc2898DeriveBytes(password, authSalt, ITERATIONS))
            {
                authKey = generator.GetBytes(KEY_BIT_SIZE / BITS_PER_BYTE);
            }

            int nonSecretPayloadLength = cryptSalt.Length + authSalt.Length + nonSecretPayload.Length;
            return Decrypt(input, cryptKey, authKey, nonSecretPayloadLength);
        }

        #endregion

        #region - Private Methods -

        private byte[] Encrypt(byte[] secretMessage, byte[] cryptKey, byte[] authKey, byte[] nonSecretPayload = null)
        {
            if (cryptKey == null || cryptKey.Length != (KEY_BIT_SIZE / BITS_PER_BYTE))
            {
                throw new ArgumentException(string.Format("Key needs to be {0} bit.", KEY_BIT_SIZE), "cryptKey");
            }

            if (authKey == null || authKey.Length != (KEY_BIT_SIZE / BITS_PER_BYTE))
            {
                throw new ArgumentException(string.Format("Key needs to be {0} bit.", KEY_BIT_SIZE), "authKey");
            }

            if (secretMessage == null || secretMessage.Length <= 0)
            {
                throw new ArgumentException("Secret message is required.");
            }

            nonSecretPayload = nonSecretPayload ?? new byte[] { };

            byte[] cipherText;
            byte[] iv;

            using (AesManaged aes = GetAesEncryptor())
            {
                aes.GenerateIV();
                iv = aes.IV;

                using (ICryptoTransform encryptor = aes.CreateEncryptor(cryptKey, iv))
                {
                    using (MemoryStream cipherStream = new MemoryStream())
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(cipherStream, encryptor, CryptoStreamMode.Write))
                        {
                            using (BinaryWriter binaryWriter = new BinaryWriter(cryptoStream))
                            {
                                binaryWriter.Write(secretMessage);
                            }
                        }

                        cipherText = cipherStream.ToArray();
                    }
                }
            }

            byte[] result = null;
            using (HMACSHA256 hmac = new HMACSHA256(authKey))
            {
                using (MemoryStream encryptedStream = new MemoryStream())
                {
                    using (BinaryWriter binaryWriter = new BinaryWriter(encryptedStream))
                    {
                        // Prepend non-secret payload if any
                        binaryWriter.Write(nonSecretPayload);
                        // Prepend IV
                        binaryWriter.Write(iv);
                        // Write cipher text
                        binaryWriter.Write(cipherText);
                        binaryWriter.Flush();

                        // Authenticate all data
                        byte[] tag = hmac.ComputeHash(encryptedStream.ToArray());
                        binaryWriter.Write(tag);
                    }

                    result = encryptedStream.ToArray();
                }
            }

            return result;
        }

        private byte[] Decrypt(byte[] encryptedMessage, byte[] cryptKey, byte[] authKey, int nonSecretPayloadLength = 0)
        {
            if (cryptKey == null || cryptKey.Length != (KEY_BIT_SIZE / BITS_PER_BYTE))
            {
                throw new ArgumentException(string.Format("Crypt Key needs to be {0} bit.", KEY_BIT_SIZE), "cryptKey");
            }

            if (authKey == null || authKey.Length != (KEY_BIT_SIZE / BITS_PER_BYTE))
            {
                throw new ArgumentException(string.Format("Auth Key needs to be {0} bit.", KEY_BIT_SIZE), "authKey");
            }

            if (encryptedMessage == null || encryptedMessage.Length <= 0)
            {
                throw new ArgumentException("Encrypted message is required.");
            }

            byte[] result = null;
            using (HMACSHA256 hmac = new HMACSHA256(authKey))
            {
                byte[] sentTag = new byte[hmac.HashSize / BITS_PER_BYTE];
                byte[] calculatedTag = hmac.ComputeHash(encryptedMessage, 0, encryptedMessage.Length - sentTag.Length);
                int ivLength = (BLOCK_BIT_SIZE / BITS_PER_BYTE);

                if (encryptedMessage.Length < sentTag.Length + nonSecretPayloadLength + ivLength)
                {
                    return null;
                }

                Array.Copy(encryptedMessage, encryptedMessage.Length - sentTag.Length, sentTag, 0, sentTag.Length);

                int compare = 0;
                for (int i = 0; i < sentTag.Length; i++)
                {
                    compare |= sentTag[i] ^ calculatedTag[i];
                }

                if (compare != 0)
                {
                    return null;
                }

                using (AesManaged aes = GetAesEncryptor())
                {
                    byte[] iv = new byte[ivLength];
                    Array.Copy(encryptedMessage, nonSecretPayloadLength, iv, 0, iv.Length);

                    using (ICryptoTransform decryptor = aes.CreateDecryptor(cryptKey, iv))
                    {
                        using (MemoryStream plainTextStream = new MemoryStream())
                        {
                            using (CryptoStream decryptorStream = new CryptoStream(plainTextStream, decryptor, CryptoStreamMode.Write))
                            {
                                using (BinaryWriter binaryWriter = new BinaryWriter(decryptorStream))
                                {
                                    binaryWriter.Write(encryptedMessage, nonSecretPayloadLength + iv.Length, encryptedMessage.Length - nonSecretPayloadLength - iv.Length - sentTag.Length);
                                }
                            }

                            result = plainTextStream.ToArray();
                        }
                    }
                }
            }

            return result;
        }

        private AesManaged GetAesEncryptor()
        {
            return new AesManaged()
            {
                KeySize = KEY_BIT_SIZE,
                BlockSize = BLOCK_BIT_SIZE,
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7
            };
        }

        private byte[] GetNonSecretPayload(string nonSecretKey)
        {
            byte[] nonSecretPayload = null;
            if (!string.IsNullOrWhiteSpace(nonSecretKey))
            {
                nonSecretPayload = Encoding.UTF8.GetBytes(nonSecretKey);
            }
            nonSecretPayload = nonSecretPayload ?? new byte[] { };
            return nonSecretPayload;
        }

        #endregion

    }
}

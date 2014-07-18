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
    internal abstract class EncryptionStrategyBase
    {

        #region - Constructor -

        public EncryptionStrategyBase()
        {
        }

        #endregion

        #region - Public Methods -

        public abstract byte[] Encrypt(byte[] input, string password, string nonSecretKey);

        public abstract byte[] Decrypt(byte[] input, string password, string nonSecretKey);

        public virtual byte[] EncryptFromUTF8(string input, string password, string nonSecretKey)
        {
            byte[] messageBytes = Encoding.UTF8.GetBytes(input);
            return Encrypt(messageBytes, password, nonSecretKey);
        }

        public virtual string DecryptToUTF8(byte[] encrypted, string password, string nonSecretKey)
        {
            byte[] decrypted = Decrypt(encrypted, password, nonSecretKey);
            return Encoding.UTF8.GetString(decrypted);
        }

        public virtual string EncryptToBase64String(string input, string password, string nonSecretKey)
        {
            byte[] encrypted = EncryptFromUTF8(input, password, nonSecretKey);
            return Convert.ToBase64String(encrypted);
        }

        public virtual string DecryptFromBase64String(string input, string password, string nonSecretKey)
        {
            byte[] encrypted = Convert.FromBase64String(input);
            return DecryptToUTF8(encrypted, password, nonSecretKey);
        }

        #endregion

    }
}

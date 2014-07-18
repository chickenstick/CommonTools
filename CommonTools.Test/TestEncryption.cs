#region - Using Statements -

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using CommonTools.Encryption;

#endregion

namespace CommonTools.Test
{
    [TestClass]
    public class TestEncryption
    {

        #region - Public Methods -

        [TestMethod]
        public void TestEncryptionWithNonSecretKey()
        {
            const string PASSWORD = "password123";
            const string NON_SECRET_KEY = "NDMT1US*R";
            const string ORIGINAL_STRING = "Hello, world!";

            EncryptionHelper helper = new EncryptionHelper(EncryptionStrategy.Default);
            byte[] utf8 = Encoding.UTF8.GetBytes(ORIGINAL_STRING);
            byte[] encryptedBytes = helper.Encrypt(utf8, PASSWORD, NON_SECRET_KEY);

            byte[] decryptedBytes = helper.Decrypt(encryptedBytes, PASSWORD, NON_SECRET_KEY);
            string decrypted = Encoding.UTF8.GetString(decryptedBytes);

            Assert.AreEqual(ORIGINAL_STRING, decrypted);
        }

        [TestMethod]
        public void TestEncryptionWithoutNonSecretKey()
        {
            const string PASSWORD = "password123";
            const string ORIGINAL_STRING = "Hello, world!";

            EncryptionHelper helper = new EncryptionHelper(EncryptionStrategy.Default);
            byte[] utf8 = Encoding.UTF8.GetBytes(ORIGINAL_STRING);
            byte[] encryptedBytes = helper.Encrypt(utf8, PASSWORD, null);

            byte[] decryptedBytes = helper.Decrypt(encryptedBytes, PASSWORD, null);
            string decrypted = Encoding.UTF8.GetString(decryptedBytes);

            Assert.AreEqual(ORIGINAL_STRING, decrypted);
        }

        [TestMethod]
        public void TestStringEncryptionWithNonSecretKey()
        {
            const string PASSWORD = "password123";
            const string NON_SECRET_KEY = "NDMT1US*R";
            const string ORIGINAL_STRING = "Hello, world!";

            EncryptionHelper helper = new EncryptionHelper(EncryptionStrategy.Default);
            byte[] encryptedBytes = helper.EncryptFromUTF8(ORIGINAL_STRING, PASSWORD, NON_SECRET_KEY);
            string decrypted = helper.DecryptToUTF8(encryptedBytes, PASSWORD, NON_SECRET_KEY);

            Assert.AreEqual(ORIGINAL_STRING, decrypted);
        }

        [TestMethod]
        public void TestStringEncryptionWithoutNonSecretKey()
        {
            const string PASSWORD = "password123";
            const string ORIGINAL_STRING = "Hello, world!";

            EncryptionHelper helper = new EncryptionHelper(EncryptionStrategy.Default);
            byte[] encryptedBytes = helper.EncryptFromUTF8(ORIGINAL_STRING, PASSWORD, null);
            string decrypted = helper.DecryptToUTF8(encryptedBytes, PASSWORD, null);

            Assert.AreEqual(ORIGINAL_STRING, decrypted);
        }

        [TestMethod]
        public void TestBase64StringEncryptionWithNonSecretKey()
        {
            const string PASSWORD = "password123";
            const string NON_SECRET_KEY = "NDMT1US*R";
            const string ORIGINAL_STRING = "Hello, world!";

            EncryptionHelper helper = new EncryptionHelper(EncryptionStrategy.Default);
            string encrypted = helper.EncryptToBase64String(ORIGINAL_STRING, PASSWORD, NON_SECRET_KEY);
            string decrypted = helper.DecryptFromBase64String(encrypted, PASSWORD, NON_SECRET_KEY);

            Assert.AreEqual(ORIGINAL_STRING, decrypted);
        }

        [TestMethod]
        public void TestBase64StringEncryptionWithoutNonSecretKey()
        {
            const string PASSWORD = "password123";
            const string ORIGINAL_STRING = "Hello, world!";

            EncryptionHelper helper = new EncryptionHelper(EncryptionStrategy.Default);
            string encrypted = helper.EncryptToBase64String(ORIGINAL_STRING, PASSWORD, null);
            string decrypted = helper.DecryptFromBase64String(encrypted, PASSWORD, null);

            Assert.AreEqual(ORIGINAL_STRING, decrypted);
        }

        #endregion

    }
}

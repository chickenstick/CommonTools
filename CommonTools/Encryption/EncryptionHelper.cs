#region - Using Statements -

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace CommonTools.Encryption
{
    public sealed class EncryptionHelper
    {

        #region - Fields -
        EncryptionStrategyBase _encryptionStrategy;
        
        #endregion

        #region - Constructor -

        public EncryptionHelper(EncryptionStrategy strategy)
        {
            this.Strategy = strategy;
            _encryptionStrategy = EncryptionStrategyFactory.GetEncryptionStrategy(strategy);
        }

        #endregion

        #region - Properties -

        public EncryptionStrategy Strategy { get; private set; }

        #endregion

        #region - Public Methods -

        public byte[] Encrypt(byte[] input, string password, string nonSecretKey)
        {
            return _encryptionStrategy.Encrypt(input, password, nonSecretKey);
        }

        public byte[] Decrypt(byte[] input, string password, string nonSecretKey)
        {
            return _encryptionStrategy.Decrypt(input, password, nonSecretKey);
        }

        public byte[] EncryptFromUTF8(string input, string password, string nonSecretKey)
        {
            return _encryptionStrategy.EncryptFromUTF8(input, password, nonSecretKey);
        }

        public string DecryptToUTF8(byte[] encrypted, string password, string nonSecretKey)
        {
            return _encryptionStrategy.DecryptToUTF8(encrypted, password, nonSecretKey);
        }

        public string EncryptToBase64String(string input, string password, string nonSecretKey)
        {
            return _encryptionStrategy.EncryptToBase64String(input, password, nonSecretKey);
        }

        public string DecryptFromBase64String(string input, string password, string nonSecretKey)
        {
            return _encryptionStrategy.DecryptFromBase64String(input, password, nonSecretKey);
        }

        #endregion

    }
}

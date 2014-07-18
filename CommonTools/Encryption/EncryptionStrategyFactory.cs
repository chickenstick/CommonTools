#region - Using Statements -

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace CommonTools.Encryption
{
    internal static class EncryptionStrategyFactory
    {

        #region - Public Methods -

        public static EncryptionStrategyBase GetEncryptionStrategy(EncryptionStrategy strategy)
        {
            EncryptionStrategyBase result = null;

            switch (strategy)
            {
                case EncryptionStrategy.Default:
                    result = new DefaultEncryptionStrategy();
                    break;

                default:
                    throw new ArgumentException(string.Format("Could not find an encryption strategy for the value \"{0}\".", strategy), "strategy");
            }

            return result;
        }

        #endregion

    }
}

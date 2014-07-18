#region - Using Statements -

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonTools.Data.DataProviders;

#endregion

namespace CommonTools.Data
{
    public static class DatabaseFactory
    {

        #region - Public Methods -

        public static DataProviderBase GetDatabase(DatabaseType type, string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException("connectionString");
            }

            DataProviderBase provider = null;

            switch (type)
            {
                case DatabaseType.Default:
                case DatabaseType.SqlServer:
                    provider = new SqlServerDataProvider(connectionString);
                    break;

                default:
                    throw new ArgumentException("Unknown database type.", "type");
            }

            return provider;
        }

        #endregion

    }
}

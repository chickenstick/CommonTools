#region - Using Statements -

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

#endregion

namespace CommonTools.Data.DataProviders
{
    public sealed class SqlServerDataProvider : DataProviderBase
    {

        #region - Fields -

        private SqlConnection _connection;

        #endregion

        #region - Constructor -

        public SqlServerDataProvider(string connectionString)
            : base(DatabaseType.SqlServer, connectionString)
        {
        }

        #endregion

        #region - Properties -

        protected override DbConnection Connection
        {
            get
            {
                return _connection;
            }
        }

        #endregion

        #region - Public Methods -

        public override DbCommand GetDbCommand(DataCommand command)
        {
            SqlCommand dbCommand = new SqlCommand(command.CommandText);
            dbCommand.CommandType = command.CommandType;
            dbCommand.CommandTimeout = command.CommandTimeout;
            dbCommand.Connection = _connection;

            foreach (DatabaseParameter dbParam in command.GetParameters())
            {
                dbCommand.Parameters.Add(dbParam.ToSqlParameter());
            }

            return dbCommand;
        }

        #endregion

        #region - Protected Methods -

        protected override void InitializeDbConnection()
        {
            _connection = new SqlConnection(this.ConnectionString);
        }

        protected override void DisposeDbConnection()
        {
            if (_connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }
        }

        #endregion

    }
}

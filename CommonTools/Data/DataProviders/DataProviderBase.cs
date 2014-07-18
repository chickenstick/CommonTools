#region - Using Statements -

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

#endregion

namespace CommonTools.Data.DataProviders
{
    public abstract class DataProviderBase : IDisposable
    {

        #region - Constructor -

        public DataProviderBase(DatabaseType databaseType, string connectionString)
        {
            this.DatabaseType = databaseType;
            this.ConnectionString = connectionString;

            InitializeDbConnection();
        }

        #endregion

        #region - Properties -

        public DatabaseType DatabaseType { get; private set; }

        public string ConnectionString { get; private set; }

        protected abstract DbConnection Connection { get; }

        protected TransactionScope Transaction { get; private set; }

        public ConnectionState ConnectionState
        {
            get
            {
                ConnectionState state = System.Data.ConnectionState.Open;
                if (this.Connection == null)
                {
                    state = System.Data.ConnectionState.Closed;
                }
                else
                {
                    state = this.Connection.State;
                }
                return state;
            }
        }

        public bool IsInTransaction
        {
            get
            {
                return (this.Transaction != null);
            }
        }

        #endregion

        #region - Public Methods -

        public void OpenConnection()
        {
            if (this.Connection != null && this.Connection.State == System.Data.ConnectionState.Closed)
            {
                this.Connection.Open();
            }
        }

        public void CloseConnection()
        {
            if (this.Connection != null)
            {
                this.Connection.Close();
            }
        }

        public void BeginTransaction()
        {
            if (this.Connection != null && this.Connection.State != ConnectionState.Closed)
            {
                throw new InvalidOperationException("The transaction must begin before the connection has been opened.");
            }

            if (this.Transaction != null)
            {
                throw new InvalidOperationException("The transaction has already been started.");
            }

            this.Transaction = new TransactionScope();
        }

        public void CompleteTransaction()
        {
            if (this.Transaction == null)
            {
                throw new InvalidOperationException("A transaction has not been started.");
            }

            this.Transaction.Complete();
            DisposeDbTransaction();
        }

        public void RollBackTransaction()
        {
            if (this.Transaction == null)
            {
                throw new InvalidOperationException("A transaction has not been started.");
            }

            DisposeDbTransaction();
        }

        public abstract DbCommand GetDbCommand(DataCommand command);

        #endregion

        #region - Protected Methods -

        protected abstract void InitializeDbConnection();

        protected abstract void DisposeDbConnection();

        protected void DisposeDbTransaction()
        {
            if (this.Transaction != null)
            {
                this.Transaction.Dispose();
                this.Transaction = null;
            }
        }

        #endregion

        #region - Dispose Methods -

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.Connection != null)
                {
                    DisposeDbConnection();
                }

                if (this.Transaction != null)
                {
                    DisposeDbTransaction();
                }
            }
        }

        #endregion

    }
}

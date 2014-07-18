#region - Using Statements -

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonTools.Data.DataProviders;

#endregion

namespace CommonTools.Data
{
    public abstract class DataCommand
    {

        //#region - Fields -

        //private Dictionary<string, DatabaseParameter> _parameters;

        //#endregion

        #region - Constructor -

        public DataCommand(string commandText, CommandType commandType, int commandTimeout)
        {
            this.CommandText = commandText;
            this.CommandType = commandType;
            this.CommandTimeout = commandTimeout;
            //_parameters = new Dictionary<string, DatabaseParameter>(StringComparer.OrdinalIgnoreCase);
        }

        public DataCommand(string commandText, CommandType commandType)
            : this(commandText, commandType, CommandTimeoutLengths.DefaultTimeout)
        {
        }

        public DataCommand(string commandText)
            : this(commandText, CommandType.StoredProcedure)
        {
        }

        public DataCommand()
            : this(string.Empty)
        {
        }

        #endregion

        #region - Properties -

        public string CommandText { get; set; }

        public CommandType CommandType { get; set; }

        public int CommandTimeout { get; set; }

        //internal List<DatabaseParameter> Parameters
        //{
        //    get
        //    {
        //        return _parameters.Values.ToList();
        //    }
        //}

        //protected DatabaseParameter this[string parameterName]
        //{
        //    get
        //    {
        //        return _parameters[parameterName];
        //    }
        //    set
        //    {
        //        _parameters[parameterName] = value;
        //    }
        //}

        #endregion

        #region - Public Methods -

        public abstract IEnumerable<DatabaseParameter> GetParameters();

        #endregion

        //#region - Protected Methods -

        //protected void AddParameter(DatabaseParameter parameter)
        //{
        //    if (_parameters.ContainsKey(parameter.Name))
        //    {
        //        throw new ArgumentException("The parameter name is already present in the list of parameters.");
        //    }

        //    _parameters.Add(parameter.Name, parameter);
        //}

        //protected void AddParameter(string name, DbType dataType, ParameterDirection direction, bool isNullable, object value)
        //{
        //    DatabaseParameter parameter = new DatabaseParameter(name, dataType, direction, isNullable, value);
        //    AddParameter(parameter);
        //}

        //protected void AddParameter(string name, DbType dataType, ParameterDirection direction, bool isNullable)
        //{
        //    DatabaseParameter parameter = new DatabaseParameter(name, dataType, direction, isNullable);
        //    AddParameter(parameter);
        //}

        //protected void AddParameter(string name, DbType dataType, ParameterDirection direction)
        //{
        //    DatabaseParameter parameter = new DatabaseParameter(name, dataType, direction);
        //    AddParameter(parameter);
        //}

        //protected void AddParameter(string name, DbType dataType)
        //{
        //    DatabaseParameter parameter = new DatabaseParameter(name, dataType);
        //    AddParameter(parameter);
        //}

        //protected void AddParameter(string name)
        //{
        //    DatabaseParameter parameter = new DatabaseParameter(name);
        //    AddParameter(parameter);
        //}

        //protected void AddParameter()
        //{
        //    DatabaseParameter parameter = new DatabaseParameter();
        //    AddParameter(parameter);
        //}

        //protected bool RemoveParameter(string parameterName)
        //{
        //    if (!_parameters.ContainsKey(parameterName))
        //    {
        //        throw new ArgumentException("The parameter name is not present in the dictionary.");
        //    }

        //    return _parameters.Remove(parameterName);
        //}

        //protected bool RemoveParameter(DatabaseParameter parameter)
        //{
        //    return RemoveParameter(parameter.Name);
        //}

        //protected bool HasParameter(string parameterName)
        //{
        //    return _parameters.ContainsKey(parameterName);
        //}

        //#endregion
    }
}

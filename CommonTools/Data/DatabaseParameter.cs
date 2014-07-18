#region - Using Statements -

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace CommonTools.Data
{
    public class DatabaseParameter
    {

        #region - Constructor -

        public DatabaseParameter(string name, object value, DbType? dataType, ParameterDirection? direction, bool? isNullable)
        {
            this.Name = name;
            this.DataType = dataType;
            this.Direction = direction;
            this.IsNullable = isNullable;
            this.Value = value;
            this.SqlDataType = null;
        }

        public DatabaseParameter(string name, object value, DbType? dataType, ParameterDirection? direction)
            : this(name, value, dataType, direction, null)
        {
        }

        public DatabaseParameter(string name, object value, DbType? dataType)
            : this(name, value, dataType, null)
        {
        }

        public DatabaseParameter(string name, object value)
            : this(name, value, null)
        {
        }

        public DatabaseParameter(string name)
            : this(name, null)
        {
        }

        public DatabaseParameter()
            : this(string.Empty)
        {
        }

        #endregion

        #region - Properties -

        public string Name { get; set; }

        public DbType? DataType { get; set; }

        public ParameterDirection? Direction { get; set; }

        public bool? IsNullable { get; set; }

        public object Value { get; set; }

        public SqlDbType? SqlDataType { get; set; }

        #endregion

        #region - Public Methods -

        public virtual SqlParameter ToSqlParameter()
        {
            SqlParameter outParam = new SqlParameter()
            {
                ParameterName = this.Name,
                Value = this.Value
            };

            if (this.SqlDataType.HasValue)
            {
                outParam.SqlDbType = this.SqlDataType.Value;
            }
            else if(this.DataType.HasValue)
            {
                outParam.DbType = this.DataType.Value;
            }

            if (this.Direction.HasValue)
            {
                outParam.Direction = this.Direction.Value;
            }

            if (this.IsNullable.HasValue)
            {
                outParam.IsNullable = this.IsNullable.Value;
            }

            return outParam;
        }

        #endregion

    }
}

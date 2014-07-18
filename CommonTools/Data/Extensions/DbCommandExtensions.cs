#region - Using Statements -

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace CommonTools.Data.Extensions
{
    public static class DbCommandExtensions
    {

        #region - Public Methods -

        public static T GetOutputParameterValue<T>(this DbCommand command, string parameterName)
        {
            return (T)command.Parameters[parameterName].Value;
        }

        public static bool TryGetOutputParameterValue<T>(this DbCommand command, string parameterName, out T value)
        {
            value = default(T);
            bool result = false;

            if (command.Parameters.Contains(parameterName))
            {
                object val = command.Parameters[parameterName].Value;
                if (val == DBNull.Value && !typeof(T).IsValueType)
                {
                    value = default(T);
                    result = true;
                }
                else if (val != DBNull.Value)
                {
                    value = (T)val;
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            else
            {
                result = false;
            }

            return result;
        }

        #endregion

    }
}

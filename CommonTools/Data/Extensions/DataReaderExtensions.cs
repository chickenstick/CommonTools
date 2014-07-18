#region - Using Statements -

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace CommonTools.Data.Extensions
{
    public static class DataReaderExtensions
    {

        #region - Public Methods -

        public static bool GetBoolean(this DbDataReader reader, string name)
        {
            int ordinal = reader.GetOrdinal(name);
            return reader.GetBoolean(ordinal);
        }

        public static bool SafeGetBoolean(this DbDataReader reader, string name)
        {
            int ordinal = reader.GetOrdinal(name);

            if (reader.IsDBNull(ordinal))
            {
                return false;
            }

            return reader.GetBoolean(ordinal);
        }

        public static byte GetByte(this DbDataReader reader, string name)
        {
            int ordinal = reader.GetOrdinal(name);
            return reader.GetByte(ordinal);
        }

        public static byte SafeGetByte(this DbDataReader reader, string name)
        {
            int ordinal = reader.GetOrdinal(name);

            if (reader.IsDBNull(ordinal))
            {
                return 0;
            }

            return reader.GetByte(ordinal);
        }

        public static long GetBytes(this DbDataReader reader, string name, long dataOffset, byte[] buffer, int bufferOffset, int length)
        {
            int ordinal = reader.GetOrdinal(name);
            return reader.GetBytes(ordinal, dataOffset, buffer, bufferOffset, length);
        }

        public static long SafeGetBytes(this DbDataReader reader, string name, long dataOffset, byte[] buffer, int bufferOffset, int length)
        {
            int ordinal = reader.GetOrdinal(name);

            if (reader.IsDBNull(ordinal))
            {
                return 0;
            }

            return reader.GetBytes(ordinal, dataOffset, buffer, bufferOffset, length);
        }

        public static char GetChar(this DbDataReader reader, string name)
        {
            int ordinal = reader.GetOrdinal(name);
            return reader.GetChar(ordinal);
        }

        public static char SafeGetChar(this DbDataReader reader, string name)
        {
            int ordinal = reader.GetOrdinal(name);

            if (reader.IsDBNull(ordinal))
            {
                return default(char);
            }

            return reader.GetChar(ordinal);
        }

        public static long GetChars(this DbDataReader reader, string name, long dataOffset, char[] buffer, int bufferOffset, int length)
        {
            int ordinal = reader.GetOrdinal(name);
            return reader.GetChars(ordinal, dataOffset, buffer, bufferOffset, length);
        }

        public static long SafeGetChars(this DbDataReader reader, string name, long dataOffset, char[] buffer, int bufferOffset, int length)
        {
            int ordinal = reader.GetOrdinal(name);

            if (reader.IsDBNull(ordinal))
            {
                return 0;
            }

            return reader.GetChars(ordinal, dataOffset, buffer, bufferOffset, length);
        }

        public static string GetDataTypeName(this DbDataReader reader, string name)
        {
            int ordinal = reader.GetOrdinal(name);
            return reader.GetDataTypeName(ordinal);
        }

        public static string SafeGetDataTypeName(this DbDataReader reader, string name)
        {
            int ordinal = reader.GetOrdinal(name);

            if (reader.IsDBNull(ordinal))
            {
                return string.Empty;
            }

            return reader.GetDataTypeName(ordinal);
        }

        public static DateTime GetDateTime(this DbDataReader reader, string name)
        {
            int ordinal = reader.GetOrdinal(name);
            return reader.GetDateTime(ordinal);
        }

        public static DateTime SafeGetDateTime(this DbDataReader reader, string name)
        {
            int ordinal = reader.GetOrdinal(name);

            if (reader.IsDBNull(ordinal))
            {
                return DateTime.MinValue;
            }

            return reader.GetDateTime(ordinal);
        }

        public static DateTime? SafeGetNullableDateTime(this DbDataReader reader, string name)
        {
            int ordinal = reader.GetOrdinal(name);

            if (reader.IsDBNull(ordinal))
            {
                return null;
            }

            return reader.GetDateTime(ordinal);
        }

        public static decimal GetDecimal(this DbDataReader reader, string name)
        {
            int ordinal = reader.GetOrdinal(name);
            return reader.GetDecimal(ordinal);
        }

        public static decimal SafeGetDecimal(this DbDataReader reader, string name)
        {
            int ordinal = reader.GetOrdinal(name);

            if (reader.IsDBNull(ordinal))
            {
                return 0.0m;
            }

            return reader.GetDecimal(ordinal);
        }

        public static double GetDouble(this DbDataReader reader, string name)
        {
            int ordinal = reader.GetOrdinal(name);
            return reader.GetDouble(ordinal);
        }

        public static double SafeGetDouble(this DbDataReader reader, string name)
        {
            int ordinal = reader.GetOrdinal(name);

            if (reader.IsDBNull(ordinal))
            {
                return 0.0d;
            }

            return reader.GetDouble(ordinal);
        }

        public static Type GetFieldType(this DbDataReader reader, string name)
        {
            int ordinal = reader.GetOrdinal(name);
            return reader.GetFieldType(ordinal);
        }

        public static Type SafeGetFieldType(this DbDataReader reader, string name)
        {
            int ordinal = reader.GetOrdinal(name);

            if (reader.IsDBNull(ordinal))
            {
                return null;
            }

            return reader.GetFieldType(ordinal);
        }

        public static T GetFieldValue<T>(this DbDataReader reader, string name)
        {
            int ordinal = reader.GetOrdinal(name);
            return reader.GetFieldValue<T>(ordinal);
        }

        public static T SafeGetFieldValue<T>(this DbDataReader reader, string name)
        {
            int ordinal = reader.GetOrdinal(name);

            if (reader.IsDBNull(ordinal))
            {
                return default(T);
            }

            return reader.GetFieldValue<T>(ordinal);
        }

        public static Task<T> GetFieldValueAsync<T>(this DbDataReader reader, string name)
        {
            int ordinal = reader.GetOrdinal(name);
            return reader.GetFieldValueAsync<T>(ordinal);
        }

        public static Task<T> GetFieldValueAsync<T>(this DbDataReader reader, string name, System.Threading.CancellationToken cancellationToken)
        {
            int ordinal = reader.GetOrdinal(name);
            return reader.GetFieldValueAsync<T>(ordinal, cancellationToken);
        }

        public static float GetFloat(this DbDataReader reader, string name)
        {
            int ordinal = reader.GetOrdinal(name);
            return reader.GetFloat(ordinal);
        }

        public static float SafeGetFloat(this DbDataReader reader, string name)
        {
            int ordinal = reader.GetOrdinal(name);

            if (reader.IsDBNull(ordinal))
            {
                return 0.0f;
            }

            return reader.GetFloat(ordinal);
        }

        public static Guid GetGuid(this DbDataReader reader, string name)
        {
            int ordinal = reader.GetOrdinal(name);
            return reader.GetGuid(ordinal);
        }

        public static Guid SafeGetGuid(this DbDataReader reader, string name)
        {
            int ordinal = reader.GetOrdinal(name);

            if (reader.IsDBNull(ordinal))
            {
                return Guid.Empty;
            }

            return reader.GetGuid(ordinal);
        }

        public static Guid? SafeGetNullableGuid(this DbDataReader reader, string name)
        {
            int ordinal = reader.GetOrdinal(name);

            if (reader.IsDBNull(ordinal))
            {
                return null;
            }

            return reader.GetGuid(ordinal);
        }

        public static short GetInt16(this DbDataReader reader, string name)
        {
            int ordinal = reader.GetOrdinal(name);
            return reader.GetInt16(ordinal);
        }

        public static short SafeGetInt16(this DbDataReader reader, string name)
        {
            int ordinal = reader.GetOrdinal(name);

            if (reader.IsDBNull(ordinal))
            {
                return 0;
            }

            return reader.GetInt16(ordinal);
        }

        public static short? SafeGetNullableInt16(this DbDataReader reader, string name)
        {
            int ordinal = reader.GetOrdinal(name);

            if (reader.IsDBNull(ordinal))
            {
                return null;
            }

            return reader.GetInt16(ordinal);
        }

        public static int GetInt32(this DbDataReader reader, string name)
        {
            int ordinal = reader.GetOrdinal(name);
            return reader.GetInt32(ordinal);
        }

        public static int SafeGetInt32(this DbDataReader reader, string name)
        {
            int ordinal = reader.GetOrdinal(name);

            if (reader.IsDBNull(ordinal))
            {
                return 0;
            }

            return reader.GetInt32(ordinal);
        }

        public static int? SafeGetNullableInt32(this DbDataReader reader, string name)
        {
            int ordinal = reader.GetOrdinal(name);

            if (reader.IsDBNull(ordinal))
            {
                return null;
            }

            return reader.GetInt32(ordinal);
        }

        public static long GetInt64(this DbDataReader reader, string name)
        {
            int ordinal = reader.GetOrdinal(name);
            return reader.GetInt64(ordinal);
        }

        public static long SafeGetInt64(this DbDataReader reader, string name)
        {
            int ordinal = reader.GetOrdinal(name);

            if (reader.IsDBNull(ordinal))
            {
                return 0;
            }

            return reader.GetInt64(ordinal);
        }

        public static long? SafeGetNullableInt64(this DbDataReader reader, string name)
        {
            int ordinal = reader.GetOrdinal(name);

            if (reader.IsDBNull(ordinal))
            {
                return null;
            }

            return reader.GetInt64(ordinal);
        }

        public static System.IO.Stream GetStream(this DbDataReader reader, string name)
        {
            int ordinal = reader.GetOrdinal(name);
            return reader.GetStream(ordinal);
        }

        public static System.IO.Stream SafeGetStream(this DbDataReader reader, string name)
        {
            int ordinal = reader.GetOrdinal(name);

            if (reader.IsDBNull(ordinal))
            {
                return null;
            }

            return reader.GetStream(ordinal);
        }

        public static string GetString(this DbDataReader reader, string name)
        {
            int ordinal = reader.GetOrdinal(name);
            return reader.GetString(ordinal);
        }

        public static string SafeGetString(this DbDataReader reader, string name)
        {
            int ordinal = reader.GetOrdinal(name);

            if (reader.IsDBNull(ordinal))
            {
                return null;
            }

            return reader.GetString(ordinal);
        }

        public static System.IO.TextReader GetTextReader(this DbDataReader reader, string name)
        {
            int ordinal = reader.GetOrdinal(name);
            return reader.GetTextReader(ordinal);
        }

        public static System.IO.TextReader SafeGetTextReader(this DbDataReader reader, string name)
        {
            int ordinal = reader.GetOrdinal(name);

            if (reader.IsDBNull(ordinal))
            {
                return null;
            }

            return reader.GetTextReader(ordinal);
        }

        public static object GetValue(this DbDataReader reader, string name)
        {
            int ordinal = reader.GetOrdinal(name);
            return reader.GetValue(ordinal);
        }

        public static object SafeGetValue(this DbDataReader reader, string name)
        {
            int ordinal = reader.GetOrdinal(name);

            if (reader.IsDBNull(ordinal))
            {
                return null;
            }

            return reader.GetValue(ordinal);
        }

        public static T GetValue<T>(this DbDataReader reader, string name)
        {
            int ordinal = reader.GetOrdinal(name);
            return (T)reader.GetValue(ordinal);
        }

        public static T SafeGetValue<T>(this DbDataReader reader, string name)
        {
            int ordinal = reader.GetOrdinal(name);

            if (reader.IsDBNull(ordinal))
            {
                return default(T);
            }

            return (T)reader.GetValue(ordinal);
        }

        public static bool IsDBNull(this DbDataReader reader, string name)
        {
            int ordinal = reader.GetOrdinal(name);
            return reader.IsDBNull(ordinal);
        }

        #endregion

    }
}

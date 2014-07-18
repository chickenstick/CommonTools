#region - Using Statements -

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace CommonTools.Enumerations
{
    /// <summary>
    /// Utility class that can be used to look up attributes of an enum value.
    /// </summary>
    public static class EnumLookup
    {

        #region - Constants -

        private const bool DEFAULT_IGNORE_CASE = false;

        #endregion

        #region - Public Methods -

        /// <summary>
        /// Parses the specified synonym.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="synonym">The synonym.</param>
        /// <param name="ignoreCase">if set to <c>true</c> ignore the case of the synonym.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">synonym</exception>
        public static T Parse<T>(string synonym, bool ignoreCase) where T : struct
        {
            T? result = GetParsedResult<T>(synonym, ignoreCase);

            if (!result.HasValue)
            {
                throw new ArgumentException(string.Format("Could not find an enum value for synonym \"{0}\".", synonym), "synonym");
            }

            return result.Value;
        }

        /// <summary>
        /// Parses the specified synonym.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="synonym">The synonym.</param>
        /// <returns></returns>
        public static T Parse<T>(string synonym) where T : struct
        {
            return Parse<T>(synonym, DEFAULT_IGNORE_CASE);
        }

        /// <summary>
        /// Tries the parse.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="synonym">The synonym.</param>
        /// <param name="ignoreCase">if set to <c>true</c> ignore the case of the synonym.</param>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        public static bool TryParse<T>(string synonym, bool ignoreCase, out T result) where T : struct
        {
            result = default(T);

            T? value = GetParsedResult<T>(synonym, ignoreCase);

            if (value.HasValue)
            {
                result = value.Value;
            }

            return value.HasValue;
        }

        /// <summary>
        /// Tries to parse the synonym.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="synonym">The synonym.</param>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        public static bool TryParse<T>(string synonym, out T result) where T : struct
        {
            result = default(T);
            return TryParse<T>(synonym, DEFAULT_IGNORE_CASE, out result);
        }

        /// <summary>
        /// Gets the synonym.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException"></exception>
        public static string GetSynonym<T>(T value) where T : struct
        {
            EnumDescriptionAttribute attribute = GetAttributeFromValue(value);
            if (attribute == null)
            {
                throw new ArgumentException(string.Format("Could not find a synonym for the enum value \"{0}\".", value));
            }

            return attribute.Synonym;
        }

        /// <summary>
        /// Tries to get the synonym.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <param name="synonym">The synonym.</param>
        /// <returns></returns>
        public static bool TryGetSynonym<T>(T value, out string synonym) where T : struct
        {
            synonym = null;

            EnumDescriptionAttribute attribute = GetAttributeFromValue(value);
            if (attribute != null)
            {
                synonym = attribute.Synonym;
            }

            return attribute != null;
        }

        #endregion

        #region - Private Methods -

        /// <summary>
        /// Gets the parsed result.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="synonym">The synonym.</param>
        /// <param name="ignoreCase">if set to <c>true</c> ignore the case of the synonym.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">T must be an enumerated type.</exception>
        private static T? GetParsedResult<T>(string synonym, bool ignoreCase) where T: struct
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type.");
            }

            T? result = null;
            StringComparison synonymComparer = GetSynonymComparer(ignoreCase);

            MemberInfo[] enumMembers = typeof(T).GetMembers(BindingFlags.Public | BindingFlags.Static);
            foreach (MemberInfo memInfo in enumMembers)
            {
                EnumDescriptionAttribute attribute = memInfo.GetCustomAttribute<EnumDescriptionAttribute>();
                if (attribute != null)
                {
                    if (synonym.Equals(attribute.Synonym, synonymComparer))
                    {
                        result = (T)System.Enum.Parse(typeof(T), memInfo.Name);
                        break;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the synonym comparer.
        /// </summary>
        /// <param name="ignoreCase">if set to <c>true</c> ignore the case of the synonym.</param>
        /// <returns></returns>
        private static StringComparison GetSynonymComparer(bool ignoreCase)
        {
            return ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
        }

        /// <summary>
        /// Gets the attribute from value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">T must be an enumerated type.</exception>
        private static EnumDescriptionAttribute GetAttributeFromValue<T>(T value) where T : struct
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type.");
            }

            string enumName = value.ToString();
            MemberInfo memInfo = typeof(T).GetMember(enumName)[0];
            return memInfo.GetCustomAttribute<EnumDescriptionAttribute>();
        }

        #endregion

    }
}

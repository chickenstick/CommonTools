#region - Using Statements -

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace CommonTools.Serialization
{
    internal static class SerializationStrategyFactory
    {

        #region - Public Methods -

        public static SerializationStrategyBase GetSerializationStrategy(SerializationType type)
        {
            SerializationStrategyBase result = null;

            switch (type)
            {
                case SerializationType.XML:
                    result = new XmlSerializationStrategy();
                    break;

                case SerializationType.Binary:
                    result = new BinarySerializationStrategy();
                    break;

                case SerializationType.JSON:
                    result = new JsonSerializationStrategy();
                    break;

                default:
                    throw new ArgumentException(string.Format("Could not find a serialization strategy for type \"{0}\".", type), "type");
            }

            return result;
        }

        #endregion

    }
}

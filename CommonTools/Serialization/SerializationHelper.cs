#region - Using Statements -

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace CommonTools.Serialization
{
    public sealed class SerializationHelper
    {

        #region - Fields -

        private SerializationStrategyBase _serializationStrategy;

        #endregion

        #region - Constructor -

        public SerializationHelper(SerializationType serializationType)
        {
            this.SerializationType = serializationType;
            _serializationStrategy = SerializationStrategyFactory.GetSerializationStrategy(serializationType);
        }

        #endregion

        #region - Properties -

        public SerializationType SerializationType { get; private set; }

        #endregion

        #region - Public Methods -

        public void Serialize<T>(Stream stream, T obj)
        {
            _serializationStrategy.Serialize(stream, obj);
        }

        public T Deserialize<T>(Stream stream)
        {
            return _serializationStrategy.Deserialize<T>(stream);
        }

        public string SerializeToBase64<T>(T obj)
        {
            return _serializationStrategy.SerializeToBase64(obj);
        }

        public T DeserializeFromBase64<T>(string base64)
        {
            return _serializationStrategy.DeserializeFromBase64<T>(base64);
        }

        #endregion

    }
}

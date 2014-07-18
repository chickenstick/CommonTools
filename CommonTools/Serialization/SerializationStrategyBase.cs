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
    internal abstract class SerializationStrategyBase
    {

        #region - Constructor -

        public SerializationStrategyBase()
        {
        }

        #endregion

        #region - Public Methods -

        public abstract void Serialize<T>(Stream stream, T obj);

        public abstract T Deserialize<T>(Stream stream);

        public string SerializeToBase64<T>(T obj)
        {
            string output = null;
            using(MemoryStream memStream = new MemoryStream())
            {
                Serialize(memStream, obj);
                output = Convert.ToBase64String(memStream.ToArray());
            }

            return output;
        }

        public T DeserializeFromBase64<T>(string base64)
        {
            T result = default(T);
            byte[] bytes = Convert.FromBase64String(base64);
            using (MemoryStream memStream = new MemoryStream(bytes))
            {
                result = Deserialize<T>(memStream);
            }

            return result;
        }

        #endregion

    }
}

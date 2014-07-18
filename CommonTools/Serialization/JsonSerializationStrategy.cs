#region - Using Statements -

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

#endregion

namespace CommonTools.Serialization
{
    internal sealed class JsonSerializationStrategy : SerializationStrategyBase
    {

        #region - Constructor -

        public JsonSerializationStrategy()
            : base()
        {
        }

        #endregion

        #region - Public Methods -

        public override void Serialize<T>(Stream stream, T obj)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string output = serializer.Serialize(obj);
            byte[] bytes = Encoding.Default.GetBytes(output);
            stream.Write(bytes, 0, bytes.Length);
        }

        public override T Deserialize<T>(Stream stream)
        {
            byte[] bytes = ReadBytesFromStream(stream);
            string json = Encoding.Default.GetString(bytes);

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Deserialize<T>(json);
        }

        #endregion

        #region - Private Methods -

        private byte[] ReadBytesFromStream(Stream stream)
        {
            List<byte> byteStream = new List<byte>();

            int val = -1;
            do
            {
                val = stream.ReadByte();
                if (val != -1)
                {
                    byteStream.Add((byte)val);
                }
            } while (val != -1);

            return byteStream.ToArray();
        }

        #endregion

    }
}

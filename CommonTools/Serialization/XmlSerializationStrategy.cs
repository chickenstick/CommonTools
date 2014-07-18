#region - Using Statements -

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

#endregion

namespace CommonTools.Serialization
{
    internal sealed class XmlSerializationStrategy:SerializationStrategyBase
    {

        #region - Constructor -

        public XmlSerializationStrategy()
            : base()
        {
        }

        #endregion

        #region - Public Methods -

        public override void Serialize<T>(Stream stream, T obj)
        {
            using (XmlWriter xWriter = GetXmlWriter(stream))
            {
                XmlSerializer xSerializer = new XmlSerializer(typeof(T));
                xSerializer.Serialize(xWriter, obj);
            }
        }

        public override T Deserialize<T>(Stream stream)
        {
            T result = default(T);

            using (XmlReader xReader = GetXmlReader(stream))
            {
                XmlSerializer xSerializer = new XmlSerializer(typeof(T));
                result = (T)xSerializer.Deserialize(xReader);
            }

            return result;
        }

        #endregion

        #region - Private Methods -

        private XmlWriter GetXmlWriter(Stream stream)
        {
            XmlWriter xWriter = XmlWriter.Create(stream);
            return xWriter;
        }

        private XmlReader GetXmlReader(Stream stream)
        {
            XmlReader xReader = XmlReader.Create(stream);
            return xReader;
        }

        #endregion

    }
}

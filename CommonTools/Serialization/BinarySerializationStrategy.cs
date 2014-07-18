#region - Using Statements -

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace CommonTools.Serialization
{
    internal sealed class BinarySerializationStrategy:SerializationStrategyBase
    {

        #region - Constructor -

        public BinarySerializationStrategy()
            : base()
        {
        }

        #endregion

        #region - Public Methods -

        public override void Serialize<T>(Stream stream, T obj)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, obj);
        }

        public override T Deserialize<T>(Stream stream)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            return (T)formatter.Deserialize(stream);
        }

        #endregion

    }
}

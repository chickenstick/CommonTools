#region - Using Statements -

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using CommonTools.Test.TestClasses;
using CommonTools.Serialization;

#endregion

namespace CommonTools.Test
{
    [TestClass]
    public class TestSerialization
    {

        #region - Public Methods -

        [TestMethod]
        public void TestXmlSerializeDeserialize()
        {
            ManagedInvoice original = CreateBasicManagedInvoice();
            SerializationHelper helper = new SerializationHelper(SerializationType.XML);

            byte[] bytes = null;
            string xml = null;
            using (MemoryStream memStream = new MemoryStream())
            {
                helper.Serialize<ManagedInvoice>(memStream, original);
                bytes = memStream.ToArray();
                xml = Encoding.Default.GetString(bytes);
            }

            if (bytes != null && bytes.Length > 0)
            {
                ManagedInvoice deserialized = null;
                using (MemoryStream memStream = new MemoryStream(bytes))
                {
                    deserialized = helper.Deserialize<ManagedInvoice>(memStream);
                }

                if (deserialized != null)
                {
                    Assert.IsTrue(original.Equals(deserialized), "The original and deserialized objects do not match.");
                }
                else
                {
                    Assert.Fail("Deserialization failed to return an object.");
                }
            }
            else
            {
                Assert.Fail("Serialization did not return a byte array.");
            }
        }

        [TestMethod]
        public void TestXmlSerializeDeserializeBase64()
        {
            ManagedInvoice original = CreateBasicManagedInvoice();
            SerializationHelper helper = new SerializationHelper(SerializationType.XML);

            string originalBase64 = helper.SerializeToBase64<ManagedInvoice>(original);

            if (!string.IsNullOrWhiteSpace(originalBase64))
            {
                ManagedInvoice deserialized = helper.DeserializeFromBase64<ManagedInvoice>(originalBase64);
                
                if (deserialized != null)
                {
                    Assert.IsTrue(original.Equals(deserialized), "The original and deserialized objects do not match.");
                }
                else
                {
                    Assert.Fail("Deserialization failed to return an object.");
                }
            }
            else
            {
                Assert.Fail("Serialization did not return a byte array.");
            }
        }

        [TestMethod]
        public void TestBinarySerializeDeserialize()
        {
            ManagedInvoice original = CreateBasicManagedInvoice();
            SerializationHelper helper = new SerializationHelper(SerializationType.Binary);

            byte[] bytes = null;
            using (MemoryStream memStream = new MemoryStream())
            {
                helper.Serialize<ManagedInvoice>(memStream, original);
                bytes = memStream.ToArray();
            }

            if (bytes != null && bytes.Length > 0)
            {
                ManagedInvoice deserialized = null;
                using (MemoryStream memStream = new MemoryStream(bytes))
                {
                    deserialized = helper.Deserialize<ManagedInvoice>(memStream);
                }

                if (deserialized != null)
                {
                    Assert.IsTrue(original.Equals(deserialized), "The original and deserialized objects do not match.");
                }
                else
                {
                    Assert.Fail("Deserialization failed to return an object.");
                }
            }
            else
            {
                Assert.Fail("Serialization did not return a byte array.");
            }
        }

        [TestMethod]
        public void TestBinarySerializeDeserializeBase64()
        {
            ManagedInvoice original = CreateBasicManagedInvoice();
            SerializationHelper helper = new SerializationHelper(SerializationType.Binary);

            string originalBase64 = helper.SerializeToBase64<ManagedInvoice>(original);

            if (!string.IsNullOrWhiteSpace(originalBase64))
            {
                ManagedInvoice deserialized = helper.DeserializeFromBase64<ManagedInvoice>(originalBase64);

                if (deserialized != null)
                {
                    Assert.IsTrue(original.Equals(deserialized), "The original and deserialized objects do not match.");
                }
                else
                {
                    Assert.Fail("Deserialization failed to return an object.");
                }
            }
            else
            {
                Assert.Fail("Serialization did not return a byte array.");
            }
        }

        [TestMethod]
        public void TestJsonSerializeDeserialize()
        {
            ManagedInvoice original = CreateBasicManagedInvoice();
            SerializationHelper helper = new SerializationHelper(SerializationType.JSON);

            byte[] bytes = null;
            using (MemoryStream memStream = new MemoryStream())
            {
                helper.Serialize<ManagedInvoice>(memStream, original);
                bytes = memStream.ToArray();
            }

            if (bytes != null && bytes.Length > 0)
            {
                ManagedInvoice deserialized = null;
                using (MemoryStream memStream = new MemoryStream(bytes))
                {
                    deserialized = helper.Deserialize<ManagedInvoice>(memStream);
                }

                if (deserialized != null)
                {
                    Assert.IsTrue(original.Equals(deserialized), "The original and deserialized objects do not match.");
                }
                else
                {
                    Assert.Fail("Deserialization failed to return an object.");
                }
            }
            else
            {
                Assert.Fail("Serialization did not return a byte array.");
            }
        }

        [TestMethod]
        public void TestJsonSerializeDeserializeBase64()
        {
            ManagedInvoice original = CreateBasicManagedInvoice();
            SerializationHelper helper = new SerializationHelper(SerializationType.JSON);

            string originalBase64 = helper.SerializeToBase64<ManagedInvoice>(original);

            if (!string.IsNullOrWhiteSpace(originalBase64))
            {
                ManagedInvoice deserialized = helper.DeserializeFromBase64<ManagedInvoice>(originalBase64);

                if (deserialized != null)
                {
                    Assert.IsTrue(original.Equals(deserialized), "The original and deserialized objects do not match.");
                }
                else
                {
                    Assert.Fail("Deserialization failed to return an object.");
                }
            }
            else
            {
                Assert.Fail("Serialization did not return a byte array.");
            }
        }

        #endregion

        #region - Helper Methods -

        private ManagedInvoice CreateBasicManagedInvoice()
        {
            ManagedInvoice result = new ManagedInvoice();
            result.FileGuid = Guid.NewGuid();
            result.CarrierCode = "T5654";
            result.SCAC = "HOLM";
            result.UserEmail = "bohrjos@chrobinson.com";
            result.UserName = "bohrjos";
            result.OriginalFileName = "test.csv";
            result.FileSubmittedDate = DateTime.Now;

            return result;
        }

        #endregion

    }
}

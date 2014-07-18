#region - Using Statements -

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using CommonTools.Enumerations;
using CommonTools.Test.TestClasses;

#endregion

namespace CommonTools.Test
{
    [TestClass]
    public class TestEnumerations
    {

        [TestMethod]
        public void TestParsing()
        {
            Assert.AreEqual(SampleEnum.Value1, EnumLookup.Parse<SampleEnum>("A"));
            Assert.AreEqual(SampleEnum.Value2, EnumLookup.Parse<SampleEnum>("B"));
            Assert.AreEqual(SampleEnum.Value3, EnumLookup.Parse<SampleEnum>("c"));

            Assert.AreEqual(SampleEnum.Value1, EnumLookup.Parse<SampleEnum>("a", true));
            Assert.AreEqual(SampleEnum.Value2, EnumLookup.Parse<SampleEnum>("b", true));
            Assert.AreEqual(SampleEnum.Value3, EnumLookup.Parse<SampleEnum>("C", true));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestExceptionThrownWhenParsingValueWithWrongCase()
        {
            SampleEnum value = EnumLookup.Parse<SampleEnum>("C");
        }

        [TestMethod]
        public void TestTryParsing()
        {
            SampleEnum value = SampleEnum.Unknown;

            Assert.IsTrue(EnumLookup.TryParse("A", out value));
            Assert.AreEqual(SampleEnum.Value1, value);
            Assert.IsTrue(EnumLookup.TryParse("B", out value));
            Assert.AreEqual(SampleEnum.Value2, value);
            Assert.IsTrue(EnumLookup.TryParse("c", out value));
            Assert.AreEqual(SampleEnum.Value3, value);

            Assert.IsFalse(EnumLookup.TryParse("a", out value));
            Assert.IsFalse(EnumLookup.TryParse("b", out value));
            Assert.IsFalse(EnumLookup.TryParse("C", out value));

            Assert.IsTrue(EnumLookup.TryParse("a", true, out value));
            Assert.AreEqual(SampleEnum.Value1, value);
            Assert.IsTrue(EnumLookup.TryParse("b", true, out value));
            Assert.AreEqual(SampleEnum.Value2, value);
            Assert.IsTrue(EnumLookup.TryParse("C", true, out value));
            Assert.AreEqual(SampleEnum.Value3, value);

            Assert.IsFalse(EnumLookup.TryParse("D", out value));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestExceptionThrownWhenGettingSynonymForValueWithoutAttribute()
        {
            string synonym = EnumLookup.GetSynonym(SampleEnum.Unknown);
        }

        [TestMethod]
        public void TestTryGetSynonym()
        {
            string synonym = string.Empty;

            Assert.IsTrue(EnumLookup.TryGetSynonym(SampleEnum.Value1, out synonym));
            Assert.AreEqual("A", synonym);

            Assert.IsFalse(EnumLookup.TryGetSynonym(SampleEnum.Unknown, out synonym));
        }

    }
}

using System.Data.SqlTypes;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NUnit.Framework;
using Unity.Nuget.NewtonsoftJson.Tests.TestObjects;

namespace Unity.Nuget.NewtonsoftJson.Tests
{
    [TestFixture]
    class BinarySerializationTests : NewtonsoftTests
    {
        static readonly byte[] k_TestData = Encoding.UTF8.GetBytes("This is some test data!!!");

        [Test]
        public void SerializeObjectForBinaryDataSucceeds()
        {
            var byteArrayClass = new ByteArrayClass
            {
                ByteArray = k_TestData,
                NullByteArray = null
            };

            var json = JsonConvert.SerializeObject(byteArrayClass, Formatting.Indented);

            StringUtils.AssertAreEqualWithNormalizedLineEndings(@"{
  ""ByteArray"": ""VGhpcyBpcyBzb21lIHRlc3QgZGF0YSEhIQ=="",
  ""NullByteArray"": null
}", json);
        }

        [Test]
        public void SerializeObjectForSqlBinaryDataSucceeds()
        {
            var sqlBinaryClass = new SqlBinaryClass
            {
                SqlBinary = new SqlBinary(k_TestData),
                NullableSqlBinary1 = new SqlBinary(k_TestData),
                NullableSqlBinary2 = null
            };

            var json = JsonConvert.SerializeObject(sqlBinaryClass, Formatting.Indented, new BinaryConverter());

            StringUtils.AssertAreEqualWithNormalizedLineEndings(@"{
  ""SqlBinary"": ""VGhpcyBpcyBzb21lIHRlc3QgZGF0YSEhIQ=="",
  ""NullableSqlBinary1"": ""VGhpcyBpcyBzb21lIHRlc3QgZGF0YSEhIQ=="",
  ""NullableSqlBinary2"": null
}", json);
        }

        [Test]
        public void DeserializeObjectForRawBinaryDataSucceeds()
        {
            const string json = @"{
  ""ByteArray"": ""VGhpcyBpcyBzb21lIHRlc3QgZGF0YSEhIQ=="",
  ""NullByteArray"": null
}";

            var byteArrayClass = JsonConvert.DeserializeObject<ByteArrayClass>(json);

            Assert.IsNotNull(byteArrayClass);
            CollectionAssert.AreEquivalent(k_TestData, byteArrayClass.ByteArray);
            Assert.AreEqual(null, byteArrayClass.NullByteArray);
        }

        [Test]
        public void DeserializeObjectForSqlBinaryDataSucceeds()
        {
            const string json = @"{
  ""SqlBinary"": ""VGhpcyBpcyBzb21lIHRlc3QgZGF0YSEhIQ=="",
  ""NullableSqlBinary1"": ""VGhpcyBpcyBzb21lIHRlc3QgZGF0YSEhIQ=="",
  ""NullableSqlBinary2"": null
}";

            var sqlBinaryClass = JsonConvert.DeserializeObject<SqlBinaryClass>(json, new BinaryConverter());

            Assert.IsNotNull(sqlBinaryClass);
            Assert.AreEqual(new SqlBinary(k_TestData), sqlBinaryClass.SqlBinary);
            Assert.AreEqual(new SqlBinary(k_TestData), sqlBinaryClass.NullableSqlBinary1);
            Assert.AreEqual(null, sqlBinaryClass.NullableSqlBinary2);
        }

        [Test]
        public void DeserializeObjectForByteArraySucceeds()
        {
            const string json = @"{
  ""ByteArray"": [0, 1, 2, 3],
  ""NullByteArray"": null
}";

            var c = JsonConvert.DeserializeObject<ByteArrayClass>(json);

            Assert.IsNotNull(c);
            Assert.AreEqual(4, c.ByteArray.Length);
            CollectionAssert.AreEquivalent(new byte[] { 0, 1, 2, 3 }, c.ByteArray);
        }
    }
}

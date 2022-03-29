using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Unity.Nuget.NewtonsoftJson.Tests.TestObjects;

namespace Unity.Nuget.NewtonsoftJson.Tests
{
    [TestFixture]
    class ObjectConversionTests : NewtonsoftTests
    {
        [Test]
        public void JArrayToObjectOnCustomObjectListSucceeds()
        {
            var jArray = JArray.Parse("[{ Value:10000000000000000000 }]");

            var list = jArray.ToObject<List<DecimalWrapper>>();

            Assert.IsNotNull(list);
            Assert.AreEqual(10000000000000000000m, list[0].Value);
        }

        [Test]
        public void JValueToObjectFromGuidToStringSucceeds()
        {
            const string expected = "91274484-3b20-48b4-9d18-7d936b2cb88f";
            var token = new JValue(new Guid(expected));

            var value = token.ToObject<string>();

            Assert.AreEqual(expected, value);
        }

        [Test]
        public void JValueToObjectFromIntegerToStringSucceeds()
        {
            var token = new JValue(1234);

            var value = token.ToObject<string>();

            Assert.AreEqual("1234", value);
        }

        [Test]
        public void JValueToObjectFromStringToIntegerSucceeds()
        {
            var token = new JValue("1234");

            var value = token.ToObject<int>();

            Assert.AreEqual(1234, value);
        }

        [Test]
        public void JObjectToObjectWithDictionaryOfGuidSucceeds()
        {
            const string key = "id";
            var anon = new JObject
            {
                [key] = Guid.NewGuid()
            };
            Assert.AreEqual(JTokenType.Guid, anon[key].Type);

            var dict = anon.ToObject<Dictionary<string, JToken>>();

            Assert.IsNotNull(dict);
            Assert.AreEqual(JTokenType.Guid, dict[key].Type);
        }

        [Test]
        public void JValueToObjectFromStringToBinarySucceeds()
        {
            const string key = "responseArray";
            const string value = "AAAAAAAAAAAAAAAAAAAAAAAAAAABAAAA";
            var o = JObject.Parse($"{{'{key}':'{value}'}}");

            var data = o[key].ToObject<byte[]>();
            var expectedBinary = Convert.FromBase64String(value);

            CollectionAssert.AreEqual(expectedBinary, data);
        }

        [Test]
        public void JValueToObjectFromGuidToBinarySucceeds()
        {
            const string key = "responseArray";
            const string value = "AAAAAAAA-AAAA-AAAA-AAAA-AAAAAAABAAAA";
            var o = JObject.Parse($"{{'{key}':'{value}'}}");

            var data = o[key].ToObject<byte[]>();
            var expectedBinary = new Guid(value).ToByteArray();

            CollectionAssert.AreEqual(expectedBinary, data);
        }

        [Test]
        public void JValueToObjectOnEnumWithConverterSucceeds()
        {
            const string key = nameof(EnumWithConverter);
            var o = JObject.Parse($"{{'{key}':'SOME_OTHER_VALUE'}}");

            var e = o[key].ToObject<EnumWithConverter>();

            Assert.AreEqual(EnumWithConverter.SomeOtherValue, e);
        }

        [Test]
        public void JValueToObjectOnEnumWithoutConverterSucceeds()
        {
            const string key = nameof(EnumWithoutConverter);
            var o = JObject.Parse($"{{'{key}':'SOME_OTHER_VALUE'}}");

            var e = o[key].ToObject<EnumWithoutConverter>();

            Assert.AreEqual(EnumWithoutConverter.SomeOtherValue, e);
        }
        [Test]
        public void JObjectFromObjectOnULongMaxValueSucceeds()
        {
            var instance = new ULongWrapper
            {
                Value = ulong.MaxValue
            };

            var output = JObject.FromObject(instance);

            StringUtils.AssertAreEqualWithNormalizedLineEndings(@"{
  ""Value"": 18446744073709551615
}", output.ToString());
        }

        [Test]
        public void JObjectFromObjectOnByteMaxValueSucceeds()
        {
            var instance = new ByteWrapper
            {
                Value = byte.MaxValue
            };

            var output = JObject.FromObject(instance);

            StringUtils.AssertAreEqualWithNormalizedLineEndings(@"{
  ""Value"": 255
}", output.ToString());
        }

        [Test]
        public void JObjectFromObjectOnCustomObjectSucceeds()
        {
            var p = new Post
            {
                Title = "How to use FromObject",
                Categories = new[]
                {
                    "LINQ to JSON"
                }
            };

            // Serialize Post to JSON then parse JSON – SLOW!
            //JObject o = JObject.Parse(JsonConvert.SerializeObject(p));
            // Create JObject directly from the Post
            var o = JObject.FromObject(p);
            o["Title"] = $"{o["Title"]} - Super effective!";

            var json = o.ToString();
            StringUtils.AssertAreEqualWithNormalizedLineEndings(@"{
  ""Title"": ""How to use FromObject - Super effective!"",
  ""Description"": null,
  ""Link"": null,
  ""Categories"": [
    ""LINQ to JSON""
  ]
}", json);
        }

        [Test]
        public void JArrayFromObjectOnIntArraySucceeds()
        {
            var a = JArray.FromObject(new List<int> { 0, 1, 2, 3, 4 });

            Assert.IsInstanceOf(typeof(JArray), a);
            Assert.AreEqual(5, a.Count);
        }
    }
}

using System;
using Newtonsoft.Json;
using NUnit.Framework;
using Unity.Nuget.NewtonsoftJson.Tests.TestObjects;

namespace Unity.Nuget.NewtonsoftJson.Tests
{
    [TestFixture]
    class DeserializeObjectTests : NewtonsoftTests
    {
        [Test]
        public void DeserializeObjectProperlyDeserializesCustomType()
        {
            const string json = @"{
        'Name': 'Bad Boys',
        'ReleaseDate': '1995-4-7T00:00:00',
        'Genres': [
          'Action',
          'Comedy'
        ]
      }";

            var m = JsonConvert.DeserializeObject<Movie>(json);

            Assert.IsNotNull(m);
            Assert.AreEqual("Bad Boys", m.Name);
        }

        [Test]
        public void DeserializeObjectDeserializesEmptyStringToNull()
        {
            var result = JsonConvert.DeserializeObject(string.Empty);

            Assert.IsNull(result);
        }

        [Test]
        public void DeserializeObjectDeserializesInteger()
        {
            var result = JsonConvert.DeserializeObject("1");

            Assert.AreEqual(1L, result);
        }

        [Test]
        public void DeserializeObjectDeserializesEmptyStringAsNullableIntegerToNull()
        {
            var value = JsonConvert.DeserializeObject<int?>("");

            Assert.IsNull(value);
        }

        [Test]
        public void DeserializeObjectDeserializesEmptyStringAsNullableDecimalToNull()
        {
            var value = JsonConvert.DeserializeObject<decimal?>("");

            Assert.IsNull(value);
        }

        [Test]
        public void DeserializeObjectDeserializesEmptyStringAsNullableDateTimeToNull()
        {
            var value = JsonConvert.DeserializeObject<DateTime?>("");

            Assert.IsNull(value);
        }

        [Test]
        public void DeserializeObjectThrowsOnStringWithUnescapedQuotes()
        {
            const string orig = @"this is a string ""that has quotes"" ";

            // Make string invalid by stripping \" \"
            var serialized = JsonConvert.SerializeObject(orig)
                .Replace(@"\""", "\"");

            Assert.Throws<JsonReaderException>(
                DeserializeInvalidString,
                "Additional text encountered after finished reading JSON content: t. Path '', line 1, position 19.");

            void DeserializeInvalidString() => JsonConvert.DeserializeObject<string>(serialized);
        }

        [Test]
        public void DeserializeObjectSucceedsForPrimitives()
        {
            var i = JsonConvert.DeserializeObject<int>("1");
            Assert.AreEqual(1, i);

            var d = JsonConvert.DeserializeObject<DateTimeOffset>(@"""\/Date(-59011455539000+0000)\/""");
            Assert.AreEqual(new DateTimeOffset(new DateTime(100, 1, 1, 1, 1, 1, DateTimeKind.Utc)), d);

            var b = JsonConvert.DeserializeObject<bool>("true");
            Assert.AreEqual(true, b);

            var n = JsonConvert.DeserializeObject<object>("null");
            Assert.IsNull(n);

            var u = JsonConvert.DeserializeObject<object>("undefined");
            Assert.IsNull(u);
        }

        [Test]
        public void DeserializeObjectDoesNotPopulateReadOnlyEnumerableObjectWithNonDefaultConstructor()
        {
            object actual = JsonConvert.DeserializeObject<HasReadOnlyEnumerableObject>("{\"foo\":{}}");

            Assert.IsNotNull(actual);
        }

        [Test]
        public void DeserializeObjectPopulatesReadOnlyEnumerableObjectWithDefaultConstructor()
        {
            object actual = JsonConvert.DeserializeObject<HasReadOnlyEnumerableObjectAndDefaultConstructor>("{\"foo\":{}}");

            Assert.IsNotNull(actual);
        }

        [Test]
        public void DeserializeObjectDoesNotPopulateConstructorArgumentEnumerableObject()
        {
            object actual = JsonConvert.DeserializeObject<AcceptsEnumerableObjectToConstructor>("{\"foo\":{}}");

            Assert.IsNotNull(actual);
        }

        [Test]
        public void DeserializeObjectDoesNotPopulateEnumerableObjectProperty()
        {
            object actual = JsonConvert.DeserializeObject<HasEnumerableObject>("{\"foo\":{}}");

            Assert.IsNotNull(actual);
        }

        [Test]
        public void DeserializeObjectDoesNotPopulateReadOnlyDictionaryObjectWithNonDefaultConstructor()
        {
            object actual = JsonConvert.DeserializeObject<HasReadOnlyDictionary>("{\"foo\":{'key':'value'}}");

            Assert.IsNotNull(actual);
        }

        [Test]
        public void DeserializeObjectDoesNotRequireIgnoredPropertiesWithItemsRequired()
        {
            const string json = @"{
  ""exp"": 1483228800,
  ""active"": true
}";

            var value = JsonConvert.DeserializeObject<ItemsRequiredObjectWithIgnoredProperty>(json);

            Assert.IsNotNull(value);
            Assert.AreEqual(value.Expiration, new DateTime(2017, 1, 1, 0, 0, 0, DateTimeKind.Utc));
            Assert.AreEqual(value.Active, true);
        }
    }
}

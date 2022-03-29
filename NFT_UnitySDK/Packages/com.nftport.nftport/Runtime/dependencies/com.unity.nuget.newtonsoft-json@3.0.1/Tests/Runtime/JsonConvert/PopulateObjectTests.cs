using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using NUnit.Framework;
using Unity.Nuget.NewtonsoftJson.Tests.TestObjects;

namespace Unity.Nuget.NewtonsoftJson.Tests
{
    [TestFixture]
    class PopulateObjectTests : NewtonsoftTests
    {
        [Test]
        public void PopulateObjectWithHeaderCommentSucceeds()
        {
            const string json = @"// file header
{
  ""prop"": 1.0
}";
            var o = new PopulateTestObject();

            JsonConvert.PopulateObject(json, o);

            Assert.AreEqual(1m, o.Prop);
        }

        [Test]
        public void PopulateObjectWithMultipleHeaderCommentSucceeds()
        {
            const string json = @"// file header
// another file header?
{
  ""prop"": 1.0
}";

            var o = new PopulateTestObject();
            JsonConvert.PopulateObject(json, o);

            Assert.AreEqual(1m, o.Prop);
        }

        [Test]
        public void PopulateObjectWithNoContentThrows()
        {
            Assert.Throws<JsonSerializationException>(Test, "No JSON content found. Path '', line 0, position 0.");

            void Test()
            {
                const string json = @"";
                var o = new PopulateTestObject();

                JsonConvert.PopulateObject(json, o);
            }
        }

        [Test]
        public void PopulateObjectWithOnlyCommentThrows()
        {
            var ex = Assert.Throws<JsonSerializationException>(Test, "No JSON content found. Path '', line 1, position 14.");

            Assert.AreEqual(1, ex.LineNumber);
            Assert.AreEqual(14, ex.LinePosition);
            Assert.AreEqual(string.Empty, ex.Path);

            void Test()
            {
                const string json = @"// file header";
                var o = new PopulateTestObject();

                JsonConvert.PopulateObject(json, o);
            }
        }

        [Test]
        public void PopulateObjectWithDateTimeOffsetRoundtripSucceeds()
        {
            const string key = "foo";
            var now = DateTimeOffset.Now;
            var dict = new Dictionary<string, object>
            {
                [key] = now
            };
            var settings = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateParseHandling = DateParseHandling.DateTimeOffset,
                DateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind
            };
            var json = JsonConvert.SerializeObject(dict, settings);

            var newDict = new Dictionary<string, object>();
            JsonConvert.PopulateObject(json, newDict, settings);

            var date = newDict[key];
            Assert.AreEqual(date, now);
        }
    }
}

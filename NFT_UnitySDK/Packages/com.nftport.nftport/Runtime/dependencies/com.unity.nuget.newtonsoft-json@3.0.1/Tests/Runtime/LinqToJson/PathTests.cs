using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace Unity.Nuget.NewtonsoftJson.Tests
{
    class PathTests : NewtonsoftTests
    {
        [Test]
        public void JValuePathEscapesQuotes()
        {
            const string key = "We're offline!";
            var v = new JValue(1);

            var o = new JObject
            {
                [key] = v
            };

            Assert.IsNotNull(o);
            Assert.AreEqual(@"['We\'re offline!']", v.Path);
        }

        [Test]
        public void JPropertyPathWithSpecialSymbol()
        {
            var o = new JObject
            {
                ["person"] = new JObject
                {
                    ["$id"] = 1
                }
            };

            var idProperty = o["person"]["$id"].Parent;
            Assert.AreEqual("person.$id", idProperty.Path);
        }

        [TestCase("['this has spaces']", "this has spaces")]
        [TestCase("['(RoundBraces)']", "(RoundBraces)")]
        [TestCase("['[SquareBraces]']", "[SquareBraces]")]
        [TestCase("['this.has.dots']", "this.has.dots")]
        public void JPropertyPathIsProperlyEscaped(string expectedPath, string propertyName)
        {
            const int v1 = int.MaxValue;
            var value = new JValue(v1);

            var o = new JObject(new JProperty(propertyName, value));
            Assert.AreEqual(expectedPath, value.Path);

            var selectedValue = (JValue)o.SelectToken(value.Path);
            Assert.AreEqual(value, selectedValue);
        }
    }
}

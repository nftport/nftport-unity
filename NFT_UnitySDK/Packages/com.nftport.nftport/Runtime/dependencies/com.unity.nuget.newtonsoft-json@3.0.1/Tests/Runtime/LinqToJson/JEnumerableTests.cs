using System;
using System.Collections;
using System.Linq;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace Unity.Nuget.NewtonsoftJson.Tests
{
    class JEnumerableTests : NewtonsoftTests
    {
        [Test]
        public void EmptyJEnumerableCount()
        {
            var tokens = new JEnumerable<JToken>();

            Assert.AreEqual(0, tokens.Count());
        }

        [Test]
        public void EmptyJEnumerableAsEnumerableIsStillEmpty()
        {
            IEnumerable tokens = new JEnumerable<JToken>();

            Assert.IsEmpty(tokens);
        }

        [Test]
        public void JEnumerableEqualsOnTwoDifferentEmptyInstancesIsTrue()
        {
            var tokens1 = new JEnumerable<JToken>();
            var tokens2 = new JEnumerable<JToken>();

            Assert.AreEqual(tokens1, tokens2);

            object o1 = new JEnumerable<JToken>();
            object o2 = new JEnumerable<JToken>();

            Assert.AreEqual(o1, o2);
        }

        [Test]
        public void EmptyJEnumerableGetHashCode()
        {
            var tokens = new JEnumerable<JToken>();

            Assert.AreEqual(0, tokens.GetHashCode());
        }

        [Test]
        public void JObjectAsJEnumerableSucceeds()
        {
            var o = new JObject(
                new JProperty("Test1", new DateTime(2000, 10, 15, 5, 5, 5, DateTimeKind.Utc)),
                new JProperty("Test2", "Test2Value"),
                new JProperty("Test3", null)
            );

            var enumerable = o.AsJEnumerable();

            Assert.IsNotNull(enumerable);
            Assert.AreEqual(o, enumerable);
            var d = enumerable["Test1"].Value<DateTime>();
            Assert.AreEqual(new DateTime(2000, 10, 15, 5, 5, 5, DateTimeKind.Utc), d);
        }

        [Test]
        public void NullJObjectAsJEnumerableReturnsNull()
        {
            JObject o = null;

            var enumerable = o.AsJEnumerable();

            Assert.IsNull(enumerable);
        }
    }
}

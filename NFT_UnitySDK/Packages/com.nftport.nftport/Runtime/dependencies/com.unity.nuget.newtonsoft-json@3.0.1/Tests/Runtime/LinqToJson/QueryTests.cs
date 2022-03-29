using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace Unity.Nuget.NewtonsoftJson.Tests
{
    class QueryTests : NewtonsoftTests
    {
        [Test]
        public void JArrayQueryExamples()
        {
            const string json = @"[
  {
    'Title': 'JSON Serializer Basics',
    'Date': '2013-12-21T00:00:00',
    'Categories': []
  },
  {
    'Title': 'Querying LINQ to JSON',
    'Date': '2014-06-03T00:00:00',
    'Categories': [
      'LINQ to JSON'
    ]
  }
]";

            var queryTestData = JArray.Parse(json);

            var serializerBasics = queryTestData
                .Single(p => (string)p["Title"] == "JSON Serializer Basics");
            Assert.IsNotNull(serializerBasics);
            IList<JToken> since2012 = queryTestData
                .Where(p => (DateTime)p["Date"] > new DateTime(2012, 1, 1)).ToList();
            Assert.AreEqual(2, since2012.Count);
            IList<JToken> linqToJson = queryTestData
                .Where(p => p["Categories"].Any(c => (string)c == "LINQ to JSON")).ToList();
            Assert.AreEqual(1, linqToJson.Count);
        }

        [Test]
        public void JObjectQueryExamples()
        {
            const string json = @"{
  ""channel"": {
    ""title"": ""James Newton-King"",
    ""link"": ""http://james.newtonking.com"",
    ""description"": ""James Newton-King's blog."",
    ""item"": [
      {
        ""title"": ""Json.NET 1.3 + New license + Now on CodePlex"",
        ""description"": ""Announcing the release of Json.NET 1.3, the MIT license and being available on CodePlex"",
        ""link"": ""http://james.newtonking.com/projects/json-net.aspx"",
        ""category"": [
          ""Json.NET"",
          ""CodePlex""
        ]
      },
      {
        ""title"": ""LINQ to JSON beta"",
        ""description"": ""Announcing LINQ to JSON"",
        ""link"": ""http://james.newtonking.com/projects/json-net.aspx"",
        ""category"": [
          ""Json.NET"",
          ""LINQ""
        ]
      }
    ]
  }
}";

            var o = JObject.Parse(json);

            Assert.AreEqual(null, o["purple"]);
            Assert.AreEqual(null, o.Value<string>("purple"));
            Assert.IsInstanceOf<JArray>(o["channel"]["item"]);
            Assert.AreEqual(2, o["channel"]["item"].Children()["title"].Count());
            Assert.AreEqual(0, o["channel"]["item"].Children()["monkey"].Count());
            Assert.AreEqual("Json.NET 1.3 + New license + Now on CodePlex", (string)o["channel"]["item"][0]["title"]);
            var expectedTitles = new[]
            {
                "Json.NET 1.3 + New license + Now on CodePlex",
                "LINQ to JSON beta"
            };
            CollectionAssert.AreEqual(expectedTitles, o["channel"]["item"].Children().Values<string>("title").ToArray());
        }

        [Test]
        public void LinqCastOnJTokenSucceeds()
        {
            JToken list = JArray.Parse("[12,55]");

            var list1 = list.AsEnumerable()
                .Values<int>()
                .ToList();
            Assert.AreEqual(12, list1[0]);
            Assert.AreEqual(55, list1[1]);
        }

        [Test]
        public void JArrayChildrenQuery()
        {
            const string json = @"[
  {
    ""title"": ""James Newton-King"",
    ""link"": ""http://james.newtonking.com"",
    ""description"": ""James Newton-King's blog."",
    ""item"": [
      {
        ""title"": ""Json.NET 1.3 + New license + Now on CodePlex"",
        ""description"": ""Announcing the release of Json.NET 1.3, the MIT license and being available on CodePlex"",
        ""link"": ""http://james.newtonking.com/projects/json-net.aspx"",
        ""category"": [
          ""Json.NET"",
          ""CodePlex""
        ]
      },
      {
        ""title"": ""LINQ to JSON beta"",
        ""description"": ""Announcing LINQ to JSON"",
        ""link"": ""http://james.newtonking.com/projects/json-net.aspx"",
        ""category"": [
          ""Json.NET"",
          ""LINQ""
        ]
      }
    ]
  },
  {
    ""title"": ""James Newton-King"",
    ""link"": ""http://james.newtonking.com"",
    ""description"": ""James Newton-King's blog."",
    ""item"": [
      {
        ""title"": ""Json.NET 1.3 + New license + Now on CodePlex"",
        ""description"": ""Announcing the release of Json.NET 1.3, the MIT license and being available on CodePlex"",
        ""link"": ""http://james.newtonking.com/projects/json-net.aspx"",
        ""category"": [
          ""Json.NET"",
          ""CodePlex""
        ]
      },
      {
        ""title"": ""LINQ to JSON beta"",
        ""description"": ""Announcing LINQ to JSON"",
        ""link"": ""http://james.newtonking.com/projects/json-net.aspx"",
        ""category"": [
          ""Json.NET"",
          ""LINQ""
        ]
      }
    ]
  }
]";

            var o = JArray.Parse(json);
            var childrenItemTitles = o.Children()["item"].Children()["title"];

            Assert.AreEqual(4, childrenItemTitles.Count());
            CollectionAssert.AreEqual(new[]
                {
                    "Json.NET 1.3 + New license + Now on CodePlex",
                    "LINQ to JSON beta",
                    "Json.NET 1.3 + New license + Now on CodePlex",
                    "LINQ to JSON beta"
                },
                childrenItemTitles.Values<string>().ToArray());
        }
    }
}

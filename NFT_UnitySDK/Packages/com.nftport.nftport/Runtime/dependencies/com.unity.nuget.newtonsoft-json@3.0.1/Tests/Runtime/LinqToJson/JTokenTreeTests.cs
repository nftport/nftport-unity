using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Unity.Nuget.NewtonsoftJson.Tests.TestObjects;

namespace Unity.Nuget.NewtonsoftJson.Tests
{
    class JTokenTreeTests : NewtonsoftTests
    {
        [Test]
        public void CreateJTokenTree()
        {
            var o = new JObject(
                new JProperty("Test1", "Test1Value"),
                new JProperty("Test2", "Test2Value"),
                new JProperty("Test3", "Test3Value"),
                new JProperty("Test4", null)
            );

            Assert.AreEqual(4, o.Properties().Count());
            StringUtils.AssertAreEqualWithNormalizedLineEndings(@"{
  ""Test1"": ""Test1Value"",
  ""Test2"": ""Test2Value"",
  ""Test3"": ""Test3Value"",
  ""Test4"": null
}", o.ToString());

            var a =
                new JArray(
                    o,
                    new DateTime(2000, 10, 10, 0, 0, 0, DateTimeKind.Utc),
                    55,
                    new JArray(
                        "1",
                        2,
                        3.0,
                        new DateTime(4, 5, 6, 7, 8, 9, DateTimeKind.Utc)
                    ),
                    new JConstructor(
                        "ConstructorName",
                        "param1",
                        2,
                        3.0
                    )
                );

            Assert.AreEqual(5, a.Count);
            StringUtils.AssertAreEqualWithNormalizedLineEndings(@"[
  {
    ""Test1"": ""Test1Value"",
    ""Test2"": ""Test2Value"",
    ""Test3"": ""Test3Value"",
    ""Test4"": null
  },
  ""2000-10-10T00:00:00Z"",
  55,
  [
    ""1"",
    2,
    3.0,
    ""0004-05-06T07:08:09Z""
  ],
  new ConstructorName(
    ""param1"",
    2,
    3.0
  )
]", a.ToString());
        }

        [Test]
        public void CreateNestedJTokenTree()
        {
            var posts = CreateTestPostList();

            var rss = new JObject(
                new JProperty("channel",
                    new JObject(
                        new JProperty("title", "James Newton-King"),
                        new JProperty("link", "http://james.newtonking.com"),
                        new JProperty("description", "James Newton-King's blog."),
                        new JProperty("item",
                            new JArray(posts.OrderBy(p => p.Title).Select(CreateJObjectFromPost))))));

            StringUtils.AssertAreEqualWithNormalizedLineEndings(@"{
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
}", rss.ToString());

            var postTitles = rss["channel"]["item"]
                .Select(p => p.Value<string>("title"))
                .ToList();
            Assert.AreEqual("Json.NET 1.3 + New license + Now on CodePlex", postTitles.ElementAt(0));
            Assert.AreEqual("LINQ to JSON beta", postTitles.ElementAt(1));

            var categories = rss["channel"]["item"]
                .Children()["category"]
                .Values<string>()
                .GroupBy(c => c)
                .OrderByDescending(g => g.Count())
                .Select(g => new { Category = g.Key, Count = g.Count() })
                .ToList();
            Assert.AreEqual("Json.NET", categories.ElementAt(0).Category);
            Assert.AreEqual(2, categories.ElementAt(0).Count);
            Assert.AreEqual("CodePlex", categories.ElementAt(1).Category);
            Assert.AreEqual(1, categories.ElementAt(1).Count);
            Assert.AreEqual("LINQ", categories.ElementAt(2).Category);
            Assert.AreEqual(1, categories.ElementAt(2).Count);

            JObject CreateJObjectFromPost(Post post)
            {
                var postCategories = post.Categories.Select(c => new JValue(c));
                return new JObject(
                    new JProperty("title", post.Title),
                    new JProperty("description", post.Description),
                    new JProperty("link", post.Link),
                    new JProperty("category", new JArray(postCategories)));
            }
        }

        static IEnumerable<Post> CreateTestPostList()
        {
            return new List<Post>
            {
                new Post
                {
                    Title = "LINQ to JSON beta",
                    Description = "Announcing LINQ to JSON",
                    Link = "http://james.newtonking.com/projects/json-net.aspx",
                    Categories = new List<string>
                    {
                        "Json.NET",
                        "LINQ"
                    }
                },
                new Post
                {
                    Title = "Json.NET 1.3 + New license + Now on CodePlex",
                    Description = "Announcing the release of Json.NET 1.3, the MIT license and being available on CodePlex",
                    Link = "http://james.newtonking.com/projects/json-net.aspx",
                    Categories = new List<string>
                    {
                        "Json.NET",
                        "CodePlex"
                    }
                }
            };
        }
    }
}

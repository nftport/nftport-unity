using System;
using System.Collections;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace Unity.Nuget.NewtonsoftJson.Tests
{
    [TestFixture]
    class JTokenReadTests : NewtonsoftTests
    {
        const string k_TestJsonWithComment = @"[
    // hi
    1,
    2,
    3
]";

        const string k_TestJsonStartingWithComment = @"
// hi
[
    1,
    2,
    3
]";

        const string k_TestJsonStartingWithUndefined = @"
undefined
[
    1,
    2,
    3
]";

        [Test]
        public void ReadFromOnJsonWithCommentIgnoresComment()
        {
            using (var textReader = new StringReader(k_TestJsonWithComment))
            using (var jsonReader = new JsonTextReader(textReader))
            {
                var a = (JArray)JToken.ReadFrom(jsonReader);

                Assert.AreEqual(3, a.Count);
                Assert.AreEqual(JTokenType.Integer, a[0].Type);
                Assert.AreEqual(1L, ((JValue)a[0]).Value);
            }
        }

        [UnityTest]
        public IEnumerator ReadFromAsyncOnJsonWithCommentIgnoresComment()
        {
            yield return AsyncUtils.Run(TestAsync);

            async Task TestAsync()
            {
                using (var textReader = new StringReader(k_TestJsonWithComment))
                using (var jsonReader = new JsonTextReader(textReader))
                {
                    var a = (JArray)await JToken.ReadFromAsync(jsonReader);

                    Assert.AreEqual(3, a.Count);
                    Assert.AreEqual(JTokenType.Integer, a[0].Type);
                    Assert.AreEqual(1L, ((JValue)a[0]).Value);
                }
            }
        }

        [Test]
        public void ReadFromWithCommentHandlingLoadOnJsonWithCommentReadsComment()
        {
            using (var textReader = new StringReader(k_TestJsonWithComment))
            using (var jsonReader = new JsonTextReader(textReader))
            {
                var a = (JArray)JToken.ReadFrom(
                    jsonReader, new JsonLoadSettings
                    {
                        CommentHandling = CommentHandling.Load
                    });

                Assert.AreEqual(4, a.Count);
                Assert.AreEqual(JTokenType.Comment, a[0].Type);
                Assert.AreEqual(" hi", ((JValue)a[0]).Value);
            }
        }

        [UnityTest]
        public IEnumerator ReadFromAsyncWithCommentHandlingLoadOnJsonWithCommentReadsComment()
        {
            yield return AsyncUtils.Run(TestAsync);

            async Task TestAsync()
            {
                using (var textReader = new StringReader(k_TestJsonWithComment))
                using (var jsonReader = new JsonTextReader(textReader))
                {
                    var a = (JArray)await JToken.ReadFromAsync(
                        jsonReader, new JsonLoadSettings
                        {
                            CommentHandling = CommentHandling.Load
                        });

                    Assert.AreEqual(4, a.Count);
                    Assert.AreEqual(JTokenType.Comment, a[0].Type);
                    Assert.AreEqual(" hi", ((JValue)a[0]).Value);
                }
            }
        }

        [Test]
        public void ReadFromOnJsonStartingWithCommentIgnoresComment()
        {
            using (var textReader = new StringReader(k_TestJsonStartingWithComment))
            using (var jsonReader = new JsonTextReader(textReader))
            {
                var a = (JArray)JToken.ReadFrom(
                    jsonReader, new JsonLoadSettings
                    {
                        CommentHandling = CommentHandling.Ignore
                    });

                Assert.AreEqual(JTokenType.Array, a.Type);
                IJsonLineInfo lineInfo = a;
                Assert.AreEqual(true, lineInfo.HasLineInfo());
                Assert.AreEqual(3, lineInfo.LineNumber);
                Assert.AreEqual(1, lineInfo.LinePosition);
            }
        }

        [UnityTest]
        public IEnumerator ReadFromAsyncOnJsonStartingWithCommentIgnoresComment()
        {
            yield return AsyncUtils.Run(TestAsync);

            async Task TestAsync()
            {
                using (var textReader = new StringReader(k_TestJsonStartingWithComment))
                using (var jsonReader = new JsonTextReader(textReader))
                {
                    var a = (JArray)await JToken.ReadFromAsync(
                        jsonReader, new JsonLoadSettings
                        {
                            CommentHandling = CommentHandling.Ignore
                        });

                    Assert.AreEqual(JTokenType.Array, a.Type);

                    IJsonLineInfo lineInfo = a;
                    Assert.AreEqual(true, lineInfo.HasLineInfo());
                    Assert.AreEqual(3, lineInfo.LineNumber);
                    Assert.AreEqual(1, lineInfo.LinePosition);
                }
            }
        }

        [Test]
        public void ReadFromWithCommentHandlingLoadOnJsonStartingWithCommentReadsComment()
        {
            using (var textReader = new StringReader(k_TestJsonStartingWithComment))
            using (var jsonReader = new JsonTextReader(textReader))
            {
                var v = (JValue)JToken.ReadFrom(
                    jsonReader, new JsonLoadSettings
                    {
                        CommentHandling = CommentHandling.Load
                    });

                Assert.AreEqual(JTokenType.Comment, v.Type);
                IJsonLineInfo lineInfo = v;
                Assert.AreEqual(true, lineInfo.HasLineInfo());
                Assert.AreEqual(2, lineInfo.LineNumber);
                Assert.AreEqual(5, lineInfo.LinePosition);
            }
        }

        [UnityTest]
        public IEnumerator ReadFromAsyncWithCommentHandlingLoadOnJsonStartingWithCommentReadsComment()
        {
            yield return AsyncUtils.Run(TestAsync);

            async Task TestAsync()
            {
                using (var textReader = new StringReader(k_TestJsonStartingWithComment))
                using (var jsonReader = new JsonTextReader(textReader))
                {
                    var v = (JValue)await JToken.ReadFromAsync(
                        jsonReader, new JsonLoadSettings
                        {
                            CommentHandling = CommentHandling.Load
                        });

                    Assert.AreEqual(JTokenType.Comment, v.Type);

                    IJsonLineInfo lineInfo = v;
                    Assert.AreEqual(true, lineInfo.HasLineInfo());
                    Assert.AreEqual(2, lineInfo.LineNumber);
                    Assert.AreEqual(5, lineInfo.LinePosition);
                }
            }
        }

        [Test]
        public void ReadFromOnJsonStartingUndefinedSucceeds()
        {
            using (var textReader = new StringReader(k_TestJsonStartingWithUndefined))
            using (var jsonReader = new JsonTextReader(textReader))
            {
                var v = (JValue)JToken.ReadFrom(jsonReader);

                Assert.AreEqual(JTokenType.Undefined, v.Type);
                IJsonLineInfo lineInfo = v;
                Assert.AreEqual(true, lineInfo.HasLineInfo());
                Assert.AreEqual(2, lineInfo.LineNumber);
                Assert.AreEqual(9, lineInfo.LinePosition);
            }
        }

        [UnityTest]
        public IEnumerator ReadFromAsyncOnJsonStartingUndefinedSucceeds()
        {
            yield return AsyncUtils.Run(TestAsync);

            async Task TestAsync()
            {
                using (var textReader = new StringReader(k_TestJsonStartingWithUndefined))
                using (var jsonReader = new JsonTextReader(textReader))
                {
                    var v = (JValue)await JToken.ReadFromAsync(jsonReader);

                    Assert.AreEqual(JTokenType.Undefined, v.Type);
                    IJsonLineInfo lineInfo = v;
                    Assert.AreEqual(true, lineInfo.HasLineInfo());
                    Assert.AreEqual(2, lineInfo.LineNumber);
                    Assert.AreEqual(9, lineInfo.LinePosition);
                }
            }
        }

        [Test]
        public void ReadFromStartingAtArrayEndThrows()
        {
            using (var textReader = new StringReader(@"[]"))
            using (var jsonReader = new JsonTextReader(textReader))
            {
                jsonReader.Read();
                jsonReader.Read();

                Assert.Throws<JsonReaderException>(
                    ReadFrom,
                    @"Error reading JToken from JsonReader. Unexpected token: EndArray. Path '', line 1, position 2.");

                void ReadFrom() => JToken.ReadFrom(jsonReader);
            }
        }

        [UnityTest]
        public IEnumerator ReadFromAsyncStartingAtArrayEndThrows()
        {
            yield return AsyncUtils.Run(TestAsync);

            async Task TestAsync()
            {
                var textReader = new StringReader(@"[]");
                var jsonReader = new JsonTextReader(textReader);
                await jsonReader.ReadAsync();
                await jsonReader.ReadAsync();

                await AsyncUtils.ThrowsAsync<JsonReaderException>(ReadFromAsync);

                async Task ReadFromAsync()
                {
                    using (textReader)
                    using (jsonReader)
                    {
                        await JToken.ReadFromAsync(jsonReader);
                    }
                }
            }
        }

        [Test]
        public void FromObjectOnGuidSucceeds()
        {
            var expected = new JValue(Guid.NewGuid());

            var token = JToken.FromObject(expected);

            Assert.IsTrue(JToken.DeepEquals(expected, token));
            Assert.AreEqual(expected.Type, token.Type);
        }

        [Test]
        public void FromObjectOnTimeSpanSucceeds()
        {
            var expected = new JValue(TimeSpan.FromDays(1));

            var token = JToken.FromObject(expected);

            Assert.IsTrue(JToken.DeepEquals(expected, token));
            Assert.AreEqual(expected.Type, token.Type);
        }

        [Test]
        public void FromObjectOnUriSucceeds()
        {
            var expected = new JValue(new Uri("http://www.newtonsoft.com"));

            var token = JToken.FromObject(expected);

            Assert.IsTrue(JToken.DeepEquals(expected, token));
            Assert.AreEqual(expected.Type, token.Type);
        }
    }
}

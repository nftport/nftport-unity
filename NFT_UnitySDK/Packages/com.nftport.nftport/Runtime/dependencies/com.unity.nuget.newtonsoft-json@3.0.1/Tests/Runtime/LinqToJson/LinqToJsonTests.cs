using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json.Converters;
using System.Linq;
using Unity.Nuget.NewtonsoftJson.Tests.TestObjects;

namespace Unity.Nuget.NewtonsoftJson.Tests
{
    [TestFixture]
    class LinqToJsonTest : NewtonsoftTests
    {
        [Test]
        public void ForEachIsUsableOnJArray()
        {
            var items = new JArray(new JObject(new JProperty("name", "value!")));

            foreach (var jToken in items)
            {
                var friend = (JObject)jToken;
                StringUtils.AssertAreEqualWithNormalizedLineEndings(@"{
  ""name"": ""value!""
}", friend.ToString());
            }
        }

        [Test]
        public void JsonSerializerDeserializeOnJObjectSucceeds()
        {
            var o = new JObject(
                new JProperty("Name", "John Smith"),
                new JProperty("BirthDate", new DateTime(1983, 3, 20))
            );
            var serializer = new JsonSerializer();

            var p = (Person)serializer.Deserialize(new JTokenReader(o), typeof(Person));

            Assert.AreEqual("John Smith", p.Name);
        }

        [Test]
        [SuppressMessage(
            "ReSharper", "CollectionNeverUpdated.Local",
            Justification = "Collection isn't meant to be updated")]
        public void JObjectThrowsArgumentExceptionWhenUsingInvalidIndex()
        {
            var o = new JObject();

            Assert.Throws<ArgumentException>(
                AccessOutOfRangeElement,
                "Accessed JObject values with invalid key value: 0. Object property name expected.");

            void AccessOutOfRangeElement() => Assert.IsNull(o[0]);
        }

        [Test]
        [SuppressMessage(
            "ReSharper", "CollectionNeverUpdated.Local",
            Justification = "Collection isn't meant to be updated")]
        public void JArrayThrowsArgumentExceptionWhenUsingInvalidIndex()
        {
            var a = new JArray();

            Assert.Throws<ArgumentException>(
                AccessOutOfRangeElement,
                @"Accessed JArray values with invalid key value: ""purple"". Int32 array index expected.");

            void AccessOutOfRangeElement() => Assert.IsNull(a["purple"]);
        }

        [Test]
        public void JConstructorThrowsArgumentExceptionWhenUsingInvalidKey()
        {
            var c = new JConstructor("ConstructorValue");

            Assert.Throws<ArgumentException>(
                AccessOutOfRangeElement,
                @"Accessed JConstructor values with invalid key value: ""purple"". Argument position index expected.");

            void AccessOutOfRangeElement() => Assert.IsNull(c["purple"]);
        }

        [Test]
        public void JsonWriterSerializeOnJObjectWithCustomConverterSucceeds()
        {
            var o = new JObject(
                new JProperty("Test1", new DateTime(2000, 10, 15, 5, 5, 5, DateTimeKind.Utc)),
                new JProperty("Test2", new DateTimeOffset(2000, 10, 15, 5, 5, 5, new TimeSpan(11, 11, 0))),
                new JProperty("Test3", "Test3Value"),
                new JProperty("Test4", null)
            );

            var serializer = new JsonSerializer();
            serializer.Converters.Add(new JavaScriptDateTimeConverter());
            using (var sw = new StringWriter())
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                writer.Formatting = Formatting.Indented;

                serializer.Serialize(writer, o);

                var json = sw.ToString();
                StringUtils.AssertAreEqualWithNormalizedLineEndings(@"{
  ""Test1"": new Date(
    971586305000
  ),
  ""Test2"": new Date(
    971546045000
  ),
  ""Test3"": ""Test3Value"",
  ""Test4"": null
}", json);
            }
        }

        [Test]
        public void JTokenWriterTokenIsStillValidAfterDisposal()
        {
            var testDates = new List<DateTimeOffset>
            {
                new DateTimeOffset(new DateTime(100, 1, 1, 1, 1, 1, DateTimeKind.Utc)),
                new DateTimeOffset(2000, 1, 1, 1, 1, 1, TimeSpan.Zero),
                new DateTimeOffset(2000, 1, 1, 1, 1, 1, TimeSpan.FromHours(13)),
                new DateTimeOffset(2000, 1, 1, 1, 1, 1, TimeSpan.FromHours(-3.5)),
            };
            var jsonSerializer = new JsonSerializer();

            JTokenWriter jsonWriter;
            using (jsonWriter = new JTokenWriter())
            {
                jsonSerializer.Serialize(jsonWriter, testDates);
            }

            Assert.AreEqual(4, jsonWriter.Token.Children().Count());
        }

        [Test]
        public void JsonSerializerSerializeWithPreserveReferencesHandlingObjectsSucceeds()
        {
            var dic1 = new Dictionary<string, object>();
            var dic2 = new Dictionary<string, object>();
            var dic3 = new Dictionary<string, object>();
            var list1 = new List<object>();
            var list2 = new List<object>();
            dic1.Add("list1", list1);
            dic1.Add("list2", list2);
            dic1.Add("dic1", dic1);
            dic1.Add("dic2", dic2);
            dic1.Add("dic3", dic3);
            dic1.Add("integer", 12345);
            list1.Add("A string!");
            list1.Add(dic1);
            list1.Add(new List<object>());
            dic3.Add("dic3", dic3);

            var json = SerializeWithoutUnusedIdProperties(dic1);

            StringUtils.AssertAreEqualWithNormalizedLineEndings(@"{
  ""$id"": ""1"",
  ""list1"": [
    ""A string!"",
    {
      ""$ref"": ""1""
    },
    []
  ],
  ""list2"": [],
  ""dic1"": {
    ""$ref"": ""1""
  },
  ""dic2"": {},
  ""dic3"": {
    ""$id"": ""3"",
    ""dic3"": {
      ""$ref"": ""3""
    }
  },
  ""integer"": 12345
}", json);

            string SerializeWithoutUnusedIdProperties(object o)
            {
                using (var writer = new JTokenWriter())
                {
                    var serializer = JsonSerializer.Create(
                        new JsonSerializerSettings
                        {
                            Formatting = Formatting.Indented,
                            PreserveReferencesHandling = PreserveReferencesHandling.Objects
                        });
                    serializer.Serialize(writer, o);

                    var token = writer.Token;
                    if (token is JContainer container)
                    {
                        RemoveUnusedIds(container);
                    }

                    return token.ToString();
                }
            }

            void RemoveUnusedIds(JContainer container)
            {
                var properties = container.Descendants()
                    .OfType<JProperty>()
                    .ToList();

                // find all the $id properties in the JSON
                var ids = properties.Where(d => d.Name == "$id").ToList();
                if (ids.Count <= 0)
                    return;

                // find all the $ref properties in the JSON
                var refs = properties.Where(d => d.Name == "$ref").ToList();
                foreach (var idProperty in ids)
                {
                    // check whether the $id property is used by a $ref
                    if (IsIdReferenced(idProperty))
                        continue;

                    // remove unused $id
                    idProperty.Remove();
                }

                bool IsIdReferenced(JProperty idProperty)
                {
                    return refs.Any(r => idProperty.Value.ToString() == r.Value.ToString());
                }
            }
        }
    }
}

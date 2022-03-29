using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using NUnit.Framework;
using Unity.Nuget.NewtonsoftJson.Tests.TestObjects;

namespace Unity.Nuget.NewtonsoftJson.Tests
{
    [TestFixture]
    class DefaultSettingsTests : NewtonsoftTests
    {
        [Test]
        public void SerializeObjectWithoutSettingsAsArgumentUseStaticSettings()
        {
            JsonConvert.DefaultSettings = CreateIndentedSettings;
            var dummy = new Dummy
            {
                Test = new[] { 1, 2, 3 }
            };
            var json = JsonConvert.SerializeObject(dummy);

            StringUtils.AssertAreEqualWithNormalizedLineEndings(@"{
  ""Test"": [
    1,
    2,
    3
  ]
}", json);
        }

        [Test]
        public void SerializeObjectWithSettingsAsArgumentsUsePassedSettings()
        {
            JsonConvert.DefaultSettings = CreateIndentedSettings;
            var dummy = new Dummy
            {
                Test = new[] { 1, 2, 3 }
            };
            var json = JsonConvert.SerializeObject(dummy,
                new JsonSerializerSettings
                {
                    Formatting = Formatting.None
                });

            StringUtils.AssertAreEqualWithNormalizedLineEndings(@"{""Test"":[1,2,3]}", json);
        }

        [Test]
        public void SerializeObjectWithCustomContractResolver()
        {
            JsonConvert.DefaultSettings = CreateSettings;
            var e = new Employee
            {
                FirstName = "Eric",
                LastName = "Example",
                BirthDate = new DateTime(1980, 4, 20, 0, 0, 0, DateTimeKind.Utc),
                Department = "IT",
                JobTitle = "Web Dude"
            };

            var json = JsonConvert.SerializeObject(e);

            StringUtils.AssertAreEqualWithNormalizedLineEndings(@"{
  ""firstName"": ""Eric"",
  ""lastName"": ""Example"",
  ""birthDate"": ""1980-04-20T00:00:00Z"",
  ""department"": ""IT"",
  ""jobTitle"": ""Web Dude""
}", json);

            JsonSerializerSettings CreateSettings()
                => new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };
        }

        [Test]
        public void SerializeObjectWithManyConvertersRespectOrder()
        {
            JsonConvert.DefaultSettings = CreateSettings;
            var json = JsonConvert.SerializeObject(
                new[]
                {
                    new DateTime(2000, 12, 12, 4, 2, 4, DateTimeKind.Utc)
                }, new JsonSerializerSettings
                {
                    Formatting = Formatting.None,
                    Converters =
                    {
                        // should take precedence
                        new JavaScriptDateTimeConverter(),
                        new IsoDateTimeConverter
                        {
                            DateTimeFormat = "dd"
                        }
                    }
                });

            StringUtils.AssertAreEqualWithNormalizedLineEndings(@"[new Date(976593724000)]", json);

            JsonSerializerSettings CreateSettings()
                => new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    Converters =
                    {
                        new IsoDateTimeConverter
                        {
                            DateTimeFormat = "yyyy"
                        }
                    }
                };
        }

        [Test]
        public void JsonSerializerCreateDefaultUsesJsonConvertDefaultSettings()
        {
            JsonConvert.DefaultSettings = CreateIndentedSettings;
            IList<int> l = new List<int> { 1, 2, 3 };

            using (var sw = new StringWriter())
            {
                var serializer = JsonSerializer.CreateDefault();
                serializer.Serialize(sw, l);

                StringUtils.AssertAreEqualWithNormalizedLineEndings(@"[
  1,
  2,
  3
]", sw.ToString());
            }
        }

        [Test]
        public void JsonSerializerCreateDefaultWithCustomSettingsMergesThemWithJsonConvertDefaultSettings()
        {
            JsonConvert.DefaultSettings = CreateIndentedSettings;
            IList<int> l = new List<int> { 1, 2, 3 };
            var serializer = JsonSerializer.CreateDefault(
                new JsonSerializerSettings
                {
                    Converters = { new IntConverter() }
                });

            using (var sw = new StringWriter())
            {
                serializer.Serialize(sw, l);

                StringUtils.AssertAreEqualWithNormalizedLineEndings(@"[
  2,
  4,
  6
]", sw.ToString());
            }

            using (var sw = new StringWriter())
            {
                serializer.Converters.Clear();
                serializer.Serialize(sw, l);

                StringUtils.AssertAreEqualWithNormalizedLineEndings(@"[
  1,
  2,
  3
]", sw.ToString());
            }
        }

        [Test]
        public void JsonSerializerCreateDefaultOverridesJsonConvertDefaultSettingsWithLocalChanges()
        {
            JsonConvert.DefaultSettings = CreateIndentedSettings;
            IList<int> l = new List<int> { 1, 2, 3 };

            using (var sw = new StringWriter())
            {
                var serializer = JsonSerializer.CreateDefault();
                serializer.Formatting = Formatting.None;
                serializer.Serialize(sw, l);

                Assert.AreEqual(@"[1,2,3]", sw.ToString());
            }
        }

        [Test]
        public void JsonSerializerCreateUsesDefaultSettings()
        {
            JsonConvert.DefaultSettings = CreateIndentedSettings;
            IList<int> l = new List<int> { 1, 2, 3 };

            using (var sw = new StringWriter())
            {
                var serializer = JsonSerializer.Create();
                serializer.Serialize(sw, l);

                Assert.AreEqual(@"[1,2,3]", sw.ToString());
            }
        }

        [Test]
        public void JsonSerializerNewInstanceUsesDefaultSettings()
        {
            JsonConvert.DefaultSettings = CreateIndentedSettings;
            IList<int> l = new List<int> { 1, 2, 3 };

            using (var sw = new StringWriter())
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(sw, l);

                Assert.AreEqual(@"[1,2,3]", sw.ToString());
            }
        }

        static JsonSerializerSettings CreateIndentedSettings()
            => new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
    }
}

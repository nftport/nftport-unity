using System;
using Newtonsoft.Json;
using NUnit.Framework;
using Unity.Nuget.NewtonsoftJson.Tests.TestObjects;

namespace Unity.Nuget.NewtonsoftJson.Tests
{
    [TestFixture]
    class SerializeObjectTests : NewtonsoftTests
    {
        // [Test]
        // Anonymous objects are not supported on IL2CPP
        public void SerializeObjectSucceedsFoAnonymousObject()
        {
            var anonymousObject = new
            {
                test = new[] { 1, 2, 3 }
            };

            var json = JsonConvert.SerializeObject(anonymousObject);

            StringUtils.AssertAreEqualWithNormalizedLineEndings(@"{
  ""test"": [
    1,
    2,
    3
  ]
}", json);
        }

        [Test]
        public void SerializeObjectWithUtcDateTimeZoneHandlingSucceeds()
        {
            var json = JsonConvert.SerializeObject(
                new DateTime(2000, 1, 1, 1, 1, 1, DateTimeKind.Unspecified),
                new JsonSerializerSettings
                {
                    DateTimeZoneHandling = DateTimeZoneHandling.Utc
                });

            Assert.AreEqual(@"""2000-01-01T01:01:01Z""", json);
        }

        [Test]
        public void SerializeObjectWithCustomJsonConverterAsAttributeSucceeds()
        {
            var clobber = new ClobberMyProperties
            {
                One = "Red",
                Two = "Green",
                Three = "Yellow",
                Four = "Black"
            };

            var json = JsonConvert.SerializeObject(clobber);

            Assert.AreEqual("{\"One\":\"Uno-1-Red\",\"Two\":\"Dos-2-Green\",\"Three\":\"Tres-1337-Yellow\",\"Four\":\"Black\"}", json);
        }

        [Test]
        public void SerializeObjectWithWrongParametersPassedToJsonConverterAsAttributeThrows()
        {
            var value = new IncorrectJsonConvertParameters { One = "Boom" };

            Assert.Throws<JsonException>(SerializeWrongParameters);

            void SerializeWrongParameters() => JsonConvert.SerializeObject(value);
        }

        [Test]
        public void SerializeObjectWithCustomJsonConverterAsAttributeSupportsType()
        {
            var value = new OverloadWithTypeParameter();

            var json = JsonConvert.SerializeObject(value);

            Assert.AreEqual("{\"Overload\":\"Type\"}", json);
        }

        [Test]
        public void SerializeObjectWithCustomJsonConverterAsAttributeSupportsObject()
        {
            var value = new OverloadWithUnhandledParameter();

            var json = JsonConvert.SerializeObject(value);

            Assert.AreEqual("{\"Overload\":\"object(System.String)\"}", json);
        }

        [Test]
        public void SerializeObjectWithCustomJsonConverterAsAttributeSupportsPrimitives()
        {
            {
                var value = new OverloadWithIntParameter();
                var json = JsonConvert.SerializeObject(value);
                Assert.AreEqual("{\"Overload\":\"int\"}", json);
            }

            {
                // uint -> long
                var value = new OverloadWithUIntParameter();
                var json = JsonConvert.SerializeObject(value);
                Assert.AreEqual("{\"Overload\":\"long\"}", json);
            }

            {
                var value = new OverloadWithLongParameter();
                var json = JsonConvert.SerializeObject(value);
                Assert.AreEqual("{\"Overload\":\"long\"}", json);
            }

            {
                // ulong -> double
                var value = new OverloadWithULongParameter();
                var json = JsonConvert.SerializeObject(value);
                Assert.AreEqual("{\"Overload\":\"double\"}", json);
            }

            {
                var value = new OverloadWithShortParameter();
                var json = JsonConvert.SerializeObject(value);
                Assert.AreEqual("{\"Overload\":\"short\"}", json);
            }

            {
                // ushort -> int
                var value = new OverloadWithUShortParameter();
                var json = JsonConvert.SerializeObject(value);
                Assert.AreEqual("{\"Overload\":\"int\"}", json);
            }

            {
                // sbyte -> short
                var value = new OverloadWithSByteParameter();
                var json = JsonConvert.SerializeObject(value);
                Assert.AreEqual("{\"Overload\":\"short\"}", json);
            }

            {
                var value = new OverloadWithByteParameter();
                var json = JsonConvert.SerializeObject(value);
                Assert.AreEqual("{\"Overload\":\"byte\"}", json);
            }

            {
                // char -> int
                var value = new OverloadWithCharParameter();
                var json = JsonConvert.SerializeObject(value);
                Assert.AreEqual("{\"Overload\":\"int\"}", json);
            }

            {
                // bool -> (object)bool
                var value = new OverloadWithBoolParameter();
                var json = JsonConvert.SerializeObject(value);
                Assert.AreEqual("{\"Overload\":\"object(System.Boolean)\"}", json);
            }

            {
                // float -> double
                var value = new OverloadWithFloatParameter();
                var json = JsonConvert.SerializeObject(value);
                Assert.AreEqual("{\"Overload\":\"double\"}", json);
            }

            {
                var value = new OverloadWithDoubleParameter();
                var json = JsonConvert.SerializeObject(value);
                Assert.AreEqual("{\"Overload\":\"double\"}", json);
            }
        }

        [Test]
        public void SerializeObjectSupportsGenericParentsOfANonGenericClass()
        {
            var json = JsonConvert.SerializeObject(new NonGenericChildClass());

            Assert.AreEqual("{\"Data\":null}", json);
        }
    }
}

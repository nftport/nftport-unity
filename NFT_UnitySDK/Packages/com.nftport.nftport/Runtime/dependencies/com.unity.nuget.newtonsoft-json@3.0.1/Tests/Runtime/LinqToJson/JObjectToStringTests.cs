using System.Globalization;
using System.Linq;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace Unity.Nuget.NewtonsoftJson.Tests
{
    class JObjectToStringTests : NewtonsoftTests
    {
        [Test]
        public void JTokenToString()
        {
            const string json = @"{
  CPU: 'Intel',
  Drives: [
    'DVD read/writer',
    ""500 gigabyte hard drive""
  ]
}";

            var o = JObject.Parse(json);

            StringUtils.AssertAreEqualWithNormalizedLineEndings(@"{
  ""CPU"": ""Intel"",
  ""Drives"": [
    ""DVD read/writer"",
    ""500 gigabyte hard drive""
  ]
}", o.ToString());

            var list = o.Value<JArray>("Drives");
            StringUtils.AssertAreEqualWithNormalizedLineEndings(@"[
  ""DVD read/writer"",
  ""500 gigabyte hard drive""
]", list.ToString());

            var cpuProperty = o.Property("CPU");
            Assert.AreEqual(@"""CPU"": ""Intel""", cpuProperty.ToString());

            var drivesProperty = o.Property("Drives");
            StringUtils.AssertAreEqualWithNormalizedLineEndings(@"""Drives"": [
  ""DVD read/writer"",
  ""500 gigabyte hard drive""
]", drivesProperty.ToString());
        }

        [Test]
        public void JTokenToStringTypes()
        {
          const string json = @"{""Color"":2,""Establised"":new Date(1264118400000),""Width"":1.1,""Employees"":999,""RoomsPerFloor"":[1,2,3,4,5,6,7,8,9],""Open"":false,""Symbol"":""@"",""Mottos"":[""Hello World"",""öäüÖÄÜ\\'{new Date(12345);}[222]_µ@²³~"",null,"" ""],""Cost"":100980.1,""Escape"":""\r\n\t\f\b?{\\r\\n\""'"",""product"":[{""Name"":""Rocket"",""ExpiryDate"":new Date(949532490000),""Price"":0},{""Name"":""Alien"",""ExpiryDate"":new Date(-62135596800000),""Price"":0}]}";

          var o = JObject.Parse(json);

          StringUtils.AssertAreEqualWithNormalizedLineEndings(@"""Establised"": new Date(
  1264118400000
)", o.Property("Establised").ToString());
          StringUtils.AssertAreEqualWithNormalizedLineEndings(@"new Date(
  1264118400000
)", o.Property("Establised").Value.ToString());
          Assert.AreEqual(@"""Width"": 1.1", o.Property("Width").ToString());
          Assert.AreEqual(@"1.1", ((JValue)o.Property("Width").Value).ToString(CultureInfo.InvariantCulture));
          Assert.AreEqual(@"""Open"": false", o.Property("Open").ToString());
          Assert.AreEqual(@"False", o.Property("Open").Value.ToString());
        }

        [Test]
        public void JTokenToStringOnNullAndUndefinedSucceeds()
        {
          const string json = @"[null,undefined]";

          var a = JArray.Parse(json);
          StringUtils.AssertAreEqualWithNormalizedLineEndings(@"[
  null,
  undefined
]", a.ToString());
          Assert.AreEqual(@"", a.Children().ElementAt(0).ToString());
          Assert.AreEqual(@"", a.Children().ElementAt(1).ToString());

        }
    }
}

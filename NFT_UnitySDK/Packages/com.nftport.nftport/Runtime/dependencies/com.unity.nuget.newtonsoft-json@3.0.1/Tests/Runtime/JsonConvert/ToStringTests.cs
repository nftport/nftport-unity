using System;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Unity.Nuget.NewtonsoftJson.Tests
{
    [TestFixture]
    class ToStringTests : NewtonsoftTests
    {
        [Test]
        public void ToStringEnsureEscapedArrayLength()
        {
            const char nonAsciiChar = (char)257;
            const char escapableNonQuoteAsciiChar = '\0';
            var value = nonAsciiChar + @"\" + escapableNonQuoteAsciiChar;

            var convertedValue = JsonConvert.ToString((object)value);

            Assert.AreEqual(@"""" + nonAsciiChar + @"\\\u0000""", convertedValue);
        }

        [Test]
        public void ToStringThrowsForVersion()
        {
            Assert.Throws<ArgumentException>(VersionToString, "Unsupported type: System.Version. Use the JsonSerializer class to get the object's JSON representation.");

            void VersionToString() => JsonConvert.ToString(new Version(1, 0));
        }

        [Test]
        public void ToStringSucceedsForGuid()
        {
            var guid = new Guid("BED7F4EA-1A96-11d2-8F08-00A0C9A6186D");

            var json = JsonConvert.ToString(guid);

            Assert.AreEqual(@"""bed7f4ea-1a96-11d2-8f08-00a0c9a6186d""", json);
        }

        [Test]
        public void ToStringWritesEnumsAsNumberByDefault()
        {
            var json = JsonConvert.ToString(StringComparison.CurrentCultureIgnoreCase);

            Assert.AreEqual("1", json);
        }

        [Test]
        public void ToStringSucceedsForPrimitivesAsObject()
        {
            object value = 1;
            Assert.AreEqual("1", JsonConvert.ToString(value));

            value = 1.1;
            Assert.AreEqual("1.1", JsonConvert.ToString(value));

            value = 1.1m;
            Assert.AreEqual("1.1", JsonConvert.ToString(value));

            value = (float)1.1;
            Assert.AreEqual("1.1", JsonConvert.ToString(value));

            value = (short)1;
            Assert.AreEqual("1", JsonConvert.ToString(value));

            value = (long)1;
            Assert.AreEqual("1", JsonConvert.ToString(value));

            value = (byte)1;
            Assert.AreEqual("1", JsonConvert.ToString(value));

            value = (uint)1;
            Assert.AreEqual("1", JsonConvert.ToString(value));

            value = (ushort)1;
            Assert.AreEqual("1", JsonConvert.ToString(value));

            value = (sbyte)1;
            Assert.AreEqual("1", JsonConvert.ToString(value));

            value = (ulong)1;
            Assert.AreEqual("1", JsonConvert.ToString(value));

            value = new DateTime(DateTimeUtils.InitialJavaScriptDateTicks, DateTimeKind.Utc);
            Assert.AreEqual(@"""1970-01-01T00:00:00Z""", JsonConvert.ToString(value));

            value = new DateTime(DateTimeUtils.InitialJavaScriptDateTicks, DateTimeKind.Utc);
            Assert.AreEqual(@"""\/Date(0)\/""", JsonConvert.ToString((DateTime)value, DateFormatHandling.MicrosoftDateFormat, DateTimeZoneHandling.RoundtripKind));

            value = new DateTimeOffset(DateTimeUtils.InitialJavaScriptDateTicks, TimeSpan.Zero);
            Assert.AreEqual(@"""1970-01-01T00:00:00+00:00""", JsonConvert.ToString(value));

            value = new DateTimeOffset(DateTimeUtils.InitialJavaScriptDateTicks, TimeSpan.Zero);
            Assert.AreEqual(@"""\/Date(0+0000)\/""", JsonConvert.ToString((DateTimeOffset)value, DateFormatHandling.MicrosoftDateFormat));

            Assert.AreEqual("null", JsonConvert.ToString((object)null));

            value = DBNull.Value;
            Assert.AreEqual("null", JsonConvert.ToString(value));

            value = "I am a string";
            Assert.AreEqual(@"""I am a string""", JsonConvert.ToString(value));

            value = true;
            Assert.AreEqual("true", JsonConvert.ToString(value));

            value = 'c';
            Assert.AreEqual(@"""c""", JsonConvert.ToString(value));
        }

        [Test]
        public void ToStringSucceedsForFloatAndDouble()
        {
            Assert.AreEqual("1.1", JsonConvert.ToString(1.1));
            Assert.AreEqual("1.11", JsonConvert.ToString(1.11));
            Assert.AreEqual("1.111", JsonConvert.ToString(1.111));
            Assert.AreEqual("1.1111", JsonConvert.ToString(1.1111));
            Assert.AreEqual("1.11111", JsonConvert.ToString(1.11111));
            Assert.AreEqual("1.111111", JsonConvert.ToString(1.111111));
            Assert.AreEqual("1.0", JsonConvert.ToString(1.0));
            Assert.AreEqual("1.0", JsonConvert.ToString(1d));
            Assert.AreEqual("-1.0", JsonConvert.ToString(-1d));
            Assert.AreEqual("1.01", JsonConvert.ToString(1.01));
            Assert.AreEqual("1.001", JsonConvert.ToString(1.001));
            Assert.AreEqual(JsonConvert.PositiveInfinity, JsonConvert.ToString(double.PositiveInfinity));
            Assert.AreEqual(JsonConvert.NegativeInfinity, JsonConvert.ToString(double.NegativeInfinity));
            Assert.AreEqual(JsonConvert.NaN, JsonConvert.ToString(double.NaN));
        }

        [Test]
        public void ToStringSucceedsForDecimal()
        {
            Assert.AreEqual("1.1", JsonConvert.ToString(1.1m));
            Assert.AreEqual("1.11", JsonConvert.ToString(1.11m));
            Assert.AreEqual("1.111", JsonConvert.ToString(1.111m));
            Assert.AreEqual("1.1111", JsonConvert.ToString(1.1111m));
            Assert.AreEqual("1.11111", JsonConvert.ToString(1.11111m));
            Assert.AreEqual("1.111111", JsonConvert.ToString(1.111111m));
            Assert.AreEqual("1.0", JsonConvert.ToString(1.0m));
            Assert.AreEqual("-1.0", JsonConvert.ToString(-1.0m));
            Assert.AreEqual("-1.0", JsonConvert.ToString(-1m));
            Assert.AreEqual("1.0", JsonConvert.ToString(1m));
            Assert.AreEqual("1.01", JsonConvert.ToString(1.01m));
            Assert.AreEqual("1.001", JsonConvert.ToString(1.001m));
            Assert.AreEqual("79228162514264337593543950335.0", JsonConvert.ToString(decimal.MaxValue));
            Assert.AreEqual("-79228162514264337593543950335.0", JsonConvert.ToString(decimal.MinValue));
        }

        [Test]
        public void ToStringProperlyEscapesString()
        {
            const string v = "It's a good day\r\n\"sunshine\"";

            var json = JsonConvert.ToString(v);

            Assert.AreEqual(@"""It's a good day\r\n\""sunshine\""""", json);
        }

        [Test]
        public void ToStringWithDelimiterProperlyDelimitsString()
        {
            var v = "<b>hi " + '\u20AC' + "</b>";

            var json = JsonConvert.ToString(v, '"');
            Assert.AreEqual(@"""<b>hi " + '\u20AC' + @"</b>""", json);

            json = JsonConvert.ToString(v, '"', StringEscapeHandling.EscapeHtml);
            Assert.AreEqual(@"""\u003cb\u003ehi " + '\u20AC' + @"\u003c/b\u003e""", json);

            json = JsonConvert.ToString(v, '"', StringEscapeHandling.EscapeNonAscii);
            Assert.AreEqual(@"""<b>hi \u20ac</b>""", json);
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NUnit.Framework;
using Unity.Nuget.NewtonsoftJson.Tests.TestObjects;

namespace Unity.Nuget.NewtonsoftJson.Tests
{
    [TestFixture]
    class DeserializeDateTests : NewtonsoftTests
    {
        [TestCaseSource(nameof(GetDateTimeTestData))]
        public void DeserializeObjectForDateTimeLocalSucceeds(DateTimeResult expected, DateTime toTest)
        {
            var result = TestDateTime(toTest);

            Assert.AreEqual(expected.IsoDateRoundtrip, result.IsoDateRoundtrip);
            Assert.AreEqual(expected.IsoDateLocal, result.IsoDateLocal);
            Assert.AreEqual(expected.IsoDateUnspecified, result.IsoDateUnspecified);
            Assert.AreEqual(expected.IsoDateUtc, result.IsoDateUtc);
            Assert.AreEqual(expected.MsDateRoundtrip, result.MsDateRoundtrip);
            Assert.AreEqual(expected.MsDateLocal, result.MsDateLocal);
            Assert.AreEqual(expected.MsDateUnspecified, result.MsDateUnspecified);
            Assert.AreEqual(expected.MsDateUtc, result.MsDateUtc);
        }

        static IEnumerable<TestCaseData> GetDateTimeTestData()
        {
            // Local
            var date = new DateTime(2000, 1, 1, 1, 1, 1, DateTimeKind.Local);
            var expectedResult = GetExpectedResultForLocal("2000-01-01T01:01:01");
            yield return new TestCaseData(expectedResult, date);

            // Local with milliseconds
            date = new DateTime(2000, 1, 1, 1, 1, 1, 999, DateTimeKind.Local);
            expectedResult = GetExpectedResultForLocal("2000-01-01T01:01:01.999");
            yield return new TestCaseData(expectedResult, date);

            // Local from ticks
            date = new DateTime(636556897826822481, DateTimeKind.Local);
            expectedResult = GetExpectedResultForLocal("2018-03-03T16:03:02.6822481");
            yield return new TestCaseData(expectedResult, date);

            // Unspecified
            date = new DateTime(2000, 1, 1, 1, 1, 1, DateTimeKind.Unspecified);
            expectedResult = GetExpectedResultForUnspecified("2000-01-01T01:01:01");
            yield return new TestCaseData(expectedResult, date);

            // Utc
            date = new DateTime(2000, 1, 1, 1, 1, 1, DateTimeKind.Utc);
            expectedResult = GetExpectedResultForUtc("2000-01-01T01:01:01");
            yield return new TestCaseData(expectedResult, date);

            // Utc from ticks
            date = new DateTime(621355968000000000, DateTimeKind.Utc);
            expectedResult = GetExpectedResultForUtc("1970-01-01T00:00:00");
            yield return new TestCaseData(expectedResult, date);

            // MinValue
            date = DateTime.MinValue;
            expectedResult = GetExpectedResultForLimits("0001-01-01T00:00:00");
            yield return new TestCaseData(expectedResult, date);

            // MaxValue
            date = DateTime.MaxValue;
            expectedResult = GetExpectedResultForLimits("9999-12-31T23:59:59.9999999");
            yield return new TestCaseData(expectedResult, date);

            DateTimeResult GetExpectedResultForUnspecified(string isoDateUnspecified)
            {
                var toIsoOffset = GetIsoOffset();
                var toMsOffset = GetMicrosoftOffset();
                var toJavaScriptTicks = GetJavaScriptsTicks();
                return new DateTimeResult
                {
                    IsoDateRoundtrip = isoDateUnspecified,
                    IsoDateLocal = isoDateUnspecified + toIsoOffset,
                    IsoDateUnspecified = isoDateUnspecified,
                    IsoDateUtc = $"{isoDateUnspecified}Z",
                    MsDateRoundtrip = $@"\/Date({toJavaScriptTicks}{toMsOffset})\/",
                    MsDateLocal = $@"\/Date({toJavaScriptTicks}{toMsOffset})\/",
                    MsDateUnspecified = $@"\/Date({toJavaScriptTicks}{toMsOffset})\/",
                    MsDateUtc = $@"\/Date({DateTimeUtils.ConvertDateTimeToJavaScriptTicks(date.ToLocalTime())})\/",
                };
            }

            DateTimeResult GetExpectedResultForLocal(string isoDateUnspecified)
            {
                var result = GetExpectedResultForUnspecified(isoDateUnspecified);
                result.IsoDateRoundtrip = isoDateUnspecified + GetIsoOffset();
                result.IsoDateUtc = date.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.FFFFFFFK");
                return result;
            }

            DateTimeResult GetExpectedResultForUtc(string isoDateUnspecified)
            {
                var toLocal = date.ToLocalTime().ToString("yyyy-MM-ddTHH:mm:ss");
                var toIsoOffset = GetIsoOffset();
                var toJavaScriptTicks = GetJavaScriptsTicks();
                var toUnspecified = DateTime.SpecifyKind(date, DateTimeKind.Unspecified);
                var toMsOffset = GetMicrosoftOffset();
                var result = GetExpectedResultForUnspecified(isoDateUnspecified);
                result.IsoDateRoundtrip = $"{isoDateUnspecified}Z";
                result.IsoDateLocal = toLocal + toIsoOffset;
                result.MsDateRoundtrip = $@"\/Date({toJavaScriptTicks})\/";
                result.MsDateUnspecified = $@"\/Date({DateTimeUtils.ConvertDateTimeToJavaScriptTicks(toUnspecified)}{toMsOffset})\/";
                return result;
            }

            DateTimeResult GetExpectedResultForLimits(string isoDateUnspecified)
            {
                var toJavaScriptTicks = GetJavaScriptsTicks();
                var result = GetExpectedResultForUnspecified(isoDateUnspecified);
                result.MsDateRoundtrip = $@"\/Date({toJavaScriptTicks})\/";
                result.MsDateUnspecified = $@"\/Date({toJavaScriptTicks})\/";
                return result;
            }

            string GetIsoOffset() => GetOffset(date, DateFormatHandling.IsoDateFormat);

            string GetMicrosoftOffset() => GetOffset(date, DateFormatHandling.MicrosoftDateFormat);

            long GetJavaScriptsTicks() => DateTimeUtils.ConvertDateTimeToJavaScriptTicks(date);
        }

        [TestCaseSource(nameof(GetDateTimeOffsetTestData))]
        public void DeserializeObjectForDateTimeOffsetSucceeds(
            DateTimeOffset offset, string expectedIso, string expectedMs)
        {
            var result = TestDateTime(offset);

            Assert.AreEqual(expectedIso, result.IsoDateRoundtrip);
            Assert.AreEqual(expectedMs, result.MsDateRoundtrip);
        }

        static IEnumerable<TestCaseData> GetDateTimeOffsetTestData()
        {
            // 2000-01-01
            var offset = new DateTimeOffset(2000, 1, 1, 1, 1, 1, TimeSpan.Zero);
            yield return new TestCaseData(offset, "2000-01-01T01:01:01+00:00", @"\/Date(946688461000+0000)\/");

            // 2000-01-01 with 1h offset
            yield return new TestCaseData(offset.ToOffset(TimeSpan.FromHours(1)).AddHours(-1), "2000-01-01T01:01:01+01:00", @"\/Date(946684861000+0100)\/");

            // 2000-01-01 with 1.5h offset
            yield return new TestCaseData(offset.ToOffset(TimeSpan.FromHours(1.5)).AddHours(-1.5), "2000-01-01T01:01:01+01:30", @"\/Date(946683061000+0130)\/");

            // 2000-01-01 with 13h offset
            yield return new TestCaseData(offset.ToOffset(TimeSpan.FromHours(13)).AddHours(-13), "2000-01-01T01:01:01+13:00", @"\/Date(946641661000+1300)\/");

            // From ticks
            yield return new TestCaseData(new DateTimeOffset(634663873826822481, TimeSpan.Zero), "2012-03-03T16:03:02.6822481+00:00", @"\/Date(1330790582682+0000)\/");

            // MinValue
            yield return new TestCaseData(DateTimeOffset.MinValue, "0001-01-01T00:00:00+00:00", @"\/Date(-62135596800000+0000)\/");

            // MaxValue
            yield return new TestCaseData(DateTimeOffset.MaxValue, "9999-12-31T23:59:59.9999999+00:00", @"\/Date(253402300799999+0000)\/");
        }

        static DateTimeResult TestDateTime<T>(T value)
        {
            var result = new DateTimeResult
            {
                IsoDateRoundtrip = TestDateTimeFormat(value, DateFormatHandling.IsoDateFormat, DateTimeZoneHandling.RoundtripKind)
            };

            if (value is DateTime)
            {
                result.IsoDateLocal = TestDateTimeFormat(value, DateFormatHandling.IsoDateFormat, DateTimeZoneHandling.Local);
                result.IsoDateUnspecified = TestDateTimeFormat(value, DateFormatHandling.IsoDateFormat, DateTimeZoneHandling.Unspecified);
                result.IsoDateUtc = TestDateTimeFormat(value, DateFormatHandling.IsoDateFormat, DateTimeZoneHandling.Utc);
            }

            result.MsDateRoundtrip = TestDateTimeFormat(value, DateFormatHandling.MicrosoftDateFormat, DateTimeZoneHandling.RoundtripKind);
            if (value is DateTime)
            {
                result.MsDateLocal = TestDateTimeFormat(value, DateFormatHandling.MicrosoftDateFormat, DateTimeZoneHandling.Local);
                result.MsDateUnspecified = TestDateTimeFormat(value, DateFormatHandling.MicrosoftDateFormat, DateTimeZoneHandling.Unspecified);
                result.MsDateUtc = TestDateTimeFormat(value, DateFormatHandling.MicrosoftDateFormat, DateTimeZoneHandling.Utc);
            }

            TestDateTimeFormat(value, new IsoDateTimeConverter());

            return result;
        }

        static string TestDateTimeFormat<T>(T value, DateFormatHandling format, DateTimeZoneHandling timeZoneHandling)
        {
            var date = value is DateTime
                ? JsonConvert.ToString((DateTime)(object)value, format, timeZoneHandling)
                : JsonConvert.ToString((DateTimeOffset)(object)value, format);

            if (timeZoneHandling != DateTimeZoneHandling.RoundtripKind)
            {
                return date.Trim('"');
            }

            var parsed = JsonConvert.DeserializeObject<T>(date);
            if (value.Equals(parsed))
            {
                return date.Trim('"');
            }

            var valueTicks = GetTicks(value);
            var parsedTicks = GetTicks(parsed);

            valueTicks = valueTicks / 10000 * 10000;

            Assert.AreEqual(valueTicks, parsedTicks);

            return date.Trim('"');
        }

        static void TestDateTimeFormat<T>(T value, JsonConverter converter)
        {
            var date = Write(value, converter);
            var parsed = Read<T>(date, converter);

            try
            {
                Assert.AreEqual(value, parsed);
            }
            catch (Exception)
            {
                // JavaScript ticks aren't as precise, recheck after rounding
                var valueTicks = GetTicks(value);
                var parsedTicks = GetTicks(parsed);

                valueTicks = valueTicks / 10000 * 10000;

                Assert.AreEqual(valueTicks, parsedTicks);
            }
        }

        static long GetTicks(object value)
        {
            return value is DateTime time ? time.Ticks : ((DateTimeOffset)value).Ticks;
        }

        static string Write(object value, JsonConverter converter)
        {
            using (var sw = new StringWriter())
            using (var writer = new JsonTextWriter(sw))
            {
                converter.WriteJson(writer, value, null);
                writer.Flush();
                return sw.ToString();
            }
        }

        static T Read<T>(string text, JsonConverter converter)
        {
            using (var stringReader = new StringReader(text))
            using (var reader = new JsonTextReader(stringReader))
            {
                reader.ReadAsString();
                return (T)converter.ReadJson(reader, typeof(T), null, null);
            }
        }

        static string GetOffset(DateTime d, DateFormatHandling dateFormatHandling)
        {
            var chars = new char[8];
            var localToUtcOffset = DateTime.SpecifyKind(d, DateTimeKind.Local).GetUtcOffset();
            var pos = DateTimeUtils.WriteDateTimeOffset(chars, 0, localToUtcOffset, dateFormatHandling);
            return new string(chars, 0, pos);
        }
    }
}

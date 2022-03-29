using System;
using Newtonsoft.Json;

namespace Unity.Nuget.NewtonsoftJson.Tests
{
    static class DateTimeUtils
    {
        internal const long InitialJavaScriptDateTicks = 621355968000000000;

        public static TimeSpan GetUtcOffset(this DateTime d) => TimeZoneInfo.Local.GetUtcOffset(d);

        static long ToUniversalTicks(DateTime dateTime)
        {
            return dateTime.Kind == DateTimeKind.Utc
                ? dateTime.Ticks
                : ToUniversalTicks(dateTime, dateTime.GetUtcOffset());
        }

        static long ToUniversalTicks(DateTime dateTime, TimeSpan offset)
        {
            // special case min and max value
            // they never have a timezone appended to avoid issues
            if (dateTime.Kind == DateTimeKind.Utc
                || dateTime == DateTime.MaxValue
                || dateTime == DateTime.MinValue)
            {
                return dateTime.Ticks;
            }

            var ticks = dateTime.Ticks - offset.Ticks;
            if (ticks > 3155378975999999999L)
            {
                return 3155378975999999999L;
            }

            if (ticks < 0L)
            {
                return 0L;
            }

            return ticks;
        }

        internal static long ConvertDateTimeToJavaScriptTicks(DateTime dateTime)
        {
            return ConvertDateTimeToJavaScriptTicks(dateTime, true);
        }

        internal static long ConvertDateTimeToJavaScriptTicks(DateTime dateTime, bool convertToUtc)
        {
            var ticks = convertToUtc ? ToUniversalTicks(dateTime) : dateTime.Ticks;
            return UniversalTicksToJavaScriptTicks(ticks);
        }

        static long UniversalTicksToJavaScriptTicks(long universalTicks)
        {
            var javaScriptTicks = (universalTicks - InitialJavaScriptDateTicks) / 10000;
            return javaScriptTicks;
        }

        static void CopyIntToCharArray(char[] chars, int start, int value, int digits)
        {
            while (digits-- != 0)
            {
                chars[start + digits] = (char)(value % 10 + 48);
                value /= 10;
            }
        }

        internal static int WriteDateTimeOffset(char[] chars, int start, TimeSpan offset, DateFormatHandling format)
        {
            chars[start++] = offset.Ticks >= 0L ? '+' : '-';

            var absHours = Math.Abs(offset.Hours);
            CopyIntToCharArray(chars, start, absHours, 2);
            start += 2;

            if (format == DateFormatHandling.IsoDateFormat)
            {
                chars[start++] = ':';
            }

            var absMinutes = Math.Abs(offset.Minutes);
            CopyIntToCharArray(chars, start, absMinutes, 2);
            start += 2;

            return start;
        }
    }
}

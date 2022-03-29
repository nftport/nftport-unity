using System.IO;
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace Unity.Nuget.NewtonsoftJson.Tests
{
    static class StringUtils
    {
        static readonly Regex k_NewLinesPattern = new Regex(
            @"\r\n|\n\r|\n|\r", RegexOptions.CultureInvariant | RegexOptions.Compiled);

        public static bool IsNullOrEmpty(string value) => string.IsNullOrEmpty(value);

        public static StringWriter CreateStringWriter(int capacity)
        {
            var sb = new StringBuilder(capacity);
            var sw = new StringWriter(sb, CultureInfo.InvariantCulture);
            return sw;
        }

        public static void ToCharAsUnicode(char c, char[] buffer)
        {
            buffer[0] = '\\';
            buffer[1] = 'u';
            buffer[2] = MathUtils.IntToHex((c >> 12) & '\x000f');
            buffer[3] = MathUtils.IntToHex((c >> 8) & '\x000f');
            buffer[4] = MathUtils.IntToHex((c >> 4) & '\x000f');
            buffer[5] = MathUtils.IntToHex(c & '\x000f');
        }

        public static void AssertAreEqualWithNormalizedLineEndings(string expected, string actual)
        {
            expected = NormalizeLineEndings(expected);
            actual = NormalizeLineEndings(actual);
            Assert.AreEqual(expected, actual);
        }

        public static string NormalizeLineEndings(this string self)
        {
            if (self != null)
            {
                self = k_NewLinesPattern.Replace(self, "\r\n");
            }

            return self;
        }
    }
}

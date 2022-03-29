using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Newtonsoft.Json;

namespace Unity.Nuget.NewtonsoftJson.Tests
{
    static class JavaScriptUtils
    {
        static readonly bool[] k_SingleQuoteCharEscapeFlags = new bool[128];
        static readonly bool[] k_DoubleQuoteCharEscapeFlags = new bool[128];
        static readonly bool[] k_HtmlCharEscapeFlags = new bool[128];

        const int k_UnicodeTextLength = 6;

        static JavaScriptUtils()
        {
            IList<char> escapeChars = new List<char>
            {
                '\n', '\r', '\t', '\\', '\f', '\b',
            };
            for (var i = 0; i < ' '; i++)
            {
                escapeChars.Add((char)i);
            }

            foreach (var escapeChar in escapeChars.Union(new[] { '\'' }))
            {
                k_SingleQuoteCharEscapeFlags[escapeChar] = true;
            }

            foreach (var escapeChar in escapeChars.Union(new[] { '"' }))
            {
                k_DoubleQuoteCharEscapeFlags[escapeChar] = true;
            }

            foreach (var escapeChar in escapeChars.Union(new[] { '"', '\'', '<', '>', '&' }))
            {
                k_HtmlCharEscapeFlags[escapeChar] = true;
            }
        }

        const string k_EscapedUnicodeText = "!";

        public static string ToEscapedJavaScriptString(string value, char delimiter, bool appendDelimiters, StringEscapeHandling stringEscapeHandling)
        {
            var charEscapeFlags = GetCharEscapeFlags(stringEscapeHandling, delimiter);

            using (var w = StringUtils.CreateStringWriter(value?.Length ?? 16))
            {
                char[] buffer = null;
                WriteEscapedJavaScriptString(w, value, delimiter, appendDelimiters, charEscapeFlags, stringEscapeHandling, null, ref buffer);
                return w.ToString();
            }
        }

        static bool[] GetCharEscapeFlags(StringEscapeHandling stringEscapeHandling, char quoteChar)
        {
            if (stringEscapeHandling == StringEscapeHandling.EscapeHtml)
            {
                return k_HtmlCharEscapeFlags;
            }

            return quoteChar == '"'
                ? k_DoubleQuoteCharEscapeFlags
                : k_SingleQuoteCharEscapeFlags;
        }

        static void WriteEscapedJavaScriptString(
            TextWriter writer, string s, char delimiter, bool appendDelimiters, bool[] charEscapeFlags,
            StringEscapeHandling stringEscapeHandling, IArrayPool<char> bufferPool, ref char[] writeBuffer)
        {
            // leading delimiter
            if (appendDelimiters)
            {
                writer.Write(delimiter);
            }

            if (!StringUtils.IsNullOrEmpty(s))
            {
                var lastWritePosition = FirstCharToEscape(s, charEscapeFlags, stringEscapeHandling);
                if (lastWritePosition == -1)
                {
                    writer.Write(s);
                }
                else
                {
                    if (lastWritePosition != 0)
                    {
                        if (writeBuffer == null || writeBuffer.Length < lastWritePosition)
                        {
                            writeBuffer = BufferUtils.EnsureBufferSize(bufferPool, lastWritePosition, writeBuffer);
                        }

                        // write unchanged chars at start of text.
                        s.CopyTo(0, writeBuffer, 0, lastWritePosition);
                        writer.Write(writeBuffer, 0, lastWritePosition);
                    }

                    int length;
                    for (var i = lastWritePosition; i < s.Length; i++)
                    {
                        var c = s[i];

                        if (c < charEscapeFlags.Length && !charEscapeFlags[c])
                        {
                            continue;
                        }

                        string escapedValue;

                        switch (c)
                        {
                            case '\t':
                                escapedValue = @"\t";
                                break;
                            case '\n':
                                escapedValue = @"\n";
                                break;
                            case '\r':
                                escapedValue = @"\r";
                                break;
                            case '\f':
                                escapedValue = @"\f";
                                break;
                            case '\b':
                                escapedValue = @"\b";
                                break;
                            case '\\':
                                escapedValue = @"\\";
                                break;
                            case '\u0085': // Next Line
                                escapedValue = @"\u0085";
                                break;
                            case '\u2028': // Line Separator
                                escapedValue = @"\u2028";
                                break;
                            case '\u2029': // Paragraph Separator
                                escapedValue = @"\u2029";
                                break;
                            default:
                                if (c < charEscapeFlags.Length || stringEscapeHandling == StringEscapeHandling.EscapeNonAscii)
                                {
                                    switch (c)
                                    {
                                        case '\''
                                            when stringEscapeHandling != StringEscapeHandling.EscapeHtml:
                                            escapedValue = @"\'";
                                            break;
                                        case '"'
                                            when stringEscapeHandling != StringEscapeHandling.EscapeHtml:
                                            escapedValue = @"\""";
                                            break;
                                        default:
                                        {
                                            if (writeBuffer == null || writeBuffer.Length < k_UnicodeTextLength)
                                            {
                                                writeBuffer = BufferUtils.EnsureBufferSize(bufferPool, k_UnicodeTextLength, writeBuffer);
                                            }

                                            StringUtils.ToCharAsUnicode(c, writeBuffer);

                                            // slightly hacky but it saves multiple conditions in if test
                                            escapedValue = k_EscapedUnicodeText;
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    escapedValue = null;
                                }

                                break;
                        }

                        if (escapedValue == null)
                        {
                            continue;
                        }

                        var isEscapedUnicodeText = string.Equals(escapedValue, k_EscapedUnicodeText, StringComparison.Ordinal);

                        if (i > lastWritePosition)
                        {
                            length = i - lastWritePosition + (isEscapedUnicodeText ? k_UnicodeTextLength : 0);
                            var start = isEscapedUnicodeText ? k_UnicodeTextLength : 0;

                            if (writeBuffer == null || writeBuffer.Length < length)
                            {
                                var newBuffer = BufferUtils.RentBuffer(bufferPool, length);

                                // the unicode text is already in the buffer
                                // copy it over when creating new buffer
                                if (isEscapedUnicodeText)
                                {
                                    Debug.Assert(writeBuffer != null, "Write buffer should never be null because it is set when the escaped unicode text is encountered.");
                                    Array.Copy(writeBuffer, newBuffer, k_UnicodeTextLength);
                                }

                                BufferUtils.ReturnBuffer(bufferPool, writeBuffer);

                                writeBuffer = newBuffer;
                            }

                            s.CopyTo(lastWritePosition, writeBuffer, start, length - start);

                            // write unchanged chars before writing escaped text
                            writer.Write(writeBuffer, start, length - start);
                        }

                        lastWritePosition = i + 1;
                        if (!isEscapedUnicodeText)
                        {
                            writer.Write(escapedValue);
                        }
                        else
                        {
                            writer.Write(writeBuffer, 0, k_UnicodeTextLength);
                        }
                    }

                    Debug.Assert(lastWritePosition != 0);
                    length = s.Length - lastWritePosition;
                    if (length > 0)
                    {
                        if (writeBuffer == null || writeBuffer.Length < length)
                        {
                            writeBuffer = BufferUtils.EnsureBufferSize(bufferPool, length, writeBuffer);
                        }

                        s.CopyTo(lastWritePosition, writeBuffer, 0, length);

                        // write remaining text
                        writer.Write(writeBuffer, 0, length);
                    }
                }
            }

            // trailing delimiter
            if (appendDelimiters)
            {
                writer.Write(delimiter);
            }
        }

        static int FirstCharToEscape(string s, bool[] charEscapeFlags, StringEscapeHandling stringEscapeHandling)
        {
            for (var i = 0; i != s.Length; i++)
            {
                var c = s[i];

                if (c < charEscapeFlags.Length)
                {
                    if (charEscapeFlags[c])
                    {
                        return i;
                    }
                }
                else if (stringEscapeHandling == StringEscapeHandling.EscapeNonAscii)
                {
                    return i;
                }
                else
                {
                    switch (c)
                    {
                        case '\u0085':
                        case '\u2028':
                        case '\u2029':
                            return i;
                    }
                }
            }

            return -1;
        }
    }
}

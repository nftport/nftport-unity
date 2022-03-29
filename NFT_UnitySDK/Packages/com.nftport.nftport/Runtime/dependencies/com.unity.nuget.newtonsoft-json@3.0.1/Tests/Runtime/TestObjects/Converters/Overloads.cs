using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace Unity.Nuget.NewtonsoftJson.Tests.TestObjects
{
    class OverloadsJsonConverter : JsonConverter
    {
        readonly string m_Type;

        [Preserve]
        public OverloadsJsonConverter(Type typeParam) => m_Type = "Type";

        [Preserve]
        public OverloadsJsonConverter(object objectParam) => m_Type = $"object({objectParam.GetType().FullName})";

        [Preserve]
        public OverloadsJsonConverter(byte byteParam) => m_Type = "byte";

        [Preserve]
        public OverloadsJsonConverter(short shortParam) => m_Type = "short";

        [Preserve]
        public OverloadsJsonConverter(int intParam) => m_Type = "int";

        [Preserve]
        public OverloadsJsonConverter(long longParam) => m_Type = "long";

        [Preserve]
        public OverloadsJsonConverter(double doubleParam) => m_Type = "double";

        [Preserve]
        public OverloadsJsonConverter(params int[] intParams) => m_Type = "int[]";

        [Preserve]
        public OverloadsJsonConverter(bool[] boolArrayParam) => m_Type = "bool[]";

        [Preserve]
        public OverloadsJsonConverter(IEnumerable<string> iEnumerableParam) => m_Type = "IEnumerable<string>";

        [Preserve]
        public OverloadsJsonConverter(IList<string> iListParam) => m_Type = "IList<string>";

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            => writer.WriteValue(m_Type);

        public override object ReadJson(
            JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            => throw new NotImplementedException();

        public override bool CanConvert(Type objectType) => objectType == typeof(int);
    }

    class OverloadWithTypeParameter
    {
        [Preserve]
        [JsonConverter(typeof(OverloadsJsonConverter), typeof(int))]
        public int Overload { get; set; }
    }

    class OverloadWithUnhandledParameter
    {
        [Preserve]
        [JsonConverter(typeof(OverloadsJsonConverter), "str")]
        public int Overload { get; set; }
    }

    class OverloadWithIntParameter
    {
        [Preserve]
        [JsonConverter(typeof(OverloadsJsonConverter), 1)]
        public int Overload { get; set; }
    }

    class OverloadWithUIntParameter
    {
        [Preserve]
        [JsonConverter(typeof(OverloadsJsonConverter), 1U)]
        public int Overload { get; set; }
    }

    class OverloadWithLongParameter
    {
        [Preserve]
        [JsonConverter(typeof(OverloadsJsonConverter), 1L)]
        public int Overload { get; set; }
    }

    class OverloadWithULongParameter
    {
        [Preserve]
        [JsonConverter(typeof(OverloadsJsonConverter), 1UL)]
        public int Overload { get; set; }
    }

    class OverloadWithShortParameter
    {
        [Preserve]
        [JsonConverter(typeof(OverloadsJsonConverter), (short)1)]
        public int Overload { get; set; }
    }

    class OverloadWithUShortParameter
    {
        [Preserve]
        [JsonConverter(typeof(OverloadsJsonConverter), (ushort)1)]
        public int Overload { get; set; }
    }

    class OverloadWithSByteParameter
    {
        [Preserve]
        [JsonConverter(typeof(OverloadsJsonConverter), (sbyte)1)]
        public int Overload { get; set; }
    }

    class OverloadWithByteParameter
    {
        [Preserve]
        [JsonConverter(typeof(OverloadsJsonConverter), (byte)1)]
        public int Overload { get; set; }
    }

    class OverloadWithCharParameter
    {
        [Preserve]
        [JsonConverter(typeof(OverloadsJsonConverter), 'a')]
        public int Overload { get; set; }
    }

    class OverloadWithBoolParameter
    {
        [Preserve]
        [JsonConverter(typeof(OverloadsJsonConverter), true)]
        public int Overload { get; set; }
    }

    class OverloadWithFloatParameter
    {
        [Preserve]
        [JsonConverter(typeof(OverloadsJsonConverter), 1.5f)]
        public int Overload { get; set; }
    }

    class OverloadWithDoubleParameter
    {
        [Preserve]
        [JsonConverter(typeof(OverloadsJsonConverter), 1.5)]
        public int Overload { get; set; }
    }
}

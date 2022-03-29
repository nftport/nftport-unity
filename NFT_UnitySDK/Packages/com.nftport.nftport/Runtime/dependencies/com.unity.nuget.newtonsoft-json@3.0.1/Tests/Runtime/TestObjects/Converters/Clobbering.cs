using System;
using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace Unity.Nuget.NewtonsoftJson.Tests.TestObjects
{
    class ClobberMyProperties
    {
        [Preserve]
        [JsonConverter(typeof(ClobberingJsonConverter), "Uno", 1)]
        public string One { get; set; }

        [Preserve]
        [JsonConverter(typeof(ClobberingJsonConverter), "Dos", 2)]
        public string Two { get; set; }

        [Preserve]
        [JsonConverter(typeof(ClobberingJsonConverter), "Tres")]
        public string Three { get; set; }

        [Preserve]
        public string Four { get; set; }
    }

    class ClobberingJsonConverter : JsonConverter
    {
        public string ClobberValueString { get; private set; }

        public int ClobberValueInt { get; private set; }

        [Preserve]
        public ClobberingJsonConverter(string clobberValueString)
            : this(clobberValueString, 1337) {}

        [Preserve]
        public ClobberingJsonConverter(string clobberValueString, int clobberValueInt)
        {
            ClobberValueString = clobberValueString;
            ClobberValueInt = clobberValueInt;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(ClobberValueString + "-" + ClobberValueInt + "-" + value);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            => throw new NotImplementedException();

        public override bool CanConvert(Type objectType) => objectType == typeof(string);
    }

    class IncorrectJsonConvertParameters
    {
        /// <summary>
        /// We deliberately use the wrong number/type of arguments for ClobberingJsonConverter to ensure an
        /// exception is thrown.
        /// </summary>
        [JsonConverter(typeof(ClobberingJsonConverter), "Uno", "Blammo")]
        public string One { get; set; }
    }
}

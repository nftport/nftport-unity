using System;
using Newtonsoft.Json;

namespace Unity.Nuget.NewtonsoftJson.Tests.TestObjects
{
    class IntConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var i = (int)value;
            writer.WriteValue(i * 2);
        }

        public override object ReadJson(
            JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            => throw new NotImplementedException();

        public override bool CanConvert(Type objectType) => objectType == typeof(int);
    }
}

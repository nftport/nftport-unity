using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Unity.Nuget.NewtonsoftJson.Tests.TestObjects
{
    class ListOfIds<T> : JsonConverter
        where T : Bar, new()
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var list = (IList<T>)value;

            writer.WriteStartArray();
            foreach (var item in list)
            {
                writer.WriteValue(item.Id);
            }

            writer.WriteEndArray();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            IList<T> list = new List<T>();

            reader.Read();
            while (reader.TokenType != JsonToken.EndArray)
            {
                var id = (long)reader.Value;

                list.Add(new T
                {
                    Id = Convert.ToInt32(id)
                });

                reader.Read();
            }

            return list;
        }

        public override bool CanConvert(Type objectType) => typeof(IList<T>).IsAssignableFrom(objectType);
    }
}

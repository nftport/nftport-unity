using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace Unity.Nuget.NewtonsoftJson.Tests.TestObjects
{
    sealed class HasReadOnlyDictionary
    {
        [Preserve]
        [JsonProperty("foo")]
        public IReadOnlyDictionary<string, string> Foo { get; } = new ReadOnlyDictionary<string, string>(new Dictionary<string, string>());

        [Preserve]
        [JsonConstructor]
        public HasReadOnlyDictionary([JsonProperty("bar")] int bar) { }
    }

    sealed class HasReadOnlyEnumerableObject
    {
        [Preserve]
        [JsonProperty("foo")]
        public EnumerableWithConverter Foo { get; } = new EnumerableWithConverter();

        [Preserve]
        [JsonConstructor]
        public HasReadOnlyEnumerableObject([JsonProperty("bar")] int bar) { }
    }

    sealed class HasReadOnlyEnumerableObjectAndDefaultConstructor
    {
        [Preserve]
        [JsonProperty("foo")]
        public EnumerableWithConverter Foo { get; } = new EnumerableWithConverter();

        [Preserve]
        [JsonConstructor]
        public HasReadOnlyEnumerableObjectAndDefaultConstructor() { }
    }

    sealed class AcceptsEnumerableObjectToConstructor
    {
        [Preserve]
        [JsonConstructor]
        public AcceptsEnumerableObjectToConstructor
        (
            [JsonProperty("foo")] EnumerableWithConverter foo,
            [JsonProperty("bar")] int bar
        ) { }
    }

    sealed class HasEnumerableObject
    {
        [Preserve]
        [JsonProperty("foo")]
        public EnumerableWithConverter Foo { get; set; } = new EnumerableWithConverter();

        [Preserve]
        [JsonConstructor]
        public HasEnumerableObject([JsonProperty("bar")] int bar) { }
    }

    [Preserve]
    [JsonConverter(typeof(Converter))]
    sealed class EnumerableWithConverter : IEnumerable<int>
    {
        [Preserve]
        sealed class Converter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
                => objectType == typeof(Foo);

            public override object ReadJson(
                JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                reader.Skip();
                return new EnumerableWithConverter();
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                writer.WriteStartObject();
                writer.WriteEndObject();
            }
        }

        public IEnumerator<int> GetEnumerator()
        {
            yield break;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}

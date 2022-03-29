using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Unity.Nuget.NewtonsoftJson.Tests.TestObjects
{
    [JsonConverter(typeof(StringEnumConverter))]
    enum EnumWithConverter
    {
        [EnumMember(Value = "SOME_VALUE")]
        SomeValue,

        [EnumMember(Value = "SOME_OTHER_VALUE")]
        SomeOtherValue
    }
}

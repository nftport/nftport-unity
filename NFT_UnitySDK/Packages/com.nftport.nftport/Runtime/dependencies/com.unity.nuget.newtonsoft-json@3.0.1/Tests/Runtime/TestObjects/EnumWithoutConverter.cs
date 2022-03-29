using System.Runtime.Serialization;

namespace Unity.Nuget.NewtonsoftJson.Tests.TestObjects
{
    enum EnumWithoutConverter
    {
        [EnumMember(Value = "SOME_VALUE")]
        SomeValue,

        [EnumMember(Value = "SOME_OTHER_VALUE")]
        SomeOtherValue
    }
}

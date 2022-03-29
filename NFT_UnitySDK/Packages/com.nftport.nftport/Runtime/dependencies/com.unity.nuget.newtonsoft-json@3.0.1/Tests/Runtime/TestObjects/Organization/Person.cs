using System;
using System.ComponentModel;
using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace Unity.Nuget.NewtonsoftJson.Tests.TestObjects
{
    [Preserve]
    [Description("DescriptionAttribute description!")]
    [JsonObject(
        Id = "Person", Title = "Title!", Description = "JsonObjectAttribute description!",
        MemberSerialization = MemberSerialization.OptIn)]
    class Person
    {
        [Preserve]
        [JsonProperty]
        public string Name { get; set; }

        [Preserve]
        [JsonProperty]
        public DateTime BirthDate { get; set; }

        [Preserve]
        [JsonProperty]
        public DateTime LastModified { get; set; }

        // not serialized
        [Preserve]
        public string Department { get; set; }
    }
}

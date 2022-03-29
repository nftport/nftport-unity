using System;
using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace Unity.Nuget.NewtonsoftJson.Tests.TestObjects
{
    [Preserve]
    [JsonObject(ItemRequired = Required.Always)]
    sealed class ItemsRequiredObjectWithIgnoredProperty
    {
        static readonly DateTime k_UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        [Preserve]
        [JsonProperty("exp")]
        int expiration
        {
            get => (int)(Expiration - k_UnixEpoch).TotalSeconds;
            set => Expiration = k_UnixEpoch.AddSeconds(value);
        }

        [Preserve]
        public bool Active { get; set; }

        [Preserve]
        [JsonIgnore]
        public DateTime Expiration { get; set; }
    }
}

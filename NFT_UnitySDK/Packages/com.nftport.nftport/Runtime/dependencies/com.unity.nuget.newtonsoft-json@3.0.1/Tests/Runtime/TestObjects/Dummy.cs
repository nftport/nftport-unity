using UnityEngine.Scripting;

namespace Unity.Nuget.NewtonsoftJson.Tests.TestObjects
{
    class Dummy
    {
        [Preserve]
        public int[] Test { get; set; }
    }
}

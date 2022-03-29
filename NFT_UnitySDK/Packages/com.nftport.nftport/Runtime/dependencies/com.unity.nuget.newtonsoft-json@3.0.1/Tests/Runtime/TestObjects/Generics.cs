using UnityEngine.Scripting;

namespace Unity.Nuget.NewtonsoftJson.Tests.TestObjects
{
    class GenericBaseClass<T1, T2>
    {
        [Preserve]
        public virtual T2 Data { get; set; }
    }

    class GenericIntermediateClass<T1> : GenericBaseClass<T1, string>
    {
        [Preserve]
        public override string Data { get; set; }
    }

    [Preserve]
    class NonGenericChildClass : GenericIntermediateClass<int> { }
}

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Unity.Nuget.NewtonsoftJson.Tests.TestObjects
{
    class Foo
    {
        public Foo()
        {
            Bars = new List<Bar>();
        }

        [JsonConverter(typeof(ListOfIds<Bar>))]
        public List<Bar> Bars { get; set; }
    }
}

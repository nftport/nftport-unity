using System;
using System.Collections.Generic;
using UnityEngine.Scripting;

namespace Unity.Nuget.NewtonsoftJson.Tests.TestObjects
{
    [Preserve]
    class Movie
    {
        [Preserve]
        public string Name { get; set; }

        [Preserve]
        public string Description { get; set; }

        [Preserve]
        public string Classification { get; set; }

        [Preserve]
        public string Studio { get; set; }

        [Preserve]
        public DateTime? ReleaseDate { get; set; }

        [Preserve]
        public List<string> ReleaseCountries { get; set; }
    }
}

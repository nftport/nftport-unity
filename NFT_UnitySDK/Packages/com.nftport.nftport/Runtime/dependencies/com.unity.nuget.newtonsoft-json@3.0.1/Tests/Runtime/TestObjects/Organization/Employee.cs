using System;
using UnityEngine.Scripting;

namespace Unity.Nuget.NewtonsoftJson.Tests.TestObjects
{
    class Employee : IPerson
    {
        [Preserve]
        public string FirstName { get; set; }

        [Preserve]
        public string LastName { get; set; }

        [Preserve]
        public DateTime BirthDate { get; set; }

        [Preserve]
        public string Department { get; set; }

        [Preserve]
        public string JobTitle { get; set; }
    }
}

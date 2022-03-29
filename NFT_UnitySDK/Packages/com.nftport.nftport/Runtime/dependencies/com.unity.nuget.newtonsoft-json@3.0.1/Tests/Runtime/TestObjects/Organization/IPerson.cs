using System;

namespace Unity.Nuget.NewtonsoftJson.Tests.TestObjects
{
    interface IPerson
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        DateTime BirthDate { get; set; }
    }
}

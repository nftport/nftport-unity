using System.Collections.Generic;

namespace Unity.Nuget.NewtonsoftJson.Tests.TestObjects
{
    class Post
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public IList<string> Categories { get; set; }
    }
}

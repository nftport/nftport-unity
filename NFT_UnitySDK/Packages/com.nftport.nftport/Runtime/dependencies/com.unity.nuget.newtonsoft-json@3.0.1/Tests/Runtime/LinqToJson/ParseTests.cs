using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace Unity.Nuget.NewtonsoftJson.Tests
{
    class ParseTests : NewtonsoftTests
    {
        [Test]
        public void JArrayParseOnIncompleteContainersThrows()
        {
            Assert.Throws<JsonReaderException>(
                () => JArray.Parse("[1,"),
                "Unexpected end of content while loading JArray. Path '[0]', line 1, position 3.");

            Assert.Throws<JsonReaderException>(
                () => JArray.Parse("[1"),
                "Unexpected end of content while loading JArray. Path '[0]', line 1, position 2.");

            Assert.Throws<JsonReaderException>(
                () => JObject.Parse("{'key':1,"),
                "Unexpected end of content while loading JObject. Path 'key', line 1, position 9.");

            Assert.Throws<JsonReaderException>(
                () => JObject.Parse("{'key':1"),
                "Unexpected end of content while loading JObject. Path 'key', line 1, position 8.");
        }

        [Test]
        public void JObjectParseProperlyEscapesPath()
        {
            const string json = @"{
  ""frameworks"": {
    ""dnxcore50"": {
      ""dependencies"": {
        ""System.Xml.ReaderWriter"": {
          ""source"": ""NuGet""
        }
      }
    }
  }
}";

            var o = JObject.Parse(json);

            var v1 = o["frameworks"]["dnxcore50"]["dependencies"]["System.Xml.ReaderWriter"]["source"];
            Assert.AreEqual("frameworks.dnxcore50.dependencies['System.Xml.ReaderWriter'].source", v1.Path);
            var v2 = o.SelectToken(v1.Path);
            Assert.AreEqual(v1, v2);
        }

        [Test]
        public void JArrayParseOnDoubleSucceeds()
        {
            var j = JArray.Parse("[-1E+4,100.0e-2]");

            var value = (double)j[0];
            Assert.AreEqual(-10000d, value);

            value = (double)j[1];
            Assert.AreEqual(1d, value);
        }

        [Test]
        public void JObjectParseSucceeds()
        {
            const string json = @"{
  CPU: 'Intel',
  Drives: [
    'DVD read/writer',
    ""500 gigabyte hard drive""
  ]
}";

            var o = JObject.Parse(json);

            IList<JProperty> properties = o.Properties().ToList();
            Assert.AreEqual("CPU", properties[0].Name);
            Assert.AreEqual("Intel", (string)properties[0].Value);
            Assert.AreEqual("Drives", properties[1].Name);

            var list = (JArray)properties[1].Value;
            Assert.AreEqual(2, list.Children().Count());
            Assert.AreEqual("DVD read/writer", (string)list.Children().ElementAt(0));
            Assert.AreEqual("500 gigabyte hard drive", (string)list.Children().ElementAt(1));

            var parameterValues = o.Properties()
                .Where(p => p.Value is JValue)
                .Select(p => (JValue)p.Value)
                .ToList();
            Assert.AreEqual(1, parameterValues.Count);
            Assert.AreEqual("Intel", parameterValues[0].ToString(CultureInfo.InvariantCulture));
        }

        [Test]
        public void JArrayParseOnIntArraySucceeds()
        {
            const string json = @"[0,1,2,3,4,5,6,7,8,9]";

            var a = JArray.Parse(json);

            var list = a.Values<int>().ToList();
            var expected = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            CollectionAssert.AreEqual(expected, list);
        }

        [Test]
        public void JObjectParseOnGoogleSearchApiSucceeds()
        {
            const string json = @"{
    results:
        [
            {
                GsearchResultClass:""GwebSearch"",
                unescapedUrl : ""http://www.google.com/"",
                url : ""http://www.google.com/"",
                visibleUrl : ""www.google.com"",
                cacheUrl :
""http://www.google.com/search?q=cache:zhool8dxBV4J:www.google.com"",
                title : ""Google"",
                titleNoFormatting : ""Google"",
                content : ""Enables users to search the Web, Usenet, and
images. Features include PageRank,   caching and translation of
results, and an option to find similar pages.""
            },
            {
                GsearchResultClass:""GwebSearch"",
                unescapedUrl : ""http://news.google.com/"",
                url : ""http://news.google.com/"",
                visibleUrl : ""news.google.com"",
                cacheUrl :
""http://www.google.com/search?q=cache:Va_XShOz_twJ:news.google.com"",
                title : ""Google News"",
                titleNoFormatting : ""Google News"",
                content : ""Aggregated headlines and a search engine of many of the world's news sources.""
            },

            {
                GsearchResultClass:""GwebSearch"",
                unescapedUrl : ""http://groups.google.com/"",
                url : ""http://groups.google.com/"",
                visibleUrl : ""groups.google.com"",
                cacheUrl :
""http://www.google.com/search?q=cache:x2uPD3hfkn0J:groups.google.com"",
                title : ""Google Groups"",
                titleNoFormatting : ""Google Groups"",
                content : ""Enables users to search and browse the Usenet
archives which consist of over 700   million messages, and post new
comments.""
            },

            {
                GsearchResultClass:""GwebSearch"",
                unescapedUrl : ""http://maps.google.com/"",
                url : ""http://maps.google.com/"",
                visibleUrl : ""maps.google.com"",
                cacheUrl :
""http://www.google.com/search?q=cache:dkf5u2twBXIJ:maps.google.com"",
                title : ""Google Maps"",
                titleNoFormatting : ""Google Maps"",
                content : ""Provides directions, interactive maps, and
satellite/aerial imagery of the United   States. Can also search by
keyword such as type of business.""
            }
        ],

    adResults:
        [
            {
                GsearchResultClass:""GwebSearch.ad"",
                title : ""Gartner Symposium/ITxpo"",
                content1 : ""Meet brilliant Gartner IT analysts"",
                content2 : ""20-23 May 2007- Barcelona, Spain"",
                url :
""http://www.google.com/url?sa=L&ai=BVualExYGRo3hD5ianAPJvejjD8-s6ye7kdTwArbI4gTAlrECEAEYASDXtMMFOAFQubWAjvr_____AWDXw_4EiAEBmAEAyAEBgAIB&num=1&q=http://www.gartner.com/it/sym/2007/spr8/spr8.jsp%3Fsrc%3D_spain_07_%26WT.srch%3D1&usg=__CxRH06E4Xvm9Muq13S4MgMtnziY="",

                impressionUrl :
""http://www.google.com/uds/css/ad-indicator-on.gif?ai=BVualExYGRo3hD5ianAPJvejjD8-s6ye7kdTwArbI4gTAlrECEAEYASDXtMMFOAFQubWAjvr_____AWDXw_4EiAEBmAEAyAEBgAIB"",

                unescapedUrl :
""http://www.google.com/url?sa=L&ai=BVualExYGRo3hD5ianAPJvejjD8-s6ye7kdTwArbI4gTAlrECEAEYASDXtMMFOAFQubWAjvr_____AWDXw_4EiAEBmAEAyAEBgAIB&num=1&q=http://www.gartner.com/it/sym/2007/spr8/spr8.jsp%3Fsrc%3D_spain_07_%26WT.srch%3D1&usg=__CxRH06E4Xvm9Muq13S4MgMtnziY="",

                visibleUrl : ""www.gartner.com""
            }
        ]
}
";

            var o = JObject.Parse(json);

            var resultObjects = o["results"].Children<JObject>().ToList();
            Assert.AreEqual(32, resultObjects.Properties().Count());
            Assert.AreEqual(32, resultObjects.Values().Count());
            Assert.AreEqual(4, resultObjects.Values("GsearchResultClass").Count());
            Assert.AreEqual(5, o.PropertyValues().Cast<JArray>().Children().Count());

            var resultUrls = o["results"].Children().Values<string>("url").ToList();
            var expectedUrls = new List<string>
            {
                "http://www.google.com/",
                "http://news.google.com/",
                "http://groups.google.com/",
                "http://maps.google.com/"
            };
            CollectionAssert.AreEqual(expectedUrls, resultUrls);

            var descendants = o.Descendants().ToList();
            Assert.AreEqual(89, descendants.Count);
        }

        [Test]
        public void JObjectParseWithPrecedingCommentsSucceeds()
        {
            const string json = @"/* blah */ {'hi':'hi!'}";

            var o = JObject.Parse(json);

            Assert.AreEqual("hi!", (string)o["hi"]);
        }

        [Test]
        public void JArrayParseWithPrecedingCommentsSucceeds()
        {
            const string json = @"/* blah */ ['hi!']";

            var a = JArray.Parse(json);

            Assert.AreEqual("hi!", (string)a[0]);
        }
    }
}

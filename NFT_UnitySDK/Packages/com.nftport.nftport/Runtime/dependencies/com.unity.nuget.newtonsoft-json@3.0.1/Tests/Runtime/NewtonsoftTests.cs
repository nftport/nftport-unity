using System;
using System.Globalization;
using System.Threading;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Unity.Nuget.NewtonsoftJson.Tests
{
    abstract class NewtonsoftTests
    {
        CultureInfo m_PreviousThreadCulture;
        CultureInfo m_PreviousThreadUiCulture;
        Func<JsonSerializerSettings> m_PreviousDefaultSettings;

        [SetUp]
        protected void TestSetup()
        {
            m_PreviousThreadCulture = Thread.CurrentThread.CurrentCulture;
            m_PreviousThreadUiCulture = Thread.CurrentThread.CurrentUICulture;
            m_PreviousDefaultSettings = JsonConvert.DefaultSettings;

            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
            JsonConvert.DefaultSettings = null;
        }

        [TearDown]
        protected void TearDown()
        {
            Thread.CurrentThread.CurrentCulture = m_PreviousThreadCulture;
            Thread.CurrentThread.CurrentUICulture = m_PreviousThreadUiCulture;
            JsonConvert.DefaultSettings = m_PreviousDefaultSettings;
        }
    }
}

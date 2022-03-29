namespace Unity.Nuget.NewtonsoftJson.Tests.TestObjects
{
    class DateTimeResult
    {
        public string IsoDateRoundtrip { get; set; }
        public string IsoDateLocal { get; set; }
        public string IsoDateUnspecified { get; set; }
        public string IsoDateUtc { get; set; }

        public string MsDateRoundtrip { get; set; }
        public string MsDateLocal { get; set; }
        public string MsDateUnspecified { get; set; }
        public string MsDateUtc { get; set; }
    }
}

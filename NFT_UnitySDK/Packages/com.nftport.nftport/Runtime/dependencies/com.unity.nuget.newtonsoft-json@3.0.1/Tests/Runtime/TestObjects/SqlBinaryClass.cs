using System.Data.SqlTypes;

namespace Unity.Nuget.NewtonsoftJson.Tests
{
    class SqlBinaryClass
    {
        public SqlBinary SqlBinary { get; set; }
        public SqlBinary? NullableSqlBinary1 { get; set; }
        public SqlBinary? NullableSqlBinary2 { get; set; }
    }
}

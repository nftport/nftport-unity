namespace Unity.Nuget.NewtonsoftJson.Tests
{
    static class MathUtils
    {
        public static char IntToHex(int n)
        {
            if (n <= 9)
            {
                return (char)(n + 48);
            }

            return (char)(n - 10 + 97);
        }
    }
}

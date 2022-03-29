using Newtonsoft.Json;

namespace Unity.Nuget.NewtonsoftJson.Tests
{
    static class BufferUtils
    {
        public static char[] RentBuffer(IArrayPool<char> bufferPool, int minSize)
        {
            if (bufferPool == null)
            {
                return new char[minSize];
            }

            var buffer = bufferPool.Rent(minSize);
            return buffer;
        }

        public static void ReturnBuffer(IArrayPool<char> bufferPool, char[] buffer)
        {
            bufferPool?.Return(buffer);
        }

        public static char[] EnsureBufferSize(IArrayPool<char> bufferPool, int size, char[] buffer)
        {
            if (bufferPool == null)
            {
                return new char[size];
            }

            if (buffer != null)
            {
                bufferPool.Return(buffer);
            }

            return bufferPool.Rent(size);
        }
    }
}

using System;
using System.Collections;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Unity.Nuget.NewtonsoftJson.Tests
{
    static class AsyncUtils
    {
        public static async Task<TException> ThrowsAsync<TException>(
            Func<Task> action, params string[] possibleMessages)
            where TException : Exception
        {
            try
            {
                await action();

                Assert.Fail($"Exception of type {typeof(TException).Name} expected. No exception thrown.");
                return null;
            }
            catch (TException ex)
            {
                if (possibleMessages == null
                    || possibleMessages.Length == 0)
                {
                    return ex;
                }

                foreach (var possibleMessage in possibleMessages)
                {
                    if (string.Equals(possibleMessage.NormalizeLineEndings(), ex.Message.NormalizeLineEndings()))
                    {
                        return ex;
                    }
                }

                throw new Exception(
                    $"Unexpected exception message.{Environment.NewLine}"
                    + $"Expected one of: {string.Join(Environment.NewLine, possibleMessages)}{Environment.NewLine}"
                    + $"Got: {ex.Message}{Environment.NewLine}{Environment.NewLine}{ex}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception of type {typeof(TException).Name} expected; got exception of type {ex.GetType().Name}.", ex);
            }
        }

        public static IEnumerator Run(Func<Task> asyncTest)
        {
            var testTask = asyncTest();
            while (!testTask.IsCompleted)
                yield return null;

            if (testTask.Exception is null)
                yield break;

            if (testTask.Exception.InnerException is null)
                throw testTask.Exception;

            throw testTask.Exception.InnerException;
        }
    }
}

namespace AsynEnumerableWeb.Api;

public static class AsyncEnumerableExtensions
{
    public static async IAsyncEnumerable<T> AsAsyncEnumerable<T>(this IEnumerable<T> enumerable)
    {
        foreach (var item in enumerable)
        {
            yield return item;
            await Task.Yield(); // Yield control to avoid blocking
        }
    }
}
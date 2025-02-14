namespace AsynEnumerableWeb.Api;

public class RandomStringGenerator
{
    private static readonly Random _random = new();
    private const string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    public static async IAsyncEnumerable<string> GenerateRandomStringsAsync(int count, int length)
    {
        for (int i = 0; i < count; i++)
        {
            yield return GenerateRandomString(length);
            await Task.Delay(750); // Random delay between 1 and 500 ms
        }
    }

    private static string GenerateRandomString(int length)
    {
        char[] buffer = new char[length];
        for (int i = 0; i < length; i++)
        {
            buffer[i] = _chars[_random.Next(_chars.Length)];
        }
        return new string(buffer);
    }
}
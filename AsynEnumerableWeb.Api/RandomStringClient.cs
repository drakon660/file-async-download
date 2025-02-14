using Flurl;
using Flurl.Http;

namespace AsynEnumerableWeb.Api;

public class RandomStringClient
{
    private readonly string _baseUrl;
    private readonly ILogger _logger;

    public RandomStringClient(string baseUrl, ILogger logger)
    {
        _baseUrl = baseUrl;
        _logger = logger;
    }
    
    public async IAsyncEnumerable<string> GetRandomStringsAsync(int count, int length)
    {
        var response = await _baseUrl
            .SetQueryParams(new { count, length })
            .GetAsync(HttpCompletionOption.ResponseHeadersRead)
            .ConfigureAwait(false);
        

        IAsyncEnumerable<string> data = response.ResponseMessage.Content.ReadFromJsonAsAsyncEnumerable<string>();

        await foreach (string randomString in data)
        {
            yield return randomString;
        }
    }

    public async Task<IReadOnlyList<string>> GetRandomStrings(int count, int length)
    {
        var response = await _baseUrl
            .SetQueryParams(new { count, length })
            .GetJsonAsync<List<string>>();

        return response;
    }
}
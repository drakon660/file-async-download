using Microsoft.AspNetCore.Mvc;

namespace AsynEnumerableWeb.Api.Controllers;


[ApiController]
[Route("recurrent")]
public class RecurrentController
{
    private readonly ILogger<RecurrentController> _logger;

    public RecurrentController(ILogger<RecurrentController> logger)
    {
        _logger = logger;
    }
    
    [HttpGet]
    public async IAsyncEnumerable<string> GetRandomStrings([FromQuery] int count = 5, [FromQuery] int length = 10)
    {
        await foreach (var randomString in RandomStringGenerator.GenerateRandomStringsAsync(count, length))
        {
            _logger.LogInformation(randomString);
            yield return randomString;
        }
    }


    [HttpGet("client-async")]
    public async IAsyncEnumerable<string?> GetRandomStringClient([FromQuery] int count = 5, [FromQuery] int length = 10)
    {
        var client = new RandomStringClient("http://localhost:5042/recurrent/", _logger);
        await foreach (var randomString in client.GetRandomStringsAsync(count,length))
        {
            _logger.LogInformation(randomString);
            yield return randomString;
        }
    }
    [HttpGet("client-sync")]
    public async Task<IReadOnlyList<string>> GetRandomStringClientSync([FromQuery] int count = 5, [FromQuery] int length = 10)
    {
        var client = new RandomStringClient("http://localhost:5042/recurrent/", _logger);
        return await client.GetRandomStrings(count,length);
    }
}
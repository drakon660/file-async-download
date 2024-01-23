using Microsoft.EntityFrameworkCore;

namespace AsyncEnumerable.Core;

public class DataBrowser
{
    private readonly IDbContextFactory<ChromeContext> _contextFactory;

    public DataBrowser(IDbContextFactory<ChromeContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }
    
    public async IAsyncEnumerable<string> GetUsersNames()
    {
        var chromeContext = await _contextFactory.CreateDbContextAsync();

        var users = chromeContext.Users.AsNoTracking().AsAsyncEnumerable();

        await foreach (var user in users)
        {
            await Task.Delay(5);
            yield return $"{user.Name}";
        }
    }

    public async Task GetNames()
    {
        await foreach (var userName in GetUsersNames())
        {
            Console.WriteLine(userName);    
        }
    }

    public void GetNamesBlocked()
    {
        foreach (var userName in GetUsersNames().ToBlockingEnumerable())
        {
            Console.WriteLine(userName);    
        }
    }
}
using System.Collections;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

namespace AsyncEnumerable.Core;

public class DataBrowser
{
    private readonly IDbContextFactory<ChromeContext> _contextFactory;

    public DataBrowser(IDbContextFactory<ChromeContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }
    
    public async IAsyncEnumerable<string> GetUsersNames([EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var chromeContext = await _contextFactory.CreateDbContextAsync(cancellationToken);
    
        var users = chromeContext.Users.AsNoTracking().AsAsyncEnumerable().WithCancellation(cancellationToken);
    
        await foreach (var user in users)
        {
            //if (cancellationToken.IsCancellationRequested) break;
            cancellationToken.ThrowIfCancellationRequested();
            
            await Task.Delay(5,cancellationToken);
            yield return $"{user.Name}";
        }
    }

    public async Task GetNames(IAsyncEnumerable<string> users, CancellationToken cancellationToken)
    {
        await foreach (var userName in users.WithCancellation(cancellationToken))
        {
            Console.WriteLine(userName);    
        }
    }
    
    public void GetNamesBlocked(CancellationToken cancellationToken)
    {
        foreach (var userName in GetUsersNames(cancellationToken).ToBlockingEnumerable())
        {
            Console.WriteLine(userName);    
        }
    }

    public void Loop()
    {
        List<int> five = Enumerable.Range(0, 5).ToList();

        IEnumerator<int> fiveEnumerator = five.GetEnumerator();
        
        while(fiveEnumerator.MoveNext())
        {
            int value = fiveEnumerator.Current;
            Console.WriteLine(value);
        }
    }

    public IEnumerator GetEnumerator()
    {
        throw new NotImplementedException();
    }
}

public class MyList<T> : IEnumerable<T> where T : struct
{
    private readonly T[] _t;

    public MyList(int count)
    {
        _t = new T[count];
    }
    
    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < _t.Length; i ++)
            yield return _t[i];
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

// public class GetEnumerator<T> : IEnumerator<T>
// {
//     private T _current;
//
//     public bool MoveNext()
//     {
//         throw new NotImplementedException();
//     }
//
//     public void Reset()
//     {
//         throw new NotImplementedException();
//     }
//
//     T IEnumerator<T>.Current => _current;
//
//     public object Current { get; }
//
//     public void Dispose()
//     {
//         // TODO release managed resources here
//     }
// }
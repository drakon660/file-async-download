// See https://aka.ms/new-console-template for more information

using System.Runtime.CompilerServices;

var producerTokenSource = new CancellationTokenSource();

producerTokenSource.Cancel();

var asyncEnumerable = Produce(producerTokenSource.Token); // deferred

var consumerTokenSource = new CancellationTokenSource();

consumerTokenSource.Cancel(); // cancel consuming

await Consume(asyncEnumerable, consumerTokenSource.Token); // consumer should fail

ReadKey();


static async Task Consume(IAsyncEnumerable<int> asyncEnumerable, CancellationToken cancellationToken)
{
    await foreach (var item in asyncEnumerable)
        Console.WriteLine(item);
}

static async IAsyncEnumerable<int> Produce([EnumeratorCancellation] CancellationToken cancellationToken)
{
    const int DelayMs = 100;
    const int NumberOfIterations = 10;

    var random = new Random();

    for (int i = 0; i < NumberOfIterations; i++)
    {
        cancellationToken.ThrowIfCancellationRequested();

        await Task.Delay(DelayMs, cancellationToken);
        yield return random.Next();
    }
}

static void ReadKey()
{
    Console.WriteLine("Press any key to quit");
    Console.ReadKey();
}
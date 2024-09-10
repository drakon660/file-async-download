// See https://aka.ms/new-console-template for more information

using AsyncEnumerable.Core;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;

string connectionString =
    "Server=tcp:soapkit.database.windows.net,1433;Initial Catalog=chrome;Persist Security Info=False;User ID=drakon660;Password=Ekn117723@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

var serviceCollection = new ServiceCollection();
serviceCollection.AddDbContextFactory<ChromeContext>();
serviceCollection.AddScoped<DataBrowser>();
var sp = serviceCollection.BuildServiceProvider();

var dataBrowser = sp.GetRequiredService<DataBrowser>();

//dataBrowser.Loop();

// var order = new Order();
// var notification = new OrderNotification();
// var notification2 = new OrderNotification2();
// order.PlaceOrder();


//dataBrowser.GetNamesBlocked();
var producerTokenSource = new CancellationTokenSource();
var users = dataBrowser.GetUsersNames(producerTokenSource.Token);
//producerTokenSource.Cancel();

var consumerTokenSource  = new CancellationTokenSource();
consumerTokenSource.Cancel();

await dataBrowser.GetNames(users, consumerTokenSource.Token);

Console.WriteLine("Hello, World!");
Console.ReadKey();
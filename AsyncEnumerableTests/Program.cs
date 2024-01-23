// See https://aka.ms/new-console-template for more information

using AsyncEnumerable.Core;
using Microsoft.Extensions.DependencyInjection;

string connectionString =
    "Server=tcp:soapkit.database.windows.net,1433;Initial Catalog=chrome;Persist Security Info=False;User ID=drakon660;Password=Ekn117723;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

var serviceCollection = new ServiceCollection();
serviceCollection.AddDbContextFactory<ChromeContext>();
serviceCollection.AddScoped<DataBrowser>();
var sp = serviceCollection.BuildServiceProvider();

var dataBrowser = sp.GetRequiredService<DataBrowser>();
dataBrowser.GetNamesBlocked();
await dataBrowser.GetNames();

//Randomizer.Seed = new Random(8675309);

// var testUsers = new Faker<User>()
//     .RuleFor(x=>x.Name, (f, u) => f.Name.FirstName() + " " + f.Name.LastName());
//
// var usersToAdd = testUsers.Generate(5000);

//await using var chromeContext = new ChromeContext();
//chromeContext.Users.AddRange(usersToAdd);
//await chromeContext.SaveChangesAsync();

//var users = await chromeContext.Users.ToListAsync();


Console.WriteLine("Hello, World!");
Console.ReadKey();
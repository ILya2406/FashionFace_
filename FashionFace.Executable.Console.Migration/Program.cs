using FashionFace.Repositories.Context;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var configuration =
    new ConfigurationBuilder()
#if !DEBUG
    .AddJsonFile("appsettings.json", optional: false)
#else
    .AddJsonFile("appsettings.Development.json", optional: false)
#endif
    .Build();

var serviceCollection =
    new ServiceCollection();

serviceCollection.AddSingleton<IConfiguration>(configuration);

serviceCollection
    .AddDbContext<ApplicationDatabaseContext>(
        options =>
            options.UseNpgsql(
                "name=Database:ConnectionString",
                serverOptions =>
                {
                    serverOptions
                        .MigrationsAssembly(
                            typeof(ApplicationDatabaseContext).Assembly.FullName
                        );
                }
            )
    );

var serviceProvider = serviceCollection.BuildServiceProvider();
using var scope = serviceProvider.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<ApplicationDatabaseContext>();
try
{
    db.Database.Migrate();
}
catch (Exception exception)
{
    Console.WriteLine(exception.Message);
}
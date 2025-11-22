using FashionFace.Repositories.Context;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Serilog;

var configuration =
    new ConfigurationBuilder()
#if !DEBUG
    .AddJsonFile("appsettings.json", optional: false)
#else
    .AddJsonFile("appsettings.Development.json", optional: false)
#endif
    .AddEnvironmentVariables()
    .Build();

Log.Logger =
    new LoggerConfiguration()
        .ReadFrom
        .Configuration(
            configuration
        )
        .Enrich
        .FromLogContext()
        .CreateLogger();

var serviceCollection =
    new ServiceCollection();

serviceCollection.AddSingleton<IConfiguration>(configuration);

serviceCollection.AddLogging(
    loggingBuilder =>
    {
        loggingBuilder.ClearProviders();
        loggingBuilder.AddSerilog();
    }
);

var connectionString =
    Environment.GetEnvironmentVariable("Database__ConnectionString")
    ?? configuration["Database:ConnectionString"];

Log.Information("Connection string used: {Conn}", connectionString);

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

var serviceProvider =
    serviceCollection.BuildServiceProvider();

var logger =
    serviceProvider.GetRequiredService<ILogger<Program>>();

logger.LogInformation(configuration["Database:ConnectionString"]);

using var scope = serviceProvider.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<ApplicationDatabaseContext>();

try
{
    logger.LogInformation("Migration started");
    db.Database.Migrate();
    logger.LogInformation("Migration finished");

}
catch (Exception exception)
{
    logger.LogInformation("Failed to migrate");
    logger.LogInformation(exception.Message);
}
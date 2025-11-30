using FashionFace.Dependencies.Serialization.Implementations;
using FashionFace.Dependencies.Serialization.Interfaces;

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using FashionFace.Executable.Blazor.WebAssembly;
using FashionFace.Executable.Blazor.WebAssembly.Services;

using Serilog;

var builder = WebAssemblyHostBuilder.CreateDefault(
    args
);
builder.RootComponents.Add<App>(
    "#app"
);
builder.RootComponents.Add<HeadOutlet>(
    "head::after"
);

var builderConfiguration =
    builder.Configuration;

builderConfiguration
    .AddJsonFile(
        "appsettings.json"
    );

Log.Logger =
    new LoggerConfiguration()
        .ReadFrom
        .Configuration(
            builderConfiguration
        )
        .Enrich
        .FromLogContext()
        .CreateLogger();

var serviceCollection =
    builder.Services;

var apiBaseUrl =
    builderConfiguration["Api:BaseUrl"];

serviceCollection.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiBaseUrl), });

/*serviceCollection.AddSingleton<IDangerousHttpClientBuilder, DangerousHttpClientBuilder>();

serviceCollection
    .AddScoped<HttpClient>(
        serviceProvider =>
        {
            var factory =
                serviceProvider.GetRequiredService<IDangerousHttpClientBuilder>();

            var httpClient =
                factory
                    .Build(
                        apiBaseUrl
                    );

            return
                httpClient;
        }
    );*/

serviceCollection.AddScoped<ISerializationDecorator, SerializationDecorator>();

serviceCollection.AddScoped<AuthorizationService>();

serviceCollection.AddLogging(
    loggingBuilder =>
    {
        loggingBuilder.ClearProviders();
        loggingBuilder.AddSerilog();
    }
);

await builder
    .Build()
    .RunAsync();
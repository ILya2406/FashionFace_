using System.Net.Http;

using FashionFace.Dependencies.HttpClient.Implementations;
using FashionFace.Dependencies.HttpClient.Interfaces;
using FashionFace.Executable.Blazor.App.Components;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Serilog;

var builder = WebApplication.CreateBuilder(
    args
);

// Add services to the container.
builder
    .Services
    .AddRazorComponents()
    .AddInteractiveServerComponents();

var builderConfiguration =
    builder.Configuration;

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

serviceCollection.AddSingleton<IDangerousHttpClientBuilder, DangerousHttpClientBuilder>();

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
    );

serviceCollection.AddLogging(
    loggingBuilder =>
    {
        loggingBuilder.ClearProviders();
        loggingBuilder.AddSerilog();
    }
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler(
        "/Error",
        createScopeForErrors: true
    );

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app
    .MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
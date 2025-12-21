using FashionFace.Common.Constants.Constants;
using FashionFace.Common.Extensions.Dependencies.Implementations;
using FashionFace.Dependencies.RabbitMq.Facades.Interfaces;
using FashionFace.Dependencies.RabbitMq.Interfaces;
using FashionFace.Executable.Worker.UserEvents.Args;
using FashionFace.Executable.Worker.UserEvents.Interfaces;
using FashionFace.Repositories.Context;
using FashionFace.Services.ConfigurationSettings.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using RabbitMQ.Client.Events;

using Serilog;

var builder =
    Host
        .CreateApplicationBuilder(
            args
        );

var builderConfiguration =
    builder.Configuration;

var serviceCollection =
    builder.Services;

Log.Logger =
    new LoggerConfiguration()
        .ReadFrom
        .Configuration(
            builderConfiguration
        )
        .Enrich
        .FromLogContext()
        .CreateLogger();

var redisSection = builderConfiguration.GetSection(
    "Redis"
);
serviceCollection.Configure<RedisSettings>(
    redisSection
);

var applicationSection = builderConfiguration.GetSection(
    "Application"
);
serviceCollection.Configure<ApplicationSettings>(
    applicationSection
);

var rabbitMqSection = builderConfiguration.GetSection(
    "RabbitMq"
);
serviceCollection.Configure<RabbitMqSettings>(
    rabbitMqSection
);

serviceCollection.AddLogging(
    loggingBuilder =>
    {
        loggingBuilder.ClearProviders();
        loggingBuilder.AddSerilog();
    }
);

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

serviceCollection.AddStackExchangeRedisCache(
    options =>
    {
        options.Configuration = redisSection["Configuration"];
        options.InstanceName = redisSection["InstanceName"];
    }
);

serviceCollection.SetupDependencies();

//builder.Services.AddHostedService<FilterResultTalentValidationWorker>();

var host =
    builder.Build();

var serviceProvider =
    host.Services;

var queueConnectionCreateDomainFacadeBuilder =
    serviceProvider.GetRequiredService<IQueueConnectionCreateDomainFacadeBuilder>();

var publishSubscribeChannelService =
    serviceProvider.GetRequiredService<IPublishSubscribeChannelService>();

var channelSubscribeService =
    serviceProvider.GetRequiredService<IChannelSubscribeService>();

var queueConnectionCreateDomainFacade =
    queueConnectionCreateDomainFacadeBuilder.Build();

var connection =
    await
        queueConnectionCreateDomainFacade.CreateAsync();

var channel =
    await
        publishSubscribeChannelService
            .CreateFanout(
                connection,
                RabbitMqChannelsConstants.UserProfileUpdateExchange,
                RabbitMqChannelsConstants.UserProfileUpdateQueue
            );

var asyncEventHandler =
    GetEventHandler();

await
    channelSubscribeService
        .Subscribe(
            channel,
            asyncEventHandler
        );

host.Run();

return;

AsyncEventHandler<BasicDeliverEventArgs> GetEventHandler()
{
    var eventHandlerBuilderArgs =
        new EventHandlerBuilderArgs(
            serviceProvider
        );

    var eventHandlerBuilder =
        serviceProvider.GetRequiredService<IUserProfileUpdatedEventHandlerBuilder>();

    var asyncEventHandler =
        eventHandlerBuilder
            .Build(
                eventHandlerBuilderArgs
            );
    return
        asyncEventHandler;
}
using System;
using System.Reflection;
using System.Threading.Tasks;

using FashionFace.Common.Extensions.Dependencies.Implementations;
using FashionFace.Common.Models.Models.Commands;
using FashionFace.Dependencies.RabbitMq.Facades.Interfaces;
using FashionFace.Dependencies.RabbitMq.Interfaces;
using FashionFace.Dependencies.SignalR.Implementations;
using FashionFace.Executable.Worker.UserEvents.Args;
using FashionFace.Executable.Worker.UserEvents.Interfaces;
using FashionFace.Executable.Worker.UserEvents.Workers;
using FashionFace.Repositories.Context;
using FashionFace.Repositories.Context.Models.IdentityEntities;
using FashionFace.Services.ConfigurationSettings.Models;

using MassTransit;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using RabbitMQ.Client;

using Serilog;

using StackExchange.Redis;

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

serviceCollection.SetupDependencies();

serviceCollection.AddStackExchangeRedisCache(
    options =>
    {
        options.Configuration = redisSection["Configuration"];
        options.InstanceName = redisSection["InstanceName"];
    }
);

serviceCollection
    .AddSignalR(
        options =>
        {
            options.AddFilter<HubExceptionsFilter>();
        }
    )
    .AddStackExchangeRedis(options =>
    {
        options.Configuration.ChannelPrefix =
            $"signalr:{builder.Environment.EnvironmentName}";

        options.ConnectionFactory = async writer =>
        {
            var config = new ConfigurationOptions
            {
                EndPoints = { redisSection["Configuration"] },
                Password = redisSection["Password"],
                AbortOnConnectFail = false,
                ConnectRetry = 20,
                ReconnectRetryPolicy = new ExponentialRetry(500, (int)TimeSpan.FromSeconds(30).TotalMilliseconds),
            };

            var muxer = await ConnectionMultiplexer.ConnectAsync(config, writer);

            muxer.ConnectionFailed += (sender, args) =>
                Console.WriteLine($"Redis connection failed: {args.Exception?.Message}");
            muxer.ConnectionRestored += (sender, args) =>
                Console.WriteLine("Redis connection restored");

            return muxer;
        };
    });

serviceCollection.AddMassTransit(
    configurator =>
    {
        configurator.AddConsumers(Assembly.GetExecutingAssembly());
        configurator.AddSagaStateMachines(Assembly.GetExecutingAssembly());
        configurator.AddSagas(Assembly.GetExecutingAssembly());
        configurator.AddActivities(Assembly.GetExecutingAssembly());

        configurator.SetKebabCaseEndpointNameFormatter();

        configurator.AddConfigureEndpointsCallback((name, cfg) =>
        {
            cfg.PrefetchCount = 32;
            cfg.ConcurrentMessageLimit = 8;

            cfg.UseMessageRetry(
                retryConfigurator => retryConfigurator.Exponential(
                    5,
                    TimeSpan.FromSeconds(
                        5
                    ),
                    TimeSpan.FromSeconds(
                        15
                    ),
                    TimeSpan.FromSeconds(
                        30
                    )
                )
            );
        });

        configurator.UsingRabbitMq(
            (
                context,
                cfg
            ) =>
            {
                cfg.Host(
                    rabbitMqSection["Host"],
                    ushort.Parse(rabbitMqSection["Port"]),
                    rabbitMqSection["VHost"],
                    hostConfigurator =>
                    {
                        hostConfigurator.Username(
                            rabbitMqSection["Username"]
                        );
                        hostConfigurator.Password(
                            rabbitMqSection["Password"]
                        );
                    }
                );

                cfg.ConfigureEndpoints(context);
            }
        );
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

serviceCollection
    .AddIdentity<ApplicationUser, ApplicationRole>()
    .AddRoleManager<RoleManager<ApplicationRole>>()
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<ApplicationDatabaseContext>()
    .AddRoles<ApplicationRole>();

serviceCollection.SetupDependencies();

serviceCollection
    .AddScoped<UserToUserChatMessageSendOutboxPendingWorker, UserToUserChatMessageSendOutboxPendingWorker>();
serviceCollection
    .AddScoped<UserToUserChatMessageSendOutboxClaimedRetryWorker, UserToUserChatMessageSendOutboxClaimedRetryWorker>();
serviceCollection
    .AddScoped<UserToUserChatMessageReadOutboxPendingWorker, UserToUserChatMessageReadOutboxPendingWorker>();
serviceCollection
    .AddScoped<UserToUserChatMessageReadOutboxClaimedRetryWorker, UserToUserChatMessageReadOutboxClaimedRetryWorker>();
serviceCollection
    .AddScoped<UserToUserChatMessageSendNotificationOutboxPendingWorker,
        UserToUserChatMessageSendNotificationOutboxPendingWorker>();
serviceCollection
    .AddScoped<UserToUserChatMessageSendNotificationOutboxClaimedRetryWorker,
        UserToUserChatMessageSendNotificationOutboxClaimedRetryWorker>();
serviceCollection
    .AddScoped<UserToUserChatMessageReadNotificationOutboxPendingWorker,
        UserToUserChatMessageReadNotificationOutboxPendingWorker>();
serviceCollection
    .AddScoped<UserToUserChatMessageReadNotificationOutboxClaimedRetryWorker,
        UserToUserChatMessageReadNotificationOutboxClaimedRetryWorker>();

builder.Services.AddHostedService<UserToUserChatMessageSendOutboxPendingWorker>();
builder.Services.AddHostedService<UserToUserChatMessageSendOutboxClaimedRetryWorker>();

builder.Services.AddHostedService<UserToUserChatMessageReadOutboxPendingWorker>();
builder.Services.AddHostedService<UserToUserChatMessageReadOutboxClaimedRetryWorker>();

builder.Services.AddHostedService<UserToUserChatMessageSendNotificationOutboxPendingWorker>();
builder.Services.AddHostedService<UserToUserChatMessageSendNotificationOutboxClaimedRetryWorker>();

builder.Services.AddHostedService<UserToUserChatMessageReadNotificationOutboxPendingWorker>();
builder.Services.AddHostedService<UserToUserChatMessageReadNotificationOutboxClaimedRetryWorker>();

var host =
    builder.Build();

var serviceProvider =
    host.Services;

var publishSubscribeChannelService =
    serviceProvider.GetRequiredService<IPublishSubscribeChannelService>();

var channelSubscribeService =
    serviceProvider.GetRequiredService<IChannelSubscribeService>();

var queueConnectionCreateDomainFacade =
    serviceProvider.GetRequiredService<IQueueConnectionCreateDomainFacade>();

var connection =
    await
        queueConnectionCreateDomainFacade.CreateAsync();

await
    Subscribe<IUserToUserChatInvitationAcceptedNotificationHandlerBuilder, HandleUserToUserInvitationAcceptedOutbox>(
        publishSubscribeChannelService,
        connection,
        serviceProvider,
        channelSubscribeService
    );

await
    Subscribe<IUserToUserChatInvitationCanceledNotificationHandlerBuilder, HandleUserToUserInvitationCanceledOutbox>(
        publishSubscribeChannelService,
        connection,
        serviceProvider,
        channelSubscribeService
    );

await
    Subscribe<IUserToUserChatInvitationCreatedNotificationHandlerBuilder, HandleUserToUserInvitationCreatedOutbox>(
        publishSubscribeChannelService,
        connection,
        serviceProvider,
        channelSubscribeService
    );

await
    Subscribe<IUserToUserChatInvitationRejectedNotificationHandlerBuilder, HandleUserToUserInvitationRejectedOutbox>(
        publishSubscribeChannelService,
        connection,
        serviceProvider,
        channelSubscribeService
    );

await
    Subscribe<IUserToUserChatMessageReadNotificationHandlerBuilder, HandleUserToUserMessageReadNotificationOutbox>(
        publishSubscribeChannelService,
        connection,
        serviceProvider,
        channelSubscribeService
    );

await
    Subscribe<IUserToUserChatMessageReadHandlerBuilder, HandleUserToUserMessageReadOutbox>(
        publishSubscribeChannelService,
        connection,
        serviceProvider,
        channelSubscribeService
    );

await
    Subscribe<IUserToUserChatMessageSendNotificationHandlerBuilder, HandleUserToUserMessageSendNotificationOutbox>(
        publishSubscribeChannelService,
        connection,
        serviceProvider,
        channelSubscribeService
    );

await
    Subscribe<IUserToUserChatMessageSendHandlerBuilder, HandleUserToUserMessageSendOutbox>(
        publishSubscribeChannelService,
        connection,
        serviceProvider,
        channelSubscribeService
    );

host.Run();

return;

static async Task Subscribe<TService, TOutbox>(
    IPublishSubscribeChannelService publishSubscribeChannelService,
    IConnection connection,
    IServiceProvider serviceProvider,
    IChannelSubscribeService channelSubscribeService
)
    where TService : class, IHandlerBuilderBase
    where TOutbox : class
{
    var channel =
        await
            publishSubscribeChannelService
                .CreateDirect(
                    connection,
                    $"{typeof(TOutbox).FullName}.exchange",
                    $"{typeof(TOutbox).FullName}.queue"
                );

    var eventHandlerBuilderArgs =
        new EventHandlerBuilderArgs(
            serviceProvider
        );

    var eventHandlerBuilder =
        serviceProvider.GetRequiredService<TService>();

    var eventHandler =
        eventHandlerBuilder
            .Build(
                eventHandlerBuilderArgs
            );

    await
        channelSubscribeService
            .Subscribe(
                channel,
                eventHandler
            );
}
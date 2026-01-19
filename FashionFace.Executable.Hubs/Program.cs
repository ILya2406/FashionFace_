using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using FashionFace.Common.Extensions.Dependencies.Implementations;
using FashionFace.Common.Extensions.Implementations;
using FashionFace.Dependencies.SignalR.Implementations;
using FashionFace.Repositories.Context;
using FashionFace.Repositories.Context.Models.IdentityEntities;
using FashionFace.Services.ConfigurationSettings.Models;

using MassTransit;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;

using Serilog;

using StackExchange.Redis;

using Swashbuckle.AspNetCore.SwaggerGen;

var builder =
    WebApplication
        .CreateBuilder(
            args
        );

var builderConfiguration =
    builder.Configuration;

var serviceCollection =
    builder.Services;

var corsSection = builderConfiguration.GetSection(
    "Cors"
);
serviceCollection.Configure<CorsSettings>(
    corsSection
);

var jwtSection = builderConfiguration.GetSection(
    "Jwt"
);
serviceCollection.Configure<JwtSettings>(
    jwtSection
);

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

Log.Logger =
    new LoggerConfiguration()
        .ReadFrom
        .Configuration(
            builderConfiguration
        )
        .Enrich
        .FromLogContext()
        .CreateLogger();


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

serviceCollection.AddDataProtection();

serviceCollection.AddStackExchangeRedisCache(options =>
{
    options.Configuration = redisSection["Configuration"];
    options.InstanceName =  redisSection["InstanceName"];
});

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
                AbortOnConnectFail = false,
                AllowAdmin = true,
                ConnectRetry = 20,
                ConnectTimeout = 10000,
                SyncTimeout = 10000,
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

serviceCollection.AddEndpointsApiExplorer();
serviceCollection.AddSwaggerGen(
    options =>
    {
        var xmlFilename = $"{
            Assembly
                .GetExecutingAssembly()
                .GetName()
                .Name
        }.xml";
        var filePath = Path.Combine(
            AppContext.BaseDirectory,
            xmlFilename
        );

        options.IncludeXmlComments(
            filePath
        );

        options.AddSecurityDefinition(
            "Bearer",
            new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. \n\nEnter: Bearer {token}",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
            }
        );

        options.AddSecurityRequirement(
            document => new()
            {
                [new(
                    "Bearer",
                    document
                )] = [],
            }
        );

        options.TagActionsBy(
            api =>
            {
                var groupAttr =
                    api
                        .ActionDescriptor
                        .EndpointMetadata
                        .OfType<ApiExplorerSettingsAttribute>()
                        .FirstOrDefault()
                        ?.GroupName;

                return [groupAttr ?? "Default",];
            }
        );

        options.DocInclusionPredicate(
            (
                _,
                apiDesc
            ) =>
            {
                var isSuccess =
                    !apiDesc
                        .TryGetMethodInfo(
                            out var _
                        );

                if (isSuccess)
                {
                    return false;
                }

                var groupName =
                    apiDesc
                        .ActionDescriptor
                        .EndpointMetadata
                        .OfType<ApiExplorerSettingsAttribute>()
                        .FirstOrDefault()
                        ?.GroupName;

                var isNotEmptyGroupName =
                    groupName.IsNotEmpty();

                return
                    isNotEmptyGroupName;
            }
        );
    }
);

serviceCollection
    .AddAuthentication(
        options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }
    )
    .AddJwtBearer(
        "Bearer",
        options =>
        {
            options.RequireHttpsMetadata = true;
            options.SaveToken = true;
            options.TokenValidationParameters = new()
            {
                ValidateIssuer = true,
                ValidIssuer = jwtSection["Issuer"],
                ValidateAudience = true,
                ValidAudience = jwtSection["Audience"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        jwtSection["Secret"]
                    )
                ),
                ClockSkew = TimeSpan.FromMinutes(
                    30
                ),
                ValidateLifetime = true,
            };
            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var accessToken = context.Request.Query["access_token"];
                    var path = context.HttpContext.Request.Path;
                    if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hubs"))
                    {
                        context.Token = accessToken;
                    }
                    return Task.CompletedTask;
                }
            };
        }
    );

var corsSettings =
    corsSection.Get<CorsSettings>();

foreach (var item in corsSettings.OriginList)
{
    Log.Information(
        $"cors: {item}"
    );
}

serviceCollection
    .AddCors(
        corsOptions =>
        {
            corsOptions.AddDefaultPolicy(
                policy =>
                {
                    policy
                        .WithOrigins(
                            corsSettings.OriginList
                        )
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                }
            );
        }
    );

serviceCollection.SetupDependencies();
serviceCollection.AddSingleton<IUserIdProvider, UserIdProvider>();

var app =
    builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapHub<UserNotificationHub>($"/hubs/{nameof(UserNotificationHub)}")
    .RequireAuthorization();

app.MapHub<AdminNotificationHub>($"/hubs/{nameof(AdminNotificationHub)}")
    .RequireAuthorization();

app.MapHub<UserToUserChatNotificationHub>($"/hubs/{nameof(UserToUserChatNotificationHub)}")
    .RequireAuthorization();

app.MapHub<UserToUserChatInvitationNotificationHub>($"/hubs/{nameof(UserToUserChatInvitationNotificationHub)}")
    .RequireAuthorization();

app.Run();
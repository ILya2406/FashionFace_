using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

using FashionFace.Common.Extensions.Dependencies.Implementations;
using FashionFace.Common.Extensions.Implementations;
using FashionFace.Dependencies.SignalR.Implementations;
using FashionFace.Repositories.Context;
using FashionFace.Repositories.Context.Models.IdentityEntities;
using FashionFace.Services.ConfigurationSettings.Models;

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
                ClockSkew = TimeSpan.FromSeconds(
                    30
                ),
                ValidateLifetime = true,
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

serviceCollection
    .AddSignalR(
        options => { options.AddFilter<HubExceptionsFilter>(); }
    )
    .AddRedis(
        redisSection["Configuration"],
        options =>
        {
            options.Configuration.ChannelPrefix = "signalr";
        }
    );

serviceCollection.AddStackExchangeRedisCache(options =>
{
    options.Configuration = redisSection["Configuration"];
    options.InstanceName =  redisSection["InstanceName"];
});

serviceCollection.SetupDependencies();
serviceCollection.AddSingleton<IUserIdProvider, UserIdProvider>();

var app =
    builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapHub<UserNotificationHub>($"/hubs/{nameof(UserNotificationHub)}")
    .RequireAuthorization();

app.MapHub<AdminNotificationHub>($"/hubs/{nameof(AdminNotificationHub)}")
    .RequireAuthorization();

app.Run();
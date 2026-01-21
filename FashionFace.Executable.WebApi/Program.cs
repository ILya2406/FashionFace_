using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

using FashionFace.Common.Extensions.Dependencies.Implementations;
using FashionFace.Common.Extensions.Implementations;
using FashionFace.Executable.WebApi.Configurations;
using FashionFace.Repositories.Context;
using FashionFace.Repositories.Context.Models.IdentityEntities;
using FashionFace.Services.ConfigurationSettings.Models;

using MassTransit;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using Serilog;

using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(
    args
);

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

var nanoBananaSection = builderConfiguration.GetSection(
    "NanoBanana"
);
serviceCollection.Configure<NanoBananaSettings>(
    nanoBananaSection
);

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

var imageKitSection = builderConfiguration.GetSection(
    "ImageKit"
);
serviceCollection.Configure<ImageKitSettings>(
    imageKitSection
);

serviceCollection.AddLogging(
    loggingBuilder =>
    {
        loggingBuilder.ClearProviders();
        loggingBuilder.AddSerilog();
    }
);

serviceCollection.SetupDependencies();

serviceCollection.AddMassTransit(
    configurator =>
    {
        configurator.SetKebabCaseEndpointNameFormatter();

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

serviceCollection.AddStackExchangeRedisCache(options =>
{
    options.Configuration = redisSection["Configuration"];
    options.InstanceName = redisSection["InstanceName"];
});

// todo : to use BCript for legacy users
//serviceCollection.AddScoped<IPasswordHasher<ApplicationUser>, BcryptPasswordHasher<ApplicationUser>>();

var filters =
    new[]
    {
        typeof(ExceptionFilter),
    };

serviceCollection
    .AddControllersWithViews(
        options =>
        {
            foreach (var filter in filters)
            {
                options
                    .Filters
                    .Add(
                        filter
                    );
            }
        }
    )
    .AddJsonOptions(
        options =>
        {
            options
                .JsonSerializerOptions
                .Converters
                .Add(
                    new JsonStringEnumConverter(
                        new PascalCaseNamingPolicy()
                    )
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
            new()
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
            new()
            {
                {
                    new()
                    {
                        Reference = new()
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer",
                        },
                    },
                    Array.Empty<string>()
                },
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


var app =
    builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Serve static files from /app/files/media at URL /files
var filesPath = "/app/files/media";
if (!Directory.Exists(filesPath))
{
    Directory.CreateDirectory(filesPath);
}
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(filesPath),
    RequestPath = "/files",
    ServeUnknownFileTypes = true
});

app.UseRouting();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
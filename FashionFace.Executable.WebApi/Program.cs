using System;
using System.IO;
using System.Reflection;
using System.Text;

using FashionFace.Common.Extensions.Implementations;
using FashionFace.Services.ConfigurationSettings.Models;
using FashionFace.Repositories.Context;
using FashionFace.Repositories.Context.Models;
using FashionFace.Executable.WebApi.Configurations;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(
    args
);

var serviceCollection =
    builder.Services;

var builderConfiguration =
    builder.Configuration;

var nanoBananaSection = builderConfiguration.GetSection(
    "NanoBanana"
);
serviceCollection.Configure<NanoBananaSettings>(
    nanoBananaSection
);

var jwtSection = builderConfiguration.GetSection(
    "Jwt"
);
serviceCollection.Configure<JwtSettings>(
    jwtSection
);

serviceCollection.SetupDependencies();

var filters =
    new[]
    {
        typeof(ExceptionFilter),
    };

serviceCollection.AddControllersWithViews(
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

serviceCollection
    .AddDataProtection();

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
    }
);

builder
    .Services
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
                ValidIssuer = builderConfiguration["JWT:Issuer"],
                ValidateAudience = true,
                ValidAudience = builderConfiguration["JWT:Audience"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        builderConfiguration["JWT:Secret"]
                    )
                ),
                ClockSkew = TimeSpan.FromSeconds(
                    30
                ),
                ValidateLifetime = true,
            };
        }
    );

var app =
    builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
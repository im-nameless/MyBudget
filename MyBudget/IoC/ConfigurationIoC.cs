using System;
using Application.Dto;
using AutoMapper;
using Domain.Core.Interfaces.Services;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Domain.Core.Interfaces.Repositories;
using MyBudget.Domain.Services.Services;
using MyBudget.Infra.Data.Context;
using MyBudget.Infra.Data.Repositories;
using Application.Interfaces;
using Application;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Domain.Auth;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;
using Npgsql;

namespace MyBudget.IoC;

public static class ConfigurationIoC
{
    public static void LoadServices(IServiceCollection services, IConfiguration config)
    {
        services.AddHttpContextAccessor();
        services.TryAddSingleton(config);

        services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
        
        services.AddScoped(typeof(IServiceBase<>), typeof(ServiceBase<>));

        services.AddScoped(typeof(IApplicationServiceBase<,>), typeof(ApplicationServiceBase<,>));
        services.AddScoped<IApplicationServiceLogin, ApplicationServiceLogin>();
        services.AddScoped<IApplicationServiceIncome, ApplicationServiceIncome>();
        services.AddScoped<IApplicationServiceExpense, ApplicationServiceExpense>();
        services.AddScoped<IApplicationServiceUser, ApplicationServiceUser>();
    }

    public static void LoadDatabase(IServiceCollection services, IConfiguration config)
    {
        var connection = config.GetConnectionString("DefaultConnection");
        services.AddDbContext<MyBudgetContext>(options =>
        {
            var connection = config.GetConnectionString("DefaultConnection") ?? throw new Exception("DefaultConnection not found 2");
            options.UseNpgsql(connection)
                   .EnableSensitiveDataLogging()
                   .EnableDetailedErrors();
        });

        using var context = services.BuildServiceProvider()
                                    .GetRequiredService<MyBudgetContext>();

        context.Database.Migrate();
    }

    public static void LoadMapper(IServiceCollection services)
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<User, UserDto>().ReverseMap();
        });

        var mapper = config.CreateMapper();
        services.AddSingleton(mapper);
    }

    public static void LoadSwagger(IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.LoadOpenApiOptions();
        });
    }
    
    private static void LoadOpenApiOptions(this SwaggerGenOptions options)
    {
        var securityScheme = new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Scheme = "Bearer",
            BearerFormat = "JWT",
            Type = SecuritySchemeType.ApiKey,
            In = ParameterLocation.Header,
            Description = "JWT based security",
        };
        var securityReq = new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] {}
            }
        };
        var contact = new OpenApiContact()
        {
            Name = "MyBudget",
            Email = "giovanni.teixeira@hotmail.com",
            Url = new Uri("mailto:giovanni.teixeira@hotmail.com")
        };
        var info = new OpenApiInfo()
        {
            Version = "v1",
            Title = "MyBudget | V1",
            Description = "API designed to My Budget",
            Contact = contact,
        };

        options.SwaggerDoc("v1", info);
        options.AddSecurityDefinition("Bearer", securityScheme);
        options.AddSecurityRequirement(securityReq);
    }

    public static void AddAuthentication(IServiceCollection services, IConfiguration config)
    {
        var jwtSettings = config.GetSection("JwtSettings").Get<JwtSettings>();
        services.AddSingleton(jwtSettings);
        
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.UseSecurityTokenValidators = true;
            options.IncludeErrorDetails = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
            };
        });
    }
}

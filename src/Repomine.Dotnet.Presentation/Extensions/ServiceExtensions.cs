using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace Repomine.Dotnet.Presentation.Extensions;

public static class ServiceExtensions
{
    public static void AddApiVersioningService(this IServiceCollection services)
    {
        services.AddApiVersioning(config =>
        {
            // Specify the default API Version as 1.0
            config.DefaultApiVersion = new ApiVersion(1, 0);
            // If the client hasn't specified the API version in the request, use the default API version number 
            config.AssumeDefaultVersionWhenUnspecified = true;
            // Advertise the API versions supported for the particular endpoint
            config.ReportApiVersions = true;
        });
    }

    public static void AddSwaggerService(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Repomine - Dotnet",
                Description = "Modern, dynamic, customizable ASP .Net starter project for enterprise solutions.",
                Contact = new OpenApiContact { Name = "repomine", Email = "repomine@outlook.com", Url = new Uri("https://repomine.com") }
            });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                Description = "Input your Bearer token in this format - Bearer {your token here} to access this API",
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" },
                        Scheme = "Bearer",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    }, new List<string>()
                },
            });
        });
    }
}
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Ambev.DeveloperEvaluation.WebApi.Common;

/// <summary>
/// Configuration for Swagger documentation.
/// </summary>
public static class SwaggerConfiguration
{
    /// <summary>
    /// Configures Swagger services.
    /// </summary>
    /// <param name="services">The service collection.</param>
    public static void ConfigureSwaggerServices(IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Ambev Developer Evaluation API",
                Version = "v1",
                Description = "API for managing sales records with DDD principles",
                Contact = new OpenApiContact
                {
                    Name = "Developer Evaluation Team",
                    Email = "evaluation@ambev.com"
                }
            });

            // Add XML comments
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            if (File.Exists(xmlPath))
            {
                c.IncludeXmlComments(xmlPath);
            }

            // Configure security schemes if needed
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                    Array.Empty<string>()
                }
            });
        });
    }
}

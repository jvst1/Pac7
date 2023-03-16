using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using WeatherPrediction.Infrastructure.Helpers;
using Swashbuckle.AspNetCore.SwaggerGen;
using WeatherPrediction.Infrastructure.Filters;

namespace WeatherPrediction.Infrastructure.Extensions
{
    public static class ServicesExtensions
    {
        public static void AddSwagger(this IServiceCollection services, string title, string version, Action<SwaggerGenOptions> customOptions = null)
        {
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = title, Version = version });

                c.AddSecurityDefinition(
                    "Bearer",
                    new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = "Copie 'Bearer ' + token",
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey
                    });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            In = ParameterLocation.Header,
                            Name = "Bearer",
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new List<string>()
                    }
                });

                c.OperationFilter<AddRequiredHeaderParameter>();
                c.DocumentFilter<SwaggerAddEnumDescriptions>();
                c.SchemaFilter<SwaggerExcludeFilter>();

                customOptions?.Invoke(c);
            });
        }

        public static AppSettings AddAppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var appSettingsSection = configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();

            return appSettings;
        }
    }
}
using System;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Template.Application.Options;

namespace Template.Application.Configurations
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            SwaggerOptions swaggerOptions = new SwaggerOptions();
            configuration.GetSection("SwaggerOptions").Bind(swaggerOptions);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = swaggerOptions.Title,
                    Description = swaggerOptions.Description,
                    TermsOfService = new Uri(swaggerOptions.TermsOfServiceUrl),
                    Contact = new OpenApiContact
                    {
                        Name = "Alex Botelho",
                        Email = swaggerOptions.ContactEmail,
                        Url = new Uri(swaggerOptions.ContactUrl)
                    }
                });
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app, IConfiguration configuration, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger(c =>
                {
                    c.PreSerializeFilters.Add((swagger, httpReq) =>
                    {
                        swagger.Servers = new List<OpenApiServer>
                        {
                            new OpenApiServer
                            {
                                Url = $"https: //{httpReq.Host.Value}{new PathString(configuration.GetSection("BasePath").Value.ToLower())}"
                            }
                        };
                    });
                });

                app.UseSwaggerUI();
            }

            return app;
        }
    }
}

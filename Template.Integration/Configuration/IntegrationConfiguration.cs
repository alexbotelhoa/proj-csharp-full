using Microsoft.Extensions.DependencyInjection;

using Template.Integration.Services;
using Template.Integration.Integrations;
using Template.Integration.Services.Interfaces;
using Template.Integration.Integrations.Interfaces;

namespace Template.Integration.Configurations
{
    public static class IntegrationConfiguration
    {
        public static IServiceCollection AddIntegrationConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IUserIntegration, UserIntegration>();
            services.AddScoped<IUserIntegrationService, UserIntegrationService>();

            return services;
        }
    }
}

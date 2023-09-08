using Template.DataAccess.Services;
using Template.DataAccess.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Template.DataAccess.Configurations
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection AddServiceConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IUserDatabaseService, UserDatabaseService>();

            return services;
        }
    }
}

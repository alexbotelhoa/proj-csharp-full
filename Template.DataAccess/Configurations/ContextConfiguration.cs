using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Template.DataAccess.Context;

namespace Template.DataAccess.Configurations
{
    public static class ContextConfiguration
    {
        public static IServiceCollection AddContextConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CoreContext>(x => x.UseNpgsql(configuration["ConnectionStrings:DefaultConnection"]));

            return services;
        }
    }
}

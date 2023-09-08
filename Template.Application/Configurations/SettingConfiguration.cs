using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Template.Application.Options;
using Template.Integration.Options;

namespace Template.Application.Configurations
{
    public static class SettingConfiguration
    {
        public static IServiceCollection AddSettingConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SwaggerOptions>(configuration.GetSection("SwaggerOptions"));
            services.Configure<ComunicacaoHttp>(configuration.GetSection("ComunicacaoHttp"));

            services.AddSingleton(sp => sp.GetRequiredService<IOptions<SwaggerOptions>>().Value);
            services.AddSingleton(sp => sp.GetRequiredService<IOptions<ComunicacaoHttp>>().Value);

            return services;
        }
    }
}

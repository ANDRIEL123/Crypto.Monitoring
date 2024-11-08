using Crypto.Monitoring.src.Services;
using Crypto.Monitoring.src.Services.Interfaces;

namespace Crypto.Monitoring.Infra
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ICryptoService, CryptoService>();

            return services;
        }
    }
}
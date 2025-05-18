using Crypto.Monitoring.Services;
using Crypto.Monitoring.Services.Interfaces;
using Crypto.Monitoring.src.Services;

namespace Crypto.Monitoring.Infra
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ICryptoService, CryptoService>();
            services.AddScoped<IDiscordBotService, DiscordBotService>();

            return services;
        }
    }
}
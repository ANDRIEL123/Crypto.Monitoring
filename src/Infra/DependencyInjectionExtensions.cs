using Crypto.Monitoring.Services;
using Crypto.Monitoring.Services.Interfaces;

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
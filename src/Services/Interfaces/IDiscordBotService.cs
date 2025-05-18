using Crypto.Monitoring.Models;

namespace Crypto.Monitoring.Services.Interfaces
{
    public interface IDiscordBotService
    {
        Task SendMessageToChannel(string message);
    }
}
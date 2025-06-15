using Crypto.Monitoring.Services.Interfaces;

namespace Crypto.Monitoring.Services
{
    public class DiscordBotService : IDiscordBotService
    {
        public async Task SendMessageToChannel(string message)
        {
            var payload = new
            {
                content = message
            };

            using var httpClient = new HttpClient();
            var response = await httpClient.PostAsJsonAsync(Environment.GetEnvironmentVariable("DISCORD_WEBHOOK_URL"), payload);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Mensagem enviada com sucesso para o discord!");
            }
            else
            {
                Console.WriteLine($"Erro: {response.StatusCode}");
            }
        }
    }
}

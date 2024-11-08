using Crypto.Monitoring.src.Services.Interfaces;

namespace Crypto.Monitoring.src.Services
{
    public class CryptoService : ICryptoService
    {
        private readonly string BaseAddress = "https://api.coingecko.com";

        public async Task<object> GetCurrentCrypto(string symbol = "bitcoin", string currency = "brl")
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseAddress);

                var response = await client.GetAsync(GetUrl(symbol, currency));
                var responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
        }

        /// <summary>
        /// CoinGecko Api Url
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="currency"></param>
        /// <returns></returns>
        private static string GetUrl(string symbol = "bitcoin", string currency = "BRL")
        {
            return $"/api/v3/simple/price?ids={symbol}&vs_currencies={currency}";
        }
    }
}
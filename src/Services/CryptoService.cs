using Binance.Net.Clients;
using Crypto.Monitoring.Services.Interfaces;
using CryptoExchange.Net.Authentication;

namespace Crypto.Monitoring.Services
{
    public class CryptoService : ICryptoService
    {
        private readonly BinanceRestClient _binanceClient;
        private readonly IDiscordBotService _discordBotService;

        public CryptoService(IDiscordBotService discordBotService)
        {
            _binanceClient = new BinanceRestClient(options =>
            {
                options.ApiCredentials = new ApiCredentials(Environment.GetEnvironmentVariable("BINANCE_API_KEY")!, Environment.GetEnvironmentVariable("BINANCE_API_SECRET")!);
            });

            _discordBotService = discordBotService;
        }

        /// <summary>
        /// Current balance for all my crypto
        /// </summary>
        /// <returns></returns>
        public async Task ConsultingBalance()
        {
            var currentBalanceText = "Saldos da minhas moedas\n\n";
            var currentBalance = 0m;

            var account = await _binanceClient.SpotApi.Account.GetAccountInfoAsync();

            if (account.Success)
            {
                foreach (var balance in account.Data.Balances)
                {
                    if (balance.Available > 0)
                    {
                        var price = await GetPriceAndChange(balance.Asset + "BRL");

                        var balanceAsset = price.Item1 > 0 ? balance.Available * price.Item1 : balance.Available;

                        currentBalanceText += $"Moeda: {balance.Asset}\n";
                        currentBalanceText += $"Saldo Disponível: {balance.Available}\n";
                        currentBalanceText += $"Valor total em R$: {Math.Round(balanceAsset), 2}\n";
                        currentBalanceText += $"Variação nas últimas 24hrs: {price.Item2}%\n\n";

                        currentBalance += balanceAsset;
                    }
                }
            }
            else
                Console.WriteLine($"Erro ao obter saldo: {account.Error}");

            currentBalanceText += $"\nSaldo total atual R$: {Math.Round(currentBalance, 2)}\n";
            // Console.WriteLine(currentBalanceText);

            // Send to discord
            await _discordBotService.SendMessageToChannel(currentBalanceText);
        }

        // TODO As 10 cryptos que tiveram o maior aumento ou queda nas últimas 24 horas



        /// <summary>
        /// Get current price and variation in the last 24 hours
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public async Task<(decimal, decimal)> GetPriceAndChange(string symbol)
        {
            // Melhorar isso
            if (symbol == "BRLBRL")
                return default;

            var finalSymbol = symbol.ToUpper();

            var ticker = await _binanceClient.SpotApi.ExchangeData.GetTickerAsync(finalSymbol);

            if (ticker.Success)
                return (ticker.Data.LastPrice, ticker.Data.PriceChangePercent);
            else
            {
                Console.WriteLine($"Erro ao obter preço para {symbol}: {ticker.Error}");
            }

            return default;

            // TODO Send to discord
        }
    }
}
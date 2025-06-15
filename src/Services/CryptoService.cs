using System.Text;
using Binance.Net.Clients;
using Crypto.Monitoring.Services.Interfaces;
using CryptoExchange.Net.Authentication;

namespace Crypto.Monitoring.Services
{
    public class CryptoService : ICryptoService
    {
        private readonly BinanceRestClient _binanceClient;
        private readonly IDiscordBotService _discordBotService;
        private StringBuilder _resumeText = new();

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
            _resumeText.Append("\n\n**SALDO DAS MINHAS MOEDAS**\n\n");
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

                        _resumeText.Append($"Moeda: {balance.Asset}\n");
                        _resumeText.Append($"Saldo Disponível: {balance.Available}\n");
                        _resumeText.Append($"Valor total em R$: {Math.Round(balanceAsset), 2}\n");
                        _resumeText.Append($"Variação nas últimas 24hrs: {price.Item2}%\n");

                        currentBalance += balanceAsset;
                    }
                }
            }
            else
                Console.WriteLine($"Erro ao obter saldo: {account.Error}");

            _resumeText.Append($"\n**SALDO TOTAL ATUAL R$:** {Math.Round(currentBalance, 2)}\n");

            // Send to discord
            await _discordBotService.SendMessageToChannel(GetMessageToSend());
        }

        /// <summary>
        /// As 10 cryptos que tiveram o maior aumento ou queda nas últimas 24 horas
        /// </summary>
        /// <returns></returns>
        public async Task GetTopCryptoChange()
        {
            _resumeText.Append("\n**TOP 10 CRYPTOS COM MAIOR VARIAÇÃO**\n\n");
            var data = await _binanceClient.SpotApi.ExchangeData.GetTickersAsync();
            var top10 = data.Data
                .Where(x => x.Symbol.EndsWith("BRL") && x.PriceChangePercent != 0)
                .Select(x => new
                {
                    x.Symbol,
                    x.PriceChangePercent,
                    x.LastPrice
                })
                .OrderByDescending(x => Math.Abs(x.PriceChangePercent))
                .Take(10)
                .ToList();

            foreach (var crypto in top10)
            {
                _resumeText.Append($"Moeda: {crypto.Symbol}\n");
                _resumeText.Append($"Variação: {Math.Round(crypto.PriceChangePercent), 2}%\n");
                _resumeText.Append($"Ultimo preço: {Math.Round(crypto.LastPrice, 2)} R$\n\n");
            }

            // Send to discord
            await _discordBotService.SendMessageToChannel(GetMessageToSend());
        }

        /// <summary>
        /// Top 10 cryptos com maior preço e sua variação nas últimas 24 horas
        /// </summary>
        /// <returns></returns>
        public async Task GetTopCryptoChangeMarketCap()
        {
            _resumeText.Append("\n**TOP 10 CRYPTOS COM MAIOR PREÇO E SUA VARIAÇÃO**\n\n");
            var data = await _binanceClient.SpotApi.ExchangeData.GetTickersAsync();
            var top10 = data.Data
                .Where(x => x.Symbol.EndsWith("BRL"))
                .Select(x => new
                {
                    x.Symbol,
                    x.PriceChangePercent,
                    x.LastPrice
                })
                .OrderByDescending(x => x.LastPrice)
                .Take(10)
                .ToList();

            foreach (var crypto in top10)
            {
                _resumeText.Append($"Moeda: {crypto.Symbol}\n");
                _resumeText.Append($"Variação: {Math.Round(crypto.PriceChangePercent), 2}%\n");
                _resumeText.Append($"Ultimo preço: {Math.Round(crypto.LastPrice, 2)} R$\n\n");
            }

            // Send to discord
            await _discordBotService.SendMessageToChannel(GetMessageToSend());
        }

        /// <summary>
        /// Get current price and variation in the last 24 hours
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        private async Task<(decimal, decimal)> GetPriceAndChange(string symbol)
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
        }

        private string GetMessageToSend()
        {
            _resumeText.Append("================");
            return _resumeText.ToString();
        }
    }
}
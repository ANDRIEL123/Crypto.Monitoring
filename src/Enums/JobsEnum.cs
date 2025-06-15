using System.ComponentModel;

namespace Crypto.Monitoring.src.Enums
{
    public enum JobsEnum
    {
        [Description("Faz uma resumo de variação de preço das minhas cryptomoedas")]
        CurrentBalanceMyCryptosJob,

        [Description("Envia as 10 cryptos que tiveram o maior aumento ou queda nas últimas 24 horas")]
        TopCryptoChange,

        [Description("Envia um resumo de variação do top 10 (Capital)")]
        Top10MarketCapCryptoChange
    }
}

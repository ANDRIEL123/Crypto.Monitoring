namespace Crypto.Monitoring.Models
{
    public class CryptoPrices
    {
        public string CryptoName { get; set; }

        public List<CryptoPrice> CryptoPriceVariations { get; set; }

        public int Days { get; set; }
    }

    public class CryptoPrice
    {
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
    }
}
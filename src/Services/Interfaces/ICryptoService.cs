namespace Crypto.Monitoring.src.Services.Interfaces
{
    public interface ICryptoService
    {
        Task<object> GetCurrentCrypto(string symbol = "bitcoin", string currency = "BRL");
    }
}
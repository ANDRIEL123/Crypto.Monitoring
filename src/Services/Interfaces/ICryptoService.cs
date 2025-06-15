namespace Crypto.Monitoring.Services.Interfaces
{
    public interface ICryptoService
    {
        Task ConsultingBalance();

        Task GetTopCryptoChange();
    }
}
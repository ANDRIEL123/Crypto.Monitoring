using Crypto.Monitoring.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace hangfire.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class CryptoController : ControllerBase
    {
        private readonly ICryptoService _cryptoService;

        public CryptoController(ICryptoService cryptoService)
        {
            _cryptoService = cryptoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAccountBalance()
        {
            await _cryptoService.ConsultingBalance();

            return Ok();
        }
    }
}
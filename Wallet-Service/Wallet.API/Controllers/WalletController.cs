using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Wallet.Core.Interfaces;
using Wallet.Dtos;
using Wallet.Utilties;
using Wallet.Utilties.Requests;

namespace Wallet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {

        private readonly UrlConfiguration _walletOptions;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<WalletController> _logger;
        private readonly IMapper _mapper;
        private readonly IWalletServices _walletServices;

        public WalletController(ILogger<WalletController> logger, IMapper mapper, IWalletServices walletServices)
        {
            _mapper = mapper;
            _logger = logger;
            _walletServices = walletServices;


        }


        [HttpPost]
        public async Task<IActionResult> CreateWallet(UserWalletDto userWalletDto)
        {
            var result = await _walletServices.CreateWalletAsync(userWalletDto);
            return StatusCode(result.StatusCode, result);
        }


        [HttpPatch("{userId}/activate")]
        public async Task<IActionResult> ActivateWallet(int userId)
        {
            var result = await _walletServices.ActivateWallet(userId);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPatch("{userId}/deactivate")]

        public async Task<IActionResult> DeactivateWallet(int userId)
        {
            var result = await _walletServices.DeactivateWallet(userId);
            return StatusCode(result.StatusCode, result);
        }


        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserWallet(int userId)
        {
            var result = await _walletServices.GetUserWalletAsync( userId);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("walletTransfer")]
        public async Task<IActionResult> TransferToWallet(WalletTransferDto walletTransferDto)
        {
            var result = await _walletServices.TransferToWallet(walletTransferDto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("walletTransactions/{id:Guid}")]
        public async Task<IActionResult> GetWalletTransactions(int id, [FromQuery]TransactionParameters parameters)
        {
            var result = await _walletServices.GetWalletTransactionsAsync(id, parameters);
            return StatusCode(result.StatusCode, result);
        }
        
    }
}
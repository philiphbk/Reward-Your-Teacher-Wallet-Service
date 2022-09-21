using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Wallet.Core.Interfaces;
using Wallet.Dtos;
using Wallet.Dtos.WithdrawalDtos;

namespace Wallet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WithdrawalController : ControllerBase
    {
        private readonly IPayStackService _payStackService;

        public WithdrawalController(IPayStackService payStackService)
        {
            _payStackService = payStackService;
        }
        [HttpPost]
        public async Task<IActionResult> Get(string amount, int walletId)
        {
            //return Ok(_payStackService.CreateRecipient(request.source, request.amount, request.walletId));
            //await _payStackService.CreateTransferRecipient(request.name, request.account_number, request.bank_code, walletId);
            var transaction = await _payStackService.InitiateTransfer(amount, walletId);
            return StatusCode(transaction.StatusCode, transaction);


        }
    }

}

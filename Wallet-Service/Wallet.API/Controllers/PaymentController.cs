using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Wallet.Core.Interfaces;
using Wallet.Dtos.PaymentDtos;

namespace Wallet.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPayStackService _payStackService;

        public PaymentController(IPayStackService payStackService)
        {
            _payStackService = payStackService;
        }

        [HttpPost("paystack/pay/{userId}")]
        public async Task<IActionResult> PayUsingPayStack(PayStackPaymentDto details, int userId)
        {
            var link = await _payStackService.GetPaymentLink(details, userId);
            return Ok(link);
        }

        [HttpPost("transaction/{id}")]
        public async Task<IActionResult> ConfirmTransaction(string id)
        {
            var result = await _payStackService.ConfirmTransactionByRef(id);
            if (result)
                return Ok(new { Message = "Transaction Successful" });
            else
                return BadRequest(new { Message = "Transaction not successful" });

        }
    }
}

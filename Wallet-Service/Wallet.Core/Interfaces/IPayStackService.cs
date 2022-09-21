using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Dtos;
using Wallet.Dtos.PaymentDtos;

namespace Wallet.Core.Interfaces
{
    public interface IPayStackService
    {
        Task<PayStackLinkResponseDto> GetPaymentLink(PayStackPaymentDto details, int userId);
        Task<bool> ConfirmTransactionById(string transactionId);
        Task<bool> ConfirmTransactionByRef(string txRef);
        Task CreateRecipient(string source, int amount, int walletId);
        Task CreateTransferRecipient(string name, string accountNumber, string bankCode, int walletId);
        Task<ExecutionResponse<UserTransactionDto>> InitiateTransfer(string amount, int walletId);
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Core.Interfaces;
using Wallet.Data;
using Wallet.Dtos;
using Wallet.Dtos.PaymentDtos;
using Wallet.Dtos.WithdrawalDtos;
using Wallet.Model.Enums;
using Wallet.Utilties;
using Wallet.Utilties.Requests;

namespace Wallet.Core.Services
{
    public class PayStackService : IPayStackService
    {
        private readonly IConfiguration _config;
        private readonly IHttpService _httpService;
        private readonly ITransactionService _txService;
        private readonly AppDbContext _db;
        private readonly UrlConfiguration _walletOptions;
        private readonly IMockService _mockService;

        public PayStackService(IConfiguration config, IHttpService httpService,
            IOptionsSnapshot<UrlConfiguration> walletoptions, ITransactionService txService, AppDbContext db, IMockService mockService)
        {
            _config = config;
            _httpService = httpService;
            _txService = txService;
            _db = db;
            _mockService = mockService;
            //_walletOptions = walletoptions.Value;
        }

        public Task<bool> ConfirmTransactionById(string transactionId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ConfirmTransactionByRef(string txRef)
        {
            var tx = await _txService.GetTransactionAsync(txRef);

            if (tx == null) return false;
            if (tx.Status == TransactionStatus.Success) return true;

            var request = new GetRequest();
            request.Url = $"https://api.paystack.co/transaction/verify/{txRef}";
            request.AccessToken = _config["WalletApi:Key"];
            var response = await _httpService.SendGetRequest<PayStackTransactionDto>(request);

            if (response.Status == "true")
            {
                if (response.Data.Status == "success")
                {
                    var wallet = _db.Wallets.FirstOrDefault(x => x.Id.Equals(tx.WalletId));
                    wallet.Balance += Convert.ToInt32(tx.Amount);
                    tx.Status = TransactionStatus.Success;
                    tx.Updated = DateTime.Now;
                    await _db.SaveChangesAsync();
                    return true;
                }
                else
                    return false;
            }
            return false;

        }

        public async Task CreateRecipient(string source, int amount, int walletId)
        {
            var data = new WithdrawalPostRequest()
            {
                Source = source,
                Amount = amount,
            };

            var request = new JsonContentPostRequest<WithdrawalPostRequest>()
            {
                Url = "https://api.paystack.co/transfer",
                Data = data,
                AccessToken = _config["WalletApi:Key"]
            };

            var response = await _httpService.SendPostRequest<PayStackRecipientResponseDto, WithdrawalPostRequest>(request);
            if (response.Status == "true")
            {
                var wallet = _db.Wallets.Where(x => x.Id == walletId).FirstOrDefault();
                wallet.RecipientCode = response.Data.TransferCode;
            }


        }

        public async Task CreateTransferRecipient(string name, string accountNumber, string bankCode, int walletId)
        {
            var data = new PayStackRequestTransferRecipientDto()
            {
                name = name,
                account_number = accountNumber,
                bank_code = bankCode,
            };

            var request = new JsonContentPostRequest<PayStackRequestTransferRecipientDto>()
            {
                Url = "https://api.paystack.co/transferrecipient",
                Data = data,
                AccessToken = _config["WalletApi:Key"]
            };

            var response = await _httpService.SendPostRequest<PayStackResponseTransferRecipientDto, PayStackRequestTransferRecipientDto>(request);
            if (response.status == true)
            {
                var wallet = _db.Wallets.Where(x => x.Id == walletId).FirstOrDefault();
                wallet.RecipientCode = response.data.recipient_code;
                await _db.SaveChangesAsync();
            }

        }

        public async Task<PayStackLinkResponseDto> GetPaymentLink(PayStackPaymentDto details, int userId)
        {
            var request = new JsonContentPostRequest<PayStackPaymentDto>();
            details.Amount = details.Amount + "00";
            request.Data = details;
            request.Url = "https://api.paystack.co/transaction/initialize";
            request.AccessToken = _config["WalletApi:Key"];

            var response = await _httpService.SendPostRequest<PayStackLinkResponseDto, PayStackPaymentDto>(request);
            if (response.Status == "true")
                await _txService.CreateFundingTransactionAsync(response.Data.Reference, details.Amount, userId);

            return response;
        }

        public async Task<ExecutionResponse<UserTransactionDto>> InitiateTransfer(string amount, int walletId)
        {
            var wallet = _db.Wallets.Where(x => x.Id == walletId).FirstOrDefault();
            var actualAmount = Convert.ToDouble(amount + "00");
            if (wallet.Balance < actualAmount)
            {
                return new ExecutionResponse<UserTransactionDto>()
                {
                    Status = false,
                    Message = "Insufficient balance",
                    StatusCode = 400,
                    Data = null
                };
            }
            var data = new PayStackRequestInitiateTransferDto()
            {
                amount = Convert.ToInt32(amount + "00"),
                recipient = wallet.RecipientCode
            };


            var request = new JsonContentPostRequest<PayStackRequestInitiateTransferDto>()
            {
                Url = "https://api.paystack.co/transfer",
                Data = data,
                AccessToken = _config["WalletApi:Key"]

            };

            var transaction = new UserTransactionDto()
            {
                Amount = amount + "00",
                WalletId = wallet.Id,
                Status = TransactionStatus.Pending,
                Created = DateTime.Now,
                Updated = DateTime.Now
            };

            var response = _mockService.SendPostRequest(request);
            if (response.status == true)
            {
                transaction.Status = TransactionStatus.Success;
                wallet.Balance -= Convert.ToDouble(amount + "00");
                wallet.TransferCode = response.data.transfer_code;
                await _db.SaveChangesAsync();
                return new ExecutionResponse<UserTransactionDto>()
                {
                    Status = true,
                    Message = "Transfer done",
                    StatusCode = 200,
                    Data = transaction
                };
            }
            else
            {
                transaction.Status = TransactionStatus.Failed;
                return new ExecutionResponse<UserTransactionDto>()
                {
                    Status = false,
                    Message = "Transfer failed",
                    StatusCode = 400,
                    Data = transaction
                };
            }

        }
    }
}

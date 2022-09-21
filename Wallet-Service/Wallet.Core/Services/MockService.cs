using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Wallet.Core.Interfaces;
using Wallet.Dtos.WithdrawalDtos;
using Wallet.Utilties.Requests;

namespace Wallet.Core.Services
{
    public class MockService : IMockService
    {


        public PayStackResponseInitiateTransferDto SendPostRequest(JsonContentPostRequest<PayStackRequestInitiateTransferDto> request)
        {
            return new PayStackResponseInitiateTransferDto()
            {
                status = true,
                message = "Transfer completed",
                data = new PayStackReponseInitiateTransferDataDto()
                {
                    amount = request.Data.amount,
                    currency = "NGN",
                    recipient = request.Data.recipient,
                    status = "success",
                    transfer_code = new Guid().ToString(),
                    createdAt = DateTime.Now,
                    updatedAt = DateTime.Now



                }
            };
        }
    }
}

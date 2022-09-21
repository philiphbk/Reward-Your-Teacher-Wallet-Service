using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Dtos.WithdrawalDtos;
using Wallet.Utilties.Requests;

namespace Wallet.Core.Interfaces
{
    public interface IMockService
    {
        PayStackResponseInitiateTransferDto SendPostRequest(JsonContentPostRequest<PayStackRequestInitiateTransferDto> request);

    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Dtos;
using Wallet.Model;
using Wallet.Utilties.Requests;

namespace Wallet.Core.Interfaces
{

    public interface IWalletServices
    {
        Task<ExecutionResponse<UserWalletDto>> CreateWalletAsync(UserWalletDto userWallet);
        Task<ExecutionResponse<UserWalletUpdateDto>> ActivateWallet(int userId);
        Task<ExecutionResponse<UserWalletUpdateDto>> DeactivateWallet(int userId);
        Task<ExecutionResponse<UserWalletDto>> GetUserWalletAsync(int userId);
        Task<ExecutionResponse<UserTransactionDto>> TransferToWallet(WalletTransferDto walletTransferDto);
        Task<PagedExecutionResponse<IEnumerable<UserTransactionDto>>> GetWalletTransactionsAsync
            (int id, TransactionParameters parameters);





    }





	
}

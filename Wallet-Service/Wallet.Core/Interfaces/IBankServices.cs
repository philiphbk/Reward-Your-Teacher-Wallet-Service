using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Dtos;
using Wallet.Model;

namespace Wallet.Core.Interfaces
{
    public interface IBankServices
    {
        Task<ExecutionResponse<UserBankDto>> CreateBankAsync(UserBankDto accountDetailDto);
        Task<ExecutionResponse<UserBank>> DeleteBankAsync(int accountId, int userId);
        Task<ExecutionResponse<UserBankDto>> GetBankAsync(int userId);
        Task<ExecutionResponse<IEnumerable<BankModelDto>>> SeedBanks();
        Task<ExecutionResponse<IEnumerable<BankModelDto>>> GetBanks();

    }
}

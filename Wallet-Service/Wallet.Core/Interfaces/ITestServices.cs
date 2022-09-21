using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Dtos;

namespace Wallet.Core.Interfaces
{
    public interface ITestServices
    {
        Task<ExecutionResponse<String>> AddTest(string test);
        Task<PagedExecutionResponse<IEnumerable<String>>> ALlTest();
        Task<ExecutionResponse<BankDto>> GetBanksAsync();
    }
}

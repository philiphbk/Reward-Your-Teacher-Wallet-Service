using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Core.Interfaces;
using Wallet.Core.Repository;
using Wallet.Dtos;
using Wallet.Dtos.Pagination;

namespace Wallet.Core.Services
{
    public class TestServices : ITestServices
    {
        private IResponseFactory _responseService;
        private IHttpGenericFactory _httpGenericFactory;

        public TestServices(IResponseFactory responseService, IHttpGenericFactory httpGenericFactory)
        {
            _responseService = responseService;
            _httpGenericFactory = httpGenericFactory;
        }
        public async Task<ExecutionResponse<String>> AddTest(string test)
        {
            return _responseService.ExecutionResponse<String>($"Test added {test}", null);
        }

        public async Task<PagedExecutionResponse<IEnumerable<String>>> ALlTest()
        {
            var newList = new List<string>();

            for (int i = 0; i < 10; i++)
            {
                newList.Add($"string {i}");
            }
            PagedList<string> pagedTransactions = PagedList<string>.ToPagedList(newList.AsQueryable(), 1, 10);

            return _responseService.PagedExecutionResponse<IEnumerable<string>>("Successfully retrieved test", newList, 10, true);
        }

        public async Task<ExecutionResponse<BankDto>> GetBanksAsync()
        {
            var getBanks = await _httpGenericFactory.Get(new HttpGetOrDelete()
            {
                BaseUrl = "https://wema-alatdev-apimgt.azure-api.net/alat-test/api/Shared/GetAllBanks"
            }, "");

            var response = JsonConvert.DeserializeObject<BankDto>(getBanks.Item2);

            return _responseService.ExecutionResponse<BankDto>($"banks", response);
        }
    }
}

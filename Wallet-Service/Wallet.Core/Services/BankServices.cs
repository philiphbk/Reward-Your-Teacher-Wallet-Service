using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Core.Interfaces;
using Wallet.Core.Repository;
using Wallet.Data;
using Wallet.Dtos;
using Wallet.Model;

namespace Wallet.Core.Services
{
    public class BankServices : IBankServices
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _Db;
        private IResponseFactory _responseService;
        private readonly ITestServices _testServices;

        public BankServices(IMapper mapper,
                                      AppDbContext Db, IResponseFactory responseFactory, ITestServices testServices)
        {
            _mapper = mapper;
            _Db = Db;
            _responseService = responseFactory;
            _testServices = testServices;
        }
        public async Task<ExecutionResponse<UserBankDto>> CreateBankAsync(UserBankDto userBankDto)
        {
            var user = _Db.Wallets.Where(x => x.UserId == userBankDto.UserId).Any();
            if (!user)
            {
                return _responseService.ExecutionResponse<UserBankDto>("Wallet not found", null, false, 400);
            }


            var account = _mapper.Map<UserBank>(userBankDto);


            _Db.Add(account);
            await _Db.SaveChangesAsync();
            var accountToReturn = _mapper.Map<UserBankDto>(account);

            return _responseService.ExecutionResponse<UserBankDto>("Bank Account created", accountToReturn, true, 200);
        }

        public async Task<ExecutionResponse<UserBank>> DeleteBankAsync(int accountId, int userId)
        {
            var account = _Db.UserBanks.FirstOrDefault(x => x.UserId == userId && x.Id == accountId);
            if (account == null)
            {
                return _responseService.ExecutionResponse<UserBank>("Bank account details does not exist", null, false, 400);
            }

            _Db.Remove(account);
            await _Db.SaveChangesAsync();
            return _responseService.ExecutionResponse<UserBank>("Bank account detail deleted", account, true, 200);

        }

        public async Task<ExecutionResponse<UserBankDto>> GetBankAsync(int userId)
        {
            var account = _Db.UserBanks.FirstOrDefault(x => x.Id == userId);
            if (account == null)
            {
                return _responseService.ExecutionResponse<UserBankDto>("Bank account details does not exist", null, false, 400);
            }
            var accountToReturn = _mapper.Map<UserBankDto>(account);
            return _responseService.ExecutionResponse<UserBankDto>("Bank account detail found", accountToReturn, true, 200);
        }

        public async Task<ExecutionResponse<IEnumerable<BankModelDto>>> SeedBanks()
        {
            var banks = await _testServices.GetBanksAsync();
            var bankModel = banks.Data.result;
            var bankToReturn = _mapper.Map<IEnumerable<Bank>>(bankModel);


            await _Db.Banks.AddRangeAsync(bankToReturn);
            await _Db.SaveChangesAsync();
            return _responseService.ExecutionResponse<IEnumerable<BankModelDto>>("Bank seeded", bankModel, true, 200);
        }

        private int GetAccountId(int accountId) => _Db.UserBanks.FirstOrDefault(x => x.Id == accountId).Id;

        public async Task<ExecutionResponse<IEnumerable<BankModelDto>>> GetBanks()
        {
            var banks = _Db.Banks.ToList();
            if (!banks.Any())
            {
                var bankModelDto = await SeedBanks();
                return _responseService.ExecutionResponse<IEnumerable<BankModelDto>>("Bank seeded", bankModelDto.Data, true, 200);
            }
            var bankToReturn = _mapper.Map<IEnumerable<BankModelDto>>(banks);
            return _responseService.ExecutionResponse<IEnumerable<BankModelDto>>("Bank list already exists", bankToReturn, true, 200);
        }
    }
}

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Core.Interfaces;
using Wallet.Data;
using Wallet.Dtos;
using Wallet.Model;

namespace Wallet.Core.Repository
{
    public class WalletRepository : IWalletRepository
    {

        private readonly AppDbContext _Db;
        private IMapper _mapper;

        public WalletRepository()
        {
        }

        public WalletRepository(AppDbContext Db,
                                IMapper mapper)
        {
            _Db = Db;
            _mapper = mapper;
        }

        public async Task<UserWalletDto> Create(UserWalletDto userWalletDto)
        {
            var userWallet = _mapper.Map<UserWalletDto, UserWallet>(userWalletDto);
            _Db.Wallets.Add(userWallet);
            await _Db.SaveChangesAsync();

            return _mapper.Map<UserWallet, UserWalletDto>(userWallet);

        }

        public Task CreateWalletAsync(UserWalletDto userWalletDto)
        {
            throw new NotImplementedException();
        }


    }
}

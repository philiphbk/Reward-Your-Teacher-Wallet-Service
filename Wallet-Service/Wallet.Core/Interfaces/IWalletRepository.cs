using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Dtos;

namespace Wallet.Core.Interfaces
{
    public interface IWalletRepository
    {


        Task CreateWalletAsync(UserWalletDto userWalletDto);

    }
}

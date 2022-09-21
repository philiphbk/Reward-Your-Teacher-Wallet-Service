using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Dtos
{
    public record WalletTransferDto(int WalletId,
            int SenderOrReceiverWalletId, string Amount, string Description);
}

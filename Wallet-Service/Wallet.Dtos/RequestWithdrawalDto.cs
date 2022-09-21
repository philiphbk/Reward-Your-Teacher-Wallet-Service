using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Dtos
{
    public class RequestWithdrawalDto
    {
        public string source { get; set; }
        public int amount { get; set; }
        public int walletId { get; set; }
    }
}

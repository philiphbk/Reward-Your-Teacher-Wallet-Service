using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Dtos.WithdrawalDtos
{
    public class PayStackRequestInitiateTransferDto
    {
        public string source = "balance";
        public int amount { get; set; }

        public string recipient { get; set; }
    }
}

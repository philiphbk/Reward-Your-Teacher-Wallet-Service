using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Utilties
{
    public class WithdrawalPostRequest
    {
        public string Source { get; set; }
        public int Amount { get; set; }
        public string RecipientCode { get; set; }

    }
}

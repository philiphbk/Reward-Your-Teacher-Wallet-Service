using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Dtos.WithdrawalDtos
{
    public class PayStackResponseTransferRecipientDto
    {
        public bool status { get; set; }
        public string message { get; set; }
        public PayStackResponseTransferRecipientDataDto data { get; set; }
    }
}

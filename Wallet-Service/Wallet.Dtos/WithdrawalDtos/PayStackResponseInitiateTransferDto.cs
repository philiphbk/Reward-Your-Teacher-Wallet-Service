using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Dtos.WithdrawalDtos
{
    public class PayStackResponseInitiateTransferDto
    {
        public bool status { get; set; }
        public string message { get; set; }
        public PayStackReponseInitiateTransferDataDto data { get; set; }
    }
}

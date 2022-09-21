using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Dtos.WithdrawalDtos
{
    public class PayStackRequestTransferRecipientDto
    {
        public string type = "nuban";
        public string name { get; set; }

        public string account_number { get; set; }


        public string bank_code { get; set; }
        public string currency = "NGN";
    }
}

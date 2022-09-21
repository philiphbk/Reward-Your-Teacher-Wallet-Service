using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Dtos.WithdrawalDtos
{
    public class PayStackReponseInitiateTransferDataDto
    {


        public int amount { get; set; }
        public string currency { get; set; }
        public string source = "balance";
        public string recipient { get; set; }
        public string status { get; set; }
        public string transfer_code { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
}

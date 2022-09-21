using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Dtos.PaymentDtos
{
    public class PayStackTransactionDto
    {
        public string Status { get; set; }
        public PayStackTransactionDataDto Data { get; set; }
        public string Message { get; set; }

    }
}
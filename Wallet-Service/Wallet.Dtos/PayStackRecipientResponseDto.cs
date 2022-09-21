using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Dtos
{
    public class PayStackRecipientResponseDto
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public PayStackRecipientDataResponseDto Data { get; set; }
    }
}

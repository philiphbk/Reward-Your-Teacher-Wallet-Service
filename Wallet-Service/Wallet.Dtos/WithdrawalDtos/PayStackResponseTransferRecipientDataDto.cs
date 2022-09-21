﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Dtos.WithdrawalDtos
{
    public class PayStackResponseTransferRecipientDataDto
    {
        public bool active { get; set; }
        public DateTime createdAt { get; set; }
        public string currency { get; set; }
        public string domain { get; set; }
        public int id { get; set; }
        public int integration { get; set; }
        public string name { get; set; }
        public string recipient_code { get; set; }
        public string type { get; set; }
        public DateTime updatedAt { get; set; }
        public bool is_deleted { get; set; }
        public PayStackResponseTransferRecipientDetailsDto details { get; set; }
    }
}

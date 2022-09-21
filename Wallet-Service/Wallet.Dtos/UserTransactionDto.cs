using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Model;
using Wallet.Model.Enums;

namespace Wallet.Dtos
{
    public class UserTransactionDto
    {
        public int Id { get; set; }
        public TransactionStatus Status { get; set; } = TransactionStatus.Pending;
        public TransactionType Type { get; set; } = TransactionType.Funding;
        public string Amount { get; set; }
        public int WalletId { get; set; }
#nullable enable
        public string? TransactionReference { get; set; }
        public string? Description { get; set; }
        public int? SenderOrReceiverWalletId { get; set; }
        public Bank? UserBank { get; set; }
#nullable disable
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;
        
    }
}

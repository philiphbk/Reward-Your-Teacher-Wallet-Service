using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Model
{
    public class UserBank
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; }
        [Required]
        public int? BankId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string AccountName { get; set; }

        public string AccountNumber { get; set; }
        public int? WalletId { get; set; }
        [ForeignKey("WalletId")]
        public virtual UserWallet Wallet { get; set; }

    }
}

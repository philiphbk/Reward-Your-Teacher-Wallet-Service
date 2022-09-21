using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Model
{
    public class UserWallet
    {
        public UserWallet()
        {
            // Transactions = new HashSet<UserTransaction>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? BankId { get; set; }
        public int UserId { get; set; }
        public double Balance { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public string Currency { get; set; }
        public bool Status { get; set; }
        public ICollection<UserTransaction> Transactions { get; set; }
        [ForeignKey("BankId")]

        public virtual Bank Bank { get; set; }
        public string RecipientCode { get; set; }
        public string TransferCode { get; set; }

    }
}

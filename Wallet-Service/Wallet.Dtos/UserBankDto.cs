using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Model;

namespace Wallet.Dtos
{
    public class UserBankDto
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AccountNumber { get; set; }
        public int BankId { get; set; }
        public int UserId { get; set; }

    }
}

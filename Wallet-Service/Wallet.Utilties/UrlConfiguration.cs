using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Utilties
{
    public class UrlConfiguration
    {
        //list of configurations to be used
        public const string WalletApi = "WalletApi";

        [Required]
        public string Url { get; set; }
        [Required]
        public string Key { get; set; }
        [Range(10, 100)]
        public int Count { get; set; }
    }


}

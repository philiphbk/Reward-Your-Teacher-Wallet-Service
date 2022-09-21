using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Dtos.PaymentDtos
{
    public class PayStackLinqDataDto
    {
        [JsonProperty(PropertyName = "authorization_url")]
        public string AuthorizationUrl { get; set; }
        [JsonProperty(PropertyName = "access_code")]
        public string AccessCode { get; set; }
        public string Reference { get; set; }
    }
}

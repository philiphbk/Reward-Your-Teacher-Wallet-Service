using Newtonsoft.Json;
using System;

namespace Wallet.Dtos
{
    public class PayStackRecipientDataResponseDto
    {
        public int Integration { get; set; }
        public string Domain { get; set; }
        public int Amount { get; set; }
        public string Currency { get; set; }
        public string Source { get; set; }
        public string Reason { get; set; }
        public int Recipient { get; set; }
        public string Status { get; set; }
        [JsonProperty(PropertyName = "transfer_code")]
        public string TransferCode { get; set; }
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
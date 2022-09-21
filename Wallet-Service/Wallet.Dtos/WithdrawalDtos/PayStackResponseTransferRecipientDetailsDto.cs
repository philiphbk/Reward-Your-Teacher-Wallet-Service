namespace Wallet.Dtos.WithdrawalDtos
{
    public class PayStackResponseTransferRecipientDetailsDto
    {
        public object authorization_code { get; set; }
        public string account_number { get; set; }
        public string account_name { get; set; }
        public string bank_code { get; set; }
        public string bank_name { get; set; }
    }
}
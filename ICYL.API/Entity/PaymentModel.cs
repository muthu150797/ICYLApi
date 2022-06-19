namespace ICYL.API.Entity
{
    public class PaymentModel
    {
        public string ApiLoginID { get; set; } = "";
        public string ApiTransactionKey { get; set; } = "";
        public decimal Amount { get; set; } = 0;
        public int BatchId { get; set; }
        public string settlementState { get; set; } 
        public int PaymentConfigId { get; set; } 
        public decimal AmtDonation { get; set; } 
        public decimal AmtTransactionPaid { get; set; }
        public byte IsCreditCard { get; set; } 
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public string Email { get; set; }
        public int lkpDonationCategory { get; set; }
        public decimal AmtTransaction { get; set; } 
        public string TransId { get; set; }
        public string TransDescription { get; set; }
        public int? RetVal { get; set; }
        public string? RetDesc { get; set; }

    }
    public class TransactionResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        //public AuthorizeNet.Api.Contracts.V1.messageTypeEnum resultCode { get; set; }
        //public string transId { get; set; }
        //public string responseCode { get; set; }
        //public string messageCode { get; set; }
        //public string authCode { get; set; }
        //public string description { get; set; }
        //public string errorCode { get; set; }
        //public string errorText { get; set; }
    }
}

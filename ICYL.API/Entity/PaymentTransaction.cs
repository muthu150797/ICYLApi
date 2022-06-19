using AuthorizeNet.Api.Contracts.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICYL.API.Entity
{
    public class PaymentTransaction
    {
        public PaymentTransaction()
        {
            this.PaymentConfigs = new PaymentConfig();
        }
        //public createTransactionRequest response { get; set; }
        public string TransactionToken { get; set; }
        public int TransactionId { get; set; }
        public decimal AmtTransaction { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int PaymentConfigId { get; set; }
        public string TransId { get; set; }
        public string TransResponseCode { get; set; }
        public string TransMessageCode { get; set; }
        public string TransDescription { get; set; }
        public string TransAuthCode { get; set; }
        public string TransErrorCode { get; set; }
        public string TransErrorText { get; set; }
        public Nullable<int> BatchId { get; set; }
        public string CustomerProfileId { get; set; }
        public string CustomerAddressId { get; set; }
        public string DonationCategory { get; set; }
        public virtual PaymentConfig PaymentConfigs { get; set; }

}


    public class SubscriptionTransaction
    {
        public string SubscriptionTransId { get; set; }
        public string SubscriptionResponseCode { get; set; }
        public string SubscriptionResponseText { get; set; }
        public int PaymentConfigID { get; set; }
    }
}

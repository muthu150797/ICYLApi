using ICYL.BL.CustomValidator;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Web.Mvc;

namespace ICYL.API.Entity
{


    public class PaymentConfig
    {
        private bool _isIndefinite = false;
        //public PaymentConfig()
        //{
        //    this.lstCategory = new HashSet<SelectListItem>();
        //    this.lstRecurringType = new HashSet<SelectListItem>();
        //}

        public int PaymentConfigId { get; set; }
        public decimal? AmtDonation { get; set; }
        public bool IsTransactionFeesIncluded { get; set; }
        public decimal? AmtTransactionPaid { get; set; }
        public decimal AmtTotal { get; set; }
        public bool IsRecurring { get; set; }
        public int RecurringType { get; set; }
        public string? RecurringUnits { get; set; }
        public int PaymentType { get; set; }
        public string? dtPaymentStart { get; set; }
        public string? dtPaymentEnd { get; set; }
        public bool isIndefinite { get { return _isIndefinite; } set { _isIndefinite = value; } }
        public int? PaymentMaxOccurences { get; set; }
        public int PaymentEndType { get; set; }
        public bool IsCreditCard { get; set; }
        public Nullable<bool> IsECheck { get; set; }
        public Nullable<bool> IsMailInCheck { get; set; }
        public bool IsAnonymous { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FirstNameSecond { get; set; }
        public string? LastNameSecond { get; set; }
        public string? CompanyName { get; set; }
        public string? EmailId { get; set; }
        public string? Comments { get; set; }
        public string PhoneNumber { get; set; }
        public int lkpDonationCategory { get; set; }
        //[Range(0, int.MaxValue, ErrorMessage = "Please enter a valid Street Number")]
        public string? BillingAddressLine1 { get; set; }
        //[Required(ErrorMessage = "Please Enter Billing info Street Name")]
        public string? BillingAddressLine2 { get; set; }
        public string? BillingCity { get; set; }
        public string? BillingState { get; set; }
        public string? BillingZip { get; set; }
        public string? BillingCountry { get; set; }
        public string? CCNum { get; set; }
        public string? CCCvc { get; set; }
        public string? CCHolderName { get; set; }
        public string? CCExpiry { get; set; }
        public string? BankRoutingNum { get; set; }
        public string? BankAccountNum { get; set; }
        public string? BankNameOnAccount { get; set; }
        public dynamic? BankAccountType { get; set; }
        public string? CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }

        public string? SubscriptionTransId { get; set; }
        public string? ConfirmationNumber { get; set; }
        public string? SubscriptionResponseCode { get; set; }
        public string? SubscriptionResponseText { get; set; }

        //[Range(0, int.MaxValue, ErrorMessage = "Please enter a valid Street Number")]
        public string? MailingAddressLine1 { get; set; }
        //[Required(ErrorMessage = "Please Enter MailingInfo Street Name")]
        public string? MailingAddressLine2 { get; set; }
        public string? MailingCity { get; set; }
        public string? MailingState { get; set; }
        public string? MailingZip { get; set; }
        public string? MailingCountry { get; set; }
        public Nullable<bool> isDownloaded { get; set; }
        public string? RecurringInterval { get; set; }
        public string? CheckNumber { get; set; }
       // public IEnumerable<SelectListItem>? lstCategory { get; set; }
      //  public IEnumerable<SelectListItem>? lstRecurringType { get; set; }

        public string? Address
        {
            get
            {
                return string.Format("{0} {1}"
                            , BillingAddressLine1
                            , BillingAddressLine2
                            );
            }

        }
        public string? RecurringDetails
        {
            get
            {
                return string.Format("{0} {1}"
                            , RecurringType
                            , RecurringInterval
                            );
            }

        }

        public string? MailingAddress
        {
            get
            {
                return string.Format("{0} {1}"
                            , MailingAddressLine1
                            , MailingAddressLine2
                            );
            }

        }
        public string? FullName
        {
            get
            {
                return string.Format("{0}, {1}"
                            , LastName
                            , FirstName
                            );
            }

        }


        public string? FullMailingAddress
        {
            get
            {
                return string.Format("{0} {1} {2},{3},{4}", (string)(MailingAddressLine1)
                            , (string)(MailingAddressLine1)
                            , (string)(MailingCity)
                            , (string)(MailingState)
                            , (string)(MailingZip)
                            ).Replace("  ", " ");
            }
        }

        public string? FullBillingAddress
        {
            get
            {
                return string.Format("{0} {1} {2},{3},{4}", (string)(BillingAddressLine1)
                            , (string)(BillingAddressLine2)
                            , (string)(BillingCity)
                            , (string)(BillingState)
                            , (string)(BillingZip)
                            ).Replace("  ", " ");
            }
        }
    }
}

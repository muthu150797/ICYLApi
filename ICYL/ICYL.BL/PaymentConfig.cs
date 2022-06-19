using EnterpriseLayer.Utilities;
using ICYL.BL.CustomValidator;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ICYL.BL
{


   public class PaymentConfig
    {
        private bool _isIndefinite = false;
        public PaymentConfig()
        {
            this.lstCategory = new HashSet<SelectListItem>();
            this.lstRecurringType = new HashSet<SelectListItem>();
        }

        public int PaymentConfigId { get; set; }
        [Required(ErrorMessage ="Please Enter Donation amount")]
        [Range(1.0, Double.MaxValue, ErrorMessage = "Please Enter Donation amount")]
        public decimal AmtDonation { get; set; }
        public bool IsTransactionFeesIncluded { get; set; }
        public decimal AmtTransactionPaid { get; set; }
        public decimal AmtTotal { get; set; }
        public bool IsRecurring { get; set; }
        public string RecurringType { get; set; }
        public string RecurringUnits { get; set; }
        public string PaymentType { get; set; }
        [Required(ErrorMessage = "Please select Payment Start Date")]
        public DateTime? dtPaymentStart { get; set; }
        public DateTime? dtPaymentEnd { get; set; }
        public bool isIndefinite { get { return _isIndefinite; } set { _isIndefinite = value; } }
        public int? PaymentMaxOccurences { get; set; }
        public string PaymentEndType { get; set; }
        public Nullable<bool> IsCreditCard { get; set; }
        public Nullable<bool> IsECheck { get; set; }
        public Nullable<bool> IsMailInCheck { get; set; }
        public bool IsAnonymous { get; set; }
        [Required(ErrorMessage = "Please Enter First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please Enter Last Name")]
        public string LastName { get; set; }
        public string FirstNameSecond { get; set; }
        public string LastNameSecond { get; set; }
        public string CompanyName { get; set; }
        [Required(ErrorMessage = "Please Enter Email")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string EmailId { get; set; }
        public string Comments { get; set; }
        public string PhoneNumber { get; set; }
        [Display(Name = "Donation Category")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a category")]
        public int lkpDonationCategory { get; set; }
        [Display(Name = "Street Address")]
        [Required(ErrorMessage = "Please Enter Billing info Street Number/Street Name")]
        //[Range(0, int.MaxValue, ErrorMessage = "Please enter a valid Street Number")]
        public string BillingAddressLine1 { get; set; }
        [Display(Name = "Address 2")]
        //[Required(ErrorMessage = "Please Enter Billing info Street Name")]
        public string BillingAddressLine2 { get; set; }
        [Display(Name = "City")]
        [Required(ErrorMessage = "Please Enter Billing info City")]
        public string BillingCity { get; set; }
        [Display(Name = "State")]
        [Required(ErrorMessage = "Please Enter Billing info State")]
        public string BillingState { get; set; }
        [Display(Name = "Zip")]
        [Required(ErrorMessage = "Please Enter Billing info Zip")]
        public string BillingZip { get; set; }
        public string BillingCountry { get; set; }
        [Display(Name = "Credit Card Number")]
        [ValidateCC(ErrorMessage ="Please Enter Credit Card Number")]
        [MinLength(12,ErrorMessage ="Enter a valid Credit Card Number")]
        [MaxLength(16, ErrorMessage = "Enter a valid Credit Card Number")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Enter a valid Credit Card Number - No spaces are allowed")]
        public string CCNum { get; set; }
        [Display(Name = "Credit Card CVC")]
        [Range(0, 9999, ErrorMessage = "Enter a valid CVC")]
        [ValidateCC(ErrorMessage = "Please Enter CVC")]
        public string CCCvc { get; set; }
        [ValidateCC(ErrorMessage = "Please Enter Card Holder Name")]
        [Display(Name = "Credit Card Holder Name")]
        public string CCHolderName { get; set; }
        [ValidateCC(ErrorMessage = "Please Enter Expiry Date")]
        [Display(Name = "Credit Card Expiry Date")]
        public string CCExpiry { get; set; }
        [Display(Name = "Bank Routing Number")]
        [ValidateCheck(ErrorMessage = "Please Enter Bank Routing Number")]
        public string BankRoutingNum { get; set; }
        [ValidateCheck(ErrorMessage = "Please Enter Bank Account Number")]
        public string BankAccountNum { get; set; }
        [Display(Name = "Name on the Account")]
        public string BankNameOnAccount { get; set; }
        [ValidateCheck(ErrorMessage = "Please enter Name")]
        [Display(Name = "Account Type")]
        public string BankAccountType { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }

        public string SubscriptionTransId { get; set; }
        public string ConfirmationNumber { get; set; }
        public string SubscriptionResponseCode { get; set; }
        public string SubscriptionResponseText { get; set; }

        [Display(Name = "Street Address")]
        [Required(ErrorMessage = "Please Enter Street Number/Street Name")]
        //[Range(0, int.MaxValue, ErrorMessage = "Please enter a valid Street Number")]
        public string MailingAddressLine1 { get; set; }
        [Display(Name = "Address 2")]
        //[Required(ErrorMessage = "Please Enter MailingInfo Street Name")]
        public string MailingAddressLine2 { get; set; }
        [Display(Name = "City")]
        [Required(ErrorMessage = "Please Enter MailingInfo City")]
        public string MailingCity { get; set; }
        [Display(Name = "State")]
        [Required(ErrorMessage = "Please Enter MailingInfo State")]
        public string MailingState { get; set; }
        public string MailingZip { get; set; }
        public string MailingCountry { get; set; }
        public Nullable<bool> isDownloaded { get; set; }
        public string RecurringInterval { get; set; }
        public string CheckNumber { get; set; }
        public IEnumerable<SelectListItem> lstCategory { get; set; }
        public IEnumerable<SelectListItem> lstRecurringType { get; set; }

        public string Address
        {
            get
            {
                return string.Format("{0} {1}"
                            , BillingAddressLine1
                            , BillingAddressLine2
                            );
            }

        }
        public string RecurringDetails
        {
            get
            {
                return string.Format("{0} {1}"
                            , RecurringType
                            , RecurringInterval
                            );
            }

        }

        public string MailingAddress
        {
            get
            {
                return string.Format("{0} {1}"
                            , MailingAddressLine1
                            , MailingAddressLine2
                            );
            }

        }
        public string FullName
        {
            get
            {
                return string.Format("{0}, {1}"
                            , LastName
                            , FirstName
                            );
            }

        }


        public string FullMailingAddress
        {
            get
            {
                return string.Format("{0} {1} {2},{3},{4}", Conversion.ConversionToString(MailingAddressLine1)
                            , Conversion.ConversionToString(MailingAddressLine1)
                            , Conversion.ConversionToString(MailingCity)
                            , Conversion.ConversionToString(MailingState)
                            , Conversion.ConversionToString(MailingZip)
                            ).Replace("  ", " ");
            }
        }

        public string FullBillingAddress
        {
            get
            {
                return string.Format("{0} {1} {2},{3},{4}", Conversion.ConversionToString(BillingAddressLine1)
                            , Conversion.ConversionToString(BillingAddressLine2)
                            , Conversion.ConversionToString(BillingCity)
                            , Conversion.ConversionToString(BillingState)
                            , Conversion.ConversionToString(BillingZip)
                            ).Replace("  ", " ");
            }
        }
    }
}

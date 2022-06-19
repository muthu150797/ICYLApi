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


    public class PaymentConfigUser
    {
        public int PaymentConfigId { get; set; }
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
        [Display(Name = "Street Address")]
        [Required(ErrorMessage = "Please Enter Street Address")]
        public string BillingAddressLine1 { get; set; }
        [Display(Name = "Address 2")]
        public string BillingAddressLine2 { get; set; }
        [Display(Name = "City")]
        [Required(ErrorMessage = "Please Enter City")]
        public string BillingCity { get; set; }
        [Display(Name = "State")]
        [Required(ErrorMessage = "Please Enter State")]
        public string BillingState { get; set; }
        [Display(Name = "Zip")]
        [Required(ErrorMessage = "Please Enter Zip")]
        public string BillingZip { get; set; }
        public string BillingCountry { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }

        [Display(Name = "Street Address")]
        [Required(ErrorMessage = "Please Enter Street Number")]
        public string MailingAddressLine1 { get; set; }
        [Display(Name = "Address 2")]
        public string MailingAddressLine2 { get; set; }
        [Display(Name = "City")]
        [Required(ErrorMessage = "Please Enter City")]
        public string MailingCity { get; set; }
        [Display(Name = "State")]
        [Required(ErrorMessage = "Please Enter State")]
        public string MailingState { get; set; }
        public string MailingZip { get; set; }
        public string CheckNumber { get; set; }
        public string MailingCountry { get; set; }

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
    }


    public class PaymentConfigUserandPayment
    {
        public PaymentConfigUserandPayment()
        {
            this.lstCategory = new HashSet<SelectListItem>();
        }
        public int PaymentConfigId { get; set; }
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
        [Display(Name = "Street Number")]
        [Required(ErrorMessage = "Please Enter Street Address")]
        public string BillingAddressLine1 { get; set; }
        [Display(Name = "Address 2")]
        public string BillingAddressLine2 { get; set; }
        [Display(Name = "City")]
        [Required(ErrorMessage = "Please Enter City")]
        public string BillingCity { get; set; }
        [Display(Name = "State")]
        [Required(ErrorMessage = "Please Enter State")]
        public string BillingState { get; set; }
        [Display(Name = "Zip")]
        [Required(ErrorMessage = "Please Enter Zip")]
        public string BillingZip { get; set; }
        public string BillingCountry { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }

        [Display(Name = "Street Address")]
        [Required(ErrorMessage = "Please Enter Street Address")]
        public string MailingAddressLine1 { get; set; }
        [Display(Name = "Address 2")]
        public string MailingAddressLine2 { get; set; }
        [Display(Name = "City")]
        [Required(ErrorMessage = "Please Enter City")]
        public string MailingCity { get; set; }
        [Display(Name = "State")]
        [Required(ErrorMessage = "Please Enter State")]
        public string MailingState { get; set; }
        public string MailingZip { get; set; }
        public string CheckNumber { get; set; }
        public string MailingCountry { get; set; }

        [Required(ErrorMessage = "Please Enter Donation amount")]
        [Range(1.0, Double.MaxValue, ErrorMessage = "Please Enter Donation amount")]
        public decimal AmtDonation { get; set; }

        [Display(Name = "Donation Category")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a category")]
        public int lkpDonationCategory { get; set; }
        public string PaymentType { get; set; }

        public IEnumerable<SelectListItem> lstCategory { get; set; }
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
    }
}
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICYL.API.Entity
{
    public class DonorSearch
    {
        public DonorSearch()
        {
            this.lstDonationType = new HashSet<SelectListItem>();
            this.lstCategory = new HashSet<SelectListItem>();
            this.lstPaymentType = new HashSet<SelectListItem>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FirstNameSecond { get; set; }
        public string LastNameSecond { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string DonationTypeId { get; set; }
        public string PaymentType { get; set; }
        public int? lkpDonationCategory { get; set; }
        public IEnumerable<SelectListItem> lstDonationType { get; set; }
        public IEnumerable<SelectListItem> lstCategory { get; set; }
        public IEnumerable<SelectListItem> lstPaymentType { get; set; }

        //public List<ICYL.Entity.PaymentConfig> SearchResult { get; set; }
        public List<DonorSearchResult> SearchResult { get; set; }
    }

    public class DonorSearchResult {
        public int PaymentConfigId { get; set; }
        public decimal AmtDonation { get; set; }
        public decimal AmtTransactionPaid { get; set; }
        public bool IsRecurring { get; set; }
        public string RecurringType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FirstNameSecond { get; set; }
        public string LastNameSecond { get; set; }
        public string CheckNumber { get; set; }
        public string Comments { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailId { get; set; }
        public string BillingAddressLine1 { get; set; }
        public string BillingAddressLine2 { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public string BillingZip { get; set; }
        public string MailingAddressLine1 { get; set; }
        public string MailingAddressLine2 { get; set; }
        public string MailingCity { get; set; }
        public string MailingState { get; set; }
        public string MailingZip { get; set; }
        public string DonationCategory { get; set; }
        public string SubscriptionTransId { get; set; }
        public string lkpPaymentType { get; set; }
        public string PaymentType { get; set; }
        public string SendEmail { get; set; }
        public string RecurringInterval { get; set; }
        public DateTime? dtPaymentStart { get; set; }
        public DateTime? dtPaymentEnd { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool IsAnonymous { get; set; }
        public string DonationType
        {
            get
            {
                if (string.IsNullOrEmpty(RecurringType) || RecurringType == "0")
                    return "One time";
                else
                    return "Recurring";
            }

        }
        public string RecurringDetails
        {
            get
            {
                if (string.IsNullOrEmpty(RecurringType) ||int.Parse(RecurringType) == 0)
                {
                    return "One Time";

                }
                else if(int.Parse(RecurringType) > 0)
                {
                    return string.Format("Every {0} {1}"
                    , RecurringType
                    , RecurringInterval
                    );
                }
                else
                {
                    return "";
                }
            }

        }

        public string AnonymusDescription
        {
            get
            {
                if (IsAnonymous)
                    return "Yes";
                else
                    return "No";
            }

        }

        public string MailingFullAddress
        {
            get
            {
                return string.Format("{0} {1} {2} {3} {4}"
                            , MailingAddressLine1
                            , MailingAddressLine2
                            , MailingCity
                            , MailingState
                            , MailingZip
                            );
            }

        }
        public string BillingFullAddress
        {
            get
            {
                return string.Format("{0} {1} {2} {3} {4}"
                            , BillingAddressLine1
                            , BillingAddressLine2
                            , BillingCity
                            , BillingState
                            , BillingZip
                            );
            }

        }

        public string FullNameFirst
        {
            get
            {
                return string.Format("{0} {1}"
                            , FirstName
                            , LastName
                            );
            }

        }

        public string FullNameSecond
        {
            get
            {
                return string.Format("{0} {1}"
                            , FirstNameSecond
                            , LastNameSecond
                            );
            }

        }

    }


    public class DonationSearch  
    {
        public DonationSearch()
        {
            this.lstPaymentType = new HashSet<SelectListItem>();
            this.lstCategory = new HashSet<SelectListItem>();
            this.lstTransResponseCode = new HashSet<SelectListItem>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FirstNameSecond { get; set; }
        public string LastNameSecond { get; set; }
        public string Email { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string TransId { get; set; }
        public string PaymentType { get; set; }
        public IEnumerable<SelectListItem> lstPaymentType { get; set; }
        public string AuthCode { get; set; }
        public int? TransResponseCode { get; set; }
        
        public int PaymentConfigId { get; set; }
        public List<DonationSearchResult> SearchResult { get; set; }
        public int? lkpDonationCategory { get; set; }
        public IEnumerable<SelectListItem> lstTransResponseCode { get; set; }
        public IEnumerable<SelectListItem> lstCategory { get; set; }
    }

    public class DonationSearchResult: PaymentTransaction {

        public int PaymentConfigId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FirstNameSecond { get; set; }
        public string LastNameSecond { get; set; }
        public string CheckNumber { get; set; }
        public string PaymentType { get; set; }
        public string lkpPaymentType { get; set; }
        public decimal AmtDonation { get; set; }
        public decimal AmtTransactionPaid { get; set; }
        public bool IsRecurring { get; set; }
        public string RecurringType { get; set; }
        public Nullable<bool> IsAnonymous { get; set; } 
        public string PhoneNumber { get; set; }
        public string EmailId { get; set; }
        public string BillingAddressLine1 { get; set; }
        public string BillingAddressLine2 { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public string BillingZip { get; set; }
        public string MailingAddressLine1 { get; set; }
        public string MailingAddressLine2 { get; set; }
        public string MailingCity { get; set; }
        public string MailingState { get; set; }
        public string MailingZip { get; set; }
        public int lkpDonationCategory { get; set; }
        public string DonationType
        {
            get
            {
                if (string.IsNullOrEmpty(RecurringType) || RecurringType == "0")
                    return "One time";
                else
                    return "Recurring";
            }

        }

        public string MailingAddress
        {
            get
            {
                return string.Format("{0} {1} {2} {3} {4}"
                            , MailingAddressLine1
                            , MailingAddressLine2
                            , MailingCity
                            , MailingState
                            , MailingZip
                            );
            }

        }
        public string BillingAddress
        {
            get
            {
                return string.Format("{0} {1} {2} {3} {4}"
                            , BillingAddressLine1
                            , BillingAddressLine2
                            , BillingCity
                            , BillingState
                            , BillingZip
                            );
            }

        }

        public string FullNameFirst
        {
            get
            {
                return string.Format("{0} {1}"
                            , FirstName
                            , LastName
                            );
            }

        }

        public string FullNameSecond
        {
            get
            {
                return string.Format("{0} {1}"
                            , FirstNameSecond
                            , LastNameSecond
                            );
            }

        }

    }
}

using EnterpriseLayer.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICYL.BL
{
    public class EmailReceipt
    {
        public int TransactionId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please Enter Email")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string EmailId { get; set; }
        public int lkpDonationCategory { get; set; }
        public decimal AmtDonation { get; set; }
        public string DonationCategory { get; set; }
        public string CardHolderName { get; set; }
        public string RecurringType { get; set; }
        public string PaymentType { get; set; }
        public string ConfirmationNumber { get; set; }
        public DateTime? TransactionDate { get; set; }
        public bool isRecurring
        {
            get
            {
                if(!string.IsNullOrEmpty(RecurringType) &&  Conversion.ConversionToInt(RecurringType) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
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

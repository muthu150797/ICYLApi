using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICYL.BL
{
    public class ReceiptBL
    {
        private bool _IsRecurringTransaction = false;
        public string MemberId { get; set; }
        public string EmailId { get; set; }
        public string MemberName { get; set; }
        public string CardHolderName { get; set; }
        public bool IsRecurringTransaction { get { return _IsRecurringTransaction; } set { _IsRecurringTransaction = value; } }
        public string RecurringTransaction { get; set; }
        public string Freequency { get; set; }
        public string LastFour { get; set; }
        public string PaymentType { get; set; }
        public string TransactionType { get; set; }
        public string Category { get; set; }
        public string Amount { get; set; }
        public string ApprovalCode { get; set; }
        public string ConfirmationNumber { get; set; }

        public DateTime? TransactionDate { get; set; }








    }
}

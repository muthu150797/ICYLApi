//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ICYL.Entity
{
    using System;
    
    public partial class ICYLReceipt_Result
    {
        public string MemberId { get; set; }
        public string EmailId { get; set; }
        public string MemberName { get; set; }
        public string CardHolderName { get; set; }
        public Nullable<bool> IsRecurringTransaction { get; set; }
        public string RecurringTransaction { get; set; }
        public string Freequency { get; set; }
        public string LastFour { get; set; }
        public string PaymentType { get; set; }
        public string TransactionType { get; set; }
        public string Category { get; set; }
        public string Amount { get; set; }
        public string ApprovalCode { get; set; }
        public string ConfirmationNumber { get; set; }
    }
}
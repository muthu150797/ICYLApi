namespace ICYL.API.Entity
{
    public class DonationAmount
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public int  DonationCategoryId{get;set;}
        public class AmountModelList
        {
            public bool Status { get; set; }
            public string Message { get; set; }


            public List<DonationAmount> amountList { get; set; }
        }
    }
}

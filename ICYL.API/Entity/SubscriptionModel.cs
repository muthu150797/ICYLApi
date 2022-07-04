namespace ICYL.API.Entity
{
	public class SubscriptionModel
	{
	  public int SubscriptionId { get; set; }
		public string Name { get; set; }
		public string SubscriptionStatus { get; set; }
		public string CreatedOn { get; set; }
		public string PaymentMethod{ get;set; }
		public string AccountNumber { get; set; }
		public decimal Amount { get; set; }
		public int CategoryId { get; set; }

	}
}

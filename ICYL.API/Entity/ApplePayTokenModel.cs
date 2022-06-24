namespace ICYL.API.Entity
{
	public class ApplePayTokenModel
	{
	    public string? Token { get; set; }
		public string Amount { get; set; }
		public string FirstName { get; set; }
		public string Email { get; set; }
		public bool IsCreditCard { get; set; } = true;
		public string? AmtTransactionPaid { get; set; }
		public int DonationCategoryId { get; set; }
	}
}

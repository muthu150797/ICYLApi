namespace ICYL.API.Entity
{
	public class ApplePayResponse
	{
		public string? Message { get; set; }
		public bool Status { get; set; }
		public string TransCode { get; set; } = "0";
		public string? TransactionId { get;set; }="0";
	}
}
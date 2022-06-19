namespace ICYL.API.Entity
{
	public class PaymentResponse
	{
	  public bool Status { get; set; }	= false;
		public string? ResponseText { get; set; } = "0";
		public string? ResponseCode { get; set; } = "0";
		public string? TransactionId { get; set; } = "0";
		public string? paymentStatus { get; set; } = "0";

	}
}

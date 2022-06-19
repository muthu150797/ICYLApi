namespace ICYL.API.Entity
{
	public class TransactionReport
	{
	   public string? TransId { get; set; }
		public string? Name { get; set; }
		public string? SubmitTimeLocal { get; set; }
		public string? AccountNumber { get; set; }
		public string? AccountType { get; set; }
		public decimal SettleAmount { get; set; }
		public string? Category { get; set; }
		public string? TransactionStatus { get; set; }

	}
	public class TransDailyReport
	{
		public string? StartDate { get; set; }
		public string? EndDate { get; set; }
		public bool TodayReport { get; set; }
	}
}

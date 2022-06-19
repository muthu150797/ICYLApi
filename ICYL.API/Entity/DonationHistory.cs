namespace ICYL.API.Entity
{
	public class DonationHistory
	{
		public string Message { get; set; }	
        public bool Status { get; set; }
		public List<HistoryList>? HistoryList { get; set; }
	}
	public class HistoryList
	{
		public string? EmailId { get; set; }
		public string? AmtDonation { get; set; }
		public string? Description { get; set; }
		public string? CreatedOn { get; set; }
		public string? DonationCategory { get; set; }
	}
}

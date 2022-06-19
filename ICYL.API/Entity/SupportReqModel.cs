namespace ICYL.API.Entity
{
	public class SupportReqModel
	{
	  public int UserId { get; set; }
		public string EmailId { get; set; }
		public string Ticket { get; set; }
		public string  Description { get; set; }
		public string CreatedOn { get; set; }
		public string UserName { get; internal set; }
	}
}

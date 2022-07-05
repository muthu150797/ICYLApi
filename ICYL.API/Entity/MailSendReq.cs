namespace ICYL.API.Entity
{
	public class MailSendReq
	{
	   public string? Subject { get; set; }
		public string? EmailTo{ get; set; }
		public string? Name { get; set; }
		public string? Content { get; set; }

	}
}

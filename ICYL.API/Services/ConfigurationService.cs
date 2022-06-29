using ICYL.API.Data;
using ICYL.API.Entity;
using System.Configuration;
using System.Data.SqlClient;

namespace ICYL.API.Services
{
	public class ConfigurationService
	{
		Configurations configuration = new Configurations();
		public dynamic SaveQuotes(QuotesList quotes)
		{
			var response = configuration.SaveQuotes(quotes);
			return response;
		}
		public dynamic DeleteQuotes(QuotesList quotes)
		{
			var response = configuration.DeleteQuotes(quotes);
			return response;
		}
		public dynamic SaveDonationType(DonationModel model)
		{
			var response = configuration.SaveDonationType(model);
			return response;
		}
		public dynamic BlockOrUnblockCategory(DonationModel model)
		{
			var response = configuration.BlockOrUnblockCategory(model);
			return response;
		}
		public dynamic SaveQuickDonation(DonationAmount model)
		{
			var response = configuration.SaveQuickDonation(model);
			return response;
		}
		public dynamic DeleteQuickDonation(DonationAmount model)
		{
			var response = configuration.DeleteQuickDonation(model);
			return response;
		}
		public dynamic AddSupportReq(SupportReqModel model)
		{
		  return configuration.AddSupportReq(model);
		}
		public dynamic GetSupportReq()
		{
			return configuration.GetSupportReq();
		}
	}
}

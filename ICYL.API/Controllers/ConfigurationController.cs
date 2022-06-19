using ICYL.API.Entity;
using ICYL.API.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ICYL.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[EnableCors("_myAllowSpecificOrigins")]
	public class ConfigurationController : ControllerBase
	{
		ConfigurationService config = new ConfigurationService();
		// GET: api/<ConfigurationController>
		[HttpPost]
		[Route("SaveQuotes")]
		public dynamic SaveQuotes(QuotesList quotes)
		{
			var response = config.SaveQuotes(quotes);
			return response;
		}
		[HttpPost]
		[Route("DeleteQuotes")]
		public dynamic DeleteQuotes(QuotesList quotes)
		{
			var response = config.DeleteQuotes(quotes);
			return response;
		}
		[HttpPost]
		[Route("SaveDonationType")]
		public dynamic SaveDonationType(DonationModel model)
		{
			var response = config.SaveDonationType(model);
			return response;

		}
		[HttpPost]
		[Route("DeleteDonationType")]
		public dynamic DeleteDonationType(DonationModel model)
		{
			var response = config.DeleteDonationType(model);
			return response;

		}
		[HttpPost]
		[Route("SaveQuickDonation")]
		public dynamic SaveQuickDonation(DonationAmount model)
		{
			var response = config.SaveQuickDonation(model);
			return response;
		}
		[HttpPost]
		[Route("DeleteQuickDonation")]
		public dynamic DeleteQuickDonation(DonationAmount model)
		{
			var response = config.DeleteQuickDonation(model);
			return response;
		}
		[HttpPost]
		[Route("AddSupportReq")]
		public dynamic AddSupportReq(SupportReqModel model)
		{    var response= config.AddSupportReq(model);
			return response;
		}
		[HttpPost]
		[Route("GetSupportReq")]
		public dynamic GetSupportReq()
		{
			var response = config.GetSupportReq();
			return response;
		}
	}
}

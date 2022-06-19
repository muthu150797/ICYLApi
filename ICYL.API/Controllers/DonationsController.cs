using ICYL.API.Data;
using ICYL.API.Entity;
using ICYL.API.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using static ICYL.API.Entity.DonationAmount;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ICYL.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[EnableCors("_myAllowSpecificOrigins")]
	public class DonationsController : ControllerBase
	{
		public readonly IConfiguration _configuration;
		//logger service to monitor the request and response
		private readonly ILogger _logger;
		public DonationsController(ILogger<ILogger> logger,IConfiguration configuration)
		{
			_logger = logger;
			_configuration = configuration;
		}
		DonationService donations = new DonationService();
		[HttpPost]
		[Route("GetDonations")]
		public DonationModelList GetDonations()
		{
			DonationModelList response = donations.GetDonations(_configuration);
			return response;
		}
		[HttpPost]
		[Route("GetDonationAmount")]
		public AmountModelList GetDonationAmount()
		{
			AmountModelList response = donations.GetDonationAmount();
			return response;
		}
		[HttpPost]
		[Route("CreateCharge")]
		public dynamic CreateCharge(StripeCharge createOptions)
		{
			//    Stripe.StripeConfiguration.SetApiKey("pk_test_51KOzDqSEBblrha4tQpKZrinZWWpkiyDFh7bl3YFlxt5D9psdVoGnxDwN9gDdw9he8Wp0cXLLvnHyN7CP2wopIYeS00zT9FLQty");
			Stripe.StripeConfiguration.ApiKey = "sk_test_51KOzDqSEBblrha4t8dAMak2mR4zRoUi5Lu7Uq8VeEXqh8AIMW4ZH6lvl5TNrqot6pLlGi4CUcIbcEjRcNlEaQjz900qNwZqe37";
			var options = new TokenCreateOptions
			{
				Card = new TokenCardOptions
				{
					Number = "4242424242424242",
					ExpMonth = 2,
					ExpYear = 2023,
					Cvc = "314",
				},
			};
			var service = new TokenService();
			var r = service.Create(options);
			//    var myCharge = new Stripe.ChargeCreateOptions();
			//    // always set these properties
			//    myCharge.Amount = 500;
			//    myCharge.Currency = "INR";
			//    myCharge.ReceiptEmail = "muthumani150797@gmail.com";
			//    myCharge.Description = "Sample Charge";
			//    myCharge.Source =r.Id;
			//    myCharge.Capture = true;
			//    var chargeService = new Stripe.ChargeService();
			//    Charge stripeCharge = chargeService.Create(myCharge);
			//option1
			//var creditOptions = new ChargeCreateOptions
			//{
			//	Amount = 1000,// createOptions.Amount,
			//	Currency = "INR",
			//	Source = r.Id,
			//	ReceiptEmail = "muthumani150797@gmail.com",
			//};
			//var service2 = new ChargeService();
			//var charge = service2.Create(creditOptions);
			
			//option2
			var options2 = new PaymentIntentCreateOptions
			{
				Amount = 1099,
				Currency = "inr",
				Description = "Software development services"

			};
			var service2 = new PaymentIntentService();
			var charge = service2.Create(options2);
			if (charge.Id!="")
			{

			}
			else
			{

			}
			return charge;
		}
		[HttpPost]
		[Route("GetQuotes")]
		public QuotesModel GetQuotes()
		{
			QuotesModel response = donations.GetQuotes();
			return response;
		}

		[HttpPost]
		[Route("GetDonationHistory")]
		public DonationHistory GetDonationHistory(Login detail)
		{
			DonationHistory response = donations.GetDonationHistory(detail);
			return response;
		}
		[HttpPost]
		[Route("GetDonationCount")]
		public QuotesReponseModel GetDonationCount(Login detail)
		{
			QuotesReponseModel response = donations.GetDonationCount(detail);
			return response;
		}
		// GET: api/<DonationsController>
		[HttpGet]
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

		// GET api/<DonationsController>/5
		[HttpGet("{id}")]
		public string Get(int id)
		{
			return "value";
		}

		// POST api/<DonationsController>
		[HttpPost]
		public void Post([FromBody] string value)
		{
		}

		// PUT api/<DonationsController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/<DonationsController>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}

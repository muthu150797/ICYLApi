using AuthorizeNet.Api.Contracts.V1;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using AuthorizeNet.Api.Controllers;
using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers.Bases;
using ICYL.API.Entity;
using ICYL.API.Services;
using Newtonsoft.Json;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ICYL.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PaymentController : ControllerBase
	{
		private readonly string _clientId;
		private readonly string _scretKey;
		PaymentService payment = new PaymentService();
		// addin 
		[HttpPost]
		[Route("InsertPaymentDetail")]
		public TransactionResponse InsertPaymentDetail(PaymentModel paymentDetail)
		{
			TransactionResponse response = new TransactionResponse();
			response = payment.InsertPaymentDetail(paymentDetail);
			return response;
		}
		[HttpPost]
		[Route("AddPayment")]
		public ANetApiResponse AddPayment()
		{
			var response = payment.AddPayment();
			return response;
		}
		[HttpPost]
		[Route("Donate")]
		public dynamic Donate(PaymentConfig model)
		{

			var response = payment.Donate(model);
			return response;
		}
		[HttpPost]
		[Route("DonateByApplePay")]
		public dynamic DonateByApplePay([FromBody] ApplePayTokenModel token)
		{
			var response = payment.DonateByApplePay(token);
			return response;
		}
		[HttpPost]
		[Route("GetAllTransaction")]
		public dynamic GetAllTransaction(TransDailyReport report)
		{

			var response = payment.GetAllTransaction(report);
			return response;
		}
		[HttpPost]
		[Route("GetAllSubscription")]
		public dynamic GetAllSubscription()
		{
			var response = payment.GetAllSubscription();
			return response;

		}
		[HttpPost]
		[Route("CancelSubscription")]
		public dynamic CancelSubscription(SubscriptionModel subscription)
		{
			var response = payment.CancelSubscription(subscription.SubscriptionId.ToString());
			return response;
		}
		//[HttpPost]
		//[Route("GetPaymentUrl")]
		//public   string  GetPaypalPaymentUrl(OrdersModel orders)
		//{
		//	var response = payment.GetPaypalPaymentUrl(orders);
		//	return response;
		//}
		// api/<PaymentController>
		//[HttpPost]
		//[Route("PayDonation")]
		//public TransactionResponse PayDonation(PaymentModel paymentDetail)
		//{
		//    ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;
		//    TransactionResponse response2=new TransactionResponse();
		//    // define the merchant information (authentication / transaction id)
		//    ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
		//    {
		//        name = paymentDetail.ApiLoginID,
		//        ItemElementName = ItemChoiceType.transactionKey,
		//        Item = paymentDetail.ApiTransactionKey,
		//    };

		//    var creditCard = new creditCardType
		//    {
		//        cardNumber = "4111111111111111",
		//        expirationDate = "0524",
		//        cardCode = "123"
		//    };

		//    var billingAddress = new customerAddressType
		//    {
		//        firstName = "John",
		//        lastName = "Doe",
		//        address = "123 My St",
		//        city = "OurTown",
		//        zip = "98004"
		//    };

		//    //standard api call to retrieve response
		//    var paymentType = new paymentType { Item = creditCard };

		//    // Add line Items
		//    var lineItems = new lineItemType[2];
		//    lineItems[0] = new lineItemType { itemId = "1", name = "t-shirt", quantity = 2, unitPrice = new Decimal(1.00) };
		//    lineItems[1] = new lineItemType { itemId = "2", name = "snowboard", quantity = 1, unitPrice = new Decimal(1.00) };

		//    var transactionRequest = new transactionRequestType
		//    {
		//        transactionType = transactionTypeEnum.authCaptureTransaction.Tostring(),    // charge the card
		//        amount = paymentDetail.Amount,
		//        payment = paymentType,
		//        billTo = billingAddress,
		//        lineItems = lineItems
		//    };

		//    var request = new createTransactionRequest { transactionRequest = transactionRequest };

		//    // instantiate the controller that will call the service
		//    var controller = new createTransactionController(request);
		//    controller.Execute();

		//    // get the response from the service (errors contained if any)
		//    var response = controller.GetApiResponse();

		//    // validate response
		//    if (response != null)
		//    {
		//        if (response.messages.resultCode == messageTypeEnum.Ok)
		//        {
		//            if (response.transactionResponse.messages != null)
		//            {
		//               var transactionId = response.transactionResponse.transId;
		//                var responseCode=response.transactionResponse.responseCode;
		//               var messageCode=response.transactionResponse.messages[0].code;
		//               var description= response.transactionResponse.messages[0].description;
		//                var Success = response.transactionResponse.authCode;
		//            }
		//            else
		//            {
		//               // Console.WriteLine("Failed Transaction.");
		//                if (response.transactionResponse.errors != null)
		//                {
		//                    var errorCode= response.transactionResponse.errors[0].errorCode;
		//                    var errorText= response.transactionResponse.errors[0].errorText;
		//                }
		//            }
		//        }
		//        else
		//        {
		//            if (response.transactionResponse != null && response.transactionResponse.errors != null)
		//            {
		//                var errorCode= response.transactionResponse.errors[0].errorCode;
		//                var errorText=response.transactionResponse.errors[0].errorText;
		//            }
		//            else
		//            {
		//                var errorCode = response.messages.message[0].code;
		//                var errorText= response.messages.message[0].text;
		//            }
		//        }
		//    }
		//    else
		//    {
		//       // Console.WriteLine("Null Response.");
		//    }

		//    return response2;
		//}
	}

	
}

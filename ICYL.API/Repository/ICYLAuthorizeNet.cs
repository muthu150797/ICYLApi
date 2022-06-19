using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthorizeNet.Api.Controllers;
using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers.Bases;
//using EnterpriseLayer.Utilities;
using System.Configuration;
using ICYL.API.Entity;
using ICYL.API.Repository;
using ICYL.Repository;
using System.Globalization;

namespace ICYL.API.Repository
{
	public class ICYLAuthorizeNet
	{
		// string DownloadDays = ConfigurationManager.AppSettings["DownloadDays"];

		//Production
		//const string apiLoginId = "6M8E6XguqJ";
		//const string transactionKey = "7uC727D97RCJaKwS";

		//Sandbox
		//const string apiLoginId = "9X7Kn6vb7";
		//const string transactionKey = "4e6w63MgzTNSk96u";

		////Update TransactionID for which you want to run the sample code
		//const string transactionId = "2249735976";

		////Update PayerID for which you want to run the sample code
		//const string payerId = "M8R9JRNJ3R28Y";

		//const string customerProfileId = "1915435550"; //"213213";
		//const string customerPaymentProfileId = "1828811149"; //"2132345";

		//const string shippingAddressId = "1223213";
		//const decimal amount = 12.34m;
		//const string subscriptionId = "1223213";
		//const short day = 45;
		//const string emailId = "test@test.com";

		//public string CCCardNumber { get; set; }
		//public string CCExpirationDate { get; set; }
		//public string CCCardCode { get; set; }

		//public string CustomerFirstName { get; set; }
		//public string CustomerLastName { get; set; } 
		//public string CustomerAddress { get; set; }
		//public string CustomerCity { get; set; }
		//public string CustomerZip { get; set; }

		public ICYLAuthorizeNet()
		{


		}

		public dynamic ICYLChargeCreditCard(decimal Amount, creditCardType creditCardInfo, customerAddressType CustomerInfo, orderType OrderInfo, nameAndAddressType MailTo, customerDataType CustData, int lkpCategory)
		{
			PaymentTransaction obj = new PaymentTransaction();

			//creditCardType creditCardInfo = new creditCardType
			//    {
			//    cardNumber= CCCardNumber,
			//    expirationDate = CCExpirationDate,
			//    cardCode = CCCardCode
			//    };

			//customerAddressType CustomerInfo  = new customerAddressType
			//{
			//    firstName = CustomerFirstName,
			//    lastName = CustomerLastName,
			//    address = CustomerAddress,
			//    city = CustomerCity,
			//    zip = CustomerZip
			//};


			InvokePaymentAccount(lkpCategory);
			var paymentType = new paymentType { Item = creditCardInfo };

			// Add line Items
			//var lineItems = new lineItemType[2];
			//lineItems[0] = new lineItemType { itemId = "1", name = "Donation", quantity = 1, unitPrice = new Decimal(15.00) }; 

			var transactionRequest = new transactionRequestType
			{
				transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),    // charge the card

				amount = Amount,
				payment = paymentType,
				billTo = CustomerInfo,
				shipTo = MailTo,
				customer = CustData,
				order = OrderInfo
				//,lineItems = lineItems
			};
			createTransactionResponse response = new createTransactionResponse();
			var request = new createTransactionRequest { transactionRequest = transactionRequest };
			var controller = new createTransactionController(request);
			try
			{
				controller.Execute();
				response = controller.GetApiResponse();
			}
			catch (Exception ex)
			{
				obj.TransactionToken = ex.ToString();
				return obj;
			}
			//if (isMobile)
			//{
			//getHostedProfilePageResponse resquest = new getHostedProfilePageResponse();
			// var controller2 = new getHostedProfilePageController(resquest.)                //controller2.Execute();
			//var token = resquest.token;
			//obj.TransactionToken = "";
			// obj.tr
			// return response.get;
			//}
			//obj.response = response
			if (response != null)
			{
				if (response.messages.resultCode == messageTypeEnum.Ok)
				{
					if (response.transactionResponse.messages != null)
					{
						obj.TransId = response.transactionResponse.transId;
						obj.TransResponseCode = response.messages.message[0].code;
						obj.TransDescription = response.messages.message[0].text;// response.transactionResponse.messages[0].description != "" ? response.transactionResponse.messages[0].code : "0";
					}
					else
					{
						if (response.transactionResponse.errors != null)
						{
							obj.TransErrorText = response.transactionResponse.errors[0].errorText;
							obj.TransResponseCode = response.transactionResponse.responseCode != "" ? response.transactionResponse.responseCode : "0";
							obj.TransDescription = response.messages.message[0].text;// response.transactionResponse.messages[0].description != "" ? response.transactionResponse.messages[0].code : "0";
																					 //obj.TransMessageCode = response.transactionResponse.messages[0].code;
																					 //obj.TransMessage= response.messages.ToString();
						}
					}
				}
				else
				{
					if (response.transactionResponse != null && response.transactionResponse.errors != null)
					{
						obj.TransErrorText = response.transactionResponse.errors[0].errorText;
						obj.TransDescription = response.transactionResponse.errors[0].errorText;// response.transactionResponse.messages[0].description != "" ? response.transactionResponse.messages[0].code : "0";
						obj.TransResponseCode = response.transactionResponse.responseCode != "" ? response.transactionResponse.responseCode : "0";
						//obj.TransMessage = response.messages.ToString();
					}
					else
					{
						obj.TransErrorCode = response.messages.message[0].code != "" ? response.messages.message[0].code : "0";
						obj.TransErrorText = response.messages.message[0].text;
						obj.TransDescription = response.messages.message[0].text;// response.transactionResponse.messages[0].description != "" ? response.transactionResponse.messages[0].code : "0";

						// obj.TransResponseCode = response.transactionResponse.responseCode;
						// obj.TransAuthCode = response.transactionResponse.authCode;
						// obj.TransMessage = response.messages.ToString();
					}
				}
			}
			else
			{
				obj.TransErrorCode = "Failed";
				obj.TransErrorText = "Invalid Transaction";
				//obj.TransResponseCode = response.transactionResponse.responseCode;
				// obj.TransDescription = response.transactionResponse.messages[0].description;
				//  obj.TransAuthCode = response.transactionResponse.authCode;
				// obj.TransMessage = response.messages.ToString();
			}
			return obj;
		}

		internal SubscriptionTransaction SetUpRecurringCharge(decimal amtTotal, paymentScheduleTypeInterval interval, paymentScheduleType schedule, creditCardType creditCardInfo, nameAndAddressType billTo, nameAndAddressType mailTo, customerType recurringCustInfo, int lkpDonationCategory)
		{

			InvokePaymentAccount(lkpDonationCategory);
			SubscriptionTransaction obj = new SubscriptionTransaction();
			Random rand = new Random();
			int randomAccountNumber = rand.Next(10000, int.MaxValue);
			//paymentScheduleTypeInterval interval = new paymentScheduleTypeInterval();
			//interval.length = intervalLength;                        // months can be indicated between 1 and 12
			//interval.unit = ARBSubscriptionUnitEnum.days;

			//paymentScheduleType schedule = new paymentScheduleType
			//{
			//    interval = interval,
			//    startDate = DateTime.Now.AddDays(1),      // start date should be tomorrow
			//    totalOccurrences = 9999,                          // 999 indicates no end date
			//    trialOccurrences = 3
			//}; 
			paymentType cc = new paymentType { Item = creditCardInfo };
			ARBSubscriptionType subscriptionType = new ARBSubscriptionType()
			{
				amount = amtTotal,
				paymentSchedule = schedule,
				billTo = billTo,
				payment = cc,
				trialAmountSpecified = false,
				order = null,
				shipTo = null,
				customer = null
			};

			var request = new ARBCreateSubscriptionRequest { subscription = subscriptionType };
			var controller = new ARBCreateSubscriptionController(request);          // instantiate the controller that will call the service
			controller.Execute();

			ARBCreateSubscriptionResponse response = controller.GetApiResponse();   // get the response from the service (errors contained if any)


			if (response == null)
			{
				obj.SubscriptionResponseCode = "Failed";
				obj.SubscriptionResponseText = "Invalid Transaction";
			}


			// validate response
			if (response != null && response.messages.resultCode == messageTypeEnum.Ok)
			{
				if (response != null && response.messages.message != null)
				{
					obj.SubscriptionTransId = response.subscriptionId.ToString();
					obj.SubscriptionResponseCode = response.messages.message[0].code;
					obj.SubscriptionResponseText = response.messages.message[0].text;

				}
			}
			else if (response != null)
			{
				obj.SubscriptionResponseCode = response.messages.message[0].code;
				obj.SubscriptionResponseText = response.messages.message[0].text;
			}

			return obj;
		}
		public dynamic DonateByApplePay(ApplePayTokenModel token)
		{
			var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(@token.Token);
			var tokenData = Convert.ToBase64String(plainTextBytes);
			var base64EncodedBytes = System.Convert.FromBase64String(tokenData);
			var decrypt= System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
			InvokePaymentAccount(0);
			//set up data based on transaction
			var opaqueData = new opaqueDataType { dataDescriptor = "COMMON.APPLE.INAPP.PAYMENT", dataValue = tokenData };
			//standard api call to retrieve response
			var paymentType = new paymentType { Item = opaqueData };
			var transactionRequest = new transactionRequestType
			{
				transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),    // authorize and capture transaction
				amount = (decimal)token.Amount,// Amount,
				payment = paymentType,

			};
			var request = new createTransactionRequest { transactionRequest = transactionRequest, };
			// instantiate the controller that will call the service
			var controller = new createTransactionController(request);
			try
			{
				controller.Execute();
			}
			catch (Exception ex)
			{
				return ex;
			}
			ApplePayResponse res = new ApplePayResponse();
			// get the response from the service (errors contained if any)
			var response = controller.GetApiResponse();
			// validate response
			if (response != null)
			{
				if (response.messages.resultCode == messageTypeEnum.Ok)
				{
					if (response.transactionResponse.messages != null)
					{

						res.Status = true;
						res.Message = response.transactionResponse.messages[0].description;
						res.TransactionId = response.transactionResponse.transId;
						res.TransCode = response.transactionResponse.messages[0].code;
						//Console.WriteLine("Successfully created an Apple pay transaction with Transaction ID: " + response.transactionResponse.transId);
						//Console.WriteLine("Response Code: " + response.transactionResponse.responseCode);
						//Console.WriteLine("Message Code: " + response.transactionResponse.messages[0].code);
						//Console.WriteLine("Description: " + response.transactionResponse.messages[0].description);
					}
					else
					{
						//Console.WriteLine("Failed Transaction.");
						if (response.transactionResponse.errors != null)
						{
							res.Status = false;
							res.Message = response.transactionResponse.errors[0].errorText;
							res.TransCode = response.transactionResponse.errors[0].errorCode;
							//Console.WriteLine("Error Code: " + response.transactionResponse.errors[0].errorCode);
							//Console.WriteLine("Error message: " + response.transactionResponse.errors[0].errorText);
						}
					}
				}
				else
				{

					//Console.WriteLine("Failed Transaction.");
					if (response.transactionResponse != null && response.transactionResponse.errors != null)
					{
						res.Status = false;
						res.Message = response.transactionResponse.errors[0].errorText;
						res.TransCode = response.transactionResponse.errors[0].errorCode;

						//Console.WriteLine("Error Code: " + response.transactionResponse.errors[0].errorCode);
						//Console.WriteLine("Error message: " + response.transactionResponse.errors[0].errorText);
					}
					else
					{
						res.Status = false;
						res.Message = response.messages.message[0].text;
						res.TransCode = response.messages.message[0].code;

						//Console.WriteLine("Error Code: " + response.messages.message[0].code);
						//Console.WriteLine("Error message: " + response.messages.message[0].text);
					}

				}
			}
			else
			{
				res.Status = false;
				res.Message = "Null response";
			}
			return res;
		}
		public dynamic ICYLPaypal(decimal Amount, creditCardType creditCardInfo, customerAddressType CustomerInfo, orderType OrderInfo, nameAndAddressType MailTo, customerDataType CustData, int lkpCategory)
		{
			PaymentTransaction obj = new PaymentTransaction();
			InvokePaymentAccount(lkpCategory);
			var payPalType = new payPalType
			{
				cancelUrl = "http://www.merchanteCommerceSite.com/Success/TC25262",
				successUrl = "http://www.merchanteCommerceSite.com/Success/TC25262",     // the url where the user will be returned to            
				payerID = "S6D5ETGSVYX94"
			};
			var paymentType = new paymentType { Item = payPalType };
			var transactionRequest = new transactionRequestType
			{
				transactionType = transactionTypeEnum.authOnlyTransaction.ToString(),    // capture the card only
				payment = paymentType,
				billTo = CustomerInfo,
				shipTo = MailTo,
				customer = CustData,
				order = OrderInfo,
				amount = Amount,
			};
			var request = new createTransactionRequest { transactionRequest = transactionRequest };
			// instantiate the controller that will call the service
			var controller = new createTransactionController(request);
			try
			{
				controller.Execute();
			}
			catch (Exception ex)
			{
				return ex.ToString();

			}
			// get the response from the service (errors contained if any)
			var response = controller.GetApiResponse();
			if (response != null)
			{
				if (response.messages.resultCode == messageTypeEnum.Ok)
				{
					if (response.transactionResponse.messages != null)
					{
						obj.TransId = response.transactionResponse.transId;
						obj.TransResponseCode = response.transactionResponse.responseCode;
						obj.TransMessageCode = response.transactionResponse.messages[0].code;
						obj.TransDescription = response.transactionResponse.messages[0].description;
						obj.TransAuthCode = response.transactionResponse.authCode;
						//obj.TransactionToken = response.sessionToken.ToString(); ;

					}
					else
					{
						if (response.transactionResponse.errors != null)
						{
							obj.TransErrorCode = response.transactionResponse.errors[0].errorCode;
							obj.TransErrorText = response.transactionResponse.errors[0].errorText;
							obj.TransResponseCode = response.transactionResponse.responseCode;
							//obj.TransMessageCode = response.transactionResponse.messages[0].code;
							obj.TransDescription = response.transactionResponse.messages[0].description;
							obj.TransAuthCode = response.transactionResponse.authCode;
							//obj.TransMessage= response.messages.ToString();
						}
					}
				}
				else
				{
					if (response.transactionResponse != null && response.transactionResponse.errors != null)
					{
						obj.TransErrorCode = response.transactionResponse.errors[0].errorCode;
						obj.TransErrorText = response.transactionResponse.errors[0].errorText;
						obj.TransResponseCode = response.transactionResponse.responseCode;
						obj.TransAuthCode = response.transactionResponse.authCode;
					}
					else
					{
						obj.TransErrorCode = response.messages.message[0].code;
						obj.TransErrorText = response.messages.message[0].text;
					}
				}
			}
			else
			{
				obj.TransErrorCode = "Failed";
				obj.TransErrorText = "Invalid Transaction";
			}
			return obj;
		}
		public dynamic GetAllTransaction(TransDailyReport reports)
		{
			List<TransactionReport> reportResponse = new List<TransactionReport>();
			InvokePaymentAccount(0);
			// unique batch id
			string batchId = "";
			var batchList = getBatchList(reports);
			for (int i = 0; i < batchList.Count; i++)
			{
				batchId = batchList[i].BatchId.ToString();

				var request = new getTransactionListRequest();
				request.batchId = batchId;
				request.paging = new Paging
				{
					limit = 1000,
					offset = 1
				};
				request.sorting = new TransactionListSorting
				{
					orderBy = TransactionListOrderFieldEnum.id,
					orderDescending = true
				};

				// instantiate the controller that will call the service
				var controller = new getTransactionListController(request);
				try
				{
					controller.Execute();
				}
				catch (Exception ex)
				{
					return ex.Message;
				}

				// get the response from the service (errors contained if any)
				var response = controller.GetApiResponse();

				if (response != null && response.messages.resultCode == messageTypeEnum.Ok)
				{
					foreach (var transaction in response.transactions)
					{
						TransactionReport report = new TransactionReport();
						report.TransId = transaction.transId;
						report.Name = transaction.firstName;
						report.TransactionStatus = transaction.transactionStatus;
						report.SubmitTimeLocal = transaction.submitTimeLocal.ToString();
						report.SettleAmount = transaction.settleAmount;
						report.AccountType = transaction.accountType;
						report.Category = GetCustomerProfile(transaction.transId);
						report.AccountNumber = transaction.accountNumber;
						reportResponse.Add(report);
					}
				}
			}
			var unSettledPaymentRes = GetUnSettledTransaction();
			if (unSettledPaymentRes != null)
			{
				foreach (var transaction in unSettledPaymentRes)
				{
					TransactionReport report = new TransactionReport();
					report.TransId = transaction.transId;
					report.Name = transaction.firstName;
					report.TransactionStatus = transaction.transactionStatus;
					report.SubmitTimeLocal = transaction.submitTimeUTC.ToString();
					report.SettleAmount = transaction.settleAmount;
					report.AccountType = transaction.accountType;
					report.Category = GetCustomerProfile(transaction.transId);
					report.AccountNumber = transaction.accountNumber;
					reportResponse.Add(report);
				}
			}
			return reportResponse;
		}
		public dynamic GetUnSettledTransaction()
		{
			InvokePaymentAccount(0);
			var request = new getUnsettledTransactionListRequest();
			request.status = TransactionGroupStatusEnum.any;
			request.statusSpecified = true;
			request.paging = new Paging
			{
				limit = 1000,
				offset = 1
			};
			request.sorting = new TransactionListSorting
			{
				orderBy = TransactionListOrderFieldEnum.id,
				orderDescending = true
			};
			// instantiate the controller that will call the service
			var controller = new getUnsettledTransactionListController(request);
			controller.Execute();
			// get the response from the service (errors contained if any)
			var response = controller.GetApiResponse();
			if (response != null && response.messages.resultCode == messageTypeEnum.Ok)
			{
				if (response.transactions != null)
					return response.transactions;
				else
					return null;
			}
			return null;
		}
		public dynamic GetAllSubscription()
		{
			List<SubscriptionModel> subsList = new List<SubscriptionModel>();
			InvokePaymentAccount(0);
			var request = new ARBGetSubscriptionListRequest { searchType = ARBGetSubscriptionListSearchTypeEnum.subscriptionActive };    // only gets active subscriptions

			var controller = new ARBGetSubscriptionListController(request);          // instantiate the controller that will call the service
			controller.Execute();

			ARBGetSubscriptionListResponse response = controller.GetApiResponse();   // get the response from the service (errors contained if any)

			// validate response
			if (response != null && response.messages.resultCode == messageTypeEnum.Ok)
			{
				if (response != null && response.messages.message != null && response.subscriptionDetails != null)
				{
					foreach (var transaction in response.subscriptionDetails)
					{
						SubscriptionModel report = new SubscriptionModel();
						report.SubscriptionId = transaction.id;
						report.Name = transaction.firstName;
						report.Amount = transaction.amount;
						report.SubscriptionStatus = transaction.status.ToString();
						report.CreatedOn = transaction.createTimeStampUTC.ToString();
						report.PaymentMethod = transaction.paymentMethod.ToString();
						report.AccountNumber = transaction.accountNumber;
						subsList.Add(report);
					}
					return subsList;
				}
			}
			else if (response != null)
			{
			}
			return response;
			return null;
		}
		public dynamic CancelSubscription(string subscriptionId)
		{
			InvokePaymentAccount(0);
			SubscriptionResModel res = new SubscriptionResModel();
			//Please change the subscriptionId according to your request
			var request = new ARBCancelSubscriptionRequest { subscriptionId = subscriptionId };
			var controller = new ARBCancelSubscriptionController(request);                          // instantiate the controller that will call the service
			controller.Execute();

			ARBCancelSubscriptionResponse response = controller.GetApiResponse();                   // get the response from the service (errors contained if any)

			// validate response
			if (response != null && response.messages.resultCode == messageTypeEnum.Ok)
			{
				if (response != null && response.messages.message != null)
				{
					res.StatusCode = 200;
					res.Message = "Success";
				}
			}
			else if (response != null)
			{
				res.StatusCode = 400;
				res.Message = "Failed to cancel subscription,try again";
			}

			return res;
		}
		public PaymentTransaction ICYLeCheck(decimal Amount, bankAccountType bankAccount, customerAddressType CustomerInfo, orderType OrderInfo, nameAndAddressType MailTo, customerDataType CustData, int lkpCategory)
		{
			InvokePaymentAccount(lkpCategory);
			PaymentTransaction obj = new PaymentTransaction();
			Random rand = new Random();
			int randomAccountNumber = rand.Next(10000, int.MaxValue);

			//var bankAccount = new bankAccountType
			//{
			//    accountType = bankAccountTypeEnum.checking,
			//    routingNumber = "125008547",
			//    accountNumber = randomAccountNumber.ToString(),
			//    nameOnAccount = "John Doe",
			//    echeckType = echeckTypeEnum.WEB,   // change based on how you take the payment (web, telephone, etc)
			//    bankName = "Wells Fargo Bank NA",
			//    // checkNumber     = "101"                 // needed if echeckType is "ARC" or "BOC"
			//};
			// standard api call to retrieve response
			var paymentType = new paymentType { Item = bankAccount };

			var transactionRequest = new transactionRequestType
			{
				transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),    // refund type
				payment = paymentType,
				amount = Amount,
				order = OrderInfo,
				billTo = CustomerInfo,
				shipTo = MailTo,
				customer = CustData

			};

			var request = new createTransactionRequest { transactionRequest = transactionRequest };
			var controller = new createTransactionController(request);
			controller.Execute();

			var response = controller.GetApiResponse();

			// validate response
			if (response != null)
			{
				if (response.messages.resultCode == messageTypeEnum.Ok)
				{
					if (response.transactionResponse.messages != null)
					{
						//Console.WriteLine("Successfully created transaction with Transaction ID: " + response.transactionResponse.transId);
						//Console.WriteLine("Response Code: " + response.transactionResponse.responseCode);
						//Console.WriteLine("Message Code: " + response.transactionResponse.messages[0].code);
						//Console.WriteLine("Description: " + response.transactionResponse.messages[0].description);
						//Console.WriteLine("Success, Transaction Code : " + response.transactionResponse.transId);

						obj.TransId = response.transactionResponse.transId;
						obj.TransResponseCode = response.transactionResponse.responseCode;
						obj.TransMessageCode = response.transactionResponse.messages[0].code;
						obj.TransDescription = response.transactionResponse.messages[0].description;
						obj.TransAuthCode = response.transactionResponse.transId;

					}
					else
					{
						if (response.transactionResponse.errors != null)
						{
							obj.TransErrorCode = response.transactionResponse.errors[0].errorCode;
							obj.TransErrorText = response.transactionResponse.errors[0].errorText;
						}
					}
				}
				else
				{
					Console.WriteLine("Failed Transaction.");
					if (response.transactionResponse != null && response.transactionResponse.errors != null)
					{
						obj.TransErrorCode = response.transactionResponse.errors[0].errorCode;
						obj.TransErrorText = response.transactionResponse.errors[0].errorText;
					}
					else
					{
						obj.TransErrorCode = response.transactionResponse.errors[0].errorCode;
						obj.TransErrorText = response.transactionResponse.errors[0].errorText;
					}
				}
			}
			else
			{
				obj.TransErrorCode = "Failed";
				obj.TransErrorText = "Invalid Transaction";
			}
			return obj;
		}
		public dynamic GetCustomerProfile(string transId)
		{
			InvokePaymentAccount(0);
			var request = new getTransactionDetailsRequest();
			request.transId = transId;

			// instantiate the controller that will call the service
			var controller = new getTransactionDetailsController(request);
			controller.Execute();

			// get the response from the service (errors contained if any)
			var response = controller.GetApiResponse();
			if (response.transaction.order != null && response.messages.resultCode == messageTypeEnum.Ok)
				return response.transaction.order.description;
			else
				//if (response != null && response.messages.resultCode == messageTypeEnum.Ok)
				//{
				//	if (response.transaction == null)
				//		return "exception";

				//}
				//else if (response != null)
				//{
				//	return response.transaction.order.description;
				//}
				return "";
		}
		public SubscriptionTransaction SetUpRecurringCharge(decimal Amount, paymentScheduleTypeInterval interval, paymentScheduleType schedule, creditCardType creditCardInfo
						, nameAndAddressType BillTo, orderType orderInfo, nameAndAddressType MailTo, customerType customer, int lkpCategory)
		{
			InvokePaymentAccount(lkpCategory);
			SubscriptionTransaction obj = new SubscriptionTransaction();
			Random rand = new Random();
			int randomAccountNumber = rand.Next(10000, int.MaxValue);
			//paymentScheduleTypeInterval interval = new paymentScheduleTypeInterval();
			//interval.length = intervalLength;                        // months can be indicated between 1 and 12
			//interval.unit = ARBSubscriptionUnitEnum.days;

			//paymentScheduleType schedule = new paymentScheduleType
			//{
			//    interval = interval,
			//    startDate = DateTime.Now.AddDays(1),      // start date should be tomorrow
			//    totalOccurrences = 9999,                          // 999 indicates no end date
			//    trialOccurrences = 3
			//}; 
			paymentType cc = new paymentType { Item = creditCardInfo };
			ARBSubscriptionType subscriptionType = new ARBSubscriptionType()
			{
				amount = Amount,
				paymentSchedule = schedule,
				billTo = BillTo,
				payment = cc,
				trialAmountSpecified = false,
				order = orderInfo,
				shipTo = MailTo,
				customer = customer
			};

			var request = new ARBCreateSubscriptionRequest { subscription = subscriptionType };
			var controller = new ARBCreateSubscriptionController(request);          // instantiate the controller that will call the service
			controller.Execute();

			ARBCreateSubscriptionResponse response = controller.GetApiResponse();   // get the response from the service (errors contained if any)


			if (response == null)
			{
				obj.SubscriptionResponseCode = "Failed";
				obj.SubscriptionResponseText = "Invalid Transaction";
			}


			// validate response
			if (response != null && response.messages.resultCode == messageTypeEnum.Ok)
			{
				if (response != null && response.messages.message != null)
				{
					obj.SubscriptionResponseCode = response.messages.message[0].code;
					obj.SubscriptionResponseText = response.messages.message[0].text;
					obj.SubscriptionTransId = response.subscriptionId.ToString();
				}
			}
			else if (response != null)
			{
				obj.SubscriptionResponseCode = response.messages.message[0].code;
				obj.SubscriptionResponseText = response.messages.message[0].text;
			}

			return obj;
		}

		public ICYL.BL.SubscriptionTransaction SetUpRecurringCharge(decimal Amount, paymentScheduleTypeInterval interval, paymentScheduleType schedule
				, bankAccountType bankAccountInfo
				, nameAndAddressType nameAndAddressInfo, nameAndAddressType MailTo, customerType customer, int lkpCategory)
		{
			InvokePaymentAccount(lkpCategory);
			ICYL.BL.SubscriptionTransaction obj = new BL.SubscriptionTransaction();
			var paymentType = new paymentType { Item = bankAccountInfo };


			ARBSubscriptionType subscriptionType = new ARBSubscriptionType()
			{
				amount = Amount,
				paymentSchedule = schedule,
				billTo = nameAndAddressInfo,
				payment = paymentType,
				shipTo = MailTo,
				trialAmountSpecified = false,
				customer = customer
			};

			var request = new ARBCreateSubscriptionRequest { subscription = subscriptionType };
			var controller = new ARBCreateSubscriptionController(request);          // instantiate the controller that will call the service
			controller.Execute();

			ARBCreateSubscriptionResponse response = controller.GetApiResponse();   // get the response from the service (errors contained if any)

			if (response == null)
			{
				obj.SubscriptionResponseCode = "Failed";
				obj.SubscriptionResponseText = "Invalid Transaction";
			}


			// validate response
			if (response != null && response.messages.resultCode == messageTypeEnum.Ok)
			{
				if (response != null && response.messages.message != null)
				{
					obj.SubscriptionTransId = response.subscriptionId.ToString();
				}
			}
			else if (response != null)
			{
				obj.SubscriptionResponseCode = response.messages.message[0].code;
				obj.SubscriptionResponseText = response.messages.message[0].text;
			}

			return obj;
		}


		public void getActiveSubscriptions(int CategoryId, int Days)
		{

			var request = new ARBGetSubscriptionListRequest { searchType = ARBGetSubscriptionListSearchTypeEnum.subscriptionActive };
			var controller = new ARBGetSubscriptionListController(request);
			controller.Execute();
			ARBGetSubscriptionListResponse response = controller.GetApiResponse();
			if (response != null && response.messages.resultCode == messageTypeEnum.Ok)
			{
				if (response != null && response.messages.message != null && response.subscriptionDetails != null)
				{
					Console.WriteLine("Success, " + response.totalNumInResultSet + " Results Returned ");
					for (int i = 0; i <= response.totalNumInResultSet - 1; i++) //Create Payment Config record.
					{
						getSubscriptionDetail(response.subscriptionDetails[i], CategoryId, Days);
						//if (response.subscriptionDetails[i].id == 7240131)
						//{

						//}
						// getCustomerProfile(Conversion.ConversionToString(response.subscriptionDetails[i].customerProfileId));
						//getCustoerTransactions(Conversion.ConversionToString(response.subscriptionDetails[i].customerProfileId));
					}
				}
			}
			else if (response != null)
			{
				Console.WriteLine("Error: " + response.messages.message[0].code + "  " + response.messages.message[0].text);
			}


		}

		public void getSubscriptionDetail(SubscriptionDetail obj, int CategoryId, int downloadDays)
		{
			int PaymentconfigId = 0;
			//int downloadDays = 0;//restrict download days 
			//Int32.TryParse(DownloadDays, out downloadDays);
			var request = new ARBGetSubscriptionRequest { subscriptionId = obj.id.ToString(), includeTransactions = true, includeTransactionsSpecified = true };
			var controller = new ARBGetSubscriptionController(request);
			controller.Execute();
			ARBGetSubscriptionResponse response = controller.GetApiResponse();

			if (response != null && response.messages.resultCode == messageTypeEnum.Ok)
			{
				if (response.subscription != null)
				{
					//if (response.subscription.paymentSchedule != null)
					//{
					//    getSubscriptionSchedule(response.subscription.paymentSchedule, obj);
					//}
					PaymentconfigId = new DonationRepository().InsertSubscriptionPaymentConfig(obj, response.subscription.paymentSchedule, CategoryId);
					if (response.subscription.arbTransactions != null)
					{
						foreach (var transaction in response.subscription.arbTransactions)
						{
							if (transaction.transId != null && (downloadDays == 0 || transaction.submitTimeUTC > DateTime.Now.AddDays(-downloadDays)))
							{
								getSubscriptionCustomerTransactions(transaction.transId);
							}
						}
					}
				}
			}

		}


		public List<PaymentBatch> getBatchList(TransDailyReport report)
		{
			getSettledBatchListRequest request = new getSettledBatchListRequest();
			List<PaymentBatch> lst = new List<PaymentBatch>();
			DateTime firstSettlementDate = DateTime.Today;//DateTime.Today.Subtract(TimeSpan.FromDays(30));
			DateTime lastSettlementDate = DateTime.Today; //DateTime.Today.AddDays(1);
														  //Console.WriteLine("First settlement date: {0} Last settlement date:{1}", firstSettlementDate,
														  //    lastSettlementDate);
			if (report.TodayReport)
			{
				request.firstSettlementDate = firstSettlementDate;
				request.lastSettlementDate = lastSettlementDate;
			}
			else
			{
				request.firstSettlementDate = DateTime.Parse(report.StartDate + "T16:00:00Z", CultureInfo.InvariantCulture);
				//request.firstSettlementDate = DateTime.ParseExact(report.StartDate, "yyyy-MM-dd'T'HH:mm:ss.fffffff'Z'", CultureInfo.InvariantCulture);
				request.lastSettlementDate = DateTime.Parse(report.EndDate + "T16:00:00Z", CultureInfo.InvariantCulture);
			}
			request.includeStatistics = true;

			var controller = new getSettledBatchListController(request);
			controller.Execute();

			var response = controller.GetApiResponse();
			if (response != null && response.messages.resultCode == messageTypeEnum.Ok)
			{
				//if (response.batchList == null)
				//    return response;
				if (response.batchList != null)
				{
					foreach (var batch in response.batchList)
					{
						PaymentBatch Obj = new PaymentBatch();

						Obj.BatchId = int.Parse(batch.batchId);
						Obj.settlementTimeUTC = batch.settlementTimeUTC;
						Obj.settlementTimeLocal = batch.settlementTimeLocal;
						Obj.settlementState = batch.settlementState;
						Obj.marketType = batch.marketType;
						Obj.product = batch.product;
						lst.Add(Obj);

					}
				}

			}
			return lst;
		}


		public List<PaymentTransaction> GetPaymentTransactions(int BatchId, int downloadDays)
		{
			//int downloadDays = 0;//restrict download days 
			//Int32.TryParse(DownloadDays, out downloadDays);
			List<PaymentTransaction> lst = new List<PaymentTransaction>();
			// unique batch id
			string batchId = (BatchId).ToString();

			var request = new getTransactionListRequest();
			request.batchId = batchId;
			request.paging = new Paging
			{
				limit = 10,
				offset = 1
			};
			request.sorting = new TransactionListSorting
			{
				orderBy = TransactionListOrderFieldEnum.id,
				orderDescending = true
			};

			var controller = new getTransactionListController(request);
			controller.Execute();

			var response = controller.GetApiResponse();

			if (response != null && response.messages.resultCode == messageTypeEnum.Ok)
			{
				if (response.transactions == null)
					return lst;


				foreach (var transaction in response.transactions)
				{

					if (transaction.transId != null && (downloadDays == 0 || transaction.submitTimeUTC > DateTime.Now.AddDays(-downloadDays)))
					{
						PaymentTransaction obj = new PaymentTransaction();

						obj.TransId = (transaction.transId);
						obj.CreatedOn = (transaction.submitTimeLocal).ToString();

						transactionDetailsType tras = GetPaymentByTransactions(transaction.transId);
						obj.TransAuthCode = tras.authCode;
						obj.TransDescription = tras.transactionStatus;
						obj.TransResponseCode = tras.responseCode.ToString();
						obj.AmtTransaction = transaction.settleAmount;
						if (transaction.accountType == "eCheck")
						{
							obj.PaymentConfigs.PaymentType = int.Parse("6");
						}
						else
						{
							obj.PaymentConfigs.PaymentType = int.Parse("5");
						}
						obj.PaymentConfigs.FirstName = transaction.firstName;
						obj.PaymentConfigs.LastName = transaction.lastName;
						if (tras.billTo != null)
						{
							obj.PaymentConfigs.MailingAddressLine1 = tras.billTo.address;
							obj.PaymentConfigs.MailingCity = tras.billTo.city;
							obj.PaymentConfigs.MailingState = tras.billTo.state;
							obj.PaymentConfigs.MailingZip = tras.billTo.zip;
							obj.PaymentConfigs.EmailId = string.IsNullOrEmpty(tras.billTo.email) ? "" : tras.billTo.email;
							obj.PaymentConfigs.PhoneNumber = tras.billTo.phoneNumber;
						}

						if (transaction.profile != null)
						{
							obj.CustomerProfileId = transaction.profile.customerProfileId;
							obj.CustomerAddressId = transaction.profile.customerAddressId;
						}
						obj.AmtTransaction = transaction.settleAmount;
						// GetPaymentByTransactions(obj.TransId);
						lst.Add(obj);
					}
				}
			}


			return lst;
		}


		public transactionDetailsType GetPaymentByTransactions(String transactionId)
		{
			transactionDetailsType type = new transactionDetailsType();
			var request = new getTransactionDetailsRequest();
			request.transId = transactionId;
			// instantiate the controller that will call the service
			var controller = new getTransactionDetailsController(request);
			controller.Execute();

			// get the response from the service (errors contained if any)
			var response = controller.GetApiResponse();

			if (response != null && response.messages.resultCode == messageTypeEnum.Ok)
			{
				if (response.transaction != null)
				{
					//Console.WriteLine("Transaction Id: {0}", response.transaction.transId);
					//Console.WriteLine("Transaction type: {0}", response.transaction.transactionType);
					//Console.WriteLine("Transaction status: {0}", response.transaction.transactionStatus);
					//Console.WriteLine("Transaction auth amount: {0}", response.transaction.authAmount);
					//Console.WriteLine("Transaction settle amount: {0}", response.transaction.settleAmount);
					type = response.transaction;
				}
			}
			return type;
		}




		public void getCustomerProfiles()
		{
			var request = new getCustomerProfileIdsRequest();

			// instantiate the controller that will call the service
			var controller = new getCustomerProfileIdsController(request);
			controller.Execute();

			// get the response from the service (errors contained if any)
			var response = controller.GetApiResponse();

			if (response != null && response.messages.resultCode == messageTypeEnum.Ok)
			{
				Console.WriteLine(response.messages.message[0].text);
				Console.WriteLine("Customer Profile Ids: ");
				foreach (var id in response.ids)
				{
					//Console.WriteLine(id);
					getCustoerTransactions(id);
				}
			}
			else if (response != null)
			{
				Console.WriteLine("Error: " + response.messages.message[0].code + "  " +
								  response.messages.message[0].text);
			}

		}


		public void getCustomerProfile(string customerProfileId)
		{
			var request = new getCustomerProfileRequest();
			request.customerProfileId = customerProfileId;

			var controller = new getCustomerProfileController(request);
			controller.Execute();

			var response = controller.GetApiResponse();

			if (response != null && response.messages.resultCode == messageTypeEnum.Ok)
			{
				Console.WriteLine(response.messages.message[0].text);
				Console.WriteLine("Customer Profile Id: " + response.profile.customerProfileId);

				if (response.subscriptionIds != null && response.subscriptionIds.Length > 0)
				{
					Console.WriteLine("List of subscriptions : ");
					for (int i = 0; i < response.subscriptionIds.Length; i++)
						Console.WriteLine(response.subscriptionIds[i]);
				}

			}
			else if (response != null)
			{
				Console.WriteLine("Error: " + response.messages.message[0].code + "  " +
								  response.messages.message[0].text);
			}

		}

		public void getCustoerTransactions(string customerProfileId)
		{
			var request = new getTransactionListForCustomerRequest();
			//request.customerProfileId = "1811474252";

			// instantiate the controller that will call the service
			var controller = new getTransactionListForCustomerController(request);
			controller.Execute();

			// get the response from the service (errors contained if any)
			var response = controller.GetApiResponse();

			if (response != null && response.messages.resultCode == messageTypeEnum.Ok)
			{
				//if (response.transactions == null)
				//    return response;

				foreach (var transaction in response.transactions)
				{
					//Console.WriteLine("Transaction Id: {0}", transaction.transId);
					//Console.WriteLine("Submitted on (Local): {0}", transaction.submitTimeLocal);
					//Console.WriteLine("Status: {0}", transaction.transactionStatus);
					//Console.WriteLine("Settle amount: {0}", transaction.settleAmount);
				}
			}
			else if (response != null)
			{
			}

		}


		public void getSubscriptionCustomerTransactions(string transactionId)
		{
			var request = new getTransactionDetailsRequest();
			request.transId = transactionId;
			// instantiate the controller that will call the service
			var controller = new getTransactionDetailsController(request);
			controller.Execute();
			// get the response from the service (errors contained if any)
			var response = controller.GetApiResponse();

			if (response != null && response.messages.resultCode == messageTypeEnum.Ok)
			{
				if (response.transaction != null)
				{

					PaymentTransaction PayObj = new PaymentTransaction();
					if (response.transaction.batch != null && response.transaction.batch.batchId != null)
					{
						PaymentBatch BatchObj = new PaymentBatch();
						BatchObj.BatchId = Int32.Parse(response.transaction.batch.batchId);
						BatchObj.settlementTimeUTC = response.transaction.batch.settlementTimeUTC;
						BatchObj.settlementTimeLocal = response.transaction.batch.settlementTimeLocal;
						BatchObj.marketType = response.transaction.batch.marketType;
						BatchObj.product = response.transaction.batch.product;
						//BatchId = new DonationRepository().AddBatches(BatchObj);
						PayObj.BatchId = BatchObj.BatchId;
					}
					PayObj.AmtTransaction = response.transaction.settleAmount;
					if (response.transaction.profile != null)
					{
						PayObj.CustomerProfileId = response.transaction.profile.customerProfileId;
						PayObj.CustomerAddressId = response.transaction.profile.customerAddressId;
					}
					if (response.transaction.transId != null)
					{
						PayObj.TransId = (response.transaction.transId).ToString();
					}
					PayObj.TransAuthCode = response.transaction.authCode;
					PayObj.TransDescription = response.transaction.transactionStatus;
					PayObj.TransResponseCode = response.transaction.responseCode.ToString();
					if (response.transaction.submitTimeLocal != null)
					{
						PayObj.CreatedOn = (response.transaction.submitTimeLocal).ToString();
					}
					if (response.transaction.subscription != null)
					{
						PayObj.PaymentConfigs.SubscriptionTransId = response.transaction.subscription.id.ToString();
					}

				}
			}

		}

		public void InvokePaymentAccount(int lkpCategory)
		{
			//string SandboxapiLoginId = "439GpmpM797Z";
			//string SandboxtransactionKey = "66D2d6Nkg98yDEsh";
			string SandboxapiLoginId = "4hZjM2X9q";
			string SandboxtransactionKey = "746hAagW28dB94rb";

			//Production - General Fund

			string ProductionapiLoginId = "4hZjM2X9q";
			string ProductiontransactionKey = "746hAagW28dB94rb";
			if (GlobalContext.VersionEnv().Trim().ToUpper() == GlobalContext.Env.TEST.ToString())
			{
				SetPaymentAccount(SandboxapiLoginId, SandboxtransactionKey, AuthorizeNet.Environment.SANDBOX);
			}
			else
			{
				try
				{
					LookupValueBL lkpVal = new LookupRepository().GetLookupValueById(lkpCategory);
					if (lkpVal != null && lkpVal.APIId != null && lkpVal.APIKey != null)
					{
						SetPaymentAccount(lkpVal.APIId, lkpVal.APIKey, AuthorizeNet.Environment.PRODUCTION);
					}
					else
					{
						SetPaymentAccount(ProductionapiLoginId, ProductiontransactionKey, AuthorizeNet.Environment.PRODUCTION);
					}
				}
				catch
				{
					SetPaymentAccount(ProductionapiLoginId, ProductiontransactionKey, AuthorizeNet.Environment.PRODUCTION);
				}

			}

		}

		public void SetPaymentAccount(string APIId, string APIKey, AuthorizeNet.Environment Env)
		{
			if (Env == AuthorizeNet.Environment.SANDBOX)
			{
				ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;
			}
			else
			{
				ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.PRODUCTION;
			}
			ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
			{
				name = APIId,
				ItemElementName = ItemChoiceType.transactionKey,
				Item = APIKey,
			};
		}




	}
}

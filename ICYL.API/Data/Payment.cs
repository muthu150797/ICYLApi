using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers.Bases;
using AuthorizeNet.Api.Controllers;
using ICYL.API.Entity;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Newtonsoft.Json;
using Nancy.Json;
using System.Text.Json;
using ICYL.Repository;
using ICYL.API.Repository;
using ICYL.API.Helpers;
using System.Globalization;

namespace ICYL.API.Data
{
	public class Payment
	{
		private readonly string _connectionstring;
		private readonly SqlConnection con;
		private readonly string _paypalClientId;
		private readonly string _paypalSecretKey;
		private readonly string _authorizeUrl;
		private readonly string _ordersURL;
		public nameAndAddressType BillTo { get; private set; }
		public nameAndAddressType MailTo { get; private set; }
		paymentScheduleTypeInterval interval = new paymentScheduleTypeInterval();
		int occurrences = 9999;// 999 indicates no end date else add code based on the interval length
		paymentScheduleType schedule = new paymentScheduleType();
		int PaymentConfigId = 0;
		DonationRepository clsDonationRepository = new DonationRepository();
		ICYLAuthorizeNet ICYLAuthorize = new ICYLAuthorizeNet();
		PaymentTransaction ObjPaymentTransaction = new PaymentTransaction();
		//List<PaymentTransaction> response = new List<PaymentTransaction>();
		List<SubscriptionTransaction> SubscriptionRes = new List<SubscriptionTransaction>();
		createTransactionRequest result = new createTransactionRequest();
		SubscriptionTransaction SubTrans = new SubscriptionTransaction();
		SubscriptionTransaction SubsTransaction = new SubscriptionTransaction();
		short intervalLength = 0;
		PaymentResponse response = new PaymentResponse();

		bool IsSuccessful = true;
		string TransErrorText = string.Empty;
		public Payment()
		{
			var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false).Build();
			_connectionstring = config.GetSection("DbConnection").Value;
			_paypalClientId = "ATQ5ks1QPEAdiXreZnc6VpTLYdM7ullvwqlNu1qBd9_4DsR5IF4zT_On6aJGNVri5h_EAehpReW5UkwC"; //config.GetSection("PaypalAccount:PaypalClientId").Value;
			_paypalSecretKey = ""; //config.GetSection("PaypalAccount:PaypalSecretkey").Value;
			_authorizeUrl = config.GetSection("PaypalAccount:AuthorizeURL").Value;
			_ordersURL = config.GetSection("PaypalAccount:OrdersURL").Value;

			con = new SqlConnection(_connectionstring);
		}
		public dynamic GetAllTransaction(TransDailyReport report)
		{
			var response = ICYLAuthorize.GetAllTransaction(report);
			return response;

		}
		public dynamic DonateByApplePay(string token)
		{
			var response = ICYLAuthorize.DonateByApplePay(token);// payment.Donate(model);
			return response;

		}
		public dynamic GetAllSubscription()
		{
			var response = ICYLAuthorize.GetAllSubscription();
			return response;

		}
		public dynamic CancelSubscription(string subscriptionId)
		{
			var response = ICYLAuthorize.CancelSubscription(subscriptionId);
			return response;
		}
		public TransactionResponse InsertPaymentDetail(PaymentModel paymentDetail)
		{
			TransactionResponse response = new TransactionResponse();
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionstring))
				{
					connection.Open();
					SqlCommand cmd = new SqlCommand("sp_insertPaymentDetails", connection);
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("@FirstName", paymentDetail.FirstName);
					cmd.Parameters.AddWithValue("@LastName", paymentDetail.LastName);
					cmd.Parameters.AddWithValue("@Email", paymentDetail.Email);
					cmd.Parameters.AddWithValue("@PaymentConfigId", paymentDetail.PaymentConfigId);
					cmd.Parameters.AddWithValue("@AmtDonation", paymentDetail.AmtDonation);
					//cmd.Parameters.AddWithValue("@AmtTransaction", paymentDetail.AmtTransaction);
					cmd.Parameters.AddWithValue("@AmtTransactionPaid", paymentDetail.AmtTransactionPaid);
					cmd.Parameters.AddWithValue("@lkpDonationCategory", paymentDetail.lkpDonationCategory);
					cmd.Parameters.AddWithValue("@IsCreditCard", paymentDetail.IsCreditCard);
					cmd.Parameters.AddWithValue("@BatchId", paymentDetail.BatchId);
					cmd.Parameters.AddWithValue("@TransId", paymentDetail.TransId);
					cmd.Parameters.AddWithValue("@TransDescription", paymentDetail.TransDescription);
					cmd.Parameters.AddWithValue("@settlementState", paymentDetail.settlementState);
					cmd.Parameters.AddWithValue("@RetDesc", "");
					cmd.Parameters.AddWithValue("@RetVal", 0);
					SqlParameter retValParam = cmd.Parameters.Add("@RetVal", SqlDbType.Int);
					retValParam.Direction = ParameterDirection.ReturnValue;
					SqlParameter retDesc = cmd.Parameters.Add("@RetDesc", SqlDbType.VarChar);
					retDesc.Direction = ParameterDirection.ReturnValue;
					var isInserted = cmd.ExecuteNonQuery();
					//var userId = cmd.ExecuteScalar();
					int status = (int)retValParam.Value;
					if (status == 1)
					{
						response.Status = true;
						response.Message = "Payment details added successfully";
					}
					else
					{
						response.Status = false;
						response.Message = "Payment details added failed, try again!";
					}
				}
			}
			catch (Exception ex)
			{
				response.Status = false;
				response.Message = ex.Message.ToString();
			}
			return response;
		}
		public ANetApiResponse AddPayment()
		{
			ANetApiResponse response = new ANetApiResponse();
			try
			{
				decimal amount = new decimal(12.01);
				string apiLoginId = "4hZjM2X9q";
				string transactionKey = "3W676uwwvDj964PZ";
				response = ChargeCreditCard.Run(apiLoginId, transactionKey, amount);
				return response;
			}
			catch (Exception ex)
			{

			}
			return response;
		}
		public dynamic Donate(PaymentConfig model)
		{
			try
			{
				//using (var client = new HttpClient())
				//{
				//	var json = JsonConvert.SerializeObject(model);
				//http://localhost:44309
				//	var data = new StringContent(json, Encoding.UTF8, "application/json");
				//	var url = "http://localhost:44309/Home/Donate";
				//	var ressponse = client.PostAsync(url, data).Result;

				if ((bool)!model.IsRecurring)
				{
					model.RecurringType = int.Parse("0");
				}
				if (!model.IsTransactionFeesIncluded)
				{
					model.AmtTransactionPaid = 0;
				}
				customerAddressType CustomerInfo = new customerAddressType
				{
					firstName = model.FirstName,
					lastName = model.LastName,
					address = model.Address,
					city = model.BillingCity,
					zip = model.BillingZip,
					email = model.EmailId,
					phoneNumber = model.PhoneNumber,
					company = model.CompanyName,
					state = model.BillingState
				};
				customerDataType CustData = new customerDataType
				{
					email = model.EmailId
				};
				Donations donation = new Donations();

				orderType OrderInfo = new orderType
				{
					description = donation.GetDonationCategory(model.lkpDonationCategory)// new EmailRepository().FindCategory(model.lkpDonationCategory.ToString())
				};
				//};
				BillTo = new nameAndAddressType
				{
					firstName = model.FirstName,
					lastName = model.LastName,
					address = model.Address,
					city = model.BillingCity,
					zip = model.BillingZip

				};
				MailTo = new nameAndAddressType
				{
					firstName = model.FirstName,
					lastName = model.LastName,
					address = model.MailingAddress,
					city = model.MailingCity,
					zip = model.MailingZip,
					state = model.MailingState
				};
				customerType RecurringCustInfo = new customerType()
				{
					email = model.EmailId,
					phoneNumber = model.PhoneNumber,
					type = customerTypeEnum.individual
				};
				//Recurring Settings
				if (model.IsRecurring)
				{
					string DateString = model.dtPaymentStart;
					string EndDateString = model.dtPaymentEnd;
					IFormatProvider culture = new CultureInfo("en-US", true);
					var dateVal = DateTime.ParseExact(DateString, "yyyy-MM-dd", culture).ToString();
					var EndDateVal = DateTime.ParseExact(EndDateString, "yyyy-MM-dd", culture).ToString();
					short intervalLength = (short)(model.RecurringType);
					interval.length = intervalLength;       // months can be indicated between 1 and 12 ; TODO. calculate based on recurring 
					if (model.RecurringType == Convert.ToInt32(GlobalContext.RecurringType.Week))
					{
						interval.unit = ARBSubscriptionUnitEnum.days;
						model.RecurringInterval = ARBSubscriptionUnitEnum.days.ToString();
					}
					else
					{
						interval.unit = ARBSubscriptionUnitEnum.months;
						model.RecurringInterval = ARBSubscriptionUnitEnum.months.ToString();
					}

					if (model.PaymentEndType == (int)(GlobalContext.PaymentEndType.EndDate))
					{


						if (model.RecurringType == Convert.ToInt32(GlobalContext.RecurringType.Week))
						{

							occurrences = ICYLHelpers.CalculateWeeks(Convert.ToDateTime(dateVal), Convert.ToDateTime(EndDateVal));
						}
						else
						{
							occurrences = ICYLHelpers.CalculateMonths(Convert.ToDateTime(dateVal), Convert.ToDateTime(EndDateVal));
						}
						model.PaymentMaxOccurences = occurrences;
					}
					else if (model.PaymentEndType == ((int)GlobalContext.PaymentEndType.occurrences))
					{
						dynamic endDate;
						occurrences = Convert.ToInt32(model.PaymentMaxOccurences);
						if (model.dtPaymentStart != null)
						{
							if (interval.unit == ARBSubscriptionUnitEnum.days)
							{
								endDate = Convert.ToDateTime(dateVal).AddDays(occurrences * Convert.ToInt32(model.RecurringType));
							}
							else
							{
								endDate = Convert.ToDateTime(dateVal).AddMonths(occurrences * Convert.ToInt32(model.RecurringType));
							}
							//model.dtPaymentEnd = endDate;
						}
					}
					schedule = new paymentScheduleType
					{
						interval = interval,
						startDate = Convert.ToDateTime(dateVal),// Convert.ToDateTime(model.dtPaymentStart),      // start date should be tomorrow  //model.PaymentEndType
						totalOccurrences = (short)occurrences,
						trialOccurrences = 0,
						trialOccurrencesSpecified = false

					};
				}
				if (model.PaymentType == ((int)GlobalContext.PaymentType.CreditCard))
				{

					creditCardType creditCardInfo = new creditCardType
					{
						cardNumber = model.CCNum,
						expirationDate = model.CCExpiry,
						cardCode = model.CCCvc

					};
					if (model.IsRecurring)
					{
						SubTrans = ICYLAuthorize.SetUpRecurringCharge(model.AmtTotal, interval, schedule, creditCardInfo, BillTo, MailTo, RecurringCustInfo, model.lkpDonationCategory);
						if (SubTrans != null)
						{
							if ((SubTrans.SubscriptionTransId) != null)
							{
								IsSuccessful = true;
								response.Status = true;
								response.TransactionId = SubTrans.SubscriptionTransId;
								response.ResponseCode = SubTrans.SubscriptionResponseCode;
								response.ResponseText = SubTrans.SubscriptionResponseText;
							}
							else
							{
								IsSuccessful = false;
								response.Status = false;
								TransErrorText = (SubTrans.SubscriptionResponseText);
								response.ResponseCode = SubTrans.SubscriptionResponseCode;
								response.ResponseText = (SubTrans.SubscriptionResponseText);
							}
							SubscriptionRes.Add(SubsTransaction);
						}
						else
						{
							IsSuccessful = false;
							response.Status = false;
							TransErrorText = (SubTrans.SubscriptionResponseText);
							response.ResponseCode = SubTrans.SubscriptionResponseCode;
							response.ResponseText = (SubTrans.SubscriptionResponseText);
						}
					}
				}

				if (model.PaymentType == Convert.ToInt32(GlobalContext.PaymentType.CreditCard))
				{
					creditCardType creditCardInfo = new creditCardType
					{
						cardNumber = model.CCNum,
						expirationDate = model.CCExpiry,
						cardCode = model.CCCvc

					};
					if (model.IsRecurring)
					{
						SubTrans = ICYLAuthorize.SetUpRecurringCharge(model.AmtTotal, interval, schedule, creditCardInfo, BillTo, OrderInfo, MailTo, RecurringCustInfo, model.lkpDonationCategory);
						if (SubTrans != null)
						{
							if ((SubTrans.SubscriptionTransId) != null)
							{
								IsSuccessful = true;
								response.Status = true;
								response.TransactionId = SubTrans.SubscriptionTransId;
								response.ResponseCode = SubTrans.SubscriptionResponseCode;
								response.ResponseText = SubTrans.SubscriptionResponseText;
							}
							else
							{
								IsSuccessful = false;
								TransErrorText = (SubTrans.SubscriptionResponseText);
								response.ResponseCode = (SubTrans.SubscriptionResponseText);
								response.ResponseText = (SubTrans.SubscriptionResponseText);
								model.SubscriptionResponseText = (SubTrans.SubscriptionResponseText);
								model.SubscriptionResponseCode = SubTrans.SubscriptionResponseCode;
							}
							SubscriptionRes.Add(SubsTransaction);
						}
						else
						{
							IsSuccessful = false;
							TransErrorText = (SubsTransaction.SubscriptionResponseText);
							response.ResponseCode = (SubTrans.SubscriptionResponseCode);
							response.Status = false;
							response.ResponseText = (SubTrans.SubscriptionResponseText);
						}
					}
					else
					{
						ObjPaymentTransaction = ICYLAuthorize.ICYLChargeCreditCard(model.AmtTotal, creditCardInfo, CustomerInfo, OrderInfo, MailTo, CustData, model.lkpDonationCategory);
						if (ObjPaymentTransaction != null)
						{
							//var errortext = ObjPaymentTransaction.TransErrorText.Trim();
							if (ObjPaymentTransaction.TransErrorText != null)
							{
								IsSuccessful = false;
								ObjPaymentTransaction.
								TransErrorText = (ObjPaymentTransaction.TransErrorText);
								response.Status = false;
								response.ResponseCode = (ObjPaymentTransaction.TransResponseCode) == null ? "0" : (ObjPaymentTransaction.TransResponseCode);
								response.ResponseText = (ObjPaymentTransaction.TransDescription) == null ? "0" : (ObjPaymentTransaction.TransDescription);
							}
							else
							{
								model.IsCreditCard = true;
								IsSuccessful = true;
								response.Status = true;
								response.TransactionId = ObjPaymentTransaction.TransId;
								response.ResponseCode = (ObjPaymentTransaction.TransResponseCode);
								response.ResponseText = (ObjPaymentTransaction.TransDescription);
							}
						}
					}
				}
				//Paypal transaction
				else if (model.PaymentType == ((int)GlobalContext.PaymentType.Paypal))
				{
					creditCardType creditCardInfo = new creditCardType
					{
						cardNumber = model.CCNum,
						expirationDate = model.CCExpiry,
						cardCode = model.CCCvc
					};
					if (model.IsRecurring)//checking recurring detail for paypal
					{
						SubTrans = ICYLAuthorize.SetUpRecurringCharge(model.AmtTotal, interval, schedule, creditCardInfo, BillTo, OrderInfo, MailTo, RecurringCustInfo, model.lkpDonationCategory);
						if (SubsTransaction != null)
						{
							if ((SubTrans.SubscriptionTransId).Trim().Length > 0)
							{
								IsSuccessful = true;
								model.SubscriptionTransId = SubTrans.SubscriptionTransId;
								model.SubscriptionResponseCode = SubTrans.SubscriptionResponseCode;
								model.SubscriptionResponseText = SubTrans.SubscriptionResponseText;
							}
							else
							{
								IsSuccessful = false;
								TransErrorText = (SubsTransaction.SubscriptionResponseText);
							}
							SubscriptionRes.Add(SubsTransaction);
						}
						else
						{
							IsSuccessful = false;
							TransErrorText = (SubsTransaction.SubscriptionResponseText);
						}
					}
					else
					{
						//Paypal transaction
						var paypalTransaction = ICYLAuthorize.ICYLPaypal(model.AmtTotal, creditCardInfo, CustomerInfo, OrderInfo, MailTo, CustData, model.lkpDonationCategory);
						//if (ObjPaymentTransaction != null)
						//{
						//	if (Conversion.ConversionToString(ObjPaymentTransaction.TransErrorText).Trim().Length > 0)
						//	{
						//		IsSuccessful = false;
						//		ObjPaymentTransaction.
						//		TransErrorText = Conversion.ConversionToString(ObjPaymentTransaction.TransErrorText);
						//	}
						//}
						return null;
					}
				}
				if (IsSuccessful)
				{
					PaymentModel detail = new PaymentModel();
					detail.AmtDonation = (decimal)model.AmtDonation;
					detail.AmtTransactionPaid = (decimal)model.AmtTransactionPaid;
					detail.PaymentConfigId = 0;
					detail.settlementState = "settlement succefully";
					detail.FirstName = model.FullName;
					detail.LastName = model.LastName;
					detail.Email = model.EmailId;
					detail.lkpDonationCategory = model.lkpDonationCategory;
					detail.AmtTransaction = detail.AmtTransaction;
					detail.TransId = response.TransactionId;
					detail.TransDescription = "";
					detail.BatchId = 1;
					detail.IsCreditCard = Convert.ToByte(model.IsCreditCard);// (dynamic)model.IsCreditCard;
					var res = InsertPaymentDetail(detail);
					if (res.Status == true)
						response.paymentStatus = "Payment details added successfully";
					else
						response.paymentStatus = "Payment details failed to add";
				}
				else
				{
					response.Status = false;
					response.paymentStatus = "Payment details failed to add";
				}

			}
			catch (Exception ex)
			{
				response.Status = false;
				response.paymentStatus = ex.Message;
			}
			return response;
		}

		//public string GetPaypalPaymentUrl(OrdersModel orders)
		//{
		//	//getting access token to generate URL for the next step
		//	using (var client = new HttpClient())
		//	{
		//		try
		//		{
		//			var data = new[]
		//			{
		//				new KeyValuePair<string, string>("grant_type", "client_credentials")
		//			};
		//			// var clineId = "ATQ5ks1QPEAdiXreZnc6VpTLYdM7ullvwqlNu1qBd9_4DsR5IF4zT_On6aJGNVri5h_EAehpReW5UkwC";
		//			//var secretKey = "EA5dV0apctu6vBOLDjCSzMVID6ix7SRgPlRo3p1Ga7mvnuUFXIT_gss8BTp0idhZBYGlr2eXpl1jn64a";
		//			var byteArray = Encoding.ASCII.GetBytes(_paypalClientId + ":" + _paypalSecretKey);
		//			client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
		//			//var url = "https://api-m.sandbox.paypal.com/v1/oauth2/token";
		//			//var response =  client.PostAsync(url, data);
		//			var response = client.PostAsync(_authorizeUrl, new FormUrlEncodedContent(data)).GetAwaiter().GetResult();
		//			string result =  response.Content.ReadAsStringAsync().Result;
		//			if ((int)response.StatusCode == 200)
		//			{
		//				PaypalTransactions paypalModel = JsonConvert.DeserializeObject<PaypalTransactions>(result);
		//				var returnUrl = GetURLByToken(paypalModel.AccessToken, orders);
		//				return returnUrl.ToString();
		//			}
		//			elfalse
		//			{
		//				return "Authorizing user is failed,please check client id and secret key";
		//			}

		//		}
		//		catch (Exception ex)
		//		{

		//		}
		//		return "";
		//	}
		//}
		//public string GetURLByToken(string accessToken, OrdersModel orders)
		//{
		//	//getting return url of paypal
		//	using (var client = new HttpClient())
		//	{
		//		try
		//		{
		//			OrdersModel model = orders;
		//			var options = new JsonSerializerOptions { WriteIndented = true };
		//			string jsonString = System.Text.Json.JsonSerializer.Serialize(orders, options);
		//			var json = JsonConvert.SerializeObject(model.ToString());
		//			client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
		//			var data = new StringContent(jsonString, Encoding.UTF8, "application/json");
		//			 var response =  client.PostAsync(_ordersURL, data).GetAwaiter().GetResult();
		//			if ((int)response.StatusCode==200 || (int)response.StatusCode == 201)
		//			{
		//				string result = response.Content.ReadAsStringAsync().Result;
		//				String returnUrl = "";
		//				ReturnURLModel returnURL = JsonConvert.DeserializeObject<ReturnURLModel>(result);
		//				for(int i=0; i < returnURL.Links.Count; i++)
		//				{
		//					if (returnURL.Links[i].Rel == "approve")
		//					{
		//						returnUrl = returnURL.Links[i].Href;
		//					}
		//				}
		//				return returnUrl;
		//			}
		//			else
		//			{
		//				return "";
		//			}
		//		}
		//		catch (Exception ex)
		//		{

		//		}
		//	}
		//	return "";
		//}

	}
}

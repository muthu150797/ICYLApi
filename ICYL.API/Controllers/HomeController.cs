using ICYL.API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AuthorizeNet.Api.Contracts.V1;
using System.Threading;
using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using ICYL.API.Entity;
using PaymentTransaction = ICYL.API.Entity.PaymentTransaction;
//using Newtonsoft
using System.Text;
using PaymentConfig = ICYL.API.Entity.PaymentConfig;
using Newtonsoft.Json;
using ICYL.Repository;
using ICYL.API.Helpers;

namespace ICYL.API.Controllers
{


	[Route("api/[controller]")]
	[ApiController]
	//[EnableCors("_myAllowSpecificOrigins")]
	public class HomeController : ControllerBase
	{
	
		public nameAndAddressType BillTo { get; private set; }
		public nameAndAddressType MailTo { get; private set; }
		paymentScheduleTypeInterval interval = new paymentScheduleTypeInterval();
		int occurrences = 9999;// 999 indicates no end date else add code based on the interval length
		paymentScheduleType schedule = new paymentScheduleType();
		int PaymentConfigId = 0;
		DonationRepository clsDonationRepository = new DonationRepository();
		ICYLAuthorizeNet ICYLAuthorize = new ICYLAuthorizeNet();
		PaymentTransaction ObjPaymentTransaction = new PaymentTransaction();
		List<PaymentTransaction> response = new List<PaymentTransaction>();
		List<SubscriptionTransaction> SubscriptionRes = new List<SubscriptionTransaction>();
		createTransactionRequest result = new createTransactionRequest();
		SubscriptionTransaction SubTrans = new SubscriptionTransaction();
		SubscriptionTransaction SubsTransaction = new SubscriptionTransaction();
		short intervalLength = 0;
		bool IsSuccessful = true;
		string TransErrorText = string.Empty;
		
		[HttpPost]
		[Route("Donate")]
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
				orderType OrderInfo = new orderType
				{
					description = "jg"// new EmailRepository().FindCategory(model.lkpDonationCategory.ToString())
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
							occurrences = ICYLHelpers.CalculateWeeks(Convert.ToDateTime(model.dtPaymentStart), Convert.ToDateTime(model.dtPaymentEnd));
						}
						else
						{
							occurrences = ICYLHelpers.CalculateMonths(Convert.ToDateTime(model.dtPaymentStart), Convert.ToDateTime(model.dtPaymentEnd));
						}
						model.PaymentMaxOccurences = occurrences;
					}
					else if (model.PaymentEndType == ((int)GlobalContext.PaymentEndType.occurrences))
					{
						dynamic? endDate = null;
						occurrences = Convert.ToInt32(model.PaymentMaxOccurences);
						if (model.dtPaymentStart != null)
						{
							if (interval.unit == ARBSubscriptionUnitEnum.days)
							{
								endDate = Convert.ToDateTime(model.dtPaymentStart).AddDays(occurrences * Convert.ToInt32(model.RecurringType));
							}
							else
							{
								endDate = Convert.ToDateTime(model.dtPaymentStart).AddMonths(occurrences * Convert.ToInt32(model.RecurringType));
							}
							model.dtPaymentEnd = endDate;
						}
					}
					schedule = new paymentScheduleType
					{
						interval = interval,
						startDate = Convert.ToDateTime( model.dtPaymentStart),      // start date should be tomorrow  //model.PaymentEndType
						totalOccurrences =(short)occurrences,
						trialOccurrences = 0,
						trialOccurrencesSpecified = false

					};
					if (model.PaymentType == ((int)GlobalContext.PaymentType.CreditCard))
					{

						creditCardType creditCardInfo = new creditCardType
						{
							cardNumber = model.CCNum,
							expirationDate = model.CCExpiry,
							cardCode = model.CCCvc

						};
						//model.IsCreditCard = true;
						if (model.IsRecurring)
						{
							PaymentResponse response = new PaymentResponse();

							SubTrans = ICYLAuthorize.SetUpRecurringCharge(model.AmtTotal, interval, schedule, creditCardInfo, BillTo, MailTo, RecurringCustInfo, model.lkpDonationCategory);
							if (SubTrans != null)
							{
								if ((SubTrans.SubscriptionTransId)!=null)
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
								TransErrorText = (SubTrans.SubscriptionResponseText);
								response.ResponseCode = SubTrans.SubscriptionResponseCode;
								response.ResponseText = (SubTrans.SubscriptionResponseText);
							}
							return response;
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
					//model.IsCreditCard = true;

					if (model.IsRecurring)
					{
						PaymentResponse response = new PaymentResponse();
						SubTrans = ICYLAuthorize.SetUpRecurringCharge(model.AmtTotal, interval, schedule, creditCardInfo, BillTo, OrderInfo, MailTo, RecurringCustInfo, model.lkpDonationCategory);
						if (SubTrans != null)
						{
							if ((SubTrans.SubscriptionTransId)!=null)
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
								model.SubscriptionResponseText= (SubTrans.SubscriptionResponseText);
								model.SubscriptionResponseCode = SubTrans.SubscriptionResponseCode;
							}
							SubscriptionRes.Add(SubsTransaction);
						}
						else
						{
							IsSuccessful = false;
							TransErrorText = (SubsTransaction.SubscriptionResponseText);
							response.ResponseCode = (SubTrans.SubscriptionResponseCode);

							response.ResponseText = (SubTrans.SubscriptionResponseText);
						}
						return response;
					}
					else
					{
						PaymentResponse response = new PaymentResponse();
						ObjPaymentTransaction = ICYLAuthorize.ICYLChargeCreditCard(model.AmtTotal, creditCardInfo, CustomerInfo, OrderInfo, MailTo, CustData, model.lkpDonationCategory);
						if (ObjPaymentTransaction != null)
						{
							//var errortext = ObjPaymentTransaction.TransErrorText.Trim();
							if (ObjPaymentTransaction.TransErrorText != null)
							{
								IsSuccessful = false;
								ObjPaymentTransaction.
								TransErrorText = (ObjPaymentTransaction.TransErrorText);
								response.ResponseCode = (ObjPaymentTransaction.TransResponseCode);
								response.ResponseText = (ObjPaymentTransaction.TransErrorText);
							}
							else
							{
								response.Status = true;
								response.ResponseCode = (ObjPaymentTransaction.TransResponseCode);
								response.ResponseText = (ObjPaymentTransaction.TransErrorText);
							}
							return response;

						}
					}
				}
			}
			catch (Exception ex)
			{

			}
			return ObjPaymentTransaction;
		}

		[HttpPost]
		[Route("InsertPayment")]
		public async Task<bool> InsertPayment(PaymentConfig model)
		{
			using (var client = new HttpClient())
			{
				//  var json = JsonConvert.SerializeObject(model);
				//http://localhost:44309
				// var data = new StringContent(json, Encoding.UTF8, "application/json");
				// var url = "http://localhost:44309/Home/Donate";
				// var response = await client.PostAsync(url, data);
				// string result = response.Content.ReadAsStringAsync().Result;
			}

			// ViewBag.Message = "";
			int PaymentConfigId = 0;
			DonationRepository clsDonationRepository = new DonationRepository();
			ICYLAuthorizeNet ICYLAuthorize = new ICYLAuthorizeNet();
			Entity.PaymentTransaction ObjPaymentTransaction = new Entity.PaymentTransaction();

			nameAndAddressType BillTo = new nameAndAddressType();
			nameAndAddressType MailTo = new nameAndAddressType();
			paymentScheduleTypeInterval interval = new paymentScheduleTypeInterval();
			int? occurrences = 9999;// 999 indicates no end date else add code based on the interval length
			paymentScheduleType schedule = new paymentScheduleType();

			short intervalLength = 0;
			bool IsSuccessful = true;
			string TransErrorText = string.Empty;
			//int milliseconds = 2000;
			//Thread.Sleep(milliseconds);
			//Validate(model);
			if (ModelState.IsValid)
			{

				//new EmailRepository().EmailDonation(model);
				//return RedirectToAction("Confirmation");
				if (!(bool)model.IsRecurring)
				{
					model.RecurringType = int.Parse("0");
				}
				if (!(bool)model.IsTransactionFeesIncluded)
				{
					model.AmtTransactionPaid = 0;
				}

				//ICYLAuthorize.CCCardNumber = "4111111111111111";
				//ICYLAuthorize.CCExpirationDate = "1028";
				//ICYLAuthorize.CCCardCode = "123";

				//ICYLAuthorize.CustomerFirstName = "John";
				//ICYLAuthorize.CustomerLastName = "John";
				//ICYLAuthorize.CustomerAddress = "123 My St";
				//ICYLAuthorize.CustomerCity = "OurTown";
				//ICYLAuthorize.CustomerZip = "98004";

				//Test Routing Number:
				//021000021
				//011401533
				//09100001
				//Test Account Number:
				//111111111
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
				orderType OrderInfo = new orderType
				{
					description = null /*new EmailRepository().FindCategory(model.lkpDonationCategory.ToString())*/
				};
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
				if ((bool)model.IsRecurring)
				{
					//intervalLength = Convert.ToInt16(model.RecurringType);
					//interval.length = intervalLength;       // months can be indicated between 1 and 12 ; TODO. calculate based on recurring 
					//    //if (model.RecurringType == ((int)GlobalContext.RecurringType.Week).ToString())
					//    //{
					//    //    interval.unit = ARBSubscriptionUnitEnum.days;
					//    //    model.RecurringInterval= ARBSubscriptionUnitEnum.days.ToString();
					//    //}
					//    else
					//{
					//    interval.unit = ARBSubscriptionUnitEnum.months;
					//    model.RecurringInterval = ARBSubscriptionUnitEnum.months.ToString();
					//}

					//if (model.PaymentEndType == ((int)GlobalContext.PaymentEndType.EndDate).ToString())
					//{
					//    if (model.RecurringType == ((int)GlobalContext.RecurringType.Week).ToString())
					//    {
					//        //occurrences = ICYLHelpers.CalculateWeeks(Convert.ToDateTime(model.dtPaymentStart), Convert.ToDateTime(model.dtPaymentEnd));
					//    }
					//    else
					//    {
					//        //occurrences = ICYLHelpers.CalculateMonths(Convert.ToDateTime(model.dtPaymentStart), Convert.ToDateTime(model.dtPaymentEnd));
					//    }
					//    model.PaymentMaxOccurences = occurrences;
					//}
					//else if (model.PaymentEndType == ((int)GlobalContext.PaymentEndType.occurrences).ToString())
					//{
					//    DateTime? endDate = null;
					//    occurrences =(int)(model.PaymentMaxOccurences);
					//    if (model.dtPaymentStart != null)
					//    {
					//        if (interval.unit == ARBSubscriptionUnitEnum.days)
					//        {
					//            endDate = Convert.ToDateTime(model.dtPaymentStart).AddDays((int)occurrences * int.Parse(model.RecurringType));
					//        }
					//        else
					//        {
					//            endDate = Convert.ToDateTime(model.dtPaymentStart).AddMonths((int)occurrences * int.Parse(model.RecurringType));
					//        }
					//        model.dtPaymentEnd = endDate;
					//    }

					//}
					//schedule = new paymentScheduleType
					//{
					//    interval = interval,
					//    startDate = Convert.ToDateTime(model.dtPaymentStart),      // start date should be tomorrow  //model.PaymentEndType
					//    totalOccurrences = Convert.ToInt16(occurrences),
					//    trialOccurrences = 0,
					//    trialOccurrencesSpecified = false

					//};

				}

				//Credit Card
				if (true)
				{
					creditCardType creditCardInfo = new creditCardType
					{
						cardNumber = model.CCNum,
						expirationDate = model.CCExpiry,
						cardCode = model.CCCvc

					};
					model.IsCreditCard = true;
					if (false/*(bool)model.IsRecurring*/)
					{
						string SubTrans = null;
						//SubscriptionTransaction SubTrans = ICYLAuthorize.SetUpRecurringCharge((decimal)model.AmtTotal, interval, schedule, creditCardInfo, BillTo, OrderInfo, MailTo, RecurringCustInfo, (int)model.lkpDonationCategory);
						if (SubTrans != null)
						{
							//if (SubTrans.SubscriptionTransId.ToString().Trim().Length > 0)
							//{
							//    IsSuccessful = true;
							//    model.SubscriptionTransId = SubTrans.SubscriptionTransId;
							//    model.SubscriptionResponseCode = SubTrans.SubscriptionResponseCode;
							//    model.SubscriptionResponseText = SubTrans.SubscriptionResponseText;
							//}
							//else
							//{
							//    IsSuccessful = false;
							//    TransErrorText = SubTrans.SubscriptionResponseText;
							//}
						}
						else
						{
							IsSuccessful = false;
							// TransErrorText = SubTrans.SubscriptionResponseText.ToString();
						}
					}
					else
					{
						ObjPaymentTransaction = ICYLAuthorize.ICYLChargeCreditCard((decimal)model.AmtTotal, creditCardInfo, CustomerInfo, OrderInfo, MailTo, CustData, (int)model.lkpDonationCategory);
						if (ObjPaymentTransaction != null)
						{
							if (ObjPaymentTransaction.TransErrorText.ToString().Trim().Length > 0)
							{
								IsSuccessful = false;
								TransErrorText = ObjPaymentTransaction.TransErrorText.ToString();
							}
						}
					}
				}
				//eCheck
				//else if (model.PaymentType == ((int)GlobalContext.PaymentType.eCheck).ToString())
				//{
				//    model.IsECheck = true;
				//    var bankAccount = new bankAccountType
				//    {
				//        accountType = bankAccountTypeEnum.checking,
				//        routingNumber = model.BankRoutingNum,
				//        accountNumber = model.BankAccountNum,
				//        nameOnAccount = string.Format("{0} {1}", model.FirstName, model.LastName),
				//        echeckType = echeckTypeEnum.WEB,   // change based on how you take the payment (web, telephone, etc)
				//        bankName = model.BankNameOnAccount,
				//        // checkNumber     = "101"                 // needed if echeckType is "ARC" or "BOC"
				//    };
				//    if (model.IsRecurring)
				//    {

				//        SubscriptionTransaction SubTrans = ICYLAuthorize.SetUpRecurringCharge(model.AmtTotal, interval, schedule, bankAccount, BillTo,MailTo,RecurringCustInfo, model.lkpDonationCategory);
				//        if (SubTrans != null)
				//        {
				//            if (SubTrans.SubscriptionTransId.ToString().Trim().Length > 0)
				//            {
				//                IsSuccessful = true;
				//                model.SubscriptionTransId = SubTrans.SubscriptionTransId;
				//                model.SubscriptionResponseCode = SubTrans.SubscriptionResponseCode;
				//                model.SubscriptionResponseText = SubTrans.SubscriptionResponseText;
				//            }
				//            else
				//            {
				//                IsSuccessful = false;
				//                TransErrorText = SubTrans.SubscriptionResponseText.ToString();
				//            }
				//        }
				//        else
				//        {
				//            IsSuccessful = false;
				//            TransErrorText = SubTrans.SubscriptionResponseText.ToString();
				//        }
				//    }
				//    else
				//    {
				//        ObjPaymentTransaction = ICYLAuthorize.ICYLeCheck(model.AmtTotal, bankAccount, CustomerInfo, OrderInfo,MailTo,CustData, model.lkpDonationCategory);
				//        if (ObjPaymentTransaction != null)
				//        {
				//            if (ObjPaymentTransaction.TransErrorText.ToString().Trim().Length > 0)                            
				//            {
				//                IsSuccessful = false;
				//                TransErrorText = ObjPaymentTransaction.TransErrorText.ToString();
				//            }
				//        }                        
				//    }
				//}

				// Test
				if (IsSuccessful)
				{
					//int rValue = 0;
					//PaymentConfigId = clsDonationRepository.AddPayments(model, 1);

					//ObjPaymentTransaction.PaymentConfigId = PaymentConfigId;
					//ObjPaymentTransaction.AmtTransaction = model.AmtTotal;

					//if (ObjPaymentTransaction != null && ObjPaymentTransaction.TransId != null)
					//{
					//    rValue = clsDonationRepository.InsertPaymentTransaction(ObjPaymentTransaction, 1);
					//}
					if (PaymentConfigId > 0)
					{
						//ReceiptBL attmt = new ReceiptBL();
						if (ObjPaymentTransaction.TransId != null)
							model.ConfirmationNumber = ObjPaymentTransaction.TransId.ToString();
						//attmt = new EmailRepository().Receipt(model);
						//attmt.TransactionDate = DateTime.Now;
						//new EmailRepository().EmailDonation(attmt);
					}
					return IsSuccessful;
				}
				else
				{
					/// ViewBag.err = TransErrorText;
					//Dropdown(model);
					return IsSuccessful;
				}

			}
			return IsSuccessful;

			//Dropdown(model);
		}

		//private void Validate(PaymentConfig model)
		//{
		//    if (model.IsRecurring &&  model.PaymentEndType == ((int)GlobalContext.PaymentEndType.EndDate).ToString())
		//    {
		//        if (model.dtPaymentEnd==null)
		//        {
		//            ModelState.AddModelError(string.Empty, string.Format("ICYL Alert, Please enter an End Date."));
		//        }
		//    }
		//    else if (model.IsRecurring && model.PaymentEndType == ((int)GlobalContext.PaymentEndType.occurrences).ToString())
		//    {
		//        if (model.PaymentMaxOccurences ==null ||  model.PaymentMaxOccurences < 1)
		//        {
		//            ModelState.AddModelError(string.Empty, string.Format("ICYL Alert, Please enter the Occurrences (Minimum 1)."));
		//        }
		//    }

		//}

		//private void Dropdown(PaymentConfig model)
		//{
		//    model.lstRecurringType = new LookupRepository().getRecurringTypeDropDown();
		//    model.lstCategory = new LookupRepository().getCategoryDropDown();
		//}


		//public ActionResult Confirmation()
		//{

		//    return View();
		//}

	}
}
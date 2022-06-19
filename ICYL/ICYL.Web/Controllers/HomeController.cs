using ICYL.BL;
using ICYL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AuthorizeNet.Api.Contracts.V1;
using ICYL.Web.Helpers;
using EnterpriseLayer.Utilities;
using System.Threading;
using System.Collections;

namespace ICYL.Web.Controllers
{

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<ICYL.BL.PaymentTransaction> lst = new List<ICYL.BL.PaymentTransaction>();
            ICYLAuthorizeNet ICYLAuthorize = new ICYLAuthorizeNet();
            GlobalContext.Role = GlobalContext.eRoles.Guest.ToString();
            return RedirectToAction("Donation");
        }
        public ActionResult DonationOriginal()
        {
            return View();
        }

        public ActionResult Donation()
        {
            ViewBag.Message = "Your contact page.";
            PaymentConfig model = new PaymentConfig();
            model.dtPaymentStart = DateTime.Now;
            model.PaymentType = "5";
            model.PaymentEndType = "1";
            model.RecurringType = "1";
            model.AmtDonation = 20;
            model.IsRecurring = false;
            model.IsTransactionFeesIncluded = true;
            Dropdown(model);
            return View(model);
        }

        [HttpPost, ActionName("Donation")]
        public ActionResult Donation(PaymentConfig model)
        {
            ViewBag.Message = "";
            int PaymentConfigId = 0;
            DonationRepository clsDonationRepository = new DonationRepository();
            ICYLAuthorizeNet ICYLAuthorize = new ICYLAuthorizeNet();
            PaymentTransaction ObjPaymentTransaction = new PaymentTransaction();

            nameAndAddressType BillTo = new nameAndAddressType();
            nameAndAddressType MailTo = new nameAndAddressType();
            paymentScheduleTypeInterval interval = new paymentScheduleTypeInterval();
            int occurrences = 9999;// 999 indicates no end date else add code based on the interval length
            paymentScheduleType schedule = new paymentScheduleType();

            short intervalLength = 0;
            bool IsSuccessful = true;
            string TransErrorText = string.Empty;
            //int milliseconds = 2000;
            //Thread.Sleep(milliseconds);
            Validate(model);
            if (ModelState.IsValid)
            {
                
                //new EmailRepository().EmailDonation(model);
                //return RedirectToAction("Confirmation");
                if (!model.IsRecurring)
                {
                    model.RecurringType = "0";
                }
                if (!model.IsTransactionFeesIncluded)
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
                    email =model.EmailId,
                    phoneNumber=model.PhoneNumber,
                    company =model.CompanyName,
                    state =model.BillingState
                };
                customerDataType CustData = new customerDataType
                {
                    email = model.EmailId
                };
                orderType OrderInfo = new orderType
                {
                    description = new EmailRepository().FindCategory(model.lkpDonationCategory.ToString())
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
                    state=model.MailingState
                };
                customerType RecurringCustInfo = new customerType()
                {
                  email = model.EmailId,
                  phoneNumber = model.PhoneNumber,
                  type= customerTypeEnum.individual
                };

                //Recurring Settings
                if (model.IsRecurring)
                {
                    intervalLength = Convert.ToInt16(model.RecurringType);
                    interval.length = intervalLength;       // months can be indicated between 1 and 12 ; TODO. calculate based on recurring 
                    if (model.RecurringType == ((int)GlobalContext.RecurringType.Week).ToString())
                    {
                        interval.unit = ARBSubscriptionUnitEnum.days;
                        model.RecurringInterval= ARBSubscriptionUnitEnum.days.ToString();
                    }
                    else
                    {
                        interval.unit = ARBSubscriptionUnitEnum.months;
                        model.RecurringInterval = ARBSubscriptionUnitEnum.months.ToString();
                    }
                    
                    if (model.PaymentEndType == ((int)GlobalContext.PaymentEndType.EndDate).ToString())
                    {
                        if (model.RecurringType == ((int)GlobalContext.RecurringType.Week).ToString())
                        {
                            occurrences = ICYLHelpers.CalculateWeeks(Convert.ToDateTime(model.dtPaymentStart), Convert.ToDateTime(model.dtPaymentEnd));
                        }
                        else
                        {
                            occurrences = ICYLHelpers.CalculateMonths(Convert.ToDateTime(model.dtPaymentStart), Convert.ToDateTime(model.dtPaymentEnd));
                        }
                        model.PaymentMaxOccurences = occurrences;
                    }
                    else if (model.PaymentEndType == ((int)GlobalContext.PaymentEndType.occurrences).ToString())
                    {
                        DateTime? endDate = null;
                        occurrences = Conversion.ConversionToInt(model.PaymentMaxOccurences);
                        if (model.dtPaymentStart != null)
                        {
                            if (interval.unit == ARBSubscriptionUnitEnum.days)
                            {
                                endDate = Conversion.ConversionToDateTime(model.dtPaymentStart).Value.AddDays(occurrences * Conversion.ConversionToInt(model.RecurringType));
                            }
                            else
                            {
                                endDate = Conversion.ConversionToDateTime(model.dtPaymentStart).Value.AddMonths(occurrences * Conversion.ConversionToInt(model.RecurringType));
                            }
                            model.dtPaymentEnd = endDate;
                        }

                    }
                    schedule = new paymentScheduleType
                    {
                        interval = interval,
                        startDate = Convert.ToDateTime(model.dtPaymentStart),      // start date should be tomorrow  //model.PaymentEndType
                        totalOccurrences = Convert.ToInt16(occurrences),
                        trialOccurrences = 0,
                        trialOccurrencesSpecified=false
                        
                    };
                     
                }

                //Credit Card
                if (model.PaymentType == ((int)GlobalContext.PaymentType.CreditCard).ToString())
                {
                    creditCardType creditCardInfo = new creditCardType
                    {
                        cardNumber = model.CCNum,
                        expirationDate = model.CCExpiry,
                        cardCode = model.CCCvc

                    };
                    model.IsCreditCard = true;
                    if (model.IsRecurring)
                    { 
                        SubscriptionTransaction SubTrans = ICYLAuthorize.SetUpRecurringCharge(model.AmtTotal, interval, schedule, creditCardInfo, BillTo, OrderInfo,MailTo, RecurringCustInfo, model.lkpDonationCategory);
                        if (SubTrans != null)
                        {
                            if (Conversion.ConversionToString(SubTrans.SubscriptionTransId).Trim().Length > 0)
                            {
                                IsSuccessful = true;
                                model.SubscriptionTransId = SubTrans.SubscriptionTransId;
                                model.SubscriptionResponseCode = SubTrans.SubscriptionResponseCode;
                                model.SubscriptionResponseText = SubTrans.SubscriptionResponseText;
                            }
                            else
                            {
                                IsSuccessful = false;
                                TransErrorText = Conversion.ConversionToString(SubTrans.SubscriptionResponseText);
                            }
                        }
                        else
                        {
                            IsSuccessful = false;
                            TransErrorText = Conversion.ConversionToString(SubTrans.SubscriptionResponseText);
                        }
                    }
                    else
                    {
                        ObjPaymentTransaction = ICYLAuthorize.ICYLChargeCreditCard(model.AmtTotal, creditCardInfo, CustomerInfo, OrderInfo,MailTo, CustData, model.lkpDonationCategory);
                        if (ObjPaymentTransaction != null)
                        {
                            if (Conversion.ConversionToString(ObjPaymentTransaction.TransErrorText).Trim().Length > 0)
                            {
                                IsSuccessful = false;
                                TransErrorText = Conversion.ConversionToString(ObjPaymentTransaction.TransErrorText);
                            }
                        }
                    }
                }
                //eCheck
                else if (model.PaymentType == ((int)GlobalContext.PaymentType.eCheck).ToString())
                {
                    model.IsECheck = true;
                    var bankAccount = new bankAccountType
                    {
                        accountType = bankAccountTypeEnum.checking,
                        routingNumber = model.BankRoutingNum,
                        accountNumber = model.BankAccountNum,
                        nameOnAccount = string.Format("{0} {1}", model.FirstName, model.LastName),
                        echeckType = echeckTypeEnum.WEB,   // change based on how you take the payment (web, telephone, etc)
                        bankName = model.BankNameOnAccount,
                        // checkNumber     = "101"                 // needed if echeckType is "ARC" or "BOC"
                    };
                    if (model.IsRecurring)
                    {
                         
                        SubscriptionTransaction SubTrans = ICYLAuthorize.SetUpRecurringCharge(model.AmtTotal, interval, schedule, bankAccount, BillTo,MailTo,RecurringCustInfo, model.lkpDonationCategory);
                        if (SubTrans != null)
                        {
                            if (Conversion.ConversionToString(SubTrans.SubscriptionTransId).Trim().Length > 0)
                            {
                                IsSuccessful = true;
                                model.SubscriptionTransId = SubTrans.SubscriptionTransId;
                                model.SubscriptionResponseCode = SubTrans.SubscriptionResponseCode;
                                model.SubscriptionResponseText = SubTrans.SubscriptionResponseText;
                            }
                            else
                            {
                                IsSuccessful = false;
                                TransErrorText = Conversion.ConversionToString(SubTrans.SubscriptionResponseText);
                            }
                        }
                        else
                        {
                            IsSuccessful = false;
                            TransErrorText = Conversion.ConversionToString(SubTrans.SubscriptionResponseText);
                        }
                    }
                    else
                    {
                        ObjPaymentTransaction = ICYLAuthorize.ICYLeCheck(model.AmtTotal, bankAccount, CustomerInfo, OrderInfo,MailTo,CustData, model.lkpDonationCategory);
                        if (ObjPaymentTransaction != null)
                        {
                            if (Conversion.ConversionToString(ObjPaymentTransaction.TransErrorText).Trim().Length > 0)                            
                            {
                                IsSuccessful = false;
                                TransErrorText = Conversion.ConversionToString(ObjPaymentTransaction.TransErrorText);
                            }
                        }                        
                    }
                }

                // Test
                if (IsSuccessful) 
                {
                    int rValue = 0;
                    PaymentConfigId = clsDonationRepository.AddPayments(model,1);

                    ObjPaymentTransaction.PaymentConfigId = PaymentConfigId;
                    ObjPaymentTransaction.AmtTransaction = model.AmtTotal;

                    if (ObjPaymentTransaction != null && ObjPaymentTransaction.TransId != null)
                    {
                        rValue = clsDonationRepository.InsertPaymentTransaction(ObjPaymentTransaction,1);
                    }
                    if(PaymentConfigId > 0)
                    {
                        ReceiptBL attmt = new ReceiptBL();
                        if(ObjPaymentTransaction.TransId!=null)
                        model.ConfirmationNumber = ObjPaymentTransaction.TransId.ToString();
                        attmt = new EmailRepository().Receipt(model);
                        attmt.TransactionDate = DateTime.Now;
                        new EmailRepository().EmailDonation(attmt);
                    }
                    return View(model);
                }
                else
                {
                    ViewBag.err = TransErrorText;
                    Dropdown(model);
                    return View(model);
                }
            }
            Dropdown(model);
            return View(model);
        }

        private void Validate(PaymentConfig model)
        {
            if (model.IsRecurring &&  model.PaymentEndType == ((int)GlobalContext.PaymentEndType.EndDate).ToString())
            {
                if (model.dtPaymentEnd==null)
                {
                    ModelState.AddModelError(string.Empty, string.Format("ICYL Alert, Please enter an End Date."));
                }
            }
            else if (model.IsRecurring && model.PaymentEndType == ((int)GlobalContext.PaymentEndType.occurrences).ToString())
            {
                if (model.PaymentMaxOccurences ==null ||  model.PaymentMaxOccurences < 1)
                {
                    ModelState.AddModelError(string.Empty, string.Format("ICYL Alert, Please enter the Occurrences (Minimum 1)."));
                }
            }

        }
            
        private void Dropdown(PaymentConfig model)
        {
            model.lstRecurringType = new LookupRepository().getRecurringTypeDropDown();
            model.lstCategory = new LookupRepository().getCategoryDropDown();
        }


        public ActionResult Confirmation()
        {
             
            return View();
        }
    }
}
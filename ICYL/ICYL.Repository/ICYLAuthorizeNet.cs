using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICYL.Repository;
using AuthorizeNet.Api.Controllers;
using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers.Bases;
using EnterpriseLayer.Utilities;
using ICYL.BL;
using System.Configuration;

namespace ICYL.Repository
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

        public ICYL.BL.PaymentTransaction ICYLChargeCreditCard(decimal Amount, creditCardType creditCardInfo, customerAddressType CustomerInfo, orderType OrderInfo, nameAndAddressType MailTo, customerDataType CustData,int lkpCategory)
        {
            ICYL.BL.PaymentTransaction obj = new BL.PaymentTransaction();

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

            var request = new createTransactionRequest { transactionRequest = transactionRequest };
            var controller = new createTransactionController(request);
            controller.Execute();
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
                    if (response.transactionResponse != null && response.transactionResponse.errors != null)
                    {
                        obj.TransErrorCode = response.transactionResponse.errors[0].errorCode;
                        obj.TransErrorText = response.transactionResponse.errors[0].errorText;
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

        public ICYL.BL.PaymentTransaction ICYLeCheck(decimal Amount, bankAccountType bankAccount, customerAddressType CustomerInfo, orderType OrderInfo, nameAndAddressType MailTo, customerDataType CustData, int lkpCategory)
        {
            InvokePaymentAccount(lkpCategory);
            ICYL.BL.PaymentTransaction obj = new BL.PaymentTransaction();
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

        public ICYL.BL.SubscriptionTransaction SetUpRecurringCharge(decimal Amount, paymentScheduleTypeInterval interval, paymentScheduleType schedule, creditCardType creditCardInfo
                        , nameAndAddressType BillTo, orderType orderInfo, nameAndAddressType MailTo, customerType customer, int lkpCategory)
        {
            InvokePaymentAccount(lkpCategory);
            ICYL.BL.SubscriptionTransaction obj = new BL.SubscriptionTransaction();
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


        public void getActiveSubscriptions(int CategoryId,int Days)
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

        public void getSubscriptionDetail(SubscriptionDetail obj,int CategoryId,int downloadDays)
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
                    PaymentconfigId = new DonationRepository().InsertSubscriptionPaymentConfig(obj, response.subscription.paymentSchedule,CategoryId);
                    if (response.subscription.arbTransactions != null)
                    {
                        foreach (var transaction in response.subscription.arbTransactions)
                        {
                            if (transaction.transId != null &&(downloadDays == 0 || transaction.submitTimeUTC > DateTime.Now.AddDays(-downloadDays) ) )
                            {
                                getSubscriptionCustomerTransactions(transaction.transId);
                            }
                        }
                    }
                }
            }

        }


        public List<BL.PaymentBatch> getBatchList()
        {
            List<BL.PaymentBatch> lst = new List<BL.PaymentBatch>();
            var firstSettlementDate = DateTime.Today.Subtract(TimeSpan.FromDays(30));
            var lastSettlementDate = DateTime.Today.AddDays(1);
            //Console.WriteLine("First settlement date: {0} Last settlement date:{1}", firstSettlementDate,
            //    lastSettlementDate);

            var request = new getSettledBatchListRequest();
            request.firstSettlementDate = firstSettlementDate;
            request.lastSettlementDate = lastSettlementDate;
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
                        BL.PaymentBatch Obj = new BL.PaymentBatch();

                        Obj.BatchId = Conversion.ConversionToInt(batch.batchId);
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


        public List<ICYL.BL.PaymentTransaction> GetPaymentTransactions(int BatchId,int downloadDays)
        {
            //int downloadDays = 0;//restrict download days 
            //Int32.TryParse(DownloadDays, out downloadDays);
            List<ICYL.BL.PaymentTransaction> lst = new List<BL.PaymentTransaction>();
            // unique batch id
            string batchId = Conversion.ConversionToString(BatchId);

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
                        ICYL.BL.PaymentTransaction obj = new BL.PaymentTransaction();

                        obj.TransId = Conversion.ConversionToString(transaction.transId);
                        obj.CreatedOn = Conversion.ConversionToString(transaction.submitTimeLocal);

                        transactionDetailsType tras = GetPaymentByTransactions(transaction.transId);
                        obj.TransAuthCode = tras.authCode;
                        obj.TransDescription = tras.transactionStatus;
                        obj.TransResponseCode = tras.responseCode.ToString();
                        obj.AmtTransaction = transaction.settleAmount;
                        if (transaction.accountType == "eCheck")
                        {
                            obj.PaymentConfigs.PaymentType = "6";
                        }
                        else
                        {
                            obj.PaymentConfigs.PaymentType = "5";
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
                    Console.WriteLine("Transaction Id: {0}", transaction.transId);
                    Console.WriteLine("Submitted on (Local): {0}", transaction.submitTimeLocal);
                    Console.WriteLine("Status: {0}", transaction.transactionStatus);
                    Console.WriteLine("Settle amount: {0}", transaction.settleAmount);
                }
            }
            else if (response != null)
            {
                Console.WriteLine("Error: " + response.messages.message[0].code + "  " +
                                  response.messages.message[0].text);
            }

        }


        public void getSubscriptionCustomerTransactions(string transactionId)
        {
            int BatchId = 0;

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
                    //Int32.TryParse(response.transaction.batch.batchId, out BatchId);
                    //List<BL.PaymentTransaction> lstTrans = new List<PaymentTransaction>();
                    //lstTrans =GetPaymentTransactions(BatchId);
                    //for (int y = 0; y <= lstTrans.Count() - 1; y++)
                    //{
                    //    lstTrans[y].BatchId = BatchId;
                    //    new DonationRepository().AddPaymentTransaction(lstTrans[y]);
                    //}
                    PaymentTransaction PayObj = new PaymentTransaction();
                    if (response.transaction.batch != null && response.transaction.batch.batchId != null)
                    {
                        PaymentBatch BatchObj = new PaymentBatch();
                        BatchObj.BatchId = Int32.Parse(response.transaction.batch.batchId);
                        BatchObj.settlementTimeUTC = response.transaction.batch.settlementTimeUTC;
                        BatchObj.settlementTimeLocal = response.transaction.batch.settlementTimeLocal;
                        BatchObj.marketType = response.transaction.batch.marketType;
                        BatchObj.product = response.transaction.batch.product;
                        BatchId = new DonationRepository().AddBatches(BatchObj);
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
                        PayObj.TransId = Conversion.ConversionToString(response.transaction.transId);
                    }
                    PayObj.TransAuthCode = response.transaction.authCode;
                    PayObj.TransDescription = response.transaction.transactionStatus;
                    PayObj.TransResponseCode = response.transaction.responseCode.ToString();
                    if (response.transaction.submitTimeLocal != null)
                    {
                        PayObj.CreatedOn = Conversion.ConversionToString(response.transaction.submitTimeLocal);
                    }
                    if (response.transaction.subscription != null)
                    {
                        PayObj.PaymentConfigs.SubscriptionTransId = response.transaction.subscription.id.ToString();
                    }
                    new DonationRepository().AddSubscriptionPaymentTransaction(PayObj);
                }
            }

        }

        public void InvokePaymentAccount(int lkpCategory)
        {
            //Sandbox
            string SandboxapiLoginId = "9X7Kn6vb7";
            string SandboxtransactionKey = "4e6w63MgzTNSk96u";

            //Production - General Fund
            string ProductionapiLoginId = "6M8E6XguqJ";
            string ProductiontransactionKey = "7uC727D97RCJaKwS";

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

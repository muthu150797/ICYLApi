using System;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using ICYL.BL;
using ICYL.Repository;
using EnterpriseLayer.Utilities;
using ICYL.Web.Helpers;
using System.IO;

namespace ICYL.Web.Controllers
{
    public class AdminController : BaseController
    {
        // GET: Admin
        public ActionResult Index()
        {
            return RedirectToAction("DonorList");
        }

        public ActionResult DonorList()
        {
            DonorSearch model = new DonorSearch();
            model.lstDonationType = new LookupRepository().getDonationTypeDropDown();
            ViewBag.MenuDonorList = "Active";
            ViewBag.MenuDonations = "";
            ViewBag.MenuSettings = "";
            model.lstCategory = new LookupRepository().getCategoryDropDown();
            model.lstPaymentType = new LookupRepository().getPaymentTypeDropDown();
            return View(model);
        }

        [HttpPost, ActionName("DonorList")]
        public ActionResult DonorList(DonorSearch model)
        {
            DonationRepository cls = new DonationRepository();
            List<DonorSearchResult> lst = new List<DonorSearchResult>();
            model.lstDonationType = new LookupRepository().getDonationTypeDropDown();
            model.lstCategory = new LookupRepository().getCategoryDropDown();
            model.lstPaymentType = new LookupRepository().getPaymentTypeDropDown();
            lst = cls.getDonorSearchList(model);
            model.SearchResult = lst;

            return View(model);
        }
        public ActionResult Donations(int? id)
        {
            DonationSearch model = new DonationSearch();
            List<ICYL.BL.DonationSearchResult> lst = new List<BL.DonationSearchResult>();
            DonationRepository cls = new DonationRepository();
            model.lstPaymentType = new LookupRepository().getPaymentTypeDropDown();
            model.lstTransResponseCode = new LookupRepository().getTransResponseCodeDropDown();
            model.lstCategory = new LookupRepository().getCategoryDropDown();
            if (id == null || id < 1)
            {
                model.TransResponseCode = 1;
            }
            if (Conversion.ConversionToInt(id) > 0)
            {
                model.PaymentConfigId = Conversion.ConversionToInt(id);
                lst = cls.getPaymentSearchList(model);
                model.SearchResult = lst;
            }

            ViewBag.MenuDonorList = "";
            ViewBag.MenuDonations = "Active";
            ViewBag.MenuSettings = "";
            LastDownloadedDate();

            return View(model);
        }
        [HttpPost, ActionName("Donations")]
        public ActionResult Donations(DonationSearch model)
        {
            DonationRepository cls = new DonationRepository();
            List<ICYL.BL.DonationSearchResult> lst = new List<BL.DonationSearchResult>();
            model.lstPaymentType = new LookupRepository().getPaymentTypeDropDown();
            model.lstTransResponseCode = new LookupRepository().getTransResponseCodeDropDown();
            model.lstCategory = new LookupRepository().getCategoryDropDown();
            lst = cls.getPaymentSearchList(model);
            model.SearchResult = lst;
            LastDownloadedDate();
            return View(model);
        }
        [NonAction]
        public void LastDownloadedDate()
        {
            LookupValueBL obj = new LookupRepository().GetLookupValueById(11);
            int NumberOfdays = 30;

           
            try
            {
                DateTime date = DateTime.Parse(obj.Value);
                NumberOfdays = (int)(DateTime.Now - date).TotalDays;
                if (NumberOfdays == 0) NumberOfdays=2;
            }
            catch
            {
                NumberOfdays = 30;
            }
            ViewBag.LastUpdated = obj.Value;
            ViewBag.DownLoaddays = NumberOfdays;
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult DownloadBatchesAndTransactions(int Days=30)
        {
            int result = 0;
            DownloadBatchesAndTransactionsFromAuthorizeNet(Days);
            return Json(result);
        }

        public void DownloadBatchesAndTransactionsFromAuthorizeNet(int Days)
        {
            ICYLAuthorizeNet clsAuthorize = new ICYLAuthorizeNet();
            DonationRepository clsDonationRes = new DonationRepository();

            List<BL.PaymentBatch> lstBatch = new List<PaymentBatch>();
            //clsAuthorize.getCustomerProfiles();  // TODO - loop through all Categories
            List<LookupValueBL> Categorylst = new LookupRepository().GetLookupValueByGroupId((int)GlobalContext.LookupGroupCategory.DonationCategory);
            if (Categorylst != null && Categorylst.Count > 0)
            {
                foreach (LookupValueBL category in Categorylst)
                {
                    clsAuthorize.InvokePaymentAccount(category.ValueId);//neet loop through 
                    clsAuthorize.getActiveSubscriptions(category.ValueId, Days); //--> download subscriptions, customer profile and (may be transactions)

                    // TODO - loop through all Categories
                    lstBatch = clsAuthorize.getBatchList();
                    for (int i = 0; i <= lstBatch.Count() - 1; i++)
                    {

                        clsDonationRes.AddBatches(lstBatch[i]);
                        List<BL.PaymentTransaction> lstTrans = new List<PaymentTransaction>();
                        lstTrans = clsAuthorize.GetPaymentTransactions(lstBatch[i].BatchId, Days);
                        for (int y = 0; y <= lstTrans.Count() - 1; y++)
                        {
                            lstTrans[y].BatchId = lstBatch[i].BatchId;
                            lstTrans[y].PaymentConfigs.lkpDonationCategory = category.ValueId;
                            clsDonationRes.AddPaymentTransaction(lstTrans[y]);
                        }

                    }
                }
            }

            //Update Downloaded Date
            LookupValueBL obj = new LookupValueBL();
            obj.ValueId = 11;
            obj.Value = DateTime.Now.ToString();
            new LookupRepository().UpdateLookup(obj);
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult ManualPayment()
        {
            PaymentConfig model = new PaymentConfig();
            model.dtPaymentStart = DateTime.Now;
            model.PaymentType = "8";
            model.lstCategory = new LookupRepository().getCategoryDropDown();
            return PartialView("_ManualPayment", model);
        }

        [HttpPost]
        [ActionName("ManualPaymentPost")]
        public ActionResult ManualPayment(PaymentConfig model)
        {
            bool Status = false;
            string ProcessMsg = string.Empty;
            if (ModelState.IsValid)
            {
                model.RecurringType = "0";
                int rValue = new DonationRepository().AddPayments(model, 2);
                if (rValue > 0)
                {
                    //Manual Payments
                    PaymentTransaction ObjPaymentTransaction = new PaymentTransaction();
                    ObjPaymentTransaction.PaymentConfigId = rValue;
                    ObjPaymentTransaction.AmtTransaction = model.AmtDonation;
                    ObjPaymentTransaction.TransId = ICYLHelpers.RandomString(10);
                    ObjPaymentTransaction.TransAuthCode = ICYLHelpers.RandomString(5);
                    ObjPaymentTransaction.TransResponseCode = "1";
                    ObjPaymentTransaction.TransDescription = "Manually Submitted";
                    rValue = new DonationRepository().InsertPaymentTransaction(ObjPaymentTransaction, 2);
                    if (rValue > 0)
                    {
                        ProcessMsg = "New Payment has been submitted successfully";
                        Status = true;
                    }
                    else
                    {
                        ProcessMsg = "Failed to submit the payments";
                        Status = false;
                    }
                }
                return new JsonResult { Data = new { status = Status, Msg = ProcessMsg } };
            }
            else
            {
                return Json(ModelState.Values.SelectMany(x => x.Errors));
            }
        }


        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult EditDonor(int ConfigId)
        {
            PaymentConfigUserandPayment model = new PaymentConfigUserandPayment();
            model = new DonationRepository().getDonorandPaymentByConfigId(ConfigId);
            model.lstCategory = new LookupRepository().getCategoryDropDown();
            return PartialView("_EditDonor", model);
        }
        [HttpPost]
        [ActionName("EditDonorPost")]
        public ActionResult EditDonor(PaymentConfigUserandPayment model)
        {
            bool Status = false;
            if (ModelState.IsValid)
            {
                string ProcessMsg = string.Empty;
                int rValue = new DonationRepository().UpdateDonorAndPayment(model);
                if (rValue > 0)
                {
                    ProcessMsg = "Details has been updated successfully";
                    Status = true;
                }
                else
                {
                    ProcessMsg = "Failed to update the Details please try again";
                    Status = true;
                }
                return new JsonResult { Data = new { status = Status, Msg = ProcessMsg } };
            }
            else
            {
                return Json(ModelState.Values.SelectMany(x => x.Errors));
            }
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult SendMailReceipt(int transactionId)
        {
            EmailReceipt model = new EmailReceipt();
            model = new EmailRepository().getEmailDetailsByTransactionId(transactionId);
            return PartialView("_MailReceipt", model);
        }
        [HttpPost]
        [ActionName("SendMailReceiptPost")]
        public ActionResult SendMailReceipt(EmailReceipt model)
        {
            bool Status = false;
            if (ModelState.IsValid)
            {
                bool rValue = false;
                string ProcessMsg = string.Empty;
                rValue = new EmailRepository().SendDonationReceipt(model);
                if (rValue)
                {
                    ProcessMsg = "Email has been sent successfully";
                    Status = true;
                }
                else
                {
                    ProcessMsg = "Failed to send email Please try again later";
                    Status = true;
                }
                return new JsonResult { Data = new { status = Status, Msg = ProcessMsg } };
            }
            else
            {
                return Json(ModelState.Values.SelectMany(x => x.Errors));
            }
        }

        public FileContentResult DownloadDonationReceipt(int transactionId)
        {
            byte[] attachment = new EmailRepository().DownloadDonationReceipt(transactionId);
            Response.AddHeader("content-disposition", string.Format("attachment;filename=DonationReceipt.pdf"));
            return new FileContentResult(attachment, "application/pdf");
        }


        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult ManualTransaction(int ConfigId)
        {
            PaymentConfig model = new PaymentConfig();
            model = new DonationRepository().getDonorByConfigId(ConfigId);
            model.lstCategory = new LookupRepository().getCategoryDropDown();
            model.PaymentType = "7";
            model.lkpDonationCategory = 0;
            return PartialView("_ManualTransaction", model);
        }

        [HttpPost]
        [ActionName("ManualTransactionPost")]
        public ActionResult ManualTransaction(PaymentConfig model)
        {
            bool Status = false;
            string ProcessMsg = string.Empty;
            ModelState.Clear();
            ValidateTransaction(model);
            if (ModelState.IsValid)
            {
                return new JsonResult { Data = new { status = Status, Msg = ProcessMsg } };
            }
            else
            {
                return Json(ModelState.Values.SelectMany(x => x.Errors));
            }
        }


        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult EditTransaction(int ConfigId)
        {
            PaymentConfig model = new PaymentConfig();
            model = new DonationRepository().getDonorByConfigId(ConfigId);
            model.lstCategory = new LookupRepository().getCategoryDropDown();
            return PartialView("_EditTransaction", model);
        }

        [HttpPost]
        [ActionName("EditTransactionPost")]
        public ActionResult EditTransaction(PaymentConfig model)
        {
            ModelState.Clear();
            ValidateTransaction(model);
            if (ModelState.IsValid)
            {
                string ProcessMsg = string.Empty;
                ProcessMsg = "Successful";
                bool Status = true;
                return new JsonResult { Data = new { status = Status, Msg = ProcessMsg } };
            }
            else
            {
                return Json(ModelState.Values.SelectMany(x => x.Errors));
            }
        }

        [HttpPost]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult DeleteDonor(int ConfigId)
        {
            int result = 0;
            if (Conversion.ConversionToInt(ConfigId) > 0)
            {
                result = new DonationRepository().DeletePayment(ConfigId);
                if(result > 0)
                {
                    new DonationRepository().DeleteDonor(ConfigId);
                }
            }
            return Json(result);
        }

        private void ValidateTransaction(PaymentConfig model)
        {
            if (model != null)
            {
                if (model.AmtDonation < 1)
                {
                    ModelState.AddModelError(string.Empty, string.Format("ICYL Alert, Donation Amount is required"));
                }
                else if (model.lkpDonationCategory < 1)
                {
                    ModelState.AddModelError(string.Empty, string.Format("ICYL Alert, Donation CAtegory is required."));
                }
                else if (model.PaymentType == null || model.PaymentType == "0")
                {
                    ModelState.AddModelError(string.Empty, string.Format("ICYL Alert, Payment Type is required"));
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, string.Format("ICYL Alert, Failed to submit the transaction."));
            }
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult SendMail(List<string> idList)
        {
            bool isSuccessful = false;
            if (ModelState.IsValid && idList?.Any() == true)
            {
                //String ids = String.Join(",", idList.ToArray());
                new EmailRepository().AdminDonationEmail(idList);
                isSuccessful = true;
            }
            return Json(isSuccessful, JsonRequestBehavior.AllowGet);
        }
    }
}
using ICYL.BL;
using ICYL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static ICYL.Repository.GlobalContext;

namespace ICYL.Web.Controllers
{
    public class EmailController : BaseController
    {
        // GET: Email
        public ActionResult Index()
        {
            ViewBag.StatusMessage = "";
            ICYLEmailBL mail = new ICYLEmailBL();
            mail = new EmailRepository().GetEmailById((int)EmailCategory.ResponseMail);
            return View(mail);
        }
        [HttpPost]
        public ActionResult Index(ICYLEmailBL collection, string Body)
        {
            bool Status = false;
            string ProcessMsg = string.Empty;
            ViewBag.StatusMessage = "";
            int rValue = 0;
            ICYLEmailBL mail = new ICYLEmailBL();
            if(!string.IsNullOrEmpty(collection.Body) && !string.IsNullOrEmpty(collection.Subject))
            {
                collection.EmailId = 1;//response email
                rValue=new EmailRepository().UpdateEmail(collection);
            }
            if (rValue > 0)
            {
                ProcessMsg = "Email Update";
                Status = true;
            }
            else
            {
                ProcessMsg = "Failed to update email";
                Status = false;
            }
            mail = new EmailRepository().GetEmailById((int)EmailCategory.ResponseMail);
            return new JsonResult { Data = new { status = Status, Msg = ProcessMsg } };
            //return View(mail);
        }


    [AcceptVerbs(HttpVerbs.Post)]  
    public JsonResult UploadFile(HttpPostedFileBase aUploadedFile)
        {
            var vReturnImagePath = string.Empty;
            if (aUploadedFile.ContentLength > 0)
            {

            }
            return Json(Convert.ToString(vReturnImagePath), JsonRequestBehavior.AllowGet);
        }
    }
}
using ICYL.BL;
using ICYL.Repository;
using ICYL.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ICYL.Web.Controllers
{
    [ICYLActionFilter]
    [Authorize]
    public class BaseController : Controller
    {
        // GET: Base
        public BaseController()
        {

        }
        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
            {
                return;
            }

            var controllerName = (string)filterContext.RouteData.Values["controller"];
            var actionName = (string)filterContext.RouteData.Values["action"];
            ICYLErrorLog model = new ICYLErrorLog(filterContext.Exception, controllerName, actionName);
            string User = "External User";//GlobalContext.UserId
            string ErrorCode = ErrorLogRepository.ErrorLogInsert(model, User);
            model.ErrorCode = ErrorCode;
            filterContext.Result = new ViewResult
            {
                ViewName = "~/views/Shared/Error.cshtml",
                ViewData = new ViewDataDictionary<ICYLErrorLog>(model)
            };
            filterContext.ExceptionHandled = true;
        }
        protected ActionResult Unauthorized()
        {
            return RedirectToAction("UnAuthorized", "Account", new { pageUrl = this.Request.RawUrl });
        }
    }
}

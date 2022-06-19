using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ICYL.Web.Filters
{
    public class ICYLActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {

            if (context.ActionDescriptor.GetCustomAttributes(typeof(SkipICYLActionFilterAttribute), false).Any())
            {
                return;
            }
            else
            {
                base.OnActionExecuting(context);
                var session = System.Web.HttpContext.Current.Session;
                if (session == null || !session.SessionID.Equals(session["__ICYLAdmin"]))
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "Account",
                        action = "Login"
                    }));
                }
            }
        }
    }
    //this can be used for AllowAnonymous 
    public class SkipICYLActionFilterAttribute : Attribute
    {

    }
}
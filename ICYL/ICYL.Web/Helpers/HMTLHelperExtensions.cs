using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc.Html;
using System;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace ICYL.Web
{
    public static class HMTLHelperExtensions
    {
        //http://www.adeptlab.com/programming/generate-barcode-using-visual-studio-local-report-or-ssrs
        //https://support.microsoft.com/en-us/kb/842419
        public static string IsSelected(this HtmlHelper html, string controller = null, string action = null)
        {
            string cssClass = "active";
            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            string currentController = (string)html.ViewContext.RouteData.Values["controller"];

            if (String.IsNullOrEmpty(controller))
                controller = currentController;

            if (String.IsNullOrEmpty(action))
                action = currentAction;

            return controller == currentController && action == currentAction ?
                cssClass : String.Empty;
        }

        public static string PageClass(this HtmlHelper html)
        {
            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            return currentAction;
        }

        public static MvcHtmlString Conditional(this HtmlHelper html, Boolean condition, String ifTrue, String ifFalse)
        {
            return MvcHtmlString.Create(condition ? ifTrue : ifFalse);
        }

    }
}
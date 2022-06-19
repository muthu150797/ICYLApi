using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ICYL.Repository
{
    #region "Enumerators"
    public enum sessionvariables
    {
        
        User,
        UserName,
        UserDisplayName,
        UserId,
        EmailId,
        Role, 
        AccountListForDropDown, 
        AdminUser,
        DisplayName
    };

    #endregion "Enumerators"


    public class AppSessionManager
    {


        public object getAttribute(string strKey)
        {
            object obj = null;
            try
            {
                if (System.Web.HttpContext.Current != null)
                    obj = System.Web.HttpContext.Current.Session[strKey];

                return obj;
            }
            catch
            {
                throw; // return (object)System.Web.HttpContext.Current.Session[strKey];
            }

        }

        public object removeAttribute(string strKey)
        {
            object obj = null;
            if (System.Web.HttpContext.Current != null)
            {
                obj = System.Web.HttpContext.Current.Session[strKey];
                System.Web.HttpContext.Current.Session.Remove(strKey);
            }
            return obj;
        }

        public void setAttribute(string strKey, string strValue)
        {
            try
            {
                if (System.Web.HttpContext.Current != null)
                    System.Web.HttpContext.Current.Session.Add(strKey, strValue);
            }
            catch
            {
                throw;
            }


        }

        public void setAttribute(string strKey, object objValue)
        {
            try
            {
                if (System.Web.HttpContext.Current != null)
                    System.Web.HttpContext.Current.Session.Add(strKey, objValue);
            }
            catch
            {
                throw;
            }


        }

        public bool SessionKeyExist(string strKey)
        {
            bool KeyExist = false;
            if (System.Web.HttpContext.Current.Session[strKey] != null)
            {
                KeyExist = true;
            }
            return KeyExist;
        }

        public bool CloseMySession()
        {
            System.Web.HttpContext.Current.Session.Abandon();
            return true;
        }

        public static void InsertCookie(string CookieName, string CookieValue)
        {
            HttpCookie oCookie2 = new HttpCookie(CookieName);

            oCookie2.Value = CookieValue;
            oCookie2.Expires = DateTime.Now.AddHours(10);
            HttpContext.Current.Response.Cookies.Add(oCookie2);
        }

        public static void InsertCookie(HttpCookie Cookie)
        {
            HttpContext.Current.Response.Cookies.Add(Cookie);
        }

        public static string GetCookieValue(string CookieName)
        {
            if (HttpContext.Current.Request.Cookies[CookieName] != null)
            {

                return HttpContext.Current.Request.Cookies[CookieName].Value;
            }
            else
            {
                return String.Empty;
            }
        }

        public static HttpCookie GetCookie(string CookieName)
        {
            if (HttpContext.Current.Request.Cookies[CookieName] != null)
            {

                return HttpContext.Current.Request.Cookies[CookieName];
            }
            else
            {
                return null;
            }
        }
        public static void RemoveCookie(string CookieName)
        {
            if (HttpContext.Current.Response.Cookies[CookieName] != null)
            {
                HttpContext.Current.Response.Cookies[CookieName].Expires = DateTime.Now.AddDays(-1);
            }
        }

        public object getQueryStringValue(string strKey)
        {
            try
            {

                return (object)System.Web.HttpContext.Current.Request.QueryString[strKey];
            }
            catch
            {
                return (object)System.Web.HttpContext.Current.Request.QueryString[strKey];
            }

        }
    }
}

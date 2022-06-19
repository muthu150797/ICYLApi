using System;
using System.Web;
 
//using System.Web.SessionState;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Xml;
using System.Reflection;
using System.IO;
using System.Security.Cryptography;
//using System.Web.Security;
using System.Configuration;
using System.Data;

//using EnterpriseLayer.Utilities;
//using System.Web.Mvc;
using System.Text.RegularExpressions;
//using EnterpriseLayer.SessionManager;

using System.ComponentModel;

namespace ICYL.API.Entity
{
    public class GlobalContext
    {
        public static string DB_CONN
        {
            get
            {
                return ""; // ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            }

        }
        //#region enums
        public enum eRoles
        {
            Admin,
            Guest

        }
        public enum Env
        {
            PRODUCTION,
            TEST,
            STAGING
        }
        public enum LookupGroupCategory
        {
            DonationCategory = 1,
            PaymentType = 2
        }
        public enum PaymentType
        {
            CreditCard = 5,
            eCheck = 6,
            Cash = 9,
            Check = 8,
            Other = 10,
            Paypal = 11
        }
        public enum PaymentEndType
        {
            Indefinite = 1,
            EndDate = 2,
            occurrences = 3
        }
        public enum RecurringType
        {
            Week = 7,
            Month = 1,
            OtherMonth = 2,
            ThreeMonths = 3,
            SixMonths = 6,
            Year = 12
        }

        public enum EmailCategory
        {
            ResponseMail = 1
        }
		// #endregion enums

		//private static HttpSessionState Session
		//{
		//    get { return HttpContext.Current.Session; }
		//}
		//#region Logged in User Related properties
		//public static string DisplayName
		//{
		//    get
		//    {
		//        return Conversion.ConversionToString(new AppSessionManager().getAttribute(sessionvariables.DisplayName.ToString()));
		//    }
		//    set
		//    {
		//        if (Conversion.ConversionToString(value).Trim().Length == 0)
		//            new AppSessionManager().removeAttribute(sessionvariables.DisplayName.ToString());
		//        else
		//            new AppSessionManager().setAttribute(sessionvariables.DisplayName.ToString(), Conversion.ConversionToString(value));
		//    }
		//}

		//public static string UserName
		//{
		//    get
		//    {
		//        return Conversion.ConversionToString(new AppSessionManager().getAttribute(sessionvariables.UserName.ToString()));
		//    }
		//    set
		//    {
		//        if (Conversion.ConversionToString(value).Trim().Length == 0)
		//            new AppSessionManager().removeAttribute(sessionvariables.UserName.ToString());
		//        else
		//            new AppSessionManager().setAttribute(sessionvariables.UserName.ToString(), Conversion.ConversionToString(value));
		//    }
		//}

		//public static Guid UserID
		//{
		//    get
		//    {
		//        if (Session["UserId"] == null)
		//            return Guid.Parse("00000000-0000-0000-0000-000000000000");
		//        return Guid.Parse(Session["UserId"].ToString());
		//    }
		//    set
		//    {
		//        if (value == Guid.Parse("00000000-0000-0000-0000-000000000000"))
		//            Session.Remove("UserId");
		//        else
		//            Session["UserId"] = value;
		//    }
		//}


		//public static string Role
		//{
		//    get
		//    {
		//        return Conversion.ConversionToString(new AppSessionManager().getAttribute(sessionvariables.Role.ToString()));
		//    }
		//    set
		//    {
		//        if (Conversion.ConversionToString(value).Trim().Length == 0)
		//            new AppSessionManager().removeAttribute(sessionvariables.Role.ToString());
		//        else
		//            new AppSessionManager().setAttribute(sessionvariables.Role.ToString(), Conversion.ConversionToString(value));
		//    }
		//}

		//#endregion Logged in User Related properties

		//#region version
		public static string Version()
		{
			return "Ver 1.0";// System.Configuration.ConfigurationManager.AppSettings["Version"];
		}
		public static string VersionDate()
		{
			return "02/14/2021";// System.Configuration.ConfigurationManager.AppSettings["VersionDate"];
		}
		public static string VersionEnv()
		{
			return "Test";//Conversion.ConversionToString(ConfigurationManager.AppSettings["VersionEnv"]);
		}
		// #endregion Version
	}
}

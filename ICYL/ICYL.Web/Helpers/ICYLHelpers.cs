using EnterpriseLayer.DataSecurity;
using EnterpriseLayer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICYL.Web.Helpers
{
    public  class ICYLHelpers
    {
        public static int CalculateWeeks(DateTime sDate, DateTime eDate)
        {
            int NumberofWeeks = 0;
            NumberofWeeks = Conversion.ConversionToInt(Math.Ceiling(((eDate - sDate).TotalDays)/7)+1);
            return NumberofWeeks > 0? NumberofWeeks:1;

        }
        //public static int CalculateMonths(DateTime sDate, DateTime eDate)
        //{
        //    int NumberofMonths = 0;
        //    NumberofMonths = Conversion.ConversionToInt(Math.Ceiling(((eDate - sDate).TotalDays) / (365.25 / 12))+1);
        //    return NumberofMonths > 0 ? NumberofMonths : 1;

        //}

        public static int CalculateMonths(DateTime startDate, DateTime endDate)
        {
            int monthsApart = 12 * (startDate.Year - endDate.Year) + startDate.Month - endDate.Month;
            return Math.Abs(monthsApart)+1;
        }

        public static string Encrypt(string value)
        {
            return DataEncryptionDecryption.EncryptionString(value);
        }

        public static string Decrypt(string value)
        {
            return DataEncryptionDecryption.DecryptionString(value);
        }


        private static Random random = new Random();
        public static string RandomString(int length)
        {
            string BatchNumber = string.Empty;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            BatchNumber = new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
            return "ICYL" + BatchNumber;
        }

    }
}
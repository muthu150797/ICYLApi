using EnterpriseLayer.DataAccess;
using EnterpriseLayer.DataAccess.MSSQL;
using EnterpriseLayer.Utilities;
using ICYL.BL;
using ICYL.Entity;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static ICYL.Repository.GlobalContext;

namespace ICYL.Repository
{
    public class EmailRepository
    {
        string SendTestEmailsTo = ConfigurationManager.AppSettings["SendTestEmailsTo"];
        string SupportEmailFrom = ConfigurationManager.AppSettings["FromEmailId"];
        string smtpHost = ConfigurationManager.AppSettings["smtpHost"];
        string smtpUserName = ConfigurationManager.AppSettings["smtpUserName"];
        string smtpPassword = ConfigurationManager.AppSettings["smtpPassword"];
        string smtpPort = ConfigurationManager.AppSettings["SMTPPort"];
        string ErrorEmailTo = ConfigurationManager.AppSettings["ErrorEmailTo"];



        private string _userName = string.Empty;
        private string _connStr = string.Empty;

        public List<string> lstTo { get; set; }
        public List<string> lstCC { get; set; }

        MailMessage objEmail = new MailMessage();
        public void SendEmail(string Subject, string Body, bool isHTML)
        {
            string toStr = string.Empty;
            try
            { 

                for (int i = 0; i <= lstTo.Count - 1; i++)
                {
                    objEmail.To.Add(lstTo[i].ToString());
                    toStr = string.Format("{0};{1}", toStr, lstTo[i].ToString());

                }
                objEmail.From = new MailAddress(SupportEmailFrom.ToString());
                objEmail.Subject = Subject.ToString();
                objEmail.Body = Body.ToString();
                objEmail.IsBodyHtml = isHTML;
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                smtp.Host = smtpHost;
                smtp.Port = Conversion.ConversionToInt(smtpPort);
                smtp.Credentials = new System.Net.NetworkCredential(smtpUserName, smtpPassword);
                smtp.Send(objEmail);
            }
            catch 
            {
                // throw;
            }
        }


        public bool SendEmail(System.Net.Mail.MailMessage email)
        {
            string toStr = string.Empty;
            bool isSuccess = false;
            string EmailId = string.Empty;
            EmailId = email.To.ToString();
            if (GlobalContext.VersionEnv().Trim().ToUpper() == GlobalContext.Env.TEST.ToString())
            {
               EmailId = SendTestEmailsTo;
               email.To.Add(EmailId);
               email.Subject += " - " + GlobalContext.VersionEnv().Trim().ToUpper();
            }
            if(string.IsNullOrEmpty(EmailId))
            {
                return false;
            }
            try
            {
                email.From = new MailAddress(SupportEmailFrom.ToString());
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                smtp.Host = smtpHost;
                smtp.Port = Conversion.ConversionToInt(smtpPort);
                smtp.Credentials = new System.Net.NetworkCredential(smtpUserName, smtpPassword);
                //smtp.EnableSsl = false;
                //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(email);
                isSuccess = true;
            }
            catch (Exception ex)
            {
                ErrorLogRepository.ErrorLogInsert("EmailRepository", "SendEmail", ex.Message, ex.StackTrace.ToString(), EmailId);
                isSuccess = false;
                // throw;
            }
            return isSuccess;
        }


        public bool AdminDonationEmail(List<string> EmailIds)
        {
            string subject = "Donation Confirmation";
            string body = string.Empty;
            string ConfirmationNum = string.Empty;
            bool isSuccess = false;

            body = " <html xmlns='http://www.w3.org/1999/xhtml'>";
            body += "<head >";
            body += "<style type='text/css'>";
            body += "body {background-color: #2E2E2E; margin: 9px 9px padding:0; text-align:left;font-family: Arial,Tahoma,Verdana,sans-serif;  color: #666666;}";
            body += ".mailStyle{border:1px solid #999999; background-color: #ffffff;  width:760px;}";
            body += "a {color:#666666;text-decoration:none;} a:hover {color:#333333; text-decoration:underline;}";
            body += "li.raquo {display: list-itemlist-style-image:url('raquo.gif'); margin: 5px 10px 0px -10px;} li.raquo a {color: #666666;}";
            body += ".title{padding: 5px 10px 0px 20px; text-align: left;font-size: 15px; font-weight: bold;  }";
            body += ".raquo{width:54px;}";
            body += ".header{background-color:#fb3e22;}";
            body += ".headertitle{font-family:Georgia, 'Times New Roman', Times, serif; font-size:34px;color: #000000;}";
            body += ".headersubtitle{font-family:Georgia, 'Times New Roman', Times, serif; font-size:16px;color: #000000;}";
            body += "</style>";
            body += "</head>";
            body += "<body>";
            body += "<div>";
            body += "<div align='center'>";
            body += "<table border='0'  cellspacing='0' cellpadding='0' class='mailStyle'>";
            body += "<tr class='header'> ";
            body += "<td style='padding:6px;width:100px;'>  ";
            body += "<img src='~/images/Logo/logo.png' alt='' />  ";
            body += "</td>";
            body += "<td> ";
            body += "<div class='headertitle'>ICYL</div> ";
            body += "<div class='headersubtitle'>Masjid</div>";
            body += "</td>";
            body += "</tr>";
            body += "<tr>";
            body += "<td style='padding: 0px 20px 0px 5px' colspan='2'  >";

            body += "<table width='680' cellpadding='2' cellspacing='2' border='0' style='padding-top:5px'>";
            body += "<tr>";
            body += "<td class='title'> Online Donation </td>";
            body += "</tr>";
            body += "<tr> <td style='height: 15px'>&nbsp;</td> </tr>";
            body += "<tr>";
            body += "<td style='width: 680px'>";
            body += "Thank you for your most generous donation! We are so fortunate to have caught the attention of people like you with large warm hearts.</td>";
            body += "</tr>";
            body += "<tr> <td style='height: 15px'>&nbsp;</td> </tr>";
            body += "<tr> <td style='height: 15px'>&nbsp;</td> </tr>";
            body += "<tr>";
            body += "<td class='title' style='padding: 0px 10px 0px 20px text-align: left'>";
            body += "<a href='http://icyl.org/'>http://icyl.org</a>";
            body += "</td>&nbsp;</td>";
            body += "</tr>";
            body += "<tr> <td > &nbsp; </td>";
            body += "</tr> ";
            body += "<tr>";
            body += "<td style='text-align: center height: 15px'>";
            body += "<div style='font-size:0pxline-height:1px width:680px height:1px background-color:#CCCCCC'></div>";
            body += "</td>";
            body += "</tr>";
            body += "<tr>";
            body += "<td style='height: 20px'>";
            body += "Sincerely, <br />";
            body += "ICYL";
            body += "</td>";
            body += "</tr>";
            body += "<tr>";
            body += "<td style='height: 20px'>";
            body += "&nbsp;</td>";
            body += "</tr>";
            body += "</table>";
            body += "</div>";
            body += "</td>";
            body += "</tr>";
            body += "</table>";
            body += "<table border='0' style='width: 720px' cellspacing='0' cellpadding='0' id='table4'>";
            body += "<tr>";
            body += "</tr>";
            body += "</table>";
            body += "</div>";
            body += "</body>";
            body += "</html>";

            System.Net.Mail.MailMessage objEmail = new System.Net.Mail.MailMessage();
            foreach(string email in EmailIds)
            {
                objEmail.To.Add(email);
            }
            objEmail.Subject = subject;
            objEmail.Body = body.ToString();
            objEmail.IsBodyHtml = true;
            isSuccess = SendEmail(objEmail);
            return isSuccess;
        }


        public bool EmailDonation(ReceiptBL obj)
        {
            bool isSuccess = false;
            try
            {
                string subject = "Email Receipt From Islamic Center of Yorba Linda";
                string body = string.Empty;
                ICYLEmailBL mail = GetEmailById((int)EmailCategory.ResponseMail);
                if (mail != null)
                {
                    if (mail != null && !string.IsNullOrEmpty(mail.Subject))
                    {
                        subject = mail.Subject;
                    }
                    if (!string.IsNullOrEmpty(mail.Body))
                    {
                        body = mail.Body.Replace("_Amount_",obj.Amount).Replace("_Name_", obj.MemberName);
                    }
                }
                else
                {
                    body = eMailManual(obj);
                }
                
                System.Net.Mail.MailMessage objEmail = new System.Net.Mail.MailMessage();
                objEmail.To.Add(obj.EmailId);
                objEmail.Subject = subject;
                objEmail.Body = body.ToString();
                byte[] data = MailAttachment(obj);
                MemoryStream ms = new MemoryStream(data);
                objEmail.Attachments.Add(new Attachment(ms, "Email_Receipt_From_Islamic_Center_of_Yorba_Linda.pdf", "application/pdf"));
                objEmail.IsBodyHtml = true;
                isSuccess = SendEmail(objEmail);
            }
            catch
            {

            }
            return isSuccess;
        }


        public byte[] MailAttachment(ReceiptBL obj)
        {
            LocalReport lr = new LocalReport();
            // string path = Path.Combine(Server.MapPath("~/PPO.CommandCenter/Reports"), "SafeListConfirmation.rdlc");
            string path = System.Web.HttpContext.Current.Server.MapPath("~/PDFReports/DonationConfirmation.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            DataTable dt = getClasstoDT(obj);
            ReportDataSource rd = new ReportDataSource("DSConfirmation", dt);
            lr.DataSources.Add(rd);
            string reportType = "PDF";
            string mimeType;
            string encoding;
            string fileNameExtension;
            string deviceInfo =
            "<DeviceInfo>" +
            "  <OutputFormat>PDF</OutputFormat>" +
            "  <PageWidth>8.5in</PageWidth>" +
            "  <PageHeight>11in</PageHeight>" +
            "  <MarginTop>0.5in</MarginTop>" +
            "  <MarginLeft>1in</MarginLeft>" +
            "  <MarginRight>1in</MarginRight>" +
            "  <MarginBottom>0.5in</MarginBottom>" +
            "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = lr.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);
            return renderedBytes;
        }

        public ReceiptBL Receipt(ICYL.BL.PaymentConfig model)
        {
            ReceiptBL obj = new ReceiptBL();
            obj.MemberId = model.LastName;
            obj.MemberName = model.FullName;
            obj.CardHolderName = model.CCHolderName;
            obj.EmailId = model.EmailId;
            obj.IsRecurringTransaction = model.IsRecurring;
            if ((model.PaymentType == ((int)GlobalContext.PaymentType.CreditCard).ToString()))
            {
                obj.PaymentType= "Credit Card";
            }
            else
            {
                if(Conversion.ConversionToInt(model.PaymentType) >0)
                {
                    obj.PaymentType = Enum.GetName(typeof(GlobalContext.PaymentType), Conversion.ConversionToInt(model.PaymentType));
                }
                obj.CardHolderName = "N/A";
            }
            obj.Category = FindCategory(model.lkpDonationCategory.ToString());
            if (obj.IsRecurringTransaction)
            {
                obj.RecurringTransaction = "Yes";
                obj.ConfirmationNumber = model.SubscriptionTransId;
                obj.Freequency = FindFrequency(model.RecurringType.ToString());
            }
            else
            {
                obj.RecurringTransaction = "No";
                obj.Freequency = "None";
                obj.ConfirmationNumber = model.ConfirmationNumber;
            }
            obj.Amount = "$"+model.AmtDonation.ToString();
            obj.TransactionDate = DateTime.Now;

            return obj;
        }

        public bool SendDonationReceipt(ICYL.BL.EmailReceipt model)
        {
            bool isSendSuccessfully = false;
            ReceiptBL obj = new ReceiptBL();
            obj.MemberId = model.LastName;
            obj.MemberName = model.FullName;
            obj.CardHolderName = model.CardHolderName;
            obj.EmailId = model.EmailId;
            obj.IsRecurringTransaction = model.isRecurring;
            if ((model.PaymentType == ((int)GlobalContext.PaymentType.CreditCard).ToString()))
            {
                obj.PaymentType = "Credit Card";
            }
            else
            {
                if (!string.IsNullOrEmpty(obj.PaymentType))
                {
                    obj.PaymentType = Enum.GetName(typeof(GlobalContext.PaymentType), Conversion.ConversionToInt(model.PaymentType));
                }
                obj.CardHolderName = "N/A";
            }
            obj.Category = FindCategory(model.lkpDonationCategory.ToString());
            if (obj.IsRecurringTransaction)
            {
                obj.RecurringTransaction = "Yes";
                obj.ConfirmationNumber = model.ConfirmationNumber;
                obj.Freequency = FindFrequency(model.RecurringType.ToString());
            }
            else
            {
                obj.RecurringTransaction = "No";
                obj.Freequency = "None";
                obj.ConfirmationNumber = model.ConfirmationNumber;
            }
            obj.Amount = "$" + model.AmtDonation.ToString();
            obj.TransactionDate = model.TransactionDate;
            isSendSuccessfully=new EmailRepository().EmailDonation(obj);
            return isSendSuccessfully;
        }

        public byte[] DownloadDonationReceipt(int transactionId)
        {
            EmailReceipt model = new EmailReceipt();
            model = getEmailDetailsByTransactionId(transactionId);
            ReceiptBL obj = new ReceiptBL();
            obj.MemberId = model.LastName;
            obj.MemberName = model.FullName;
            obj.CardHolderName = model.CardHolderName;
            obj.EmailId = model.EmailId;
            obj.IsRecurringTransaction = model.isRecurring;
            if ((model.PaymentType == ((int)GlobalContext.PaymentType.CreditCard).ToString()))
            {
                obj.PaymentType = "Credit Card";
            }
            else
            {
                if (!string.IsNullOrEmpty(model.PaymentType))
                {
                    obj.PaymentType = Enum.GetName(typeof(GlobalContext.PaymentType), Conversion.ConversionToInt(model.PaymentType));
                }
                obj.CardHolderName = "N/A";
            }
            obj.Category = FindCategory(model.lkpDonationCategory.ToString());
            if (obj.IsRecurringTransaction)
            {
                obj.RecurringTransaction = "Yes";
                obj.ConfirmationNumber = model.ConfirmationNumber;
                obj.Freequency = FindFrequency(model.RecurringType.ToString());
            }
            else
            {
                obj.RecurringTransaction = "No";
                obj.Freequency = "None";
                obj.ConfirmationNumber = model.ConfirmationNumber;
            }
            obj.Amount = "$" + model.AmtDonation.ToString();
            obj.TransactionDate = Conversion.ConversionToDateTime(model.TransactionDate);
            return MailAttachment(obj);
        }
        public DataTable getClasstoDT(ReceiptBL obj)
        {
            List<ReceiptBL> lst = new List<ReceiptBL>();
            lst.Add(obj);
            return ToDataTable<ReceiptBL>(lst);
        }

        //public DataTable getClasstoDT(List<PaymentConfig> obj)
        //{
        //    return ToDataTable<PaymentConfig>(obj);
        //}

        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }


        private string eMailManual(ReceiptBL obj)
        {
            string body = string.Empty;
            body = " <html xmlns='http://www.w3.org/1999/xhtml'>";
            body += "<head >";
            body += "<style type='text/css'>";
            body += "body {background-color: #1ea7d9; margin: 9px 9px padding:0; text-align:left;font-family: Arial,Tahoma,Verdana,sans-serif;  color: #666666;}";
            body += ".mailStyle{border:1px solid #999999; background-color: #ffffff;  width:760px;}";
            body += "a {color:#666666;text-decoration:none;} a:hover {color:#333333; text-decoration:underline;}";
            body += "li.raquo {display: list-itemlist-style-image:url('raquo.gif'); margin: 5px 10px 0px -10px;} li.raquo a {color: #666666;}";
            body += ".title{padding: 5px 10px 0px 20px; text-align: left;font-size: 15px; font-weight: bold;  }";
            body += ".raquo{width:54px;}";
            body += ".header{background-color:#fb3e22;}";
            body += ".headertitle{font-family:Georgia, 'Times New Roman', Times, serif; font-size:34px;color: #000000;}";
            body += ".headersubtitle{font-family:Georgia, 'Times New Roman', Times, serif; font-size:16px;color: #000000;}";
            body += "</style>";
            body += "</head>";
            body += "<body>";
            body += "<div>";
            body += "<div align='center'>";
            body += "<table border='0'  cellspacing='0' cellpadding='0' class='mailStyle'>";
            body += "<tr class='header'> ";
            body += "<td style='padding:6px;width:100px;'>  ";
            body += "<img src='http://donation.icyl.org/images/Logo/logo.png' alt='' />  ";
            body += "</td>";
            body += "<td> ";
            body += "<div class='headertitle'>ICYL</div> ";
            body += "<div class='headersubtitle'>Masjid</div>";
            body += "</td>";
            body += "</tr>";
            body += "<tr>";
            body += "<td style='padding: 0px 20px 0px 5px' colspan='2'  >";

            body += "<table width='680' cellpadding='2' cellspacing='2' border='0' style='padding-top:5px'>";
            body += "<tr>";
            body += "<td class='title'> Online Donation </td>";
            body += "</tr>";
            body += "<tr> <td style='height: 15px'>&nbsp;</td> </tr>";
            body += "<tr>";
            body += "<td style='width: 680px'>";
            body += "Dear " + obj.MemberName + ", <br/>";
            body += "﻿<p>Thank you for your contribution of " + obj.Amount + " to Islamic Center of Yorba Linda. Your commitment is appreciated. Attached is your receipt. If you have questions or concerns regarding your payment, please contact Islamic Center of Yorba Linda at finance@icyl.org or 714-983-7464 </p><br/>";
            body += "<p>PLEASE DO NOT REPLY TO THIS EMAIL</p>";
            body += "<p>&nbsp;</p></td>";
            body += "</tr>";
            body += "<tr> <td style='height: 15px'>&nbsp;</td> </tr>";
            body += "<tr> <td style='height: 15px'>&nbsp;</td> </tr>";
            body += "<tr>";
            body += "<td class='title' style='padding: 0px 10px 0px 20px text-align: left'>";
            body += "<a href='http://icyl.org/'>http://icyl.org</a>";
            body += "</td>&nbsp;</td>";
            body += "</tr>";
            body += "<tr> <td > &nbsp; </td>";
            body += "</tr> ";
            body += "<tr>";
            body += "<td style='text-align: center height: 15px'>";
            body += "<div style='font-size:0pxline-height:1px width:680px height:1px background-color:#CCCCCC'></div>";
            body += "</td>";
            body += "</tr>";
            body += "<tr>";
            body += "<td style='height: 20px'>";
            body += "Sincerely, <br />";
            body += "ICYL";
            body += "</td>";
            body += "</tr>";
            body += "<tr>";
            body += "<td style='height: 20px'>";
            body += "&nbsp;</td>";
            body += "</tr>";
            body += "</table>";
            body += "</div>";
            body += "</td>";
            body += "</tr>";
            body += "</table>";
            body += "<table border='0' style='width: 720px' cellspacing='0' cellpadding='0' id='table4'>";
            body += "<tr>";
            body += "</tr>";
            body += "</table>";
            body += "</div>";
            body += "</body>";
            body += "</html>";
            return body;
        }


        public string FindCategory(string lkpCategory)
        {
            string Category = string.Empty;
            var lst = new LookupRepository().getCategoryDropDown().ToList();
            Category=lst.Where(p => p.Value == lkpCategory).First().Text;
            return Category;
        }

        private string FindFrequency(string id)
        {
            string Frequency = string.Empty;
            var lst = new LookupRepository().getRecurringTypeDropDown().ToList();
            Frequency = lst.Where(p => p.Value == id).First().Text;
            return Frequency;
        }
        public void SendTestEmail(string Subject, string Body, bool isHTML)
        {
            try
            {
               
                for (int i = 0; i <= lstTo.Count - 1; i++)
                { objEmail.To.Add(lstTo[i].ToString()); }

                objEmail.From = new MailAddress(SupportEmailFrom.ToString());

                objEmail.Subject = Subject.ToString();
                objEmail.Body = Body.ToString();
                objEmail.IsBodyHtml = isHTML;

                SmtpClient smtpClient = new SmtpClient(smtpHost, Conversion.ConversionToInt(smtpPort));
                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(smtpUserName, smtpPassword);
                smtpClient.Credentials = credentials;

                smtpClient.Send(objEmail);
            }
            catch (Exception exc)
            {
                throw;
            }
        }

        public ICYLEmailBL GetEmailById(int Id)
        {
            ICYLEmailBL tc = new ICYLEmailBL();
            using (ICYLEntities dc = new ICYLEntities())
            {
                var rValue = dc.ICYLEmails.Where(a => a.EmailId == Id).FirstOrDefault();
                if (rValue != null)
                {
                    tc.EmailId = rValue.EmailId;
                    tc.Description = rValue.Description;
                    tc.Subject = rValue.Subject;
                    tc.Body = rValue.Body;
                    tc.Active = Conversion.ConversionToBool(rValue.Active);
                    tc.CreatedBy = rValue.CreatedBy;
                    tc.CreatedOn = rValue.CreatedOn;
                    tc.ModifiedBy = rValue.ModifiedBy;
                    tc.ModifiedOn = rValue.ModifiedOn;
                }
            }
            return tc;
        }

        public int UpdateEmail(ICYLEmailBL model)
        {
            string SQL = string.Empty;
            object rValue = 0;
            IStoredProcedure sp = new StoredProcedure();
            SQLParameterList para = new SQLParameterList();
            SQL = "UPDATE [dbo].[ICYLEmail]    SET [Subject] =@Subject ,[Body] =@Body,[ModifiedOn] =@ModifiedOn ,[ModifiedBy] =@ModifiedBy WHERE EmailId=@EmailId  ";
            para.AddSQLParameter("@EmailId", SqlDbType.VarChar, Conversion.ConversionToInt(model.EmailId), ParameterDirection.Input);
            para.AddSQLParameter("@Subject", SqlDbType.VarChar, Conversion.ConversionToString(model.Subject), ParameterDirection.Input);
            para.AddSQLParameter("@Body", SqlDbType.NVarChar, Conversion.ConversionToString(model.Body), ParameterDirection.Input);
            para.AddSQLParameter("@ModifiedBy", SqlDbType.VarChar, GlobalContext.UserName, ParameterDirection.Input);
            para.AddSQLParameter("@ModifiedOn", SqlDbType.DateTime, DateTime.Now, ParameterDirection.Input);
            rValue = sp.UpdateStoredProcedure(SQL, CommandType.Text, para, GlobalContext.DB_CONN);
            return Conversion.ConversionToInt(rValue);
        }


        public ICYL.BL.EmailReceipt getEmailDetailsByTransactionId(int TransactionId)
        {
            ICYL.BL.EmailReceipt obj = new BL.EmailReceipt();
            DataTable dt = new DataTable();
            dt = getDTEmailDetailsByTransactionId(TransactionId);
            obj.FirstName = Conversion.ConversionToString(dt.Rows[0]["FirstName"]);
            obj.LastName = Conversion.ConversionToString(dt.Rows[0]["LastName"]);
            obj.EmailId = Conversion.ConversionToString(dt.Rows[0]["EmailId"]);
            obj.TransactionId = TransactionId;
            obj.AmtDonation = Conversion.ConversionToDecimal(dt.Rows[0]["AmtTransaction"]);
            obj.lkpDonationCategory = Conversion.ConversionToInt(dt.Rows[0]["lkpDonationCategory"]);
            obj.ConfirmationNumber = Conversion.ConversionToString(dt.Rows[0]["ConfirmationNumber"]);
            obj.CardHolderName = Conversion.ConversionToString(dt.Rows[0]["CCHolderName"]);
            obj.RecurringType = Conversion.ConversionToString(dt.Rows[0]["RecurringType"]);
            obj.PaymentType = Conversion.ConversionToString(dt.Rows[0]["PaymentType"]);
            obj.DonationCategory = Conversion.ConversionToString(dt.Rows[0]["DonationCategory"]);
            obj.TransactionDate = Conversion.ConversionToDateTime(dt.Rows[0]["CreatedOn"]);
            return obj;
        }
        private DataTable getDTEmailDetailsByTransactionId(int TransactionId)
        {
            string SQL = string.Empty;
            IStoredProcedure sp = new StoredProcedure();
            SQLParameterList para = new SQLParameterList();
            DataTable dt = new DataTable();
            try
            {
                if (TransactionId > 0)
                {
                    SQL = " select top 1 config.FirstName,config.LastName,config.EmailId,trn.AmtTransaction,config.lkpDonationCategory,lkp.Value DonationCategory , config.RecurringType,config.CCHolderName,trn.TransId ConfirmationNumber,config.PaymentType,trn.CreatedOn ";
                    SQL += " from [PaymentTransaction] trn  inner join PaymentConfig config on config.PaymentConfigId = trn.PaymentConfigId ";
                    SQL += " left join LookupValue lkp on lkp.ValueId = config.lkpDonationCategory ";
                    SQL += " where trn.TransactionId = @TransactionId ";
                    para.AddSQLParameter("@TransactionId", SqlDbType.Int, TransactionId, ParameterDirection.Input);
                    dt = sp.GetDataTable(SQL.ToString(), CommandType.Text, para, GlobalContext.DB_CONN.ToString());
                }
            }
            catch
            {
                throw;
            }
            return dt;
        }

    }
}




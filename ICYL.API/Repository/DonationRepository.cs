using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using EnterpriseLayer.Utilities;
//using EnterpriseLayer.DataAccess;
//using EnterpriseLayer.DataAccess.MSSQL;
using System.Data;
using System.Data.SqlClient;
//using System.Data.Entity;
using System.Configuration;
//using EnterpriseLayer.SMTPEmail;
using System.Net.Mail;
using System.Web;
using System.IO;
//using ICYL.Entity;
using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers;
using ICYL.API.Entity;

namespace ICYL.API.Repository

{
	public class DonationRepository
	{
		#region Private Fields
		//ICYLEntities _ICYLEntities;

		#endregion


		public DonationRepository()
		{
			//_ICYLEntities = new ICYLEntities();
		}


		public List<DonorSearchResult> getDonorSearchList(DonorSearch model)
		{
			List<DonorSearchResult> lst = new List<DonorSearchResult>();
			//try
			//{
			//    DataTable dt = getDonorSearchDataTable(model);
			//    foreach (DataRow dr in dt.Rows)
			//    {
			//        DonorSearchResult obj = new DonorSearchResult();
			//        obj.PaymentConfigId = Conversion.ConversionToInt(dr["PaymentConfigId"]);
			//        obj.FirstName = Conversion.ConversionToString(dr["FirstName"]);
			//        obj.LastName = Conversion.ConversionToString(dr["LastName"]);
			//        obj.FirstNameSecond = Conversion.ConversionToString(dr["FirstNameSecond"]);
			//        obj.LastNameSecond = Conversion.ConversionToString(dr["LastNameSecond"]);
			//        obj.CheckNumber = Conversion.ConversionToString(dr["CheckNumber"]);
			//        obj.PhoneNumber = Conversion.ConversionToString(dr["PhoneNumber"]);
			//        obj.EmailId = Conversion.ConversionToString(dr["EmailId"]);
			//        obj.BillingAddressLine1 = Conversion.ConversionToString(dr["BillingAddressLine1"]);
			//        obj.BillingAddressLine2 = Conversion.ConversionToString(dr["BillingAddressLine2"]);
			//        obj.BillingCity = Conversion.ConversionToString(dr["BillingCity"]);
			//        obj.BillingState = Conversion.ConversionToString(dr["BillingState"]);
			//        obj.BillingZip = Conversion.ConversionToString(dr["BillingZip"]);
			//        obj.MailingAddressLine1 = Conversion.ConversionToString(dr["MailingAddressLine1"]);
			//        obj.MailingAddressLine2 = Conversion.ConversionToString(dr["MailingAddressLine2"]);
			//        obj.MailingCity = Conversion.ConversionToString(dr["MailingCity"]);
			//        obj.MailingState = Conversion.ConversionToString(dr["MailingState"]);
			//        obj.MailingZip = Conversion.ConversionToString(dr["MailingZip"]);
			//        obj.AmtDonation = Conversion.ConversionToDecimal(dr["AmtDonation"]);
			//        obj.AmtTransactionPaid = Conversion.ConversionToDecimal(dr["AmtTransactionPaid"]);
			//        obj.IsRecurring = Conversion.ConversionToBool(dr["IsRecurring"]);
			//        obj.RecurringType = Conversion.ConversionToString(dr["RecurringType"]);
			//        obj.DonationCategory = Conversion.ConversionToString(dr["DonationCategory"]);
			//        obj.SubscriptionTransId = Conversion.ConversionToString(dr["SubscriptionTransId"]);
			//        obj.PaymentType = Conversion.ConversionToString(dr["PaymentType"]);
			//        obj.lkpPaymentType = Conversion.ConversionToString(dr["lkpPaymentType"]);
			//        obj.RecurringInterval = Conversion.ConversionToString(dr["RecurringInterval"]);
			//        obj.dtPaymentStart = Conversion.ConversionToDateTime(dr["dtPaymentStart"]);
			//        obj.dtPaymentEnd = Conversion.ConversionToDateTime(dr["dtPaymentEnd"]);
			//        obj.CreatedOn = Conversion.ConversionToDateTime(dr["CreatedOn"]);
			//        obj.IsAnonymous = Conversion.ConversionToBool(dr["IsAnonymous"]);
			//        obj.Comments = Conversion.ConversionToString(dr["Comments"]); 
			//        lst.Add(obj);
			//    }
			//}
			//catch
			//{
			//    throw;
			//}
			return lst;
		}

		public List<DonationSearchResult> getPaymentSearchList(DonationSearch model)
		{
			List<DonationSearchResult> lst = new List<DonationSearchResult>();
			//try
			//{

			//    DataTable dt = getPaymentSearchDataTable(model);
			//    foreach (DataRow dr in dt.Rows)
			//    {
			//        DonationSearchResult obj = new DonationSearchResult();
			//        obj.PaymentConfigId = Conversion.ConversionToInt(dr["PaymentConfigId"]);
			//        obj.FirstName = Conversion.ConversionToString(dr["FirstName"]);
			//        obj.LastName = Conversion.ConversionToString(dr["LastName"]);
			//        obj.FirstNameSecond = Conversion.ConversionToString(dr["FirstNameSecond"]);
			//        obj.LastNameSecond = Conversion.ConversionToString(dr["LastNameSecond"]);
			//        obj.CheckNumber = Conversion.ConversionToString(dr["CheckNumber"]);

			//        obj.PaymentType = Conversion.ConversionToString(dr["PaymentType"]);
			//        obj.lkpPaymentType = Conversion.ConversionToString(dr["lkpPaymentType"]);
			//        obj.IsAnonymous = Conversion.ConversionToBool(dr["IsAnonymous"]);
			//        obj.PhoneNumber = Conversion.ConversionToString(dr["PhoneNumber"]);
			//        obj.EmailId = Conversion.ConversionToString(dr["EmailId"]);
			//        obj.IsRecurring = Conversion.ConversionToBool(dr["IsRecurring"]);
			//        obj.RecurringType = Conversion.ConversionToString(dr["RecurringType"]);
			//        obj.TransactionId = Conversion.ConversionToInt(dr["TransactionId"]);
			//        obj.AmtTransaction = Conversion.ConversionToDecimal(dr["AmtTransaction"]);
			//        obj.DonationCategory = Conversion.ConversionToString(dr["DonationCategory"]);

			//        obj.TransId = Conversion.ConversionToString(dr["TransId"]);
			//        obj.TransAuthCode = Conversion.ConversionToString(dr["TransAuthCode"]);
			//        obj.TransDescription = Conversion.ConversionToString(dr["TransDescription"]);
			//        obj.TransErrorCode = Conversion.ConversionToString(dr["TransErrorCode"]);
			//        obj.TransErrorText = Conversion.ConversionToString(dr["TransErrorText"]);

			//        obj.CreatedOn = Conversion.ConversionToString(dr["CreatedOn"]);


			//        lst.Add(obj);
			//    }
			//}
			//catch
			//{
			//    throw;
			//}
			return lst;
		}


		private DataTable getDonorSearchDataTable(DonorSearch model)
		{
			string SQL = string.Empty;
			//IStoredProcedure sp = new StoredProcedure();
			//SQLParameterList para = new SQLParameterList();
			DataTable dt = new DataTable();
			//try
			//{
			//    if (model != null)
			//    {
			//        SQL = "Select PaymentConfigId ,AmtDonation, AmtTransactionPaid,RecurringType, RecurringInterval,dtPaymentStart,dtPaymentEnd ";
			//        SQL += " ,RecurringType IsRecurring, FirstName,LastName, c.FirstNameSecond,c.LastNameSecond,c.PhoneNumber,c.Comments, c.EmailId,L.Value DonationCategory, SubscriptionTransId, ";
			//        SQL += " lkpType.Value PaymentType,lkpType.ValueId lkpPaymentType,  c.CreatedOn, c.IsAnonymous,c.BillingAddressLine1 ,c.BillingAddressLine2,c.BillingCity,c.BillingState,c.BillingZip,c.MailingAddressLine1,c.MailingAddressLine2,c.MailingCity,c.MailingState,c.MailingZip,c.CheckNumber ";
			//        SQL += " from PaymentConfig  (nolock) c left join Lookupvalue  (nolock) L ";
			//        SQL += " on c.lkpDonationCategory = L.ValueId and L.GroupId = 1 ";
			//        SQL += " left join Lookupvalue (nolock) lkpType on c.PaymentType = lkpType.ValueId and lkpType.GroupId = 2  Where 1=1 ";
			//        if (Conversion.ConversionToString(model.FirstName).Trim().Length > 0)
			//        {
			//            SQL = SQL + "and C.FirstName like @FirstName + '%' ";
			//            para.AddSQLParameter("@FirstName", SqlDbType.VarChar, Conversion.ConversionToString(model.FirstName), ParameterDirection.Input);
			//        }
			//        if (Conversion.ConversionToString(model.LastName).Trim().Length > 0)
			//        {
			//            SQL = SQL + "and C.LastName like @LastName + '%' ";
			//            para.AddSQLParameter("@LastName", SqlDbType.VarChar, Conversion.ConversionToString(model.LastName), ParameterDirection.Input);
			//        }
			//        if (Conversion.ConversionToString(model.FirstNameSecond).Trim().Length > 0)
			//        {
			//            SQL = SQL + "and C.FirstNameSecond like @FirstNameSecond + '%' ";
			//            para.AddSQLParameter("@FirstNameSecond", SqlDbType.VarChar, Conversion.ConversionToString(model.FirstNameSecond), ParameterDirection.Input);
			//        }
			//        if (Conversion.ConversionToString(model.LastNameSecond).Trim().Length > 0)
			//        {
			//            SQL = SQL + "and C.LastNameSecond like @LastNameSecond + '%' ";
			//            para.AddSQLParameter("@LastNameSecond", SqlDbType.VarChar, Conversion.ConversionToString(model.LastNameSecond), ParameterDirection.Input);
			//        }
			//        if (Conversion.ConversionToString(model.Email).Trim().Length > 0)
			//        {
			//            SQL = SQL + "and C.EmailId like @Email + '%' ";
			//            para.AddSQLParameter("@Email", SqlDbType.VarChar, Conversion.ConversionToString(model.Email), ParameterDirection.Input);
			//        }
			//        if (Conversion.ConversionToString(model.Phone).Trim().Length > 0 && !string.IsNullOrEmpty(model.Phone.Trim(new Char[] { '_', '-' })))
			//        {
			//            SQL = SQL + "and C.PhoneNumber like @Phone + '%' ";
			//            para.AddSQLParameter("@Phone", SqlDbType.VarChar, Conversion.ConversionToString(model.Phone), ParameterDirection.Input);
			//        }
			//        if (!string.IsNullOrEmpty(model.PaymentType))
			//        {
			//            SQL = SQL + "and C.PaymentType = @PaymentType ";
			//            para.AddSQLParameter("@PaymentType", SqlDbType.VarChar, Conversion.ConversionToString(model.PaymentType), ParameterDirection.Input);
			//        }
			//        if (model.lkpDonationCategory != null && model.lkpDonationCategory > 0)
			//        {
			//            SQL = SQL + "and isNull(C.lkpDonationCategory,0) = @lkpDonationCategory ";
			//            para.AddSQLParameter("@lkpDonationCategory", SqlDbType.Int, model.lkpDonationCategory, ParameterDirection.Input);
			//        }
			//        if (Conversion.ConversionToString(model.DonationTypeId).Trim().Length > 0)
			//        {
			//            if (model.DonationTypeId == "1")
			//            {
			//                SQL = SQL + " and C.RecurringType is Null OR C.RecurringType='0' ";
			//            }
			//            else if (model.DonationTypeId == "2")
			//            {
			//                SQL = SQL + " and CAST(C.RecurringType AS int)  > 0 ";
			//            }
			//        }
			//        dt = sp.GetDataTable(SQL.ToString(), CommandType.Text, para, GlobalContext.DB_CONN.ToString());
			//    }
			//}
			//catch
			//{
			//    throw;
			//}
			return dt;
		}


		private DataTable getPaymentSearchDataTable(DonationSearch model)
		{
			string SQL = string.Empty;
			//IStoredProcedure sp = new StoredProcedure();
			//SQLParameterList para = new SQLParameterList();

			DataTable dt = new DataTable();
			//try
			//{

			//    if (model != null)
			//    {
			//        SQL = "Select C.PaymentConfigId,C.FirstName,C.LastName,C.FirstNameSecond,C.LastNameSecond ";
			//        SQL += " ,C.EmailId,C.PhoneNumber, lkpType.Value PaymentType,lkpType.ValueId lkpPaymentType ,C.IsAnonymous,C.RecurringType IsRecurring,C.RecurringType,C.CheckNumber ";
			//        SQL += " ,T.TransactionId,T.AmtTransaction,L.Value DonationCategory  ";
			//        SQL += " ,T.TransId, T.TransAuthCode, T.TransDescription, T.TransErrorCode, T.TransErrorText ";
			//        SQL += " ,T.CreatedOn,T.CreatedBy ";
			//        SQL += " From  ";
			//        SQL += " PaymentTransaction (nolock) T left outer join PaymentConfig (nolock) C on C.PaymentConfigId = T.PaymentConfigId ";
			//        SQL += " left join Lookupvalue  (nolock) L  on c.lkpDonationCategory = L.ValueId and L.GroupId = 1 ";
			//        SQL += " left join Lookupvalue (nolock) lkpType on c.PaymentType = lkpType.ValueId and lkpType.GroupId = 2  Where 1=1 ";

			//        if (Conversion.ConversionToString(model.FirstName).Trim().Length > 0)
			//        {
			//            SQL = SQL + "and C.FirstName like @FirstName + '%' ";
			//            para.AddSQLParameter("@FirstName", SqlDbType.VarChar, Conversion.ConversionToString(model.FirstName), ParameterDirection.Input);
			//        }
			//        if (Conversion.ConversionToString(model.LastName).Trim().Length > 0)
			//        {
			//            SQL = SQL + "and C.LastName like @LastName + '%' ";
			//            para.AddSQLParameter("@LastName", SqlDbType.VarChar, Conversion.ConversionToString(model.LastName), ParameterDirection.Input);
			//        }

			//        if (Conversion.ConversionToString(model.FirstNameSecond).Trim().Length > 0)
			//        {
			//            SQL = SQL + "and C.FirstNameSecond like @FirstNameSecond + '%' ";
			//            para.AddSQLParameter("@FirstNameSecond", SqlDbType.VarChar, Conversion.ConversionToString(model.FirstNameSecond), ParameterDirection.Input);
			//        }
			//        if (Conversion.ConversionToString(model.LastNameSecond).Trim().Length > 0)
			//        {
			//            SQL = SQL + "and C.LastNameSecond like @LastNameSecond + '%' ";
			//            para.AddSQLParameter("@LastNameSecond", SqlDbType.VarChar, Conversion.ConversionToString(model.LastNameSecond), ParameterDirection.Input);
			//        }

			//        if (Conversion.ConversionToInt(model.PaymentConfigId) > 0)
			//        {
			//            SQL = SQL + "and T.PaymentConfigId =@PaymentConfigId ";
			//            para.AddSQLParameter("@PaymentConfigId", SqlDbType.Int, Conversion.ConversionToInt(model.PaymentConfigId), ParameterDirection.Input);
			//        }
			//        if (Conversion.ConversionToString(model.Email).Trim().Length > 0)
			//        {
			//            SQL = SQL + "and C.EmailId like @Email + '%' ";
			//            para.AddSQLParameter("@Email", SqlDbType.VarChar, Conversion.ConversionToString(model.Email), ParameterDirection.Input);
			//        }
			//        if (!string.IsNullOrEmpty(model.FromDate))
			//        {
			//            SQL += " AND Convert(DateTime, Convert(nvarchar,T.CreatedOn,101),101)>=Convert(DateTime, Convert(nvarchar,@DateFrom,101),101)";
			//            para.AddSQLParameter("@DateFrom", SqlDbType.VarChar, model.FromDate, ParameterDirection.Input);
			//        }

			//        if (!string.IsNullOrEmpty(model.ToDate))
			//        {
			//            SQL += " And Convert(DateTime, Convert(nvarchar,T.CreatedOn,101),101)<=Convert(DateTime, Convert(nvarchar,@DateTo,101),101)";
			//            para.AddSQLParameter("@DateTo", SqlDbType.VarChar, model.ToDate, ParameterDirection.Input);
			//        }
			//        if (!string.IsNullOrEmpty(model.TransId))
			//        {
			//            SQL = SQL + "and T.TransId like @TransId + '%' ";
			//            para.AddSQLParameter("@TransId", SqlDbType.VarChar, Conversion.ConversionToString(model.TransId), ParameterDirection.Input);
			//        }
			//        if (!string.IsNullOrEmpty(model.AuthCode))
			//        {
			//            SQL = SQL + "and T.TransAuthCode like @AuthCode + '%' ";
			//            para.AddSQLParameter("@AuthCode", SqlDbType.VarChar, Conversion.ConversionToString(model.AuthCode), ParameterDirection.Input);
			//        }
			//        if (!string.IsNullOrEmpty(model.PaymentType))
			//        {
			//            SQL = SQL + "and C.PaymentType = @PaymentType ";
			//            para.AddSQLParameter("@PaymentType", SqlDbType.VarChar, Conversion.ConversionToString(model.PaymentType), ParameterDirection.Input);
			//        }
			//        if (model.lkpDonationCategory != null && model.lkpDonationCategory > 0)
			//        {
			//            SQL = SQL + "and isNull(C.lkpDonationCategory,0) = @lkpDonationCategory ";
			//            para.AddSQLParameter("@lkpDonationCategory", SqlDbType.Int, model.lkpDonationCategory, ParameterDirection.Input);
			//        }
			//        if (model.TransResponseCode != null && model.TransResponseCode > 0)
			//        {
			//            if (model.TransResponseCode == 1)
			//            {
			//                SQL = SQL + "and isNull(T.TransResponseCode,1) = 1 ";
			//            }
			//            else
			//            {
			//                SQL = SQL + "and isNull(T.TransResponseCode,1) > 1 ";
			//            }
			//        }
			//        SQL += " order by T.CreatedOn ";
			//        dt = sp.GetDataTable(SQL.ToString(), CommandType.Text, para, GlobalContext.DB_CONN.ToString());
			//    }
			//}
			//catch
			//{
			//    throw;
			//}
			return dt;
		}

		public PaymentConfig getDonorByConfigId(int PaymentConfigId)
		{
			PaymentConfig obj = new PaymentConfig();
			//try
			//{
			//    DataTable dt = getDTDonorByConfigId(PaymentConfigId);
			//    obj.PaymentConfigId = Conversion.ConversionToInt(dt.Rows[0]["PaymentConfigId"]);
			//    obj.AmtDonation = Conversion.ConversionToDecimal(dt.Rows[0]["AmtDonation"]);
			//    obj.AmtTransactionPaid = Conversion.ConversionToDecimal(dt.Rows[0]["AmtTransactionPaid"]);
			//    obj.RecurringType = Conversion.ConversionToString(dt.Rows[0]["RecurringType"]);
			//    obj.IsCreditCard = Conversion.ConversionToBool(dt.Rows[0]["IsCreditCard"]);
			//    obj.IsECheck = Conversion.ConversionToBool(dt.Rows[0]["IsECheck"]);
			//    obj.IsMailInCheck = Conversion.ConversionToBool(dt.Rows[0]["IsMailInCheck"]);
			//    obj.IsAnonymous = Conversion.ConversionToBool(dt.Rows[0]["IsAnonymous"]);
			//    obj.FirstName = Conversion.ConversionToString(dt.Rows[0]["FirstName"]);
			//    obj.LastName = Conversion.ConversionToString(dt.Rows[0]["LastName"]);
			//    obj.FirstNameSecond = Conversion.ConversionToString(dt.Rows[0]["FirstNameSecond"]);
			//    obj.LastNameSecond = Conversion.ConversionToString(dt.Rows[0]["LastNameSecond"]);
			//    obj.CheckNumber = Conversion.ConversionToString(dt.Rows[0]["CheckNumber"]);
			//    obj.CompanyName = Conversion.ConversionToString(dt.Rows[0]["CompanyName"]);
			//    obj.EmailId = Conversion.ConversionToString(dt.Rows[0]["EmailId"]);
			//    obj.Comments = Conversion.ConversionToString(dt.Rows[0]["Comments"]);
			//    obj.PhoneNumber = Conversion.ConversionToString(dt.Rows[0]["PhoneNumber"]);
			//    obj.lkpDonationCategory = Conversion.ConversionToInt(dt.Rows[0]["lkpDonationCategory"]);
			//    obj.BillingAddressLine1 = Conversion.ConversionToString(dt.Rows[0]["BillingAddressLine1"]);
			//    obj.BillingAddressLine2 = Conversion.ConversionToString(dt.Rows[0]["BillingAddressLine2"]);
			//    obj.BillingCity = Conversion.ConversionToString(dt.Rows[0]["BillingCity"]);
			//    obj.BillingState = Conversion.ConversionToString(dt.Rows[0]["BillingState"]);
			//    obj.BillingZip = Conversion.ConversionToString(dt.Rows[0]["BillingZip"]);
			//    obj.BillingCountry = Conversion.ConversionToString(dt.Rows[0]["BillingCountry"]);
			//    obj.CreatedBy = Conversion.ConversionToString(dt.Rows[0]["CreatedBy"]);
			//    obj.CreatedOn = Conversion.ConversionToDateTime(dt.Rows[0]["CreatedOn"]);
			//    obj.ModifiedBy = Conversion.ConversionToString(dt.Rows[0]["ModifiedBy"]);
			//    obj.ModifiedOn = Conversion.ConversionToDateTime(dt.Rows[0]["ModifiedOn"]);
			//    obj.PaymentType = Conversion.ConversionToString(dt.Rows[0]["PaymentType"]);
			//    obj.SubscriptionTransId = Conversion.ConversionToString(dt.Rows[0]["SubscriptionTransId"]);
			//    obj.SubscriptionResponseCode = Conversion.ConversionToString(dt.Rows[0]["SubscriptionResponseCode"]);
			//    obj.SubscriptionResponseText = Conversion.ConversionToString(dt.Rows[0]["SubscriptionResponseText"]);
			//    obj.MailingAddressLine1 = Conversion.ConversionToString(dt.Rows[0]["MailingAddressLine1"]);
			//    obj.MailingAddressLine2 = Conversion.ConversionToString(dt.Rows[0]["MailingAddressLine2"]);
			//    obj.MailingCity = Conversion.ConversionToString(dt.Rows[0]["MailingCity"]);
			//    obj.MailingState = Conversion.ConversionToString(dt.Rows[0]["MailingState"]);
			//    obj.MailingZip = Conversion.ConversionToString(dt.Rows[0]["MailingZip"]);
			//    obj.MailingCountry = Conversion.ConversionToString(dt.Rows[0]["MailingCountry"]);
			//    obj.dtPaymentStart = Conversion.ConversionToDateTime(dt.Rows[0]["dtPaymentStart"]);
			//    obj.dtPaymentEnd = Conversion.ConversionToDateTime(dt.Rows[0]["dtPaymentEnd"]);
			//    obj.PaymentEndType = Conversion.ConversionToString(dt.Rows[0]["PaymentEndType"]);
			//    obj.PaymentMaxOccurences = Conversion.ConversionToInt(dt.Rows[0]["PaymentMaxOccurences"]);
			//    obj.isDownloaded = Conversion.ConversionToBool(dt.Rows[0]["isDownloaded"]);
			//    obj.RecurringInterval = Conversion.ConversionToString(dt.Rows[0]["RecurringInterval"]);
			//}
			//catch
			//{
			//    throw;
			//}
			return obj;
		}

		public PaymentConfigUserandPayment getDonorandPaymentByConfigId(int PaymentConfigId)
		{
			PaymentConfigUserandPayment obj = new PaymentConfigUserandPayment();
			//try
			//{
			//    DataTable dt = getDTDonorByConfigId(PaymentConfigId);
			//    obj.PaymentConfigId = Conversion.ConversionToInt(dt.Rows[0]["PaymentConfigId"]);
			//    obj.AmtDonation = Conversion.ConversionToDecimal(dt.Rows[0]["AmtDonation"]);
			//    obj.IsAnonymous = Conversion.ConversionToBool(dt.Rows[0]["IsAnonymous"]);
			//    obj.FirstName = Conversion.ConversionToString(dt.Rows[0]["FirstName"]);
			//    obj.LastName = Conversion.ConversionToString(dt.Rows[0]["LastName"]);
			//    obj.FirstNameSecond = Conversion.ConversionToString(dt.Rows[0]["FirstNameSecond"]);
			//    obj.LastNameSecond = Conversion.ConversionToString(dt.Rows[0]["LastNameSecond"]);
			//    obj.CheckNumber = Conversion.ConversionToString(dt.Rows[0]["CheckNumber"]);
			//    obj.CompanyName = Conversion.ConversionToString(dt.Rows[0]["CompanyName"]);
			//    obj.EmailId = Conversion.ConversionToString(dt.Rows[0]["EmailId"]);
			//    obj.Comments = Conversion.ConversionToString(dt.Rows[0]["Comments"]);
			//    obj.PhoneNumber = Conversion.ConversionToString(dt.Rows[0]["PhoneNumber"]);
			//    obj.lkpDonationCategory = Conversion.ConversionToInt(dt.Rows[0]["lkpDonationCategory"]);
			//    obj.BillingAddressLine1 = Conversion.ConversionToString(dt.Rows[0]["BillingAddressLine1"]);
			//    obj.BillingAddressLine2 = Conversion.ConversionToString(dt.Rows[0]["BillingAddressLine2"]);
			//    obj.BillingCity = Conversion.ConversionToString(dt.Rows[0]["BillingCity"]);
			//    obj.BillingState = Conversion.ConversionToString(dt.Rows[0]["BillingState"]);
			//    obj.BillingZip = Conversion.ConversionToString(dt.Rows[0]["BillingZip"]);
			//    obj.BillingCountry = Conversion.ConversionToString(dt.Rows[0]["BillingCountry"]);
			//    obj.CreatedBy = Conversion.ConversionToString(dt.Rows[0]["CreatedBy"]);
			//    obj.CreatedOn = Conversion.ConversionToDateTime(dt.Rows[0]["CreatedOn"]);
			//    obj.ModifiedBy = Conversion.ConversionToString(dt.Rows[0]["ModifiedBy"]);
			//    obj.ModifiedOn = Conversion.ConversionToDateTime(dt.Rows[0]["ModifiedOn"]);
			//    obj.PaymentType = Conversion.ConversionToString(dt.Rows[0]["PaymentType"]);
			//    obj.MailingAddressLine1 = Conversion.ConversionToString(dt.Rows[0]["MailingAddressLine1"]);
			//    obj.MailingAddressLine2 = Conversion.ConversionToString(dt.Rows[0]["MailingAddressLine2"]);
			//    obj.MailingCity = Conversion.ConversionToString(dt.Rows[0]["MailingCity"]);
			//    obj.MailingState = Conversion.ConversionToString(dt.Rows[0]["MailingState"]);
			//    obj.MailingZip = Conversion.ConversionToString(dt.Rows[0]["MailingZip"]);
			//    obj.MailingCountry = Conversion.ConversionToString(dt.Rows[0]["MailingCountry"]);
			//}
			//catch
			//{
			//    throw;
			//}
			return obj;
		}
		public PaymentConfigUser getDonorUserByConfigId(int PaymentConfigId)
		{
			PaymentConfig obj = new PaymentConfig();
		    PaymentConfigUser model = new PaymentConfigUser();
			obj = getDonorByConfigId(PaymentConfigId);
			model.PaymentConfigId = obj.PaymentConfigId;
			model.FirstName = obj.FirstName;
			model.LastName = obj.LastName;
			model.FirstNameSecond = obj.FirstNameSecond;
			model.LastNameSecond = obj.LastNameSecond;
			model.IsAnonymous = obj.IsAnonymous;
			model.CompanyName = obj.CompanyName;
			model.EmailId = obj.EmailId;
			model.Comments = obj.Comments;
			model.PhoneNumber = obj.PhoneNumber;
			model.BillingAddressLine1 = obj.BillingAddressLine1;
			model.BillingAddressLine2 = obj.BillingAddressLine2;
			model.BillingCity = obj.BillingCity;
			model.BillingState = obj.BillingState;
			model.BillingZip = obj.BillingZip;
			model.BillingCountry = obj.BillingCountry;
			model.CreatedBy = obj.CreatedBy;
			model.CreatedOn = obj.CreatedOn;
			model.ModifiedBy = obj.ModifiedBy;
			model.ModifiedOn = obj.ModifiedOn;
			model.MailingAddressLine1 = obj.MailingAddressLine1;
			model.MailingAddressLine2 = obj.MailingAddressLine2;
			model.MailingCity = obj.MailingCity;
			model.MailingState = obj.MailingState;
			model.MailingZip = obj.MailingZip;
			model.MailingCountry = obj.MailingCountry;

			return model;
		}






		private DataTable getDTDonorByConfigId(int ConfigId)
		{
			//string SQL = string.Empty;
			//IStoredProcedure sp = new StoredProcedure();
			//SQLParameterList para = new SQLParameterList();
			DataTable dt = new DataTable();
			//try
			//{
			//    if (ConfigId > 0)
			//    {
			//        SQL = " SELECT[PaymentConfigId],[AmtDonation],[AmtTransactionPaid],[RecurringType],[IsCreditCard],[IsECheck],[IsMailInCheck] ,[IsAnonymous],[FirstName],[LastName],[FirstNameSecond],[LastNameSecond],[CompanyName],CheckNumber ";
			//        SQL += ",[EmailId],[Comments],[PhoneNumber],[lkpDonationCategory],[BillingAddressLine1],[BillingAddressLine2],[BillingCity],[BillingState],[BillingZip],[BillingCountry] ";
			//        SQL += ",[PaymentType],[SubscriptionTransId] ,[SubscriptionResponseCode],[SubscriptionResponseText],[MailingAddressLine1],[MailingAddressLine2],[MailingCity],[MailingState] ";
			//        SQL += ",[MailingZip],[MailingCountry],[CustomerProfileId],[CustomerAddressId],[dtPaymentStart],[dtPaymentEnd],[PaymentEndType],[PaymentMaxOccurences],[isDownloaded],[RecurringInterval] ";
			//        SQL += ",[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn] ";
			//        SQL += " FROM[dbo].[PaymentConfig] where PaymentConfigId = @PaymentConfigId ";
			//        para.AddSQLParameter("@PaymentConfigId", SqlDbType.Int, ConfigId, ParameterDirection.Input);
			//        dt = sp.GetDataTable(SQL.ToString(), CommandType.Text, para, GlobalContext.DB_CONN.ToString());
			//    }
			//}
			//catch
			//{
			//    throw;
			//}
			return dt;
		}

		public int UpdateDonor(PaymentConfigUser model)
		{
			string SQL = string.Empty;
			object rValue = 0;
			//IStoredProcedure sp = new StoredProcedure();
			//SQLParameterList para = new SQLParameterList();
			//SQL = "UPDATE [dbo].[PaymentConfig] SET ModifiedBy =@ModifiedBy,ModifiedOn=@ModifiedOn   ";

			//if (!string.IsNullOrEmpty(model.FirstName))
			//{
			//    SQL += " ,FirstName=@FirstName ";
			//    para.AddSQLParameter("@FirstName", SqlDbType.VarChar, Conversion.ConversionToString(model.FirstName), ParameterDirection.Input);
			//}
			//if (!string.IsNullOrEmpty(model.LastName))
			//{
			//    SQL += " ,LastName=@LastName ";
			//    para.AddSQLParameter("@LastName", SqlDbType.VarChar, Conversion.ConversionToString(model.LastName), ParameterDirection.Input);
			//}
			//if (!string.IsNullOrEmpty(model.FirstNameSecond))
			//{
			//    SQL += " ,FirstNameSecond=@FirstNameSecond ";
			//    para.AddSQLParameter("@FirstNameSecond", SqlDbType.VarChar, Conversion.ConversionToString(model.FirstNameSecond), ParameterDirection.Input);
			//}
			//if (!string.IsNullOrEmpty(model.LastNameSecond))
			//{
			//    SQL += " ,LastNameSecond=@LastNameSecond ";
			//    para.AddSQLParameter("@LastNameSecond", SqlDbType.VarChar, Conversion.ConversionToString(model.LastNameSecond), ParameterDirection.Input);
			//}
			//if (!string.IsNullOrEmpty(model.CompanyName))
			//{
			//    SQL += " ,CompanyName=@CompanyName ";
			//    para.AddSQLParameter("@CompanyName", SqlDbType.VarChar, Conversion.ConversionToString(model.CompanyName), ParameterDirection.Input);
			//}
			//if (!string.IsNullOrEmpty(model.Comments))
			//{
			//    SQL += " ,Comments=@Comments ";
			//    para.AddSQLParameter("@Comments", SqlDbType.VarChar, Conversion.ConversionToString(model.Comments), ParameterDirection.Input);
			//}
			//if (!string.IsNullOrEmpty(model.PhoneNumber))
			//{
			//    SQL += " ,PhoneNumber=@PhoneNumber ";
			//    para.AddSQLParameter("@PhoneNumber", SqlDbType.VarChar, Conversion.ConversionToString(model.PhoneNumber), ParameterDirection.Input);
			//}
			//if (!string.IsNullOrEmpty(model.EmailId))
			//{
			//    SQL += " ,EmailId=@EmailId ";
			//    para.AddSQLParameter("@EmailId", SqlDbType.VarChar, Conversion.ConversionToString(model.EmailId), ParameterDirection.Input);
			//}
			//if (true)
			//{
			//    SQL += " ,IsAnonymous=@IsAnonymous ";
			//    para.AddSQLParameter("@IsAnonymous", SqlDbType.Bit, Conversion.ConversionToBool(model.IsAnonymous), ParameterDirection.Input);
			//}
			//if (!string.IsNullOrEmpty(model.BillingAddressLine1))
			//{
			//    SQL += " ,BillingAddressLine1=@BillingAddressLine1 ";
			//    para.AddSQLParameter("@BillingAddressLine1", SqlDbType.VarChar, Conversion.ConversionToString(model.BillingAddressLine1), ParameterDirection.Input);
			//}
			//if (!string.IsNullOrEmpty(model.BillingAddressLine2))
			//{
			//    SQL += " ,BillingAddressLine2=@BillingAddressLine2 ";
			//    para.AddSQLParameter("@BillingAddressLine2", SqlDbType.VarChar, Conversion.ConversionToString(model.BillingAddressLine2), ParameterDirection.Input);
			//}
			//if (!string.IsNullOrEmpty(model.BillingCity))
			//{
			//    SQL += " ,BillingCity=@BillingCity ";
			//    para.AddSQLParameter("@BillingCity", SqlDbType.VarChar, Conversion.ConversionToString(model.BillingCity), ParameterDirection.Input);
			//}
			//if (!string.IsNullOrEmpty(model.BillingState))
			//{
			//    SQL += " ,BillingState=@BillingState ";
			//    para.AddSQLParameter("@BillingState", SqlDbType.VarChar, Conversion.ConversionToString(model.BillingState), ParameterDirection.Input);
			//}
			//if (!string.IsNullOrEmpty(model.BillingZip))
			//{
			//    SQL += " ,BillingZip=@BillingZip ";
			//    para.AddSQLParameter("@BillingZip", SqlDbType.VarChar, Conversion.ConversionToString(model.BillingZip), ParameterDirection.Input);
			//}
			//if (!string.IsNullOrEmpty(model.BillingCountry))
			//{
			//    SQL += " ,BillingCountry=@BillingCountry ";
			//    para.AddSQLParameter("@BillingCountry", SqlDbType.VarChar, Conversion.ConversionToString(model.BillingCountry), ParameterDirection.Input);
			//}
			//if (!string.IsNullOrEmpty(model.MailingAddressLine1))
			//{
			//    SQL += " ,MailingAddressLine1=@MailingAddressLine1 ";
			//    para.AddSQLParameter("@MailingAddressLine1", SqlDbType.VarChar, Conversion.ConversionToString(model.MailingAddressLine1), ParameterDirection.Input);
			//}
			//if (!string.IsNullOrEmpty(model.MailingAddressLine2))
			//{
			//    SQL += " ,MailingAddressLine2=@MailingAddressLine2 ";
			//    para.AddSQLParameter("@MailingAddressLine2", SqlDbType.VarChar, Conversion.ConversionToString(model.MailingAddressLine2), ParameterDirection.Input);
			//}
			//if (!string.IsNullOrEmpty(model.MailingCity))
			//{
			//    SQL += " ,MailingCity=@MailingCity ";
			//    para.AddSQLParameter("@MailingCity", SqlDbType.VarChar, Conversion.ConversionToString(model.MailingCity), ParameterDirection.Input);
			//}
			//if (!string.IsNullOrEmpty(model.MailingState))
			//{
			//    SQL += " ,MailingState=@MailingState ";
			//    para.AddSQLParameter("@MailingState", SqlDbType.VarChar, Conversion.ConversionToString(model.MailingState), ParameterDirection.Input);
			//}
			//if (!string.IsNullOrEmpty(model.MailingZip))
			//{
			//    SQL += " ,MailingZip=@MailingZip ";
			//    para.AddSQLParameter("@MailingZip", SqlDbType.VarChar, Conversion.ConversionToString(model.MailingZip), ParameterDirection.Input);
			//}
			//if (!string.IsNullOrEmpty(model.MailingCountry))
			//{
			//    SQL += " ,MailingCountry=@MailingCountry ";
			//    para.AddSQLParameter("@MailingCountry", SqlDbType.VarChar, Conversion.ConversionToString(model.MailingCountry), ParameterDirection.Input);
			//}

			//SQL += " where PaymentConfigId=@PaymentConfigId ";
			//para.AddSQLParameter("@PaymentConfigId", SqlDbType.Int, Conversion.ConversionToInt(model.PaymentConfigId), ParameterDirection.Input);
			//para.AddSQLParameter("@ModifiedBy", SqlDbType.VarChar, Conversion.ConversionToString(model.ModifiedBy), ParameterDirection.Input);
			//para.AddSQLParameter("@ModifiedOn", SqlDbType.DateTime, DateTime.Now, ParameterDirection.Input);
			//rValue = sp.UpdateStoredProcedure(SQL, CommandType.Text, para, GlobalContext.DB_CONN);
			return 0;// Conversion.ConversionToInt(rValue);
		}

		//public int UpdateDonorAndPayment(ICYL.BL.PaymentConfigUserandPayment model)
		//{
			//PaymentBatch update is applicable only for Cash ,Check & Other
			string SQL = string.Empty;
			object rValue = 0;
			//IStoredProcedure sp = new StoredProcedure();
			//SQLParameterList para = new SQLParameterList();
			//SQL = "UPDATE [dbo].[PaymentConfig] SET ModifiedBy =@ModifiedBy,ModifiedOn=@ModifiedOn   ";

			//if (!string.IsNullOrEmpty(model.FirstName))
			//{
			//    SQL += " ,FirstName=@FirstName ";
			//    para.AddSQLParameter("@FirstName", SqlDbType.VarChar, Conversion.ConversionToString(model.FirstName), ParameterDirection.Input);
			//}
			//if (!string.IsNullOrEmpty(model.LastName))
			//{
			//    SQL += " ,LastName=@LastName ";
			//    para.AddSQLParameter("@LastName", SqlDbType.VarChar, Conversion.ConversionToString(model.LastName), ParameterDirection.Input);
			//}
			//if (!string.IsNullOrEmpty(model.FirstNameSecond))
			//{
			//    SQL += " ,FirstNameSecond=@FirstNameSecond ";
			//    para.AddSQLParameter("@FirstNameSecond", SqlDbType.VarChar, Conversion.ConversionToString(model.FirstNameSecond), ParameterDirection.Input);
			//}
			//if (!string.IsNullOrEmpty(model.LastNameSecond))
			//{
			//    SQL += " ,LastNameSecond=@LastNameSecond ";
			//    para.AddSQLParameter("@LastNameSecond", SqlDbType.VarChar, Conversion.ConversionToString(model.LastNameSecond), ParameterDirection.Input);
			//}
			//if (!string.IsNullOrEmpty(model.CompanyName))
			//{
			//    SQL += " ,CompanyName=@CompanyName ";
			//    para.AddSQLParameter("@CompanyName", SqlDbType.VarChar, Conversion.ConversionToString(model.CompanyName), ParameterDirection.Input);
			//}
			//if (!string.IsNullOrEmpty(model.Comments))
			//{
			//    SQL += " ,Comments=@Comments ";
			//    para.AddSQLParameter("@Comments", SqlDbType.VarChar, Conversion.ConversionToString(model.Comments), ParameterDirection.Input);
			//}
			//if (!string.IsNullOrEmpty(model.PhoneNumber))
			//{
			//    SQL += " ,PhoneNumber=@PhoneNumber ";
			//    para.AddSQLParameter("@PhoneNumber", SqlDbType.VarChar, Conversion.ConversionToString(model.PhoneNumber), ParameterDirection.Input);
			//}
			//if (!string.IsNullOrEmpty(model.EmailId))
			//{
			//    SQL += " ,EmailId=@EmailId ";
			//    para.AddSQLParameter("@EmailId", SqlDbType.VarChar, Conversion.ConversionToString(model.EmailId), ParameterDirection.Input);
			//}
			//if (true)
			//{
			//    SQL += " ,IsAnonymous=@IsAnonymous ";
			//    para.AddSQLParameter("@IsAnonymous", SqlDbType.Bit, Conversion.ConversionToBool(model.IsAnonymous), ParameterDirection.Input);
			//}
			//if (!string.IsNullOrEmpty(model.BillingAddressLine1))
			//{
			//    SQL += " ,BillingAddressLine1=@BillingAddressLine1 ";
			//    para.AddSQLParameter("@BillingAddressLine1", SqlDbType.VarChar, Conversion.ConversionToString(model.BillingAddressLine1), ParameterDirection.Input);
			//}
			//if (!string.IsNullOrEmpty(model.BillingAddressLine2))
			//{
			//    SQL += " ,BillingAddressLine2=@BillingAddressLine2 ";
			//    para.AddSQLParameter("@BillingAddressLine2", SqlDbType.VarChar, Conversion.ConversionToString(model.BillingAddressLine2), ParameterDirection.Input);
			//}
			//if (!string.IsNullOrEmpty(model.BillingCity))
			//{
			//    SQL += " ,BillingCity=@BillingCity ";
			//    para.AddSQLParameter("@BillingCity", SqlDbType.VarChar, Conversion.ConversionToString(model.BillingCity), ParameterDirection.Input);
			//}
			//if (!string.IsNullOrEmpty(model.BillingState))
			//{
			//    SQL += " ,BillingState=@BillingState ";
			//    para.AddSQLParameter("@BillingState", SqlDbType.VarChar, Conversion.ConversionToString(model.BillingState), ParameterDirection.Input);
			//}
			//if (!string.IsNullOrEmpty(model.BillingZip))
			//{
			//    SQL += " ,BillingZip=@BillingZip ";
			//    para.AddSQLParameter("@BillingZip", SqlDbType.VarChar, Conversion.ConversionToString(model.BillingZip), ParameterDirection.Input);
			//}
			//if (!string.IsNullOrEmpty(model.BillingCountry))
			//{
			//    SQL += " ,BillingCountry=@BillingCountry ";
			//    para.AddSQLParameter("@BillingCountry", SqlDbType.VarChar, Conversion.ConversionToString(model.BillingCountry), ParameterDirection.Input);
			//}
			//if (!string.IsNullOrEmpty(model.MailingAddressLine1))
			//{
			//    SQL += " ,MailingAddressLine1=@MailingAddressLine1 ";
			//    para.AddSQLParameter("@MailingAddressLine1", SqlDbType.VarChar, Conversion.ConversionToString(model.MailingAddressLine1), ParameterDirection.Input);
			//}
			//if (!string.IsNullOrEmpty(model.MailingAddressLine2))
			//{
			//    SQL += " ,MailingAddressLine2=@MailingAddressLine2 ";
			//    para.AddSQLParameter("@MailingAddressLine2", SqlDbType.VarChar, Conversion.ConversionToString(model.MailingAddressLine2), ParameterDirection.Input);
			//}
			//if (!string.IsNullOrEmpty(model.MailingCity))
			//{
			//    SQL += " ,MailingCity=@MailingCity ";
			//    para.AddSQLParameter("@MailingCity", SqlDbType.VarChar, Conversion.ConversionToString(model.MailingCity), ParameterDirection.Input);
			//}
			//if (!string.IsNullOrEmpty(model.MailingState))
			//{
			//    SQL += " ,MailingState=@MailingState ";
			//    para.AddSQLParameter("@MailingState", SqlDbType.VarChar, Conversion.ConversionToString(model.MailingState), ParameterDirection.Input);
			//}
			//if (!string.IsNullOrEmpty(model.MailingZip))
			//{
			//    SQL += " ,MailingZip=@MailingZip ";
			//    para.AddSQLParameter("@MailingZip", SqlDbType.VarChar, Conversion.ConversionToString(model.MailingZip), ParameterDirection.Input);
			//}
			//if (!string.IsNullOrEmpty(model.MailingCountry))
			//{
			//    SQL += " ,MailingCountry=@MailingCountry ";
			//    para.AddSQLParameter("@MailingCountry", SqlDbType.VarChar, Conversion.ConversionToString(model.MailingCountry), ParameterDirection.Input);
			//}

			//if (model.PaymentType != null && model.PaymentType == "8" || model.PaymentType == "10" || model.PaymentType == "9")
			//{

			//    if (model.PaymentType == "8" && !string.IsNullOrEmpty(model.CheckNumber))
			//    {
			//        SQL += " ,CheckNumber=@CheckNumber ";
			//        para.AddSQLParameter("@CheckNumber", SqlDbType.VarChar, Conversion.ConversionToString(model.CheckNumber), ParameterDirection.Input);
			//    }

			//    if (!string.IsNullOrEmpty(model.PaymentType))
			//    {
			//        SQL += " ,PaymentType=@PaymentType ";
			//        para.AddSQLParameter("@PaymentType", SqlDbType.VarChar, Conversion.ConversionToString(model.PaymentType), ParameterDirection.Input);
			//    }

			//    if (model.lkpDonationCategory > 0)
			//    {
			//        SQL += " ,lkpDonationCategory=@lkpDonationCategory ";
			//        para.AddSQLParameter("@lkpDonationCategory", SqlDbType.Int, Conversion.ConversionToInt(model.lkpDonationCategory), ParameterDirection.Input);
			//    }
			//    if (model.AmtDonation > 0)
			//    {
			//        SQL += " ,AmtDonation=@AmtDonation ";
			//        para.AddSQLParameter("@AmtDonation", SqlDbType.Decimal, Conversion.ConversionToDecimal(model.AmtDonation), ParameterDirection.Input);
			//    }
			//}
			//SQL += " where PaymentConfigId=@PaymentConfigId ";
			//para.AddSQLParameter("@PaymentConfigId", SqlDbType.Int, Conversion.ConversionToInt(model.PaymentConfigId), ParameterDirection.Input);
			//para.AddSQLParameter("@ModifiedBy", SqlDbType.VarChar, Conversion.ConversionToString(model.ModifiedBy), ParameterDirection.Input);
			//para.AddSQLParameter("@ModifiedOn", SqlDbType.DateTime, DateTime.Now, ParameterDirection.Input);
			//rValue = sp.UpdateStoredProcedure(SQL, CommandType.Text, para, GlobalContext.DB_CONN);
			//if(rValue !=null && Conversion.ConversionToInt(rValue.ToString()) > 0)
			//{
			//    if (model.PaymentType != null && model.PaymentType == "8" || model.PaymentType == "10" || model.PaymentType == "9")
			//    {
			//        ICYL.BL.PaymentTransaction obj = new BL.PaymentTransaction();
			//        obj.AmtTransaction = model.AmtDonation;
			//        obj.PaymentConfigId = model.PaymentConfigId;
			//        UpdatePayment(obj);
			//    }
			//}
		//	return 0;//Conversion.ConversionToInt(rValue);
		//}

		//public int UpdatePayment(ICYL.BL.PaymentTransaction model)
		//{
		//	//PaymentBatch update is applicable only for Cash ,Check & Other
		//	string SQL = string.Empty;
		//	object rValue = 0;
		//	//IStoredProcedure sp = new StoredProcedure();
		//	//SQLParameterList para = new SQLParameterList();
		//	//SQL = " update[PaymentTransaction] set AmtTransaction =@AmtTransaction, CreatedOn = CreatedOn where PaymentConfigId = @PaymentConfigId   ";
		//	//para.AddSQLParameter("@AmtTransaction", SqlDbType.Decimal, Conversion.ConversionToDecimal(model.AmtTransaction), ParameterDirection.Input);
		//	//para.AddSQLParameter("@PaymentConfigId", SqlDbType.Int, Conversion.ConversionToInt(model.PaymentConfigId), ParameterDirection.Input);
		//	//para.AddSQLParameter("@CreatedOn", SqlDbType.DateTime, DateTime.Now, ParameterDirection.Input);
		//	//rValue = sp.UpdateStoredProcedure(SQL, CommandType.Text, para, GlobalContext.DB_CONN);
		//	return 0;// Conversion.ConversionToInt(rValue);
		//}

		public int AddPayments(PaymentConfig model, int source = 1)
		{

			string SQL = string.Empty;
			object rValue = 0;
			//IStoredProcedure sp = new StoredProcedure();
			//SQLParameterList para = new SQLParameterList();
			//try
			//{
			//    if (model != null)
			//    {

			//        SQL = "INSERT INTO [dbo].[PaymentConfig] (AmtDonation,AmtTransactionPaid,RecurringType,FirstName,EmailId,lkpDonationCategory,PaymentType,IsAnonymous,BillingAddressLine1,BillingAddressLine2,BillingCity,BankAccountType   ";
			//        if (!string.IsNullOrEmpty(model.LastName))
			//        {
			//            SQL += ",LastName ";

			//        }
			//        if (!string.IsNullOrEmpty(model.FirstNameSecond))
			//        {
			//            SQL += ",FirstNameSecond ";

			//        }
			//        if (!string.IsNullOrEmpty(model.LastNameSecond))
			//        {
			//            SQL += ",LastNameSecond ";

			//        }
			//        if (!string.IsNullOrEmpty(model.CompanyName))
			//        {
			//            SQL += ",CompanyName ";

			//        }
			//        if (!string.IsNullOrEmpty(model.Comments))
			//        {
			//            SQL += ",Comments ";

			//        }
			//        if (!string.IsNullOrEmpty(model.PhoneNumber))
			//        {
			//            SQL += ",PhoneNumber ";

			//        }
			//        if (!string.IsNullOrEmpty(model.BillingState))
			//        {
			//            SQL += ",BillingState ";

			//        }
			//        if (!string.IsNullOrEmpty(model.BillingZip))
			//        {
			//            SQL += ",BillingZip ";

			//        }
			//        if (!string.IsNullOrEmpty(model.BillingCountry))
			//        {
			//            SQL += ",BillingCountry ";

			//        }

			//        if (!string.IsNullOrEmpty(model.MailingAddressLine1))
			//        {
			//            SQL += ",MailingAddressLine1 ";
			//        }
			//        if (!string.IsNullOrEmpty(model.MailingAddressLine2))
			//        {
			//            SQL += ",MailingAddressLine2 ";
			//        }
			//        if (!string.IsNullOrEmpty(model.MailingCity))
			//        {
			//            SQL += ",MailingCity ";
			//        }
			//        if (!string.IsNullOrEmpty(model.MailingState))
			//        {
			//            SQL += ",MailingState ";
			//        }
			//        if (!string.IsNullOrEmpty(model.MailingZip))
			//        {
			//            SQL += ",MailingZip ";
			//        }
			//        if (!string.IsNullOrEmpty(model.MailingCountry))
			//        {
			//            SQL += ",MailingCountry ";
			//        }
			//        if (model.dtPaymentStart != null && model.IsRecurring)
			//        {
			//            SQL += ",dtPaymentStart ";
			//        }

			//        if (model.dtPaymentEnd != null && model.IsRecurring)
			//        {
			//            SQL += ",dtPaymentEnd ";
			//        }

			//        if (!string.IsNullOrEmpty(model.PaymentEndType) && model.IsRecurring)
			//        {
			//            SQL += ",PaymentEndType ";
			//        }
			//        if (!string.IsNullOrEmpty(model.RecurringInterval) && model.IsRecurring)
			//        {
			//            SQL += ",RecurringInterval ";
			//        }

			//        if (model.IsRecurring && model.PaymentMaxOccurences != null && model.PaymentMaxOccurences > 0)
			//        {
			//            SQL += ",PaymentMaxOccurences ";
			//        }
			//        if ((model.PaymentType == ((int)GlobalContext.PaymentType.CreditCard).ToString()))
			//        {
			//            SQL += ",IsCreditCard ";
			//        }
			//        else if ((model.PaymentType == ((int)GlobalContext.PaymentType.eCheck).ToString()))
			//        {
			//            SQL += ",IsECheck ";
			//        }
			//        if (!string.IsNullOrEmpty(model.CheckNumber))
			//        {
			//            SQL += ",CheckNumber ";
			//        }

			//        SQL += " ,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy,SubscriptionTransId,SubscriptionResponseCode,SubscriptionResponseText ) values (@AmtDonation,@AmtTransactionPaid,@RecurringType,@FirstName,@EmailId,@lkpDonationCategory,@PaymentType,@IsAnonymous,@BillingAddressLine1,@BillingAddressLine2,@BillingCity,@BankAccountType ";
			//        if (!string.IsNullOrEmpty(model.LastName))
			//        {
			//            SQL += ",@LastName ";
			//            para.AddSQLParameter("@LastName", SqlDbType.VarChar, Conversion.ConversionToString(model.LastName), ParameterDirection.Input);
			//        }
			//        if (!string.IsNullOrEmpty(model.FirstNameSecond))
			//        {
			//            SQL += ",@FirstNameSecond ";
			//            para.AddSQLParameter("@FirstNameSecond", SqlDbType.VarChar, Conversion.ConversionToString(model.FirstNameSecond), ParameterDirection.Input);
			//        }
			//        if (!string.IsNullOrEmpty(model.LastNameSecond))
			//        {
			//            SQL += ",@LastNameSecond ";
			//            para.AddSQLParameter("@LastNameSecond", SqlDbType.VarChar, Conversion.ConversionToString(model.LastNameSecond), ParameterDirection.Input);
			//        }

			//        if (!string.IsNullOrEmpty(model.CompanyName))
			//        {
			//            SQL += ",@CompanyName ";
			//            para.AddSQLParameter("@CompanyName", SqlDbType.VarChar, Conversion.ConversionToString(model.CompanyName), ParameterDirection.Input);
			//        }
			//        if (!string.IsNullOrEmpty(model.Comments))
			//        {
			//            SQL += ",@Comments ";
			//            para.AddSQLParameter("@Comments", SqlDbType.VarChar, Conversion.ConversionToString(model.Comments), ParameterDirection.Input);
			//        }
			//        if (!string.IsNullOrEmpty(model.PhoneNumber))
			//        {
			//            SQL += ",@PhoneNumber ";
			//            para.AddSQLParameter("@PhoneNumber", SqlDbType.VarChar, Conversion.ConversionToString(model.PhoneNumber), ParameterDirection.Input);
			//        }
			//        if (!string.IsNullOrEmpty(model.BillingState))
			//        {
			//            SQL += ",@BillingState ";
			//            para.AddSQLParameter("@BillingState", SqlDbType.VarChar, Conversion.ConversionToString(model.BillingState), ParameterDirection.Input);
			//        }
			//        if (!string.IsNullOrEmpty(model.BillingZip))
			//        {
			//            SQL += ",@BillingZip ";
			//            para.AddSQLParameter("@BillingZip", SqlDbType.VarChar, Conversion.ConversionToString(model.BillingZip), ParameterDirection.Input);
			//        }
			//        if (!string.IsNullOrEmpty(model.BillingCountry))
			//        {
			//            SQL += ",@BillingCountry ";
			//            para.AddSQLParameter("@BillingCountry", SqlDbType.VarChar, Conversion.ConversionToString(model.BillingCountry), ParameterDirection.Input);
			//        }

			//        if (!string.IsNullOrEmpty(model.MailingAddressLine1))
			//        {
			//            SQL += ",@MailingAddressLine1 ";
			//            para.AddSQLParameter("@MailingAddressLine1", SqlDbType.VarChar, Conversion.ConversionToString(model.MailingAddressLine1), ParameterDirection.Input);
			//        }
			//        if (!string.IsNullOrEmpty(model.MailingAddressLine2))
			//        {
			//            SQL += ",@MailingAddressLine2 ";
			//            para.AddSQLParameter("@MailingAddressLine2", SqlDbType.VarChar, Conversion.ConversionToString(model.MailingAddressLine2), ParameterDirection.Input);
			//        }
			//        if (!string.IsNullOrEmpty(model.MailingCity))
			//        {
			//            SQL += ",@MailingCity ";
			//            para.AddSQLParameter("@MailingCity", SqlDbType.VarChar, Conversion.ConversionToString(model.MailingCity), ParameterDirection.Input);
			//        }
			//        if (!string.IsNullOrEmpty(model.MailingState))
			//        {
			//            SQL += ",@MailingState ";
			//            para.AddSQLParameter("@MailingState", SqlDbType.VarChar, Conversion.ConversionToString(model.MailingState), ParameterDirection.Input);
			//        }
			//        if (!string.IsNullOrEmpty(model.MailingZip))
			//        {
			//            SQL += ",@MailingZip ";
			//            para.AddSQLParameter("@MailingZip", SqlDbType.VarChar, Conversion.ConversionToString(model.MailingZip), ParameterDirection.Input);
			//        }
			//        if (!string.IsNullOrEmpty(model.MailingCountry))
			//        {
			//            SQL += ",@MailingCountry ";
			//            para.AddSQLParameter("@MailingCountry", SqlDbType.VarChar, Conversion.ConversionToString(model.MailingCountry), ParameterDirection.Input);
			//        }

			//        if (model.dtPaymentStart != null && model.IsRecurring)
			//        {
			//            SQL += ",@dtPaymentStart ";
			//            para.AddSQLParameter("@dtPaymentStart", SqlDbType.DateTime, model.dtPaymentStart, ParameterDirection.Input);
			//        }

			//        if (model.dtPaymentEnd != null && model.IsRecurring)
			//        {
			//            SQL += ",@dtPaymentEnd ";
			//            para.AddSQLParameter("@dtPaymentEnd", SqlDbType.DateTime, model.dtPaymentEnd, ParameterDirection.Input);
			//        }
			//        if (!string.IsNullOrEmpty(model.PaymentEndType) && model.IsRecurring)
			//        {
			//            int PaymentEType = 0;
			//            Int32.TryParse(model.PaymentEndType, out PaymentEType);
			//            SQL += ",@PaymentEndType ";
			//            para.AddSQLParameter("@PaymentEndType", SqlDbType.Int, PaymentEType, ParameterDirection.Input);
			//        }
			//        if (!string.IsNullOrEmpty(model.RecurringInterval) && model.IsRecurring)
			//        {
			//            SQL += ",@RecurringInterval ";
			//            para.AddSQLParameter("@RecurringInterval", SqlDbType.VarChar, model.RecurringInterval, ParameterDirection.Input);
			//        }
			//        if (model.IsRecurring && model.PaymentMaxOccurences != null && model.PaymentMaxOccurences > 0)
			//        {
			//            SQL += ",@PaymentMaxOccurences ";
			//            para.AddSQLParameter("@PaymentMaxOccurences", SqlDbType.Int, model.PaymentMaxOccurences, ParameterDirection.Input);
			//        }

			//        if ((model.PaymentType == ((int)GlobalContext.PaymentType.CreditCard).ToString()))
			//        {
			//            SQL += ",@IsCreditCard ";
			//            para.AddSQLParameter("@IsCreditCard", SqlDbType.Bit, true, ParameterDirection.Input);
			//        }
			//        else if ((model.PaymentType == ((int)GlobalContext.PaymentType.eCheck).ToString()))
			//        {
			//            SQL += ",@IsECheck ";
			//            para.AddSQLParameter("@IsECheck", SqlDbType.Bit, true, ParameterDirection.Input);
			//        }
			//        if (!string.IsNullOrEmpty(model.CheckNumber))
			//        {
			//            SQL += ",@CheckNumber ";
			//            para.AddSQLParameter("@CheckNumber", SqlDbType.VarChar, Conversion.ConversionToString(model.CheckNumber), ParameterDirection.Input);
			//        }

			//        SQL += " ,@CreatedOn,@CreatedBy,@ModifiedOn,@ModifiedBy,@SubscriptionTransId,@SubscriptionResponseCode,@SubscriptionResponseText);Select SCOPE_IDENTITY() PaymentConfigId "; //;
			//        para.AddSQLParameter("@AmtDonation", SqlDbType.Money, model.AmtDonation, ParameterDirection.Input);
			//        para.AddSQLParameter("@AmtTransactionPaid", SqlDbType.Money, model.AmtTransactionPaid, ParameterDirection.Input);
			//        para.AddSQLParameter("@RecurringType", SqlDbType.VarChar, model.RecurringType, ParameterDirection.Input);
			//        para.AddSQLParameter("@BankAccountType", SqlDbType.VarChar, Conversion.ConversionToString(model.BankAccountType), ParameterDirection.Input);
			//        para.AddSQLParameter("@FirstName", SqlDbType.VarChar, Conversion.ConversionToString(model.FirstName), ParameterDirection.Input);
			//        para.AddSQLParameter("@EmailId", SqlDbType.VarChar, Conversion.ConversionToString(model.EmailId), ParameterDirection.Input);
			//        para.AddSQLParameter("@lkpDonationCategory", SqlDbType.Int, Conversion.ConversionToInt(model.lkpDonationCategory), ParameterDirection.Input);
			//        para.AddSQLParameter("@PaymentType", SqlDbType.Int, Conversion.ConversionToInt(model.PaymentType), ParameterDirection.Input);
			//        para.AddSQLParameter("@IsAnonymous", SqlDbType.Bit, Conversion.ConversionToBool(model.IsAnonymous), ParameterDirection.Input);
			//        para.AddSQLParameter("@BillingAddressLine1", SqlDbType.VarChar, Conversion.ConversionToString(model.BillingAddressLine1), ParameterDirection.Input);
			//        para.AddSQLParameter("@BillingAddressLine2", SqlDbType.VarChar, Conversion.ConversionToString(model.BillingAddressLine2), ParameterDirection.Input);
			//        para.AddSQLParameter("@BillingCity", SqlDbType.VarChar, Conversion.ConversionToString(model.BillingCity), ParameterDirection.Input);
			//        if (source == 2)
			//        {

			//            para.AddSQLParameter("@CreatedBy", SqlDbType.VarChar, GlobalContext.UserName, ParameterDirection.Input);
			//        }
			//        else
			//        {
			//            para.AddSQLParameter("@CreatedBy", SqlDbType.VarChar, Conversion.ConversionToString(model.FullName), ParameterDirection.Input);
			//        }
			//        para.AddSQLParameter("@CreatedOn", SqlDbType.DateTime, DateTime.Now, ParameterDirection.Input);
			//        if (source == 2)
			//        {

			//            para.AddSQLParameter("@ModifiedBy", SqlDbType.VarChar, GlobalContext.UserName, ParameterDirection.Input);
			//        }
			//        else
			//        {
			//            para.AddSQLParameter("@ModifiedBy", SqlDbType.VarChar, Conversion.ConversionToString(model.FullName), ParameterDirection.Input);
			//        }

			//        para.AddSQLParameter("@ModifiedOn", SqlDbType.DateTime, DateTime.Now, ParameterDirection.Input);

			//        para.AddSQLParameter("@SubscriptionTransId", SqlDbType.VarChar, Conversion.ConversionToString(model.SubscriptionTransId), ParameterDirection.Input);
			//        para.AddSQLParameter("@SubscriptionResponseCode", SqlDbType.VarChar, Conversion.ConversionToString(model.SubscriptionResponseCode), ParameterDirection.Input);
			//        para.AddSQLParameter("@SubscriptionResponseText", SqlDbType.VarChar, Conversion.ConversionToString(model.SubscriptionResponseText), ParameterDirection.Input);

			//        //para.AddSQLParameter("@PaymentConfigId", SqlDbType.Int, 0, ParameterDirection.Output);
			//        rValue = sp.GetSingleValue(SQL, CommandType.Text, para, GlobalContext.DB_CONN);

			//    }
			//}
			//catch (Exception ex)
			//{
			//    throw;
			//}
			return 0;// Conversion.ConversionToInt(rValue);
		}

		public int AddBatches(PaymentBatch model)
		{

			string SQL = string.Empty;
			//IStoredProcedure sp = new StoredProcedure();
			//SQLParameterList para = new SQLParameterList();
			//int rValue = 0;
			//try
			//{
			//    if (model != null)
			//    {

			//        SQL = " INSERT INTO PaymentBatch (BatchId,settlementTimeUTC,settlementTimeLocal,settlementState,marketType,product) ";
			//        SQL += " Select @BatchId,@settlementTimeUTC,@settlementTimeLocal,@settlementState,@marketType,@product ";
			//        SQL += " Where not exists (Select * From PaymentBatch Where BatchId=@BatchId)";
			//        para.AddSQLParameter("@BatchId", SqlDbType.Int, Conversion.ConversionToInt(model.BatchId), ParameterDirection.Input);
			//        para.AddSQLParameter("@settlementTimeUTC", SqlDbType.DateTime, Conversion.ConversionToDateTime(model.settlementTimeUTC), ParameterDirection.Input);
			//        para.AddSQLParameter("@settlementTimeLocal", SqlDbType.DateTime, Conversion.ConversionToDateTime(model.settlementTimeLocal), ParameterDirection.Input);
			//        para.AddSQLParameter("@settlementState", SqlDbType.VarChar, Conversion.ConversionToString(model.settlementState), ParameterDirection.Input);
			//        para.AddSQLParameter("@marketType", SqlDbType.VarChar, Conversion.ConversionToString(model.marketType), ParameterDirection.Input);
			//        para.AddSQLParameter("@product", SqlDbType.VarChar, Conversion.ConversionToString(model.product), ParameterDirection.Input);


			//        rValue = sp.InsertStoredProcedure(SQL.ToString(), CommandType.Text, para, GlobalContext.DB_CONN.ToString());
			//    }
			//}
			//catch
			//{
			//    throw;
			//}
			return 0;// rValue;
		}

		public int DeleteDonor(int ConfigId)
		{
			string SQL = string.Empty;
			int rValue = 0;
			//IStoredProcedure sp = new StoredProcedure();
			//SQLParameterList para = new SQLParameterList();
			//DataTable dt = new DataTable();
			//try
			//{
			//    SQL = "delete from p from [PaymentConfig] p left join [PaymentTransaction] t on t.PaymentConfigId=p.PaymentConfigId where p.PaymentConfigId=@PaymentConfig and t.PaymentConfigId is null ";
			//    para.AddSQLParameter("@PaymentConfig", SqlDbType.Int, ConfigId, ParameterDirection.Input);
			//    rValue = sp.DeleteStoredProcedure(SQL, CommandType.Text, para, GlobalContext.DB_CONN);
			//}
			//catch (Exception ex)
			//{
			//    string errorMsg = ex.Message;
			//    rValue = 0;
			//}
			return rValue;
		}

		public int DeletePayment(int ConfigId)
		{
			//string SQL = string.Empty;
			//int rValue = 0;
			//IStoredProcedure sp = new StoredProcedure();
			//SQLParameterList para = new SQLParameterList();
			//DataTable dt = new DataTable();
			//try
			//{
			//    SQL = "delete from [PaymentTransaction] where PaymentConfigId=@PaymentConfigId ";
			//    para.AddSQLParameter("@PaymentConfigId", SqlDbType.Int, ConfigId, ParameterDirection.Input);
			//    rValue = sp.DeleteStoredProcedure(SQL, CommandType.Text, para, GlobalContext.DB_CONN);
			//}
			//catch (Exception ex)
			//{
			//    string errorMsg = ex.Message;
			//    rValue = 0;
			//}
			return 0;// rValue;
		}


		//public void AddPaymentTransaction(ICYL.BL.PaymentTransaction model)
		//{
			int PaymentConfigId = 0;
			//try
			//{

			//    ICYL.Entity.PaymentTransaction obj = _ICYLEntities.PaymentTransactions.Where(x => x.TransId == model.TransId).FirstOrDefault();
			//    if (obj == null)
			//    {
			//        InsertPaymentTransaction(model);
			//        ICYL.Entity.PaymentTransaction objTrans = _ICYLEntities.PaymentTransactions.Where(x => x.TransId == model.TransId).FirstOrDefault();
			//        if (objTrans != null)
			//        {
			//            PaymentConfigId = InsertPaymentConfig(model);
			//            if (PaymentConfigId > 0)
			//            {
			//                objTrans.PaymentConfigId = PaymentConfigId;

			//                _ICYLEntities.Entry(objTrans).State = EntityState.Modified;
			//                _ICYLEntities.SaveChanges();
			//            }
			//        }
			//    }
			//    else
			//        UpdatePaymentTransaction(model);
			//}
			//catch (Exception ex)
			//{
			//    throw;
			//}
		//}

		//public int InsertPaymentTransaction(ICYL.BL.PaymentTransaction model, int source = 1)
		//{

			//string SQL = string.Empty;
			//IStoredProcedure sp = new StoredProcedure();
			//SQLParameterList para = new SQLParameterList();
			//int rValue = 0;
			//string CreatedDt = model.CreatedOn == null ? DateTime.Now.ToString() : model.CreatedOn;
			//try
			//{
			//    if (model != null)
			//    {

			//        SQL = " INSERT INTO PaymentTransaction (PaymentConfigId,AmtTransaction,TransId,TransResponseCode,TransMessageCode,TransDescription ";
			//        SQL += " ,TransAuthCode,TransErrorCode,TransErrorText,CreatedBy,CreatedOn,BatchId)  ";
			//        SQL += " Select @PaymentConfigId,@AmtTransaction,@TransId,@TransResponseCode,@TransMessageCode,@TransDescription";
			//        SQL += " ,@TransAuthCode,@TransErrorCode,@TransErrorText,@CreatedBy,@CreatedOn,@BatchId";
			//        SQL += " Where Not Exists (Select * From PaymentTransaction Where TransId=@TransId and IsNull(BatchId,0) = @BatchId)";

			//        para.AddSQLParameter("@PaymentConfigId", SqlDbType.Int, Conversion.ConversionToInt(model.PaymentConfigId), ParameterDirection.Input);
			//        para.AddSQLParameter("@AmtTransaction", SqlDbType.Money, Conversion.ConversionToDouble(model.AmtTransaction), ParameterDirection.Input);
			//        para.AddSQLParameter("@TransId", SqlDbType.VarChar, Conversion.ConversionToString(model.TransId), ParameterDirection.Input);
			//        para.AddSQLParameter("@TransResponseCode", SqlDbType.VarChar, Conversion.ConversionToString(model.TransResponseCode), ParameterDirection.Input);
			//        para.AddSQLParameter("@TransMessageCode", SqlDbType.VarChar, Conversion.ConversionToString(model.TransMessageCode), ParameterDirection.Input);
			//        para.AddSQLParameter("@TransDescription", SqlDbType.VarChar, Conversion.ConversionToString(model.TransDescription), ParameterDirection.Input);
			//        para.AddSQLParameter("@TransAuthCode", SqlDbType.VarChar, Conversion.ConversionToString(model.TransAuthCode), ParameterDirection.Input);
			//        para.AddSQLParameter("@TransErrorCode", SqlDbType.VarChar, Conversion.ConversionToString(model.TransErrorCode), ParameterDirection.Input);
			//        para.AddSQLParameter("@TransErrorText", SqlDbType.VarChar, Conversion.ConversionToString(model.TransErrorText), ParameterDirection.Input);
			//        if(source==2)
			//        {
			//            para.AddSQLParameter("@CreatedBy", SqlDbType.VarChar, GlobalContext.UserName, ParameterDirection.Input);

			//        }
			//        else
			//        {
			//            para.AddSQLParameter("@CreatedBy", SqlDbType.VarChar, Conversion.ConversionToString(model.CreatedBy), ParameterDirection.Input);
			//        }
			//        para.AddSQLParameter("@CreatedOn", SqlDbType.DateTime, CreatedDt, ParameterDirection.Input);
			//        para.AddSQLParameter("@BatchId", SqlDbType.Int, Conversion.ConversionToInt(model.BatchId), ParameterDirection.Input);

			//        rValue = sp.InsertStoredProcedure(SQL.ToString(), CommandType.Text, para, GlobalContext.DB_CONN.ToString());
			//    }
			//}
			//catch
			//{
			//    throw;
			//}
		//	return 0;// rValue;
		//}

		//public int UpdatePaymentTransaction(ICYL.BL.PaymentTransaction model)
		//{
		//	int rValue = 0;
			//try
			//{
			//    ICYL.Entity.PaymentTransaction obj = _ICYLEntities.PaymentTransactions.Where(x => x.TransId == model.TransId).FirstOrDefault();
			//    if (obj != null)
			//    {
			//        obj.BatchId = model.BatchId;
			//        _ICYLEntities.Entry(obj).State = EntityState.Modified;
			//        _ICYLEntities.SaveChanges();
			//        rValue = obj.TransactionId;
			//    }
			//}
			//catch
			//{
			//    throw;
		//	//}
		//	return 0;// rValue;
		//}

		//public int InsertPaymentConfig(ICYL.BL.PaymentTransaction model)
		//{
		//	string name = string.Empty;
		//	int result = 0;
			//DateTime? CreatedDt = model.CreatedOn == null ? DateTime.Now : Conversion.ConversionToDateTime(model.CreatedOn);
			//try
			//{
			//    ICYL.Entity.PaymentConfig obj = new ICYL.Entity.PaymentConfig();

			//    obj.FirstName = Conversion.ConversionToString(model.PaymentConfigs.FirstName);
			//    obj.LastName = Conversion.ConversionToString(model.PaymentConfigs.LastName);
			//    obj.AmtDonation = model.AmtTransaction;
			//    obj.AmtTransactionPaid = 0;
			//    obj.EmailId = string.IsNullOrEmpty(model.PaymentConfigs.EmailId) ? "" : model.PaymentConfigs.EmailId;
			//    obj.BillingAddressLine1 = model.PaymentConfigs.BillingAddressLine1;
			//    obj.BillingCity = model.PaymentConfigs.BillingCity;
			//    obj.BillingState = model.PaymentConfigs.BillingState;
			//    obj.BillingZip = model.PaymentConfigs.BillingZip;
			//    obj.PaymentType = Conversion.ConversionToInt(model.PaymentConfigs.PaymentType);
			//    obj.PhoneNumber = model.PaymentConfigs.PhoneNumber;
			//    obj.lkpDonationCategory = model.PaymentConfigs.lkpDonationCategory;
			//    obj.isDownloaded = true;//this function is using to download the record.
			//    obj.CreatedOn = CreatedDt;
			//    _ICYLEntities.Entry(obj).State = EntityState.Added;
			//    _ICYLEntities.SaveChanges();
			//    result = obj.PaymentConfigId;

			//    return result;
			//}
			//catch (Exception ex)
			//{
			//    throw ex;
			//}
		//	return 0;
		//}

		//public void AddSubscriptionPaymentTransaction(ICYL.BL.PaymentTransaction model)
		//{
			//try
			//{

			//    ICYL.Entity.PaymentConfig obj = _ICYLEntities.PaymentConfigs.Where(x => x.SubscriptionTransId != null && x.SubscriptionTransId == model.PaymentConfigs.SubscriptionTransId.ToString()).FirstOrDefault();
			//    if (obj != null)
			//    {
			//        model.PaymentConfigId = obj.PaymentConfigId;
			//        ICYL.Entity.PaymentTransaction objTrans = _ICYLEntities.PaymentTransactions.Where(x => x.TransId == model.TransId).FirstOrDefault();
			//        if (objTrans != null)
			//        {
			//            model.TransactionId = objTrans.TransactionId;
			//            if (objTrans.BatchId == null || objTrans.BatchId == 0)
			//            {
			//                UpdatePaymentTransaction(model);
			//            }
			//        }
			//        else
			//        {
			//            InsertPaymentTransaction(model);
			//        }
			//    }
			//}
			//catch (Exception ex)
			//{
			//    throw;
			//}
		//}

		public int InsertSubscriptionPaymentConfig(SubscriptionDetail model, paymentScheduleType PaySchedule, int CategoryId = 2)
		{
			string name = string.Empty;
			int result = 0;

			//try
			//{
			//    ICYL.Entity.PaymentConfig Config = _ICYLEntities.PaymentConfigs.Where(x => x.SubscriptionTransId == model.id.ToString()).FirstOrDefault();
			//    if (Config == null)
			//    {
			//        var request = new getCustomerProfileRequest();
			//        request.customerProfileId = model.customerProfileId.ToString();

			//        var controller = new getCustomerProfileController(request);
			//        controller.Execute();
			//        var response = controller.GetApiResponse();

			//        ICYL.Entity.PaymentConfig obj = new ICYL.Entity.PaymentConfig();
			//        obj.FirstName = model.firstName;
			//        obj.LastName = model.lastName;
			//        obj.AmtDonation = model.amount;
			//        obj.AmtTransactionPaid = 0;
			//        obj.lkpDonationCategory = CategoryId;
			//        obj.CustomerProfileId = model.customerProfileId.ToString();
			//        obj.SubscriptionTransId = model.id.ToString();
			//        if (model.paymentMethod == paymentMethodEnum.creditCard)
			//        {
			//            obj.IsCreditCard = true;
			//            obj.IsECheck = false;
			//            obj.PaymentType = 5;
			//        }
			//        else if (model.paymentMethod == paymentMethodEnum.eCheck)
			//        {
			//            obj.IsCreditCard = false;
			//            obj.IsECheck = true;
			//            obj.PaymentType = 6;
			//        }
			//        if (response != null)
			//        {
			//            if (response.profile != null)
			//            {
			//                obj.EmailId = response.profile.email;
			//                if (response.profile.paymentProfiles != null && response.profile.paymentProfiles.Count() > 0)
			//                {
			//                    if (response.profile.paymentProfiles[0].billTo != null)
			//                    {
			//                        obj.CompanyName = response.profile.paymentProfiles[0].billTo.company;
			//                        obj.BillingAddressLine1 = response.profile.paymentProfiles[0].billTo.address;
			//                        obj.BillingCity = response.profile.paymentProfiles[0].billTo.city;
			//                        obj.BillingState = response.profile.paymentProfiles[0].billTo.state;
			//                        obj.BillingZip = response.profile.paymentProfiles[0].billTo.zip;
			//                        obj.PhoneNumber = response.profile.paymentProfiles[0].billTo.phoneNumber;
			//                    }
			//                }
			//                if (response.profile.shipToList != null)
			//                {
			//                    obj.MailingAddressLine1 = response.profile.shipToList[0].address;
			//                    obj.MailingCity = response.profile.shipToList[0].city;
			//                    obj.MailingState = response.profile.shipToList[0].state;
			//                    obj.MailingZip = response.profile.shipToList[0].zip;
			//                    obj.CustomerAddressId = response.profile.shipToList[0].customerAddressId;
			//                }
			//            }

			//        }
			//        obj.isDownloaded = true;
			//        obj.CreatedOn = Conversion.ConversionToDateTime(model.createTimeStampUTC);
			//        if (PaySchedule != null)
			//        {
			//            getSubscriptionSchedule(PaySchedule, obj);
			//        }
			//        _ICYLEntities.Entry(obj).State = EntityState.Added;
			//        _ICYLEntities.SaveChanges();
			//        result = obj.PaymentConfigId;
			//    }
			//    else
			//    {
			//        result = Config.PaymentConfigId;
			//    }


			//    return result;
			//}
			//catch (Exception ex)
			//{
			//    throw ex;
			//}
			return 0;
		}
		//public void getSubscriptionSchedule(paymentScheduleType response, ICYL.Entity.PaymentConfig obj)
		//{
		//    DateTime? endDate = null;
		//    if (response != null && response.interval != null)
		//    {
		//        obj.dtPaymentStart = response.startDate;
		//        obj.RecurringType = response.interval.length.ToString();
		//        obj.RecurringInterval = response.interval.unit.ToString();
		//        if (response.totalOccurrences < 9999)
		//        {
		//            obj.PaymentMaxOccurences = response.totalOccurrences;
		//            if (response.interval.unit == ARBSubscriptionUnitEnum.days)
		//            {
		//                endDate = response.startDate.AddDays(response.totalOccurrences * response.interval.length);
		//            }
		//            else
		//            {
		//                endDate = response.startDate.AddMonths(response.totalOccurrences * response.interval.length);
		//            }
		//            obj.dtPaymentEnd = endDate;
		//        }
		//    }
		// }




		//#region Donations
		//#endregion Donations

	}
}

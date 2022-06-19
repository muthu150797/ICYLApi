using EnterpriseLayer.DataAccess;
using EnterpriseLayer.DataAccess.MSSQL;
using EnterpriseLayer.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ICYL.Repository
{
    public class ErrorLogRepository
    {
        public static string ErrorLogInsert(HandleErrorInfo obj, string UserName)
        {
            string ErrorCode = string.Empty;
            IStoredProcedure sp = new StoredProcedure();
            SQLParameterList para = new SQLParameterList();
            try
            {
                para.AddSQLParameter("@ControllerName", SqlDbType.NVarChar, obj.ControllerName, ParameterDirection.Input);
                para.AddSQLParameter("@ActionName", SqlDbType.NVarChar, obj.ActionName, ParameterDirection.Input);
                para.AddSQLParameter("@ErrorMessage", SqlDbType.NVarChar, obj.Exception.Message, ParameterDirection.Input);
                para.AddSQLParameter("@StackTrace", SqlDbType.NVarChar, obj.Exception.StackTrace, ParameterDirection.Input);
                para.AddSQLParameter("@CreatedBy", SqlDbType.NVarChar, UserName, ParameterDirection.Input);
                para.AddSQLParameter("@ErrorId", SqlDbType.VarChar, ErrorCode, ParameterDirection.Output);
                sp.InsertStoredProcedure("upsICYLErrorLog", CommandType.StoredProcedure, para, GlobalContext.DB_CONN);
                ErrorCode = Conversion.ConversionToString(para["@ErrorId"].SQLParameterValue);
                return ErrorCode;
            }
            catch
            {
                ErrorCode = "ERR0XICYLLOG4";
                return ErrorCode;
            }
        }

        public static string ErrorLogInsert(string sControllerName, string sActionName, string sErrorMessage, string sStackTrace, string sUserName)
        {
            string ErrorCode = string.Empty;
            IStoredProcedure sp = new StoredProcedure();
            SQLParameterList para = new SQLParameterList();
            try
            {
                para.AddSQLParameter("@ControllerName", SqlDbType.NVarChar, sControllerName, ParameterDirection.Input);
                para.AddSQLParameter("@ActionName", SqlDbType.NVarChar, sActionName, ParameterDirection.Input);
                para.AddSQLParameter("@ErrorMessage", SqlDbType.NVarChar, sErrorMessage, ParameterDirection.Input);
                para.AddSQLParameter("@StackTrace", SqlDbType.NVarChar, sStackTrace, ParameterDirection.Input);
                para.AddSQLParameter("@CreatedBy", SqlDbType.NVarChar, sUserName, ParameterDirection.Input);
                para.AddSQLParameter("@ErrorId", SqlDbType.VarChar, ErrorCode, ParameterDirection.Output);
                sp.InsertStoredProcedure("upsICYLErrorLog", CommandType.StoredProcedure, para, GlobalContext.DB_CONN);
                ErrorCode = Conversion.ConversionToString(para["@ErrorId"].SQLParameterValue);
                return ErrorCode;
            }
            catch (Exception ex)
            {
                //Ignore or Write the error some where else - Text
                string errorMessage = ex.Message;
                return ErrorCode;
            }
        }
    }
}

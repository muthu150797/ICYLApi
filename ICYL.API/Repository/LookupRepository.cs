using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using ICYL.BL;
//using ICYL.Entity;
//using EnterpriseLayer.Utilities;
using System.Data;
//using EnterpriseLayer.DataAccess.MSSQL;
//using EnterpriseLayer.DataAccess;
//using System.Web.Mvc;
//using EnterpriseLayer.DataSecurity;
using ICYL.API.Repository;
using Microsoft.AspNetCore.Mvc.Rendering;
using ICYL.API.Entity;
using System.Data.SqlClient;

namespace ICYL.Repository
{

	public class LookupRepository
	{
		private readonly string _connectionstring;

		//public List<API.Repository.LookupGroupBL> GetLookupGroup()
		////{
		////    LookupGroupBL tc = new LookupGroupBL();
		////    List<LookupGroupBL> tlst = new List<LookupGroupBL>();
		////    using (ICYLEntities dc = new ICYLEntities())
		////    {
		////        var rValue = dc.LookupGroups;
		////        foreach (LookupGroup lg in rValue)
		////        {
		////            tc = new LookupGroupBL();
		////            tc.GroupId = lg.GroupId;
		////            tc.GroupName = lg.GroupName;
		////            tlst.Add(tc);
		////        }
		////    }
		//    return null;
		//}

		//public List<SelectListItem> getCategoryDropDown()
		//{
		//    //List<SelectListItem> lCat = new List<SelectListItem>();
		//    //List<LookupValueBL> lst = GetLookupValueByGroupId((int)GlobalContext.LookupGroupCategory.DonationCategory);
		//    //var list = new List<Object>();
		//    //lCat = (from LookupValueBL row in lst
		//    //        select new SelectListItem
		//    //        {
		//    //            Value = row.ValueId.ToString(),
		//    //            Text = row.Value

		//    //        }).ToList();
		//    //lCat.Insert(0, new SelectListItem { Value = "0", Text = "PLEASE SELECT THE DONATION CATEGORY" });
		//    return null;
		//}

		public List<SelectListItem> getPaymentTypeDropDown()
		{
			//List<SelectListItem> lCat = new List<SelectListItem>();
			//List<API.Repository.LookupValueBL> lst = GetLookupValueByGroupId((int)GlobalContext.LookupGroupCategory.PaymentType);
			//var list = new List<Object>();
			//lCat = (from LookupValueBL row in lst
			//        select new SelectListItem
			//        {
			//            Value = row.ValueId.ToString(),
			//            Text = row.Value

			//        }).ToList();
			return null;
		}
		//public List<SelectListItem> getRecurringTypeDropDown()
		//{
		//    var list = new List<SelectListItem>
		//    {
		//        new SelectListItem{ Text="Week", Value = "7"},
		//        new SelectListItem{ Text="Month", Value = "1"},
		//        new SelectListItem{ Text="Other Month", Value = "2"},
		//        new SelectListItem{ Text="Three Months", Value = "3"},
		//        new SelectListItem{ Text="Six Months", Value = "6"},
		//        new SelectListItem{ Text="Year", Value = "12"},

		//    };
		//    return list;
		//}

		//public List<SelectListItem> getDonationTypeDropDown()
		//{
		//    var list = new List<SelectListItem>
		//    {
		//        new SelectListItem{ Text="OneTime", Value = "1"},
		//        new SelectListItem{ Text="Recurring", Value = "2"},
		//    };
		//    return list;
		//}

		//     public List<SelectListItem> getTransResponseCodeDropDown()
		//     {
		//var list = new List<SelectListItem>
		//{
		//	new SelectListItem{ Text="Successful Transaction", Value = "1"},
		//	new SelectListItem{ Text="Un-Successful Transaction", Value = "2"},
		//};
		//return null;// list;
		//     }

		//public List<BL.LookupValueBL> getLookupList(API.Repository.LookupValueBLList obj)
		//{
		//    List<API.Repository.LookupValueBL> lst = new List<API.Repository.LookupValueBL>();
		//    try
		//    {
		//       // DataTable dt = getDataTableLookupList(obj);
		//        //foreach (DataRow dr in dt.Rows)
		//        //{
		//        //    BL.LookupValueBL lv = new BL.LookupValueBL();
		//        //    lv.ValueId = Conversion.ConversionToInt(dr["ValueId"]);
		//        //    lv.GroupId = Conversion.ConversionToInt(dr["GroupId"]);
		//        //    lv.Value = Conversion.ConversionToString(dr["Value"]);
		//        //    lv.ValueDescription = Conversion.ConversionToString(dr["ValueDescription"]);
		//        //    lv.DisplayOrder = Conversion.ConversionToInt(dr["DisplayOrder"]);
		//        //    lv.Active = Conversion.ConversionToBool(dr["Active"]);
		//        //    lv.CreatedBy = Conversion.ConversionToString(dr["CreatedBy"]);
		//        //    lv.CreatedOn = Conversion.ConversionToDateTime(dr["CreatedOn"]);
		//        //    lv.ModifiedBy = Conversion.ConversionToString(dr["ModifiedBy"]);
		//        //    lv.ModifiedOn = Conversion.ConversionToDateTime(dr["ModifiedOn"]);
		//        //    lst.Add(lv);
		//        //}
		//    }
		//    catch { throw; }
		//    return null;
		//}

		//public List<API.Repository.LookupValueBL> getCategoryLookupList()
		//{
		//    List<API.Repository.LookupValueBL> lst = new List<API.Repository.LookupValueBL>();
		//    try
		//    {
		//DataTable dt = getDataTableLookupCategoryList();
		//foreach (DataRow dr in dt.Rows)
		//{
		//    BL.LookupValueBL lv = new BL.LookupValueBL();
		//    lv.ValueId = Conversion.ConversionToInt(dr["ValueId"]);
		//    lv.GroupId = Conversion.ConversionToInt(dr["GroupId"]);
		//    lv.Value = Conversion.ConversionToString(dr["Value"]);
		//    lv.ValueDescription = Conversion.ConversionToString(dr["ValueDescription"]);
		//    lv.DisplayOrder = Conversion.ConversionToInt(dr["DisplayOrder"]);
		//    if (Conversion.ConversionToString(dr["APIId"]) != null)
		//    {
		//        lv.APIId = DataEncryptionDecryption.DecryptionString(Conversion.ConversionToString(dr["APIId"]));
		//    }
		//    if (Conversion.ConversionToString(dr["APIKey"]) != null)
		//    {
		//        lv.APIKey = DataEncryptionDecryption.DecryptionString(Conversion.ConversionToString(dr["APIKey"]));
		//    }
		//    lv.Active = Conversion.ConversionToBool(dr["Active"]);
		//    lv.CreatedBy = Conversion.ConversionToString(dr["CreatedBy"]);
		//    lv.CreatedOn = Conversion.ConversionToDateTime(dr["CreatedOn"]);
		//    lv.ModifiedBy = Conversion.ConversionToString(dr["ModifiedBy"]);
		//    lv.ModifiedOn = Conversion.ConversionToDateTime(dr["ModifiedOn"]);
		//    lst.Add(lv);
		//}
		//    }
		//    catch { throw; }
		//    return null;
		//}




		//public List<LookupValueBL> GetLookupValueByGroupId(int Id)
		//{
		//    LookupValueBL tc = new LookupValueBL();
		//    List<LookupValueBL> tlst = new List<LookupValueBL>();
		//    using (ICYLEntities dc = new ICYLEntities())
		//    {
		//        var rValue = dc.LookupValues.Where(x => x.GroupId == Id).OrderBy(x => x.DisplayOrder).DefaultIfEmpty();
		//        foreach (LookupValue lg in rValue)
		//        {
		//            tc = new LookupValueBL();
		//            tc.ValueId = lg.ValueId;
		//            tc.GroupId = lg.GroupId;
		//            tc.Value = lg.Value;
		//            tc.ValueDescription = lg.ValueDescription;
		//            tc.Active = Conversion.ConversionToBool(lg.Active);
		//            tc.CreatedBy = lg.CreatedBy;
		//            tc.CreatedOn = lg.CreatedOn;
		//            tc.ModifiedBy = lg.ModifiedBy;
		//            tc.ModifiedOn = lg.ModifiedOn;
		//            tc.DisplayOrder = Conversion.ConversionToInt(lg.DisplayOrder);
		//            tlst.Add(tc);
		//        }
		//    }
		//    return tlst;
		//}

		//public List<BL.LookupValueBL> GetLookupValueByGroupId(int Id)
		//{
		//    List<API.Repository.LookupValueBL> lst = new List<API.Repository.LookupValueBL>();
		//    try
		//    {
		//        //DataTable dt = GetDTLookupValueByGroupId(Id);
		//        //foreach (DataRow dr in dt.Rows)
		//        //{
		//        //    BL.LookupValueBL lv = new BL.LookupValueBL();
		//        //    lv.ValueId = Conversion.ConversionToInt(dr["ValueId"]);
		//        //    lv.GroupId = Conversion.ConversionToInt(dr["GroupId"]);
		//        //    lv.Value = Conversion.ConversionToString(dr["Value"]);
		//        //    lv.ValueDescription = Conversion.ConversionToString(dr["ValueDescription"]);
		//        //    lv.DisplayOrder = Conversion.ConversionToInt(dr["DisplayOrder"]);
		//        //    lv.Active = Conversion.ConversionToBool(dr["Active"]);
		//        //    lv.CreatedBy = Conversion.ConversionToString(dr["CreatedBy"]);
		//        //    lv.CreatedOn = Conversion.ConversionToDateTime(dr["CreatedOn"]);
		//        //    lv.ModifiedBy = Conversion.ConversionToString(dr["ModifiedBy"]);
		//        //    lv.ModifiedOn = Conversion.ConversionToDateTime(dr["ModifiedOn"]);
		//        //    lst.Add(lv);
		//        //}
		//    }
		//    catch { throw; }
		//    return null;
		//}

		//public DataTable GetDTLookupValueByGroupId(int Id)
		//{
		//    //try
		//    //{
		//    //    string SQL = string.Empty;
		//    //    IStoredProcedure sp = new StoredProcedure();
		//    //    SQLParameterList para = new SQLParameterList();
		//    //    DataTable dt = new DataTable();
		//    //    dataAccess clsDS = new dataAccess(GlobalContext.DB_CONN);
		//    //    SQL = " select ValueId, GroupId, Value, ValueDescription, isNull(Active,1) Active, CreatedBy, CreatedOn, ModifiedBy, ModifiedOn, DisplayOrder from LookupValue where GroupId = @Id and Active=1 order By DisplayOrder ";
		//    //    para.AddSQLParameter("@Id", SqlDbType.VarChar, Id, ParameterDirection.Input);
		//    //    dt = sp.GetDataTable(SQL, CommandType.Text, para, GlobalContext.DB_CONN.ToString());
		//    //    return dt;
		//    //}
		//    //catch { throw; }
		//    return null;
		//}

		public dynamic GetLookupValueById(int Id)
		{
			//LookUpModel tc = new LookUpModel().GetLookupValueById(Id);
			try
			{
				var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false).Build();
				//var connectionstring = config.GetSection("DbConnection").Value;
				//using (SqlConnection connection = new SqlConnection(connectionstring))
				//{
				//	SqlDataAdapter categoryDA;
				//	categoryDA = new SqlDataAdapter("select * from LookupValue where ValueId=" + Id, connection);
				//	categoryDA.SelectCommand.CommandType = CommandType.Text;
				//	//Using Data Table
				//	DataTable userDataTable = new DataTable();
				//	categoryDA.Fill(userDataTable);
				//	if (userDataTable.Rows != null)
				//	{
				//		foreach (DataRow row in userDataTable.Rows)
				//		{
				//			tc.ValueId = (int)row["ValueId"];
				//			tc.Value = (string)row["Value"];
				//			//tc.CreatedBy = (string)row["CreatedBy"];
				//			//tc.CreatedOn = (dynamic)row["CreatedOn"];
				//			//tc.ModifiedBy = (string)row["ModifiedBy"];
				//			//tc.ValueDescription = (string)row["ValueDescription"];
				//			tc.Active = (dynamic)row["Active"];
				//			//tc.ModifiedOn = (dynamic)row["ModifiedOn"];
				//			if (!string.IsNullOrEmpty((string)row["APIId"]))
				//			{
				//				var base64EncodedBytes = System.Convert.FromBase64String((string)row["APIId"]);
				//				var decrypt = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
				//				tc.APIId = (string)row["APIId"];
				//			}
				//			if (!string.IsNullOrEmpty((string)row["APIKey"]))
				//			{
				//				var base64EncodedBytes = System.Convert.FromBase64String((string)row["APIKey"]);
				//				var decrypt = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
				//				tc.APIKey = (string)row["APIKey"];
				//			}
				//		}
				//	}
				//	else
				//	{
				//	}
				//}
			}
			catch (Exception ex)
			{
			}
			//using (ICYLEntities dc = new ICYLEntities())
			//{
			//	var rValue = dc.LookupValues.Where(a => a.ValueId == Id).FirstOrDefault();
			//	if (rValue != null)
			//	{
			//		tc.ValueId = rValue.ValueId;
			//		tc.GroupId = rValue.GroupId;
			//		tc.Value = rValue.Value;
			//		tc.ValueDescription = rValue.ValueDescription;
			//		tc.Active = Conversion.ConversionToBool(rValue.Active);
			//		tc.CreatedBy = rValue.CreatedBy;
			//		tc.CreatedOn = rValue.CreatedOn;
			//		tc.ModifiedBy = rValue.ModifiedBy;
			//		tc.ModifiedOn = rValue.ModifiedOn;
			//		try
			//		{
			//			if (!string.IsNullOrEmpty(rValue.APIId))
			//			{
			//				tc.APIId = DataEncryptionDecryption.DecryptionString(rValue.APIId);
			//			}
			//			if (!string.IsNullOrEmpty(rValue.APIKey))
			//			{
			//				tc.APIKey = DataEncryptionDecryption.DecryptionString(rValue.APIKey);
			//			}
			//		}
			//		catch
			//		{
			//			//ignore
			//		}
			//		tc.DisplayOrder = Conversion.ConversionToInt(rValue.DisplayOrder);
			//		//  tc.lookupGroup = GetLookupGroupById(Conversion.ConversionToInt(tc.GroupId));
			//	}
			//}
			return null;
		}

		public DataTable getDataTableLookupList(API.Repository.LookupValueBLList obj)
		{
			try
			{
				string SQL = string.Empty;
				//IStoredProcedure sp = new StoredProcedure();
				//SQLParameterList para = new SQLParameterList();
				//DataTable dt = new DataTable();
				//dataAccess clsDS = new dataAccess(GlobalContext.DB_CONN);


				//SQL = " select ValueId, GroupId, Value, ValueDescription, isNull(Active,1) Active,   APIId,APIKey, CreatedBy, CreatedOn, ModifiedBy, ModifiedOn, DisplayOrder from LookupValue where GroupId <> 1 ";
				//if (obj.GroupId != null && obj.GroupId > 0)
				//{
				//    SQL += " and GroupId = @GroupId ";
				//    para.AddSQLParameter("@GroupId", SqlDbType.VarChar, obj.GroupId, ParameterDirection.Input);
				//}
				//if (!string.IsNullOrEmpty(obj.Value))
				//{
				//    SQL += " and Value like '%'+@Value+'%' ";
				//    para.AddSQLParameter("@Value", SqlDbType.VarChar, obj.Value, ParameterDirection.Input);
				//}
				//SQL += " order by DisplayOrder  ";

				//dt = sp.GetDataTable(SQL, CommandType.Text, para, GlobalContext.DB_CONN.ToString());
				return null;
			}
			catch { throw; }
		}

		//private DataTable getDataTableLookupCategoryList()
		//{
		//    try
		//    {
		//string SQL = string.Empty;
		//IStoredProcedure sp = new StoredProcedure();
		//SQLParameterList para = new SQLParameterList();
		//DataTable dt = new DataTable();
		//dataAccess clsDS = new dataAccess(GlobalContext.DB_CONN);
		//SQL = " select ValueId, GroupId, Value, ValueDescription, isNull(Active,1) Active, APIId,APIKey,  CreatedBy, CreatedOn, ModifiedBy, ModifiedOn, DisplayOrder from LookupValue where  GroupId = 1 ";
		//SQL += " order by DisplayOrder  ";

		//dt = sp.GetDataTable(SQL, CommandType.Text, para, GlobalContext.DB_CONN.ToString());
		//        return null;
		//    }
		//    catch { throw; }
		//}

		//     public API.Repository.LookupGroupBL GetLookupGroupById(int Id)
		//     {
		//API.Repository.LookupGroupBL tc = new API.Repository.LookupGroupBL();
		//        //using (ICYLEntities dc = new ICYLEntities())
		//         //{
		//         //    var rValue = dc.LookupGroups.Where(a => a.GroupId == Id).FirstOrDefault();
		//         //    if (rValue != null)
		//         //    {
		//         //        tc.GroupId = rValue.GroupId;
		//         //        tc.GroupName = rValue.GroupName;
		//         //        tc.GroupDescription = rValue.GroupDescription;
		//         //        tc.CreatedBy = rValue.CreatedBy;
		//         //        tc.CreatedOn = rValue.CreatedOn;
		//         //        tc.ModifiedBy = rValue.ModifiedBy;
		//         //        tc.ModifiedOn = rValue.ModifiedOn;
		//         //    }
		//         //}
		//         return  tc;
		//     }

		//public dynamic SaveLookup(API.Repository.LookupValueBL model)
		//{
		//    //string SQL = string.Empty;
		//     object rValue = 0;
		//    //IStoredProcedure sp = new StoredProcedure();
		//    //SQLParameterList para = new SQLParameterList();
		//    //SQL = "INSERT INTO [dbo].[LookupValue]([GroupId] ,[Value] ,[ValueDescription] ,[Active] ,[CreatedBy],[CreatedOn],[ModifiedBy],[ModifiedOn]   ";
		//    //if (model.APIId != null && model.APIId != "0")
		//    //{
		//    //    SQL += ",[APIId],[APIKey] ";
		//    //}
		//    //SQL += ",[DisplayOrder]) ";
		//    //SQL += "Values (@GroupId ,@Value ,@ValueDescription ,@Active ,@CreatedBy,@CreatedOn,@ModifiedBy,@ModifiedOn ";
		//    //para.AddSQLParameter("@GroupId", SqlDbType.VarChar, Conversion.ConversionToString(model.GroupId), ParameterDirection.Input);
		//    //para.AddSQLParameter("@Value", SqlDbType.VarChar, Conversion.ConversionToString(model.Value), ParameterDirection.Input);
		//    //para.AddSQLParameter("@ValueDescription", SqlDbType.VarChar, Conversion.ConversionToString(model.ValueDescription), ParameterDirection.Input);
		//    //para.AddSQLParameter("@Active", SqlDbType.VarChar, true, ParameterDirection.Input);
		//    //para.AddSQLParameter("@CreatedBy", SqlDbType.VarChar, GlobalContext.UserName, ParameterDirection.Input);
		//    //para.AddSQLParameter("@CreatedOn", SqlDbType.DateTime, DateTime.Now, ParameterDirection.Input);
		//    //para.AddSQLParameter("@ModifiedBy", SqlDbType.VarChar, GlobalContext.UserName, ParameterDirection.Input);
		//    //para.AddSQLParameter("@ModifiedOn", SqlDbType.DateTime, DateTime.Now, ParameterDirection.Input);
		//    //if (model.APIId != null && model.APIId != "0")
		//    //{
		//    //    SQL += " ,@APIId,@APIKey ";
		//    //    para.AddSQLParameter("@APIId", SqlDbType.VarChar, DataEncryptionDecryption.EncryptionString(model.APIId), ParameterDirection.Input);
		//    //    para.AddSQLParameter("@APIKey", SqlDbType.VarChar, DataEncryptionDecryption.EncryptionString(model.APIKey), ParameterDirection.Input);
		//    //}
		//    //SQL += " ,@DisplayOrder);Select SCOPE_IDENTITY() ";
		//    //para.AddSQLParameter("@DisplayOrder", SqlDbType.Int, Conversion.ConversionToInt(model.DisplayOrder), ParameterDirection.Input);
		//    //rValue = sp.GetSingleValue(SQL, CommandType.Text, para, GlobalContext.DB_CONN);
		//    return (rValue);
		//}



		//public int GetNextDisplayOrder(int groupId)
		//{
		//    //int maxValue = 0;
		//    //using (ICYLEntities dc = new ICYLEntities())
		//    //{
		//    //    if (groupId > 0)
		//    //    {
		//    //        try
		//    //        {
		//    //            maxValue = dc.LookupValues.Where(a => a.GroupId == groupId).Max(a => a.DisplayOrder).Value;
		//    //        }
		//    //        catch
		//    //        {

		//    //        }
		//    //    }
		//    //}
		//    return 0;// maxValue + 1;
		//}

		//public int UpdateLookup(API.Repository.LookupValueBL model)
		//{
		//    string SQL = string.Empty;
		//    object rValue = 0;
		//    //IStoredProcedure sp = new StoredProcedure();
		//    //SQLParameterList para = new SQLParameterList();
		//    //SQL = "UPDATE [dbo].[LookupValue] SET ModifiedBy =@ModifiedBy,ModifiedOn=@ModifiedOn   ";

		//    //if(!string.IsNullOrEmpty(model.Value))
		//    //{
		//    //    SQL += " ,Value=@Value ";
		//    //    para.AddSQLParameter("@Value", SqlDbType.VarChar, Conversion.ConversionToString(model.Value), ParameterDirection.Input);
		//    //}
		//    //if (!string.IsNullOrEmpty(model.ValueDescription))
		//    //{
		//    //    SQL += " ,ValueDescription=@ValueDescription ";
		//    //    para.AddSQLParameter("@ValueDescription", SqlDbType.VarChar, Conversion.ConversionToString(model.ValueDescription), ParameterDirection.Input);
		//    //}
		//    //if (model.DisplayOrder>0)
		//    //{
		//    //    SQL += " ,DisplayOrder=@DisplayOrder ";
		//    //    para.AddSQLParameter("@DisplayOrder", SqlDbType.Int, Conversion.ConversionToString(model.DisplayOrder), ParameterDirection.Input);
		//    //}
		//    //if (model.APIId != null && model.APIId != "0")
		//    //{
		//    //    SQL += ",APIId = @APIId,APIKey=@APIKey ";
		//    //    para.AddSQLParameter("@APIId", SqlDbType.VarChar, DataEncryptionDecryption.EncryptionString(model.APIId), ParameterDirection.Input);
		//    //    para.AddSQLParameter("@APIKey", SqlDbType.VarChar, DataEncryptionDecryption.EncryptionString(model.APIKey), ParameterDirection.Input);
		//    //}
		//    //SQL += " where ValueId=@ValueId ";
		//    //para.AddSQLParameter("@ValueId", SqlDbType.VarChar, Conversion.ConversionToInt(model.ValueId), ParameterDirection.Input);
		//    //para.AddSQLParameter("@ModifiedBy", SqlDbType.VarChar, GlobalContext.UserName, ParameterDirection.Input);
		//    //para.AddSQLParameter("@ModifiedOn", SqlDbType.DateTime, DateTime.Now, ParameterDirection.Input);
		//    //rValue = sp.UpdateStoredProcedure(SQL, CommandType.Text, para, GlobalContext.DB_CONN);
		//    return 0;// Conversion.ConversionToInt(rValue);
		//}


		//public int DeleteLookup(int id,bool isActive)
		//{
		//    //object rValue = 0;
		//    //try
		//    //{
		//    //    string SQL = string.Empty;
		//    //    IStoredProcedure sp = new StoredProcedure();
		//    //    SQLParameterList para = new SQLParameterList();
		//    //    SQL = "UPDATE [dbo].[LookupValue] SET Active =@Active where ValueId=@ValueId  ";
		//    //    if (isActive)
		//    //    {
		//    //        para.AddSQLParameter("@Active", SqlDbType.Bit, false, ParameterDirection.Input);
		//    //    }
		//    //    else
		//    //    {
		//    //        para.AddSQLParameter("@Active", SqlDbType.Bit, true, ParameterDirection.Input);
		//    //    }
		//    //    para.AddSQLParameter("@ValueId", SqlDbType.Int, Conversion.ConversionToInt(id), ParameterDirection.Input);
		//    //    rValue = sp.UpdateStoredProcedure(SQL, CommandType.Text, para, GlobalContext.DB_CONN);
		//    //    return Conversion.ConversionToInt(rValue);
		//    //}
		//    //catch
		//    //{
		//    //    rValue = 0;
		//    //}
		//    return 0;// Conversion.ConversionToInt(rValue);
		//}




	}

	public class LookUpValueModel
	{
		public string Value { get; set; }
		public int ValueId { get; set; }
		public bool Active { get; set; }
		public string APIId { get; set; }
		public string APIKey { get; set; }

	}

}

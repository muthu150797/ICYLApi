using ICYL.API.Entity;
using System.Data;
using System.Data.SqlClient;

namespace ICYL.API.Data
{
	public class Configurations
	{
		private readonly string _connectionstring;
		private readonly SqlConnection con;
		public Configurations()
		{
			var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false).Build();
			_connectionstring = config.GetSection("DbConnection").Value; 
			con = new SqlConnection(_connectionstring);
		}
		//saving Quotes templates
		public dynamic SaveQuotes(QuotesList quotes)
		{
			QuotesReponseModel response = new QuotesReponseModel();
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionstring))
				{
					connection.Open();
					SqlCommand cmd;
					if (quotes.QuotesId == 0) 
					{
						 cmd = new SqlCommand("insert into Quotes(QuotesTitle) values('" + quotes.QuotesTitle + "')", connection);
					}
					else
					{
						 cmd = new SqlCommand("update Quotes set QuotesTitle='"+quotes.QuotesTitle+"' where QuotesId="+ quotes.QuotesId, connection);
					}
					cmd.CommandType = CommandType.Text;
					var result=cmd.ExecuteNonQuery();
					if(result >0)
					{
						response.StatusCode= 200;
						response.Message = "Quote Saved Successfully";
					}
					else
					{
						response.StatusCode = 400;
						response.Message = "Quote failed to save";
					}
				}
			}
			catch (Exception ex)
			{
				response.StatusCode = 400;
				response.Message = "Quote failed to save";
			}
			return response;
		}
		//Deleting Quotes templates
		public dynamic DeleteQuotes(QuotesList quotes)
		{
			QuotesReponseModel response = new QuotesReponseModel();
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionstring))
				{
					connection.Open();
					SqlCommand cmd = new SqlCommand("DELETE FROM Quotes WHERE QuotesId="+quotes.QuotesId, connection);
					cmd.CommandType = CommandType.Text;
					var result = cmd.ExecuteNonQuery();
					if (result > 0)
					{
						response.StatusCode = 200;
						response.Message = "Quote deleted Successfully";
					}
					else
					{
						response.StatusCode = 400;
						response.Message = "Quote failed to delete";
					}
				}
			}
			catch (Exception ex)
			{
				response.StatusCode = 400;
				response.Message = "Quote failed to delete";
			}
			return response;
		}
		//Saving donation category
		public dynamic SaveDonationType(DonationModel model)
		{
			QuotesReponseModel response = new QuotesReponseModel();
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionstring))
				{
					connection.Open();
					SqlCommand cmd;
					if (model.Id == 0)
					{
						int active = 1;
						var query = "insert into Donation(GroupId,Active,Value,CreatedOn,Description) values(1,"+ active+",'" + model.DonationName+"',getdate(),'"+model.Description+"')";
						cmd = new SqlCommand(query, connection);
					}
					else
					{
						 cmd = new SqlCommand("update Donation set Description='" + model.Description + "',ModifiedOn=getdate() where ValueId=" + model.Id, connection);
					}
					cmd.CommandType = CommandType.Text;
					var result = cmd.ExecuteNonQuery();
					if (result > 0)
					{
						response.StatusCode = 200;
						response.Message = "Donation Gategory saved Successfully";
					}
					else
					{
						response.StatusCode = 400;
						response.Message = "Donation Gategory failed to save";
					}
				}
			}
			catch (Exception ex)
			{
				response.StatusCode = 400;
				response.Message = "Donation Gategory failed to save";
			}
			return response;
		}
		//Deleting donation category
		public dynamic DeleteDonationType(DonationModel model )
		{
			QuotesReponseModel response = new QuotesReponseModel();
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionstring))
				{
					connection.Open();
					SqlCommand cmd = new SqlCommand("DELETE FROM Donation WHERE ValueId=" + model.Id, connection);
					cmd.CommandType = CommandType.Text;
					var result = cmd.ExecuteNonQuery();
					if (result > 0)
					{
						response.StatusCode = 200;
						response.Message = "Donation type deleted Successfully";
					}
					else
					{
						response.StatusCode = 400;
						response.Message = "Donation type  failed to delete";
					}
				}
			}
			catch (Exception ex)
			{
				response.StatusCode = 400;
				response.Message = "Donation type failed to delete";
			}
			return response;
		}
		//Saving the Default Quick donation amount
		public dynamic SaveQuickDonation(DonationAmount model)
		{
			QuotesReponseModel response = new QuotesReponseModel();
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionstring))
				{
					connection.Open();
					SqlCommand cmd;
					if (model.Id == 0)
					{
						var query = "insert into DonationAmount(DonationId,Amount) values(2," + model.Amount + ")";
						cmd = new SqlCommand(query, connection);
					}
					else
					{
						cmd = new SqlCommand("update DonationAmount set Amount=" + model.Amount + " where Id=" + model.Id, connection);
					}
					cmd.CommandType = CommandType.Text;
					var result = cmd.ExecuteNonQuery();
					if (result > 0)
					{
						response.StatusCode = 200;
						response.Message = "Quick Donation amount saved Successfully";
					}
					else
					{
						response.StatusCode = 400;
						response.Message = "Quick Donation amount failed to save";
					}
				}
			}
			catch (Exception ex)
			{
				response.StatusCode = 400;
				response.Message = "Quick Donation amount failed to save";
			}
			return response;
		}
		//deleting the Default Quick donation amount 
		public dynamic DeleteQuickDonation(DonationAmount model)
		{
			QuotesReponseModel response = new QuotesReponseModel();
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionstring))
				{
					connection.Open();
					SqlCommand cmd = new SqlCommand("DELETE FROM DonationAmount WHERE Id=" + model.Id, connection);
					cmd.CommandType = CommandType.Text;
					var result = cmd.ExecuteNonQuery();
					if (result > 0)
					{
						response.StatusCode = 200;
						response.Message = "Quick donation Amount deleted Successfully";
					}
					else
					{
						response.StatusCode = 400;
						response.Message = "Quick donation amount  failed to delete";
					}
				}
			}
			catch (Exception ex)
			{
				response.StatusCode = 400;
				response.Message = "Quick donation amount type failed to delete";
			}
			return response;
		}
		public dynamic AddSupportReq(SupportReqModel model)
		{       
			UserInfo userInfo = new UserInfo();
			QuotesReponseModel response = new QuotesReponseModel();
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionstring))
				{
					connection.Open();
					SqlCommand cmd = new SqlCommand("sp_AddSupportReq", connection);
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("@UserId", model.UserId);
					cmd.Parameters.AddWithValue("@EmailId", model.EmailId);
					cmd.Parameters.AddWithValue("@Ticket", model.Ticket);
					cmd.Parameters.AddWithValue("@Description", model.Description);
					// var response = cmd.ExecuteNonQuery();
					var result = cmd.ExecuteNonQuery();
					if (Convert.ToInt32(result) >= 1)
					{
						userInfo.StatusCode = 200;
						userInfo.Status = true;
						userInfo.Message = "Support request added succesfully";
					}
					else
					{
						userInfo.StatusCode = 200;
						userInfo.Status = false;
						userInfo.Message = "Support request failed";
					}
				}
			}
			catch (Exception ex)
			{
				userInfo.StatusCode = 400;
				userInfo.Message = ex.Message;
				userInfo.Status = false;
			}
			return userInfo;
		}
		public dynamic GetSupportReq()
		{
			List<SupportReqModel> supportReqList = new List<SupportReqModel>();
			var response =0;
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionstring))
				{
					SqlDataAdapter donationAdapter = new SqlDataAdapter("sp_GetSupportReq", connection);
					donationAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
					//Using Data Table
					DataTable userDataTable = new DataTable();
					donationAdapter.Fill(userDataTable);
					if (userDataTable.Rows.Count > 0)
					{
						foreach (DataRow row in userDataTable.Rows)
						{
							SupportReqModel supportReq = new SupportReqModel();
							supportReq.UserId = Convert.ToInt32(row["UserId"]);
							supportReq.EmailId = (string)row["EmailId"];
							supportReq.UserName = (string)row["FirstName"];
							supportReq.Description = (string)row["Description"];
							supportReq.CreatedOn = row["CreatedOn"].ToString(); ;
							supportReq.Ticket = (string)row["Ticket"];
							supportReqList.Add(supportReq);
						}
					}
					else
					{
					}
				}
			}
			catch (Exception ex)
			{
				//donationList.Status = false;
			}
			return supportReqList;
		}
	}
}

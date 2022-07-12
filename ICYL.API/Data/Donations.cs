using ICYL.API.Entity;
using System;
using System.Data;
using System.Data.SqlClient;
using static ICYL.API.Entity.DonationAmount;

namespace ICYL.API.Data
{
	public class Donations
	{
		private readonly string _connectionstring;
		private readonly SqlConnection con;
		public Donations()
		{
			//
			var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false).Build();
			//var cons = config.GetSection("DbConnection").Value;
			_connectionstring = config.GetSection("DbConnection").Value;// "Server=tcp:icyldonation.database.windows.net,1433;Initial Catalog=ICYLMobiledonation;Persist Security Info=False;User ID=icyl;Password=!cyldb100$; MultipleActiveResultSets =False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";//config.GetConnectionString("DbConnection");
																		//con = new SqlConnection(cons);
		}
		public DonationModelList GetDonations(dynamic config)
		{
			List<DonationModel> donationList = new List<DonationModel>();
			DonationModelList donationModel = new DonationModelList();
			try
			{
				using (SqlConnection connection = new SqlConnection(config.GetSection("DbConnection").Value))
				{
					SqlDataAdapter donationAdapter = new SqlDataAdapter("sp_GetDonation", config.GetSection("DbConnection").Value);
					donationAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
					//Using Data Table
					DataTable userDataTable = new DataTable();
					donationAdapter.Fill(userDataTable);
					if (userDataTable.Rows.Count > 0)
					{
						foreach (DataRow row in userDataTable.Rows)
						{
							DonationModel donation = new DonationModel();
							donation.Id = Convert.ToInt32(row["ValueId"]);
							donation.DonationName = (string)row["Value"];
							// donation.AccountNumber = (string)row["AccountNumber"];
							donation.Description = (string)row["Description"];
							donationList.Add(donation);
						}
						donationModel.Status = true;
						donationModel.DonationList = donationList;
					}
					else
					{
						donationModel.Status = false;
						donationModel.DonationList = donationList;
						//donation.Message = "No data found";
						//donation.Status = false;
					}
				}
			}
			catch (Exception ex)
			{
				donationModel.Status = false;
				donationModel.Message = ex.Message;
				//donationList.Status = false;
			}
			return donationModel;
		}
		public DonationModelList GetAllCategory(dynamic config)
		{
			List<DonationModel> categoryList = new List<DonationModel>();
			DonationModelList categoryModel = new DonationModelList();
			try
			{
				using (SqlConnection connection = new SqlConnection(config.GetSection("DbConnection").Value))
				{
					SqlDataAdapter donationAdapter = new SqlDataAdapter("select * from [Donation] where GroupId=1", config.GetSection("DbConnection").Value);
					donationAdapter.SelectCommand.CommandType = CommandType.Text;
					//Using Data Table
					DataTable userDataTable = new DataTable();
					donationAdapter.Fill(userDataTable);
					if (userDataTable.Rows.Count > 0)
					{
						foreach (DataRow row in userDataTable.Rows)
						{
							DonationModel category = new DonationModel();
							category.Id = Convert.ToInt32(row["ValueId"]);
							category.DonationName = (string)row["Value"];
							category.Active = (bool)row["Active"];
							category.Description = (string)row["Description"];
							category.LoginId = row["APIId"] == DBNull.Value ? "":(string)row["APIId"];
							category.TransactionKey =row["APIKey"]==DBNull.Value ? "" : (string)row["APIKey"];
							categoryList.Add(category);
						}
						categoryModel.Status = true;
						categoryModel.DonationList = categoryList;
					}
					else
					{
						categoryModel.Status = false;
						categoryModel.DonationList = categoryList;
						//donation.Message = "No data found";
						//donation.Status = false;
					}
				}
			}
			catch (Exception ex)
			{
				categoryModel.Status = false;
				categoryModel.Message = ex.Message;
				//donationList.Status = false;
			}
			return categoryModel;
		}
		public AmountModelList GetDonationAmount()
		{
			List<DonationAmount> amountList = new List<DonationAmount>();
			AmountModelList donationAmount = new AmountModelList();
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionstring))
				{
					SqlDataAdapter donationAdapter = new SqlDataAdapter("select * from DonationAmount", connection);
					donationAdapter.SelectCommand.CommandType = CommandType.Text;
					//Using Data Table
					DataTable userDataTable = new DataTable();
					donationAdapter.Fill(userDataTable);
					if (userDataTable.Rows.Count > 0)
					{
						foreach (DataRow row in userDataTable.Rows)
						{
							DonationAmount donationList = new DonationAmount();
							donationList.Id = Convert.ToInt32(row["Id"]);
							donationList.Amount = Convert.ToInt32(row["Amount"]);
							donationList.DonationCategoryId = Convert.ToInt32(row["DonationId"]);
							amountList.Add(donationList);
						}
						donationAmount.Status = true;
						donationAmount.Message = "Success";
						donationAmount.amountList = amountList;
					}
					else
					{
						donationAmount.Status = false;
						donationAmount.Message = "No data found";
						donationAmount.amountList = amountList;
					}

				}
			}
			catch (Exception ex)
			{
				donationAmount.Status = false;
				donationAmount.Message = ex.Message;
				donationAmount.amountList = amountList;
			}
			return donationAmount;
		}

		public DonationHistory GetDonationHistory(Login detail)
		{
			List<HistoryList> historyList = new List<HistoryList>();
			DonationHistory donationList = new DonationHistory();
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionstring))
				{
					SqlDataAdapter donationAdapter = new SqlDataAdapter("sp_GetDonationHistory", connection);
					donationAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
					donationAdapter.SelectCommand.Parameters.AddWithValue("@Email", detail.UserName);
					//Using Data Table
					DataTable donationTable = new DataTable();
					donationAdapter.Fill(donationTable);
					if (donationTable.Rows.Count > 0)
					{
						foreach (DataRow row in donationTable.Rows)
						{
							HistoryList donation = new HistoryList();
							donation.EmailId = row["EmailId"].ToString();
							donation.CreatedOn = row["CreatedOn"].ToString();
							donation.AmtDonation = row["AmtDonation"].ToString();
							donation.DonationCategory = row["value"].ToString();
							donation.Description = row["Description"].ToString();
							historyList.Add(donation);
						}
						donationList.Status = true;
						donationList.HistoryList = historyList;
						donationList.Message = "Success";
					}
					else
					{
						donationList.Status = false;
						donationList.HistoryList = historyList;
						donationList.Message = "No record found";
					}

				}
			}
			catch (Exception ex)
			{
				donationList.Status = false;
				donationList.Message = ex.Message;
			}
			return donationList;

		}
		public string GetDonationCategory(int categoryId)
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionstring))
				{
					SqlDataAdapter donationAdapter = new SqlDataAdapter("select ValueId,Value from LookupValue where ValueId=" + categoryId, connection);
					donationAdapter.SelectCommand.CommandType = CommandType.Text;
					//Using Data Table
					DataTable donationTable = new DataTable();
					donationAdapter.Fill(donationTable);
					if (donationTable.Rows.Count > 0)
					{
						foreach (DataRow row in donationTable.Rows)
						{
							return row["Value"].ToString();
						}
					}
					else
					{
						return "";
					}
				}
			}
			catch (Exception ex)
			{
				return "";

			}
			return "";
		}
		public QuotesReponseModel GetDonationCount(Login detail)
		{
			QuotesReponseModel response = new QuotesReponseModel();
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionstring))
				{
					connection.Open();
					SqlCommand cmd = new SqlCommand("select count(*) from PaymentConfig where EmailId='" + detail.UserName + "'", connection);
					cmd.CommandType = CommandType.Text;
					//cmd.Parameters.Add(new SqlParameter("@Email",detail.UserName));
					var result = cmd.ExecuteScalar();
					if ((int)result > 0)
					{
						response.StatusCode = 200;
						response.donationCount = (int)result;
						response.Message = "Success";
					}
					else
					{
						response.StatusCode = 400;
						response.Message = "Failed to count donation";
					}
				}
			}
			catch (Exception ex)
			{
				response.StatusCode = 400;
				response.Message = ex.Message;
			}
			return response;
		}
		public QuotesModel GetQuotes()
		{

			QuotesModel model = new QuotesModel();
			List<QuotesList> quotesModel = new List<QuotesList>();
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionstring))
				{
					SqlDataAdapter donationAdapter = new SqlDataAdapter("select * from Quotes", connection);
					donationAdapter.SelectCommand.CommandType = CommandType.Text;
					//Using Data Table
					DataTable userDataTable = new DataTable();
					donationAdapter.Fill(userDataTable);
					if (userDataTable.Rows.Count > 0)
					{
						foreach (DataRow row in userDataTable.Rows)
						{
							QuotesList quotes = new QuotesList();
							quotes.QuotesId = Convert.ToInt32(row["QuotesId"]);
							quotes.QuotesTitle = row["QuotesTitle"].ToString();
							quotesModel.Add(quotes);
						}
						model.Status = true;
						model.QuotesLists = quotesModel;
						model.Message = "Success";
					}
					else
					{
						model.Status = false;
						model.QuotesLists = quotesModel;
						model.Message = "Failed";
					}

				}
			}
			catch (Exception ex)
			{
				model.Status = false;
				model.QuotesLists = quotesModel;

				model.Message = ex.ToString();
			}
			return model;
		}

	}
}

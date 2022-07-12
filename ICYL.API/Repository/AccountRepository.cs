using ICYL.API.Entity;
using ICYL.API.Repository;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using MimeKit;
using System.IO;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using ICYL.API.Data;

namespace ICYL.API.Repository

{
	public class AccountRepository
	{
		private readonly string _connectionstring;
		private readonly SqlConnection con;
		private readonly EmailRepository _emailRepository;
		private readonly string _sendTestEmailsTo;
		private readonly string _supportEmailFrom;
		private readonly string _smtpHost;
		private readonly string _smtpUserName;
		private readonly string _smtpPassword;
		private readonly string _smtpPort;
		private readonly string _ErrorEmailTo;
		string _OTP = "gotps";
		public AccountRepository(EmailRepository emailRepository)
		{
			var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false).Build();
			_connectionstring = config.GetSection("DbConnection").Value;
			con = new SqlConnection(_connectionstring);
			_emailRepository = emailRepository;
			_smtpHost = config.GetSection("EmailConfig:SmtpHost").Value;
			_supportEmailFrom = config.GetSection("EmailConfig:FromEmailId").Value;
			_sendTestEmailsTo = config.GetSection("EmailConfig:SendTestEmailsTo").Value;
			_smtpUserName = config.GetSection("EmailConfig:SmtpUserName").Value;
			_smtpPassword = config.GetSection("EmailConfig:SmtpPassword").Value;
			_smtpPort = config.GetSection("EmailConfig:SMTPPort").Value;
			_ErrorEmailTo = config.GetSection("EmailConfig:ErrorEmailTo").Value;
		}

		public UserInfo Register(UserDetails userDetails)
		{
			UserInfo userInfo = new UserInfo();
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionstring))
				{
					connection.Open();
					SqlCommand cmd = new SqlCommand("sp_insertUserDetail", connection);
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("@FirstName", userDetails.FirstName);
					cmd.Parameters.AddWithValue("@LastName", userDetails.LastName);
					cmd.Parameters.AddWithValue("@Email", userDetails.Email);
					cmd.Parameters.AddWithValue("@PhoneNumber", userDetails.PhoneNumber);
					cmd.Parameters.AddWithValue("@Address", userDetails.Address);
					cmd.Parameters.AddWithValue("@City", userDetails.City);
					cmd.Parameters.AddWithValue("@Password", userDetails.Password);
					cmd.Parameters.AddWithValue("@CompanyName", userDetails.CompanyName);
					cmd.Parameters.AddWithValue("@State", userDetails.State);
					cmd.Parameters.AddWithValue("@Zip", userDetails.Zip);
					cmd.Parameters.AddWithValue("@RetVal", 0);
					// var response = cmd.ExecuteNonQuery();
					var userId = cmd.ExecuteScalar();
					SqlParameter returnParameter = cmd.Parameters.Add("RetVal", SqlDbType.Int);
					returnParameter.Direction = ParameterDirection.ReturnValue;
					var userExist = 0;// (int)returnParameter.Value;
									  //if(userExist > 0)
									  //{
									  //	userInfo.StatusCode = 400;
									  //	userInfo.Status = false;
									  //	userInfo.Message = "User  already exists";
									  //	return userInfo;
									  //}
					if (Convert.ToInt32(userId) > 0)
					{
						userInfo.UserId = Convert.ToInt32(userId);
						userInfo.StatusCode = 200;
						userInfo.Status = true;
						userInfo.Message = "Registered succesfully";
					}
					else
					{
						//userInfo.UserId = int.Parse((string)userId);
						userInfo.StatusCode = 400;
						userInfo.Status = false;
						userInfo.Message = "UserName already exists";
					}
				}
			}
			catch (Exception ex)
			{
				userInfo.StatusCode = 400;
				userInfo.Status = false;
				userInfo.Message = ex.Message.ToString();
			}
			return userInfo;
		}
		public UserInfo Login(Login userDetails)
		{
			UserInfo userInfo = new UserInfo();
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionstring))
				{
					SqlDataAdapter userDataAdapter = new SqlDataAdapter("sp_Login", connection);
					userDataAdapter.SelectCommand.Parameters.AddWithValue("@Email", userDetails.UserName);
					userDataAdapter.SelectCommand.Parameters.AddWithValue("@Password", userDetails.Password);
					userDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
					//Using Data Table
					DataTable userDataTable = new DataTable();
					userDataAdapter.Fill(userDataTable);
					if (userDataTable.Rows.Count == 1)
					{
						foreach (DataRow row in userDataTable.Rows)
						{
							userInfo.UserId = Convert.ToInt32(row["Id"]);
							userInfo.Email = (string)row["Email"];
							userInfo.FirstName = (string)row["FirstName"];
							userInfo.LastName = (string)row["LastName"];
							userInfo.CompanyName = (string)row["CompanyName"];
							userInfo.PhoneNumber = (string)row["PhoneNumber"];
							userInfo.Address = (string)row["Address"];
							userInfo.City = (string)row["City"];
							userInfo.State = (string)row["State"];
							userInfo.Zip = (string)row["PostalCode"];
							userInfo.Role = (string?)(row["Role"] == DBNull.Value ? "user" : row["Role"]);
							userInfo.Message = "User verified successfully";
							userInfo.StatusCode = 200;
							userInfo.Status = true;
							// Console.WriteLine(row["Name"] + ",  " + row["Email"] + ",  " + row["Mobile"]);
						}
					}
					else
					{
						userInfo.Message = "Login failed";
						userInfo.StatusCode = 404;
						userInfo.Status = false;
					}
					//Using DataSets
					DataSet userDataset = new DataSet();
					userDataAdapter.Fill(userDataset, "userDetail");
				}
			}
			catch (Exception ex)
			{
				userInfo.Message = ex.Message.ToString();
				userInfo.StatusCode = 400;
				userInfo.Status = false;
			}
			return userInfo;
		}
		public dynamic GetUserDetail(UserModel model)
		{
			UserInfo userInfo = new UserInfo();
			Donations donation = new Donations();
			Login detail = new Login();
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionstring))
				{
					SqlDataAdapter userDataAdapter;
					userDataAdapter = new SqlDataAdapter("select * from UserProfile where Email='" + model.Email + "'", connection);
					userDataAdapter.SelectCommand.CommandType = CommandType.Text;
					//Using Data Table
					DataTable userDataTable = new DataTable();
					userDataAdapter.Fill(userDataTable);
					if (userDataTable.Rows.Count == 1)
					{
						foreach (DataRow row in userDataTable.Rows)
						{
							detail.UserName = (string)row["Email"];
							var donationCounts = donation.GetDonationCount(detail);
							userInfo.TotalDonation = donationCounts.donationCount;
							userInfo.UserId = Convert.ToInt32(row["Id"]);
							userInfo.Email = (string)row["Email"];
							userInfo.FirstName = (string)row["FirstName"];
							userInfo.LastName = (string)row["LastName"];
							userInfo.CompanyName = (string)row["CompanyName"];
							userInfo.PhoneNumber = (string)row["PhoneNumber"];
							userInfo.Address = (string)row["Address"];
							userInfo.City = (string)row["City"];
							userInfo.State = (string)row["State"];
							userInfo.Zip = (string)row["PostalCode"];
							userInfo.Role = (string?)(row["Role"] == DBNull.Value ? "user" : row["Role"]);
							userInfo.Message = "success";
							userInfo.StatusCode = 200;
							userInfo.Status = true;
							//Console.WriteLine(row["Name"] + ",  " + row["Email"] + ",  " + row["Mobile"]);
						}
					}
					else
					{
						userInfo.Message = "No user found";
						userInfo.StatusCode = 404;
						userInfo.Status = false;
					}
					//Using DataSets
					DataSet userDataset = new DataSet();
					userDataAdapter.Fill(userDataset, "userDetail");
				}
			}
			catch (Exception ex)
			{
				userInfo.Message = ex.Message.ToString();
				userInfo.StatusCode = 400;
				userInfo.Status = false;
			}
			return userInfo;
		}
		public dynamic GetAllUserDetails()
		{
			List<UserInfo> response = new List<UserInfo>();
			Donations donation = new Donations();
			Login detail = new Login();
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionstring))
				{
					SqlDataAdapter userDataAdapter;
					userDataAdapter = new SqlDataAdapter("sp_GetAllUser", connection);
					userDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
					//Using Data Table
					DataTable userDataTable = new DataTable();
					userDataAdapter.Fill(userDataTable);
					if (userDataTable.Rows.Count > 0)
					{
						foreach (DataRow row in userDataTable.Rows)
						{
							UserInfo userInfo = new UserInfo();
							detail.UserName = (string)row["Email"];
							var donationCounts = donation.GetDonationCount(detail);
							userInfo.TotalDonation = donationCounts.donationCount;
							userInfo.UserId = Convert.ToInt32(row["Id"]);
							userInfo.Email = (string)row["Email"];
							userInfo.FirstName = (string)row["FirstName"];
							//userInfo.LastName = (string)row["LastName"];
							userInfo.CompanyName = (string)row["CompanyName"];
							userInfo.PhoneNumber = (string)row["PhoneNumber"];
							userInfo.Address = (string)row["Address"];
							//userInfo.City = (string)row["City"];
							//userInfo.State = (string)row["State"];
							//userInfo.Zip = (string)row["PostalCode"];
							//userInfo.Role = (string?)(row["Role"] == DBNull.Value ? "user" : row["Role"]);
							response.Add(userInfo);
						}
					}
					return response;
					//Console.WriteLine(row["Name"] + ",  " + row["Email"] + ",  " + row["Mobile"]);
					//else
					//{
					//	userInfo.Message = "No user found";
					//	userInfo.StatusCode = 404;
					//	userInfo.Status = false;
					//}
				}
			}
			catch (Exception ex)
			{
				//userInfo.Message = ex.Message.ToString();
				//userInfo.StatusCode = 400;
				//userInfo.Status = false;
			}
			return response;
		}
		public UserInfo UpdateUser(UserInfo userDetails)
		{
			UserInfo userInfo = new UserInfo();
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionstring))
				{
					connection.Open();
					SqlCommand cmd = new SqlCommand("sp_UpdateUser", connection);
					cmd.Parameters.AddWithValue("@UserId", userDetails.UserId);
					cmd.Parameters.AddWithValue("@FirstName", userDetails.FirstName);
					cmd.Parameters.AddWithValue("@EmailId", userDetails.Email);
					cmd.Parameters.AddWithValue("@PhoneNumber", userDetails.PhoneNumber);
					cmd.Parameters.AddWithValue("@Company", userDetails.CompanyName);
					cmd.CommandType = CommandType.StoredProcedure;
					var result = cmd.ExecuteNonQuery();
					if (result >= 1)
					{
						userInfo.Message = "The user detail saved successfully";
						userInfo.StatusCode = 200;
						userInfo.Status = true;
					}
					else
					{
						userInfo.Message = "The user detail can't be saved ,try again";
						userInfo.StatusCode = 400;
						userInfo.Status = false;
					}
				}
			}
			catch (Exception ex)
			{
				userInfo.Message = ex.Message.ToString();
				userInfo.StatusCode = 400;
				userInfo.Status = false;
			}
			return userInfo;
		}
		public UserInfo ChangePassword(Login userDetails)
		{
			UserInfo userInfo = new UserInfo();
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionstring))
				{
					connection.Open();
					SqlCommand cmd = new SqlCommand("sp_ChangePassword", connection);
					cmd.Parameters.AddWithValue("@UserId", userDetails.UserId);
					cmd.Parameters.AddWithValue("@Password", userDetails.Password);
					cmd.CommandType = CommandType.StoredProcedure;
					var result = cmd.ExecuteNonQuery();
					if (result >= 1)
					{
						userInfo.Message = "The password has been changed successfully";
						userInfo.StatusCode = 200;
						userInfo.Status = true;
					}
					else
					{
						userInfo.Message = "The password can not be changed,try again";
						userInfo.StatusCode = 400;
						userInfo.Status = false;
					}
				}
			}
			catch (Exception ex)
			{
				userInfo.Message = ex.Message.ToString();
				userInfo.StatusCode = 400;
				userInfo.Status = false;
			}
			return userInfo;
		}
		public dynamic ResetPassword(Login userDetails)
		{
			OTPResponse response = new OTPResponse();
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionstring))
				{
					SqlDataAdapter userDataAdapter = new SqlDataAdapter("select * from UserProfile where Email='" + userDetails.UserName + "'", connection);
					//userDataAdapter.SelectCommand.Parameters.AddWithValue("@Email", userDetails.UserName);
					userDataAdapter.SelectCommand.CommandType = CommandType.Text;
					DataTable userDataTable = new DataTable();
					userDataAdapter.Fill(userDataTable);
					if (userDataTable.Rows.Count == 1)
					{
						foreach (DataRow row in userDataTable.Rows)
						{
							int userId = (int)row["Id"];
							string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
							var isOTPSaved = GenerateRandomOTP(6, saAllowedCharacters, userId);
							if (isOTPSaved == 200)
							{
								//send email 
								var name = (string)row["FirstName"];
								var content = "<b>hi " + name + ",</b><br/> <p>your OTP is " + _OTP + " for Reset Your Password</p>";
								var res = SendEmailForResetPwd("ICYL Donation", userDetails.UserName, name, content);
								if (res)
								{
									response.Message = "The OTP verfication has been sent to your email,please verify";
									response.Status = true;
								}
								else
								{
									response.Message = "Enter the correct email id";
									response.Status = false;
								}
							}
							else
							{
								response.Message = "The OTP does not saved in Database";
								response.Status = false;// row["Email"].Tostring();
							}
							//userInfo.FirstName = row["FirstName"].Tostring();
							//Console.WriteLine(row["Name"] + ",  " + row["Email"] + ",  " + row["Mobile"]);
						}
					}
					else
					{
						response.Status = false;
						response.Message = "User not found";
					}
				}
			}
			catch (Exception ex)
			{
				response.Status = false;
				response.Message = ex.Message;
			}
			//var res = SendEmail("hello", "<p>dddd</p>", true);
			return response;
		}
		public dynamic VerifyOTP(OTPResponse otpDetails)
		{
			Login details = new Login();
			UserInfo userInfo = new UserInfo();
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionstring))
				{
					SqlDataAdapter userDataAdapter = new SqlDataAdapter("select * from UserProfile where OTP='" + otpDetails.OTP + "'", connection);
					//userDataAdapter.SelectCommand.Parameters.AddWithValue("@Email", userDetails.UserName);
					userDataAdapter.SelectCommand.CommandType = CommandType.Text;
					DataTable userDataTable = new DataTable();
					userDataAdapter.Fill(userDataTable);
					if (userDataTable.Rows.Count == 1)
					{
						foreach (DataRow row in userDataTable.Rows)
						{
							var oTPTimeStamp = Convert.ToDateTime(row["OTPTimeStamp"]);
							var expiryMin = DateTime.Now.Minute - oTPTimeStamp.Minute;
							if (expiryMin <= 4)//check otp expiry time
							{
								//user is entered otp successfully with time limit and go to change password
								//response.Status=true;
								//response.UserId = Convert.ToInt32(row["Id"]);
								//response.Message = "User OTP verified successfully";
								details.UserId = otpDetails.UserId;
								details.Password = otpDetails.NewPassword;
								userInfo = ChangePassword(details);
								if (userInfo.Status)
								{
									userInfo.Status = true;
									userInfo.Message = "The password Changed Successfully";
								}
								else
								{
									userInfo.Status = false;
									userInfo.Message = "The password can not change,try again";
								}
							}
							else
							{
								userInfo.Status = false;
								userInfo.Message = "User OTP expired";
							}
							return userInfo;
						}
					}
					else
					{
						userInfo.Status = false;
						userInfo.Message = "OTP not verified ,try again";
					}
					return userInfo;
				}
			}
			catch (Exception ex)
			{
				userInfo.Status = false;
				userInfo.Message = ex.ToString();
				return userInfo;
			}
		}
		public int GenerateRandomOTP(int iOTPLength, string[] saAllowedCharacters, int userId)
		{
			string sOTP = String.Empty;
			string sTempChars = String.Empty;
			Random rand = new Random();
			for (int i = 0; i < iOTPLength; i++)
			{
				int p = rand.Next(0, saAllowedCharacters.Length);
				sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
				sOTP += sTempChars;
			}
			try
			{
				_OTP = sOTP;
				using (SqlConnection connection = new SqlConnection(_connectionstring))
				{
					connection.Open();
					SqlCommand cmd = new SqlCommand("sp_InsertOTP", connection);
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("@UserId", userId);
					cmd.Parameters.AddWithValue("@OTP", sOTP);
					cmd.Parameters.AddWithValue("@OTPTimeStamp", DateTime.Now);
					var result = cmd.ExecuteNonQuery();
					if (result >= 1)
						return 200;
					else
						return 400;
				}
			}
			catch (Exception ex)
			{
				return 400;
			}
		}
		private string _userName = string.Empty;
		private string _connStr = string.Empty;
		public List<string> lstTo { get; set; }
		public List<string> lstCC { get; set; }

		MailMessage objEmail = new MailMessage();
		public dynamic SendEmail(string Subject, string ToMail, string name, string content)
		{
			UserInfo userInfo = new UserInfo();

			try
			{
				// create email message
				var email = new MimeMessage();
				email.From.Add(MailboxAddress.Parse(_supportEmailFrom));
				email.To.Add(MailboxAddress.Parse(ToMail));
				email.Subject = Subject;
				email.Body = new TextPart(TextFormat.Html) { Text =content };

				// send email
				using var smtp = new MailKit.Net.Smtp.SmtpClient();

				smtp.Connect(_smtpHost, int.Parse(_smtpPort), SecureSocketOptions.StartTlsWhenAvailable);
				smtp.Authenticate(_supportEmailFrom, _smtpPassword);
				var res=smtp.Send(email);
				smtp.Disconnect(true);
				Configurations configure =new  Configurations();
				userInfo = configure.UpdateSupportReq(ToMail);
				return userInfo;
			}
			catch (Exception ex)
			{
				userInfo.StatusCode = 400;
				userInfo.Message = ex.Message;
				userInfo.Status = false;
				//Log Exception Details
				return userInfo;
			}
			return userInfo;
		}
		public dynamic SendEmailForResetPwd(string Subject, string ToMail, string name, string content)
		{
			UserInfo userInfo = new UserInfo();

			try
			{
				// create email message
				var email = new MimeMessage();
				email.From.Add(MailboxAddress.Parse(_supportEmailFrom));
				email.To.Add(MailboxAddress.Parse(ToMail));
				email.Subject = Subject;
				email.Body = new TextPart(TextFormat.Html) { Text = content };

				// send email
				using var smtp = new MailKit.Net.Smtp.SmtpClient();

				smtp.Connect(_smtpHost, int.Parse(_smtpPort), SecureSocketOptions.StartTlsWhenAvailable);
				smtp.Authenticate(_supportEmailFrom, _smtpPassword);
				var res = smtp.Send(email);
				smtp.Disconnect(true);
				return true;
			}
			catch (Exception ex)
			{
				//Log Exception Details
				return false;
			}
			return true;
		}
		public bool SendEmail(System.Net.Mail.MailMessage email)
		{
			string toStr = string.Empty;
			bool isSuccess = false;
			string EmailId = string.Empty;
			EmailId = email.To.ToString();
			//if (GlobalContext.VersionEnv().Trim().ToUpper() == GlobalContext.Env.TEST.ToString())
			//{
			//    EmailId = SendTestEmailsTo;
			//    email.To.Add(EmailId);
			//    email.Subject += " - " + GlobalContext.VersionEnv().Trim().ToUpper();
			//}
			if (string.IsNullOrEmpty(EmailId))
			{
				return false;
			}
			try
			{
				email.From = new MailAddress(_supportEmailFrom.ToString());
				System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
				smtp.Host = _smtpHost;
				smtp.Port = Convert.ToInt32(_smtpPort);
				smtp.Credentials = new System.Net.NetworkCredential(_smtpUserName, _smtpPassword);
				//smtp.EnableSsl = false;
				//smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
				smtp.Send(email);
				isSuccess = true;
			}
			catch (Exception ex)
			{
				// ErrorLogRepository.ErrorLogInsert("EmailRepository", "SendEmail", ex.Message, ex.StackTrace.ToString(), EmailId);
				isSuccess = false;
				// throw;
			}
			return isSuccess;
		}
	}
}

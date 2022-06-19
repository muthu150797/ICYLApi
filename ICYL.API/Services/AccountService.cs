using ICYL.API.Data;
using ICYL.API.Entity;
using ICYL.API.Repository;

namespace ICYL.API.Services
{
	public class AccountService
	{
		public static EmailRepository _emailRepository;
		Account accountData = new Account(_emailRepository);
		public UserInfo Register(UserDetails userDetails)
		{
			var response = accountData.Register(userDetails);
			return response;
		}
		public UserInfo Login(Login userDetails)
		{
			var response = accountData.Login(userDetails);
			return response;
		}
		public dynamic ResetPassword(Login userDetails)//request OTP for change password
		{
			var response = accountData.ResetPassword(userDetails);
			return response;
		}
		public dynamic VerifyOTP(OTPResponse otpDetails)
		{
			var response = accountData.VerifyOTP(otpDetails);
			return response;
		}
		public UserInfo ChangePassword(Login userDetails)
		{
			UserInfo response = accountData.ChangePassword(userDetails);
			return response;
		}
		public dynamic GetUserDetail(UserModel model)
		{
			UserInfo response = accountData.GetUserDetail(model);
			return response;
		}
		public dynamic GetAllUserDetails()
		{
		  var response=accountData.GetAllUserDetails();
			return response;
		}
		public UserInfo UpdateUser(UserInfo userDetails)
		{
			UserInfo response = accountData.UpdateUser(userDetails);
			return response;
		}
	}
}

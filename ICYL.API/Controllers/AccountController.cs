using AuthorizeNet.Api.Contracts.V1;
using ICYL.API.Entity;
using ICYL.API.Helpers;
using ICYL.API.Repository;
using ICYL.API.Services;
using ICYL.Repository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ICYL.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("_myAllowSpecificOrigins")]
    public class AccountController : ControllerBase
    {
        
        string TransErrorText = string.Empty;
        //logger service to monitor the request and response
        private readonly ILogger _logger;
        public AccountController(ILogger<ILogger> logger)
        {
            _logger = logger;
        }
       
        AccountService acc = new AccountService();
        // Register user details
        [HttpPost]
        [Route("Register")]
        public UserInfo Register(UserDetails userDetails)
        {
            var response = acc.Register(userDetails);
            return response;
        }
        //Login user
        [HttpPost]
        [Route("Login")]
        public UserInfo Login(Login userDetails)
        {
            var response = acc.Login(userDetails);
            return response;
        }
        [HttpPost]
        [Route("ResetPassword")]
        public dynamic ResetPassword(Login userDetails)
        {
            var response = acc.ResetPassword(userDetails);
            return response;
        }
        [HttpPost]
        [Route("VerifyOTP")]
        public dynamic VerifyOTP(OTPResponse otpDetails)
        {
            var response = acc.VerifyOTP(otpDetails);
            return response;
        }
        [HttpPost]
        [Route("GetUserDetail")]
        public dynamic GetUserDetail(UserModel model)
        {
            UserInfo response = acc.GetUserDetail(model);
            return response;
        }
        [HttpPost]
        [Route("GetAllUserDetails")]
        public dynamic GetAllUserDetails()
        {
            var response = acc.GetAllUserDetails();
            return response;
        }
        [HttpPost]
        [Route("UpdateUser")]
        public dynamic UpdateUser(UserInfo userDetails)
        {
            UserInfo response = acc.UpdateUser(userDetails);
            return response;
        }
        //[HttpPost]
        //[Route("ChangePassword")]
        //public UserInfo ChangePassword(Login userDetails)
        //{
        //    UserInfo response = acc.ChangePassword(userDetails);
        //    return response;
        //}


    }
}

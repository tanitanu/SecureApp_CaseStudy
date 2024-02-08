using Azure;
using MeetingScheduler.Common;
using MeetingScheduler.DAL;
using MeetingScheduler.Entities;
using MeetingScheduler.Model;
using Newtonsoft.Json;
using Serilog;
using System.Net;

namespace MeetingScheduler.Data
{
    public class LoginService:ILoginService
    {      
        private readonly HttpClient _httpClient; // http client object to call web api methods
        private readonly ILogger<LoginService> _logger; // Logger object

        /// <summary>
        /// Login Service constructor
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="logger"></param>
        public LoginService(HttpClient httpClient, ILogger<LoginService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        /// <summary>
        /// Get Login Details by user id
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        public UserManagerResponse? GetLoginDetails(UserLogin userLogin)
        {
            UserManagerResponse? userManagerResponse = new UserManagerResponse();
            try
            {
                _logger.LogInformation(Utility.GetStartMessage("GetLoginDetails"));               
                string inputJson = JsonConvert.SerializeObject(userLogin);
                HttpContent httpContent = new StringContent(inputJson,System.Text.Encoding.UTF8,"application/json");
                 var response =  _httpClient.PostAsync("api/user/Login", httpContent).Result;

                if(response!=null && response.StatusCode==HttpStatusCode.OK)
                {
                    userManagerResponse=JsonConvert.DeserializeObject<UserManagerResponse?>
                                                (response.Content.ReadAsStringAsync().Result);
                }
                _logger.LogInformation(Utility.GetEndMessage("GetLoginDetails"));
            }
            catch (Exception ex)
            {
                _logger.LogError(Utility.GetExceptionMessage("GetLoginDetails") + " \n" + ex.ToString()); ;               
            }

            return  userManagerResponse;
        }

        /// <summary
        /// Forgot Password
        /// </summary>
        /// <param name="forgetPassword"></param>
        /// <returns></returns>
        public UserManagerResponse? ForgotPassword(ForgetPassword forgetPassword)
        {
            UserManagerResponse? userManagerResponse = new UserManagerResponse();
            try
            {
                _logger.LogInformation(Utility.GetStartMessage("ForgotPassword"));
                var response = _httpClient.PostAsJsonAsync("/api/User/ForgotPassword", forgetPassword).Result;
                if (response != null && response.StatusCode == HttpStatusCode.OK)
                {
                    userManagerResponse = JsonConvert.DeserializeObject<UserManagerResponse?>
                                                (response.Content.ReadAsStringAsync().Result);
                }

                _logger.LogInformation(Utility.GetEndMessage("ForgotPassword"));
            }
            catch (Exception ex)
            {
                _logger.LogError(Utility.GetExceptionMessage("ForgotPassword") + " \n" + ex.ToString());
            }

            return userManagerResponse;

        }

        /// <summary>
        /// Reset Password
        /// </summary>
        /// <param name="resetPasswordRequest"></param>
        /// <returns></returns>
        public UserManagerResponse? ResetPassword(ResetPasswordRequest resetPasswordRequest)
        {
            UserManagerResponse? userManagerResponse = new UserManagerResponse();
            try
            {
                _logger.LogInformation(Utility.GetStartMessage("ResetPassword"));
                var response = _httpClient.PostAsJsonAsync("/api/User/ResetPassword", resetPasswordRequest).Result;
                if (response != null && response.StatusCode == HttpStatusCode.OK)
                {
                    userManagerResponse = JsonConvert.DeserializeObject<UserManagerResponse?>
                                                (response.Content.ReadAsStringAsync().Result);
                }
                _logger.LogInformation(Utility.GetEndMessage("ResetPassword"));
            }
            catch (Exception ex)
            {
                _logger.LogError(Utility.GetExceptionMessage("ResetPassword") + " \n" + ex.ToString());
            }

            return userManagerResponse;

        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        public List<UserVo>? GetUsers()
        {
            List<UserVo>? userVo = new List<UserVo>();
            try
            {
                _logger.LogInformation(Utility.GetStartMessage("GetUsers"));
                var response = _httpClient.GetAsync("/api/User/GetUsers").Result;
                if (response != null && response.StatusCode == HttpStatusCode.OK)
                {
                    userVo = JsonConvert.DeserializeObject<List<UserVo>?>(response.Content.ReadAsStringAsync().Result);
                }
                _logger.LogInformation(Utility.GetEndMessage("GetUsers"));
            }
            catch (Exception ex)
            {
                _logger.LogError(Utility.GetExceptionMessage("GetUsers") + " \n" + ex.ToString());
            }

            return userVo;
        }
    }
}

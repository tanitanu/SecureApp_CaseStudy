using MeetingScheduler.Common;
using MeetingScheduler.Entities;
using Newtonsoft.Json;
using System.Net;

namespace MeetingScheduler.Data
{
    public class UserRegisterService : IUserRegisterService
    {
        private readonly HttpClient _httpClient;// http client object to call web api methods
        private readonly ILogger<LoginService> _logger;// Logger object

        /// <summary>
        /// User Register Service constructor
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="logger"></param>
        public UserRegisterService(HttpClient httpClient, ILogger<LoginService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        /// <summary>
        /// Register user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public UserManagerResponse? UserRegisterDetails(UserRegister user)
        {
            UserManagerResponse? userManagerResponse = new UserManagerResponse();
            _logger.LogInformation(Utility.GetStartMessage("UserRegisterDetails"));
            try
            {
                var response = _httpClient.PostAsJsonAsync("/api/User/Register", user).Result;
                if (response != null && response.StatusCode == HttpStatusCode.OK)
                {
                    userManagerResponse = JsonConvert.DeserializeObject<UserManagerResponse?>
                                                (response.Content.ReadAsStringAsync().Result);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(Utility.GetExceptionMessage("UserRegisterDetails") + " \n" + ex.ToString());
            }
            _logger.LogInformation(Utility.GetEndMessage("UserRegisterDetails"));
            return userManagerResponse;
        }
    }
}

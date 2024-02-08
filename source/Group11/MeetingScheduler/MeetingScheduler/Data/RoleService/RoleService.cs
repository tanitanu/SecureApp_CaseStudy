using MeetingScheduler.Common;
using MeetingScheduler.Entities;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;

namespace MeetingScheduler.Data
{
    public class RoleService:IRoleService
    {

        private readonly HttpClient _httpClient; // http client object to call web api methods
        private readonly ILogger<RoleService> _logger;// Logger object
       
        /// <summary>
        /// Role Service constructor
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="logger"></param>

        public RoleService(HttpClient httpClient, ILogger<RoleService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        /// <summary>
        /// Get user role 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Role? GetRole(UserLogin user)
        {
            Role? _role = new Role();
            _logger.LogInformation(Utility.GetStartMessage("GetRole"));
            try
            {
                var response = _httpClient.PostAsJsonAsync("/api/Role/GetRole", user).Result;
                if (response != null && response.StatusCode == HttpStatusCode.OK)
                {
                    _role = JsonConvert.DeserializeObject<Role?>
                                                (response.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    _logger.LogWarning(Utility.GetUnexpectedExceptionMessage("GetRole"));
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(Utility.GetExceptionMessage("GetRole") + " \n" + ex.ToString());
            }
            _logger.LogInformation(Utility.GetEndMessage("GetRole"));
            return _role;
        }
        /// <summary>
        /// Get Roles
        /// </summary>
        /// <returns></returns>
        public List<RoleVo>? GetRoles()
        {
            List<RoleVo>? _roleVo = new List<RoleVo>();
            try
            {
                _logger.LogInformation(Utility.GetStartMessage("GetRoles"));
                var response = _httpClient.GetAsync("/api/Role/GetRoles").Result;
                if (response != null && response.StatusCode == HttpStatusCode.OK)
                {
                    _roleVo = JsonConvert.DeserializeObject<List<RoleVo>?>(response.Content.ReadAsStringAsync().Result);
                }
                _logger.LogInformation(Utility.GetEndMessage("GetRoles"));
            }
            catch (Exception ex)
            {
                _logger.LogError(Utility.GetExceptionMessage("GetRoles") + " \n" + ex.ToString());
            }

            return _roleVo;
        }

        /// <summary>
        /// Update Role
        /// </summary>
        /// <param name="id"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public UserManagerResponse? UpdateRole(int id, Role role)
        {
            UserManagerResponse? userManagerResponse = new UserManagerResponse();
            try
            {
                _logger.LogInformation("Started UpdateRole service method");
                var response = _httpClient.PutAsJsonAsync($"/api/User/{id}", role).Result;
                if (response != null && response.StatusCode == HttpStatusCode.OK)
                {
                    userManagerResponse = JsonConvert.DeserializeObject<UserManagerResponse?>
                                                (response.Content.ReadAsStringAsync().Result);
                }
                _logger.LogInformation("End GetRoles service method");
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in GetRoles service \n" + ex.ToString());
            }

            return userManagerResponse;
        }
    }
}

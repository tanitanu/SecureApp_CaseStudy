using DXC.BlogConnect.DTO;
using DXC.BlogConnect.WebApp.ServiceExtension;
using DXC.BlogConnect.WebApp.Services.Interfaces;
using Newtonsoft.Json;
using System.Configuration;
using System.Text;
/*
 * Created By: Kishore
 */
namespace DXC.BlogConnect.WebApp.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        public UserService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
        }
        public async Task<APIResponse<UserGetDTO>> GetAllUserAsync()
        {
            var resStr = await _httpClient.GetStringAsync("users");
            var apiResult = JsonConvert.DeserializeObject<APIResponse<UserGetDTO>>(resStr);
            if (apiResult != null)
            {
                return apiResult;
            }
            else
            {
                var response = new APIResponse<UserGetDTO> { IsSuccess = false };
                return response;
            }

        }
        public async Task<APIResponse<UserDTO>> AddUserAsync(UserDTO userDTO)
        {
            var json = JsonConvert.SerializeObject(userDTO);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var apiResponse = await _httpClient.PostAsync("users/Add", data);
            var result = apiResponse.Content.ReadAsStringAsync().Result;
            var apiResult = JsonConvert.DeserializeObject<APIResponse<UserDTO>>(result);

            if (apiResult != null)
            {
                return apiResult;
            }
            else
            {
                var response = new APIResponse<UserDTO> { IsSuccess = false };
                return response;
            }
        }

        public async Task<APIResponse<UserDTO>> GetUserById(int userId)
        {
            var resStr = await _httpClient.GetStringAsync(string.Format("users/{0}", userId));
            var apiResult = JsonConvert.DeserializeObject<APIResponse<UserDTO>>(resStr);
            if (apiResult != null)
            {
                return apiResult;
            }
            else
            {
                var response = new APIResponse<UserDTO> { IsSuccess = false };
                return response;
            }
        }

        public async Task<APIResponse<UserDTO>> UpdateUser(UserDTO userDTO)
        {
            var json = JsonConvert.SerializeObject(userDTO);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var apiResponse = await _httpClient.PostAsync("users/edit", data);
            var result = apiResponse.Content.ReadAsStringAsync().Result;
            var apiResult = JsonConvert.DeserializeObject<APIResponse<UserDTO>>(result);

            if (apiResult != null)
            {
                return apiResult;
            }
            else
            {
                var response = new APIResponse<UserDTO> { IsSuccess = false };
                return response;
            }
        }
        public async Task<APIResponse<UserEditDTO>> GetUserByUserId(int id)
        {
            var apiResponse = await _httpClient.GetAsync(string.Format("users/{0}", id));
            var result = apiResponse.Content.ReadAsStringAsync().Result;
            var apiResult = JsonConvert.DeserializeObject<APIResponse<UserEditDTO>>(result);

            if (apiResult != null)
            {
                return apiResult;
            }
            else
            {
                var response = new APIResponse<UserEditDTO> { IsSuccess = false };
                return response;
            }
        }

        ~UserService()
        {
            _httpClient?.Dispose();
        }
    }


}

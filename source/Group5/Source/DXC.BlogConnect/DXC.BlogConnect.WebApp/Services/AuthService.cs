using DXC.BlogConnect.DTO;
using DXC.BlogConnect.WebAPI.Models.Domain;
using DXC.BlogConnect.WebApp.ServiceExtension;
using DXC.BlogConnect.WebApp.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
/*
            * Created By: Kishore
            */
namespace DXC.BlogConnect.WebApp.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<APIResponse<string>> LoginAsync(UserLoginDTO userLoginDTO)
        {
            var json = JsonConvert.SerializeObject(userLoginDTO);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var apiResponse = await _httpClient.PostAsync("auth/login", data);
            var result = apiResponse.Content.ReadAsStringAsync().Result;
            var apiResult = JsonConvert.DeserializeObject<APIResponse<string>>(result);
            if (apiResult != null)
            {
                if (apiResult.IsSuccess)
                {
                    string? token = apiResult.Result[0];
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
                return apiResult;
            }
            else
            {
                var response = new APIResponse<string> { IsSuccess = false };
                return response;
            }


        }
        public bool IsCurrentUserInRole(string role)
        {
            var currenttok = Convert.ToString(_httpClient.DefaultRequestHeaders.Authorization);
            bool rslt = false;
            if (currenttok != null)
            {
                var handler = new JwtSecurityTokenHandler();
                //var jsonToken = handler.ReadToken(stream);
                var jsonToken = handler.ReadJwtToken(currenttok);
                // var tokenS = jsonToken;
                var currentRole = jsonToken.Claims.First(claim => claim.Type == "Roles").Value;
                //var userName = jsonToken.Claims.First(claim => claim.Type == "UserName").Value;

                if (currentRole == role)
                {
                    rslt = true;
                }
            }

            return rslt;
        }
        public bool IsLogin(string userName)
        {
            var currenttok = Convert.ToString(_httpClient.DefaultRequestHeaders.Authorization);
            bool rslt = false;
            if (currenttok != null)
            {
                var handler = new JwtSecurityTokenHandler();
                
                var jsonToken = handler.ReadJwtToken(currenttok);
                
                var CurrentuserName = jsonToken.Claims.First(claim => claim.Type == "UserName").Value;

                if (CurrentuserName == userName)
                {
                    rslt = true;
                }
            }

            return rslt;
        }
    }
}

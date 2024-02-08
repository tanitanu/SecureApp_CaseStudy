
using BlazorCARS.HealthShield.WebApp.Model;
using BlazorCARS.HealthShield.WebApp.Model.DTO;
using BlazorCARS.HealthShield.WebApp.Pages;
using BlazorCARS.HealthShield.WebApp.Services.IServices;
using Newtonsoft.Json;
using System.Text;

namespace BlazorCARS.HealthShield.WebApp.Services
{
    public class AuthService:BaseService,IAuthService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private string _apiURL;
        private readonly string _apiControllerRouteName;
        
        public AuthService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _apiURL = configuration.GetValue<string>("ServiceUrls:BlazorCARSAPI");
            _apiControllerRouteName = "auth";
        }
        public Task<T> LoginAsync<T>(UserDTO userDTO)
        {
            //HttpContent loginContent = new StringContent(JsonConvert.SerializeObject(userDTO), Encoding.UTF8, "application/json");
            
            return SendAsync<T>(new APIRequest()
            {
                RequestType = Enums.ApiType.POST,
                Url = $"{_apiURL}{_apiControllerRouteName}/login", 
               Data = userDTO
            });
        }
    }
}

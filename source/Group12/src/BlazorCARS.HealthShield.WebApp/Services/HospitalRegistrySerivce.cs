using BlazorCARS.HealthShield.WebApp.Model;
using BlazorCARS.HealthShield.WebApp.Model.DTO;
using BlazorCARS.HealthShield.WebApp.Services.IServices;
using System.Reflection;

namespace BlazorCARS.HealthShield.WebApp.Services
{
    public class HospitalRegistrySerivce : BaseService, IHospitalRegistrySerivce
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private string _apiURL;
        private readonly string _apiControllerRouteName;
        public HospitalRegistrySerivce(IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _apiURL = configuration.GetValue<string>("ServiceUrls:BlazorCARSAPI");
            _apiControllerRouteName = "hospitalregistry";
        }

        public Task<T> CreateAsync<T>(HospitalRegistryDTO CreateDto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                RequestType = Enums.ApiType.POST,
                Url = $"{_apiURL}{_apiControllerRouteName}",
                Data = CreateDto,
                Token = token
            });
        }
    }
}

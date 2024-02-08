using BlazorCARS.HealthShield.WebApp.Model.DTO;
using BlazorCARS.HealthShield.WebApp.Model;
using BlazorCARS.HealthShield.WebApp.Services.IServices;

namespace BlazorCARS.HealthShield.WebApp.Services
{
    public class HospitalService : BaseService, IHospitalService
    {
        private string _apiURL;
        private readonly IHttpClientFactory _httpClientFactory;
        private const string _apiControllerRouteName = "hospitals";

        public HospitalService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
            this._apiURL = configuration.GetValue<string>("ServiceUrls:BlazorCARSAPI");
        }

        public Task<T> CreateAsync<T>(HospitalCreateDTO CreateDto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                RequestType = Enums.ApiType.POST,
                Url = $"{_apiURL}{_apiControllerRouteName}",
                Data = CreateDto,
                Token = token
            });
        }

        public Task<T> GetAllAsync<T>(int pageSize, int pageNumber, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                RequestType = Enums.ApiType.GET,
                Url = $"{_apiURL}{_apiControllerRouteName}?pageSize={pageSize}&pageNumber={pageNumber}",
                Token = token
            });
        }

        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                RequestType = Enums.ApiType.GET,
                Url = $"{_apiURL}{_apiControllerRouteName}/{id}",
                Token = token
            });
        }

        public Task<T> UpdateAsync<T>(int id, HospitalUpdateDTO UpdateDto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                RequestType = Enums.ApiType.PUT,
                Url = $"{_apiURL}{_apiControllerRouteName}/{id}",
                Data = UpdateDto,
                Token = token
            });
        }
    }
}

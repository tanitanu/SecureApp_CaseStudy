using BlazorCARS.HealthShield.DataObject.DTO;
using BlazorCARS.HealthShield.WebApp.Model;
using BlazorCARS.HealthShield.WebApp.Services.IServices;

namespace BlazorCARS.HealthShield.WebApp.Services
{
    public class VaccineScheduleService : BaseService, IVaccineScheduleService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private string _apiURL;
        private readonly string _apiControllerRouteName;

        public VaccineScheduleService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _apiURL = configuration.GetValue<string>("ServiceUrls:BlazorCARSAPI");
            _apiControllerRouteName = "vaccineschedules";
        }

        public Task<T> CreateAsync<T>(VaccineScheduleCreateDTO CreateDto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                RequestType = Enums.ApiType.POST,
                Url = $"{_apiURL}{_apiControllerRouteName}",
                Data = CreateDto,
                Token = token
            });
        }
        public Task<T> DeleteAsync<T>(int id, string DeletedUSer, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                RequestType = Enums.ApiType.DELETE,
                Url = $"{_apiURL}{_apiControllerRouteName}/{id}/{DeletedUSer}",
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

        public Task<T> UpdateAsync<T>(int id, VaccineScheduleUpdateDTO UpdateDto, string token)
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
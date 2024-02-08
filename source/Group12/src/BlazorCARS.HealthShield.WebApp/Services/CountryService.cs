using BlazorCARS.HealthShield.DataObject.DTO;
using BlazorCARS.HealthShield.WebApp.Model;
using BlazorCARS.HealthShield.WebApp.Services.IServices;

namespace BlazorCARS.HealthShield.WebApp.Services
{
    public class CountryService : BaseService, ICountryService
    {
        private string _apiURL;
        private readonly IHttpClientFactory _httpClientFactory;
        private const string _apiControllerRouteName = "countries";

        public CountryService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
            this._apiURL = configuration.GetValue<string>("ServiceUrls:BlazorCARSAPI");
        }

        public Task<T> CreateAsync<T>(CountryCreateDTO CreateDto, string token)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public Task<T> UpdateAsync<T>(int id, CountryUpdateDTO UpdateDto, string token)
        {
            throw new NotImplementedException();
        }
    }
}

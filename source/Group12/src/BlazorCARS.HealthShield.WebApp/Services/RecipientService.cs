using BlazorCARS.HealthShield.WebApp.Model.DTO;
using BlazorCARS.HealthShield.WebApp.Model;
using BlazorCARS.HealthShield.WebApp.Services.IServices;

namespace BlazorCARS.HealthShield.WebApp.Services
{
    public class RecipientService : BaseService, IRecipientService
    {
        private string _apiURL;
        private readonly IHttpClientFactory _httpClientFactory;
        private const string _apiControllerRouteName = "recipients";

        public RecipientService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
            this._apiURL = configuration.GetValue<string>("ServiceUrls:BlazorCARSAPI");
        }

        public Task<T> CreateAsync<T>(RecipientCreateDTO CreateDto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                RequestType = Enums.ApiType.POST,
                Url = $"{_apiURL}{_apiControllerRouteName}",
                Data = CreateDto,
                Token = token
            });
        }

        //public Task<T> DeleteAsync<T>(int id, string DeletedRecipient, string token)
        //{
        //    return SendAsync<T>(new APIRequest()
        //    {
        //        RequestType = Enums.ApiType.DELETE,
        //        Url = $"{_apiURL}{_apiControllerRouteName}/{id}/{DeletedRecipient}",
        //        Token = token
        //    });
        //}

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

        public Task<T> UpdateAsync<T>(int id, RecipientUpdateDTO UpdateDto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                RequestType = Enums.ApiType.PUT,
                Url = $"{_apiURL}{_apiControllerRouteName}/{id}",
                Data = UpdateDto,
                Token = token
            });
        }

        public Task<T> UpdateProfileAsync<T>(int id, RecipientProfileUpdateDTO UpdateDto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                RequestType = Enums.ApiType.PUT,
                Url = $"{_apiURL}{_apiControllerRouteName}/{id}",
                Data = UpdateDto,
                Token = token
            });
        }
        public Task<T> GetAllDepandantsAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                RequestType = Enums.ApiType.GET,
                Url = $"{_apiURL}{_apiControllerRouteName}/depandant/{id}",
                Token = token
            });
        }
    }
}

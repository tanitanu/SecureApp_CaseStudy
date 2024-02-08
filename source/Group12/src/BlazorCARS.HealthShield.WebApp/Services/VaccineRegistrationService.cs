using BlazorCARS.HealthShield.WebApp.Model;
using BlazorCARS.HealthShield.WebApp.Model.DTO;
using BlazorCARS.HealthShield.WebApp.Services.IServices;
using System.Reflection;

namespace BlazorCARS.HealthShield.WebApp.Services
{
    public class VaccineRegistrationService : BaseService, IVaccineRegistrationService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private string _apiURL;
        private readonly string _apiControllerRouteName;
        public VaccineRegistrationService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _apiURL = configuration.GetValue<string>("ServiceUrls:BlazorCARSAPI");
            _apiControllerRouteName = "vaccineregistrations";
        }

        public Task<T> CreateAsync<T>(VaccineRegistrationCreateDTO CreateDto, string token)
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

        public Task<T> GetActiveAppointmentByHospitalAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                RequestType = Enums.ApiType.GET,
                Url = $"{_apiURL}{_apiControllerRouteName}/hospitalactiveappointment/{id}",
                Token = token
            });
        }

        public Task<T> GetActiveappoinmentAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                RequestType = Enums.ApiType.GET,
                Url = $"{_apiURL}{_apiControllerRouteName}/active/{id}",
                Token = token
            });
        }
    }
}


using BlazorCARS.HealthShield.Utility.Response;
using BlazorCARS.HealthShield.WebApp.Model;
using BlazorCARS.HealthShield.WebApp.Services.IServices;
using Newtonsoft.Json;
using System.Text;
/*
  Created by JAYaseelan
 */
namespace BlazorCARS.HealthShield.WebApp.Services
{
    public class BaseService : IBaseService
    {
        public APIResponse response { get; set; }
        public IHttpClientFactory _httpClient { get; set; }
        public BaseService(IHttpClientFactory httpClient)
        {
            response = new APIResponse();
            _httpClient = httpClient;
        }

        public async Task<T> SendAsync<T>(APIRequest reqeust)
        {
            try
            {
                var client = _httpClient.CreateClient("BlazorCARSAPI");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(reqeust.Url);
                if (reqeust.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(reqeust.Data), Encoding.UTF8, "application/json");
                }

                switch (reqeust.RequestType)
                {
                    case Enums.ApiType.POST:
                        {
                            message.Method = HttpMethod.Post;
                            break;
                        }
                    case Enums.ApiType.PUT:
                        {
                            message.Method = HttpMethod.Put;
                            break;
                        }
                    case Enums.ApiType.DELETE:
                        {
                            message.Method = HttpMethod.Delete;
                            break;
                        }
                    default:
                        {
                            message.Method = HttpMethod.Get;
                            break;
                        }
                }
                HttpResponseMessage response = null;
                if (!string.IsNullOrWhiteSpace(reqeust.Token))
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", reqeust.Token);
                }
                response = await client.SendAsync(message);
                var content = await response.Content.ReadAsStringAsync();
                var ApiResponse = JsonConvert.DeserializeObject<T>(content);
                return ApiResponse;
            }
            catch (Exception ex)
            {
                var dto = new APIResponse
                {
                    ErrorMessages = new List<string> { Convert.ToString(ex.Message) },
                    IsSuccess = false
                };
                var res = JsonConvert.SerializeObject(dto);
                var APIResponse = JsonConvert.DeserializeObject<T>(res);
                return APIResponse;
            }
        }
    }
}

using DXC.BlogConnect.DTO;
using DXC.BlogConnect.WebAPI.Models.DTO;
using DXC.BlogConnect.WebAPI.Services.Interfaces;
using DXC.BlogConnect.WebApp.Services.Interfaces;
using Newtonsoft.Json;
using System.Text;

namespace DXC.BlogConnect.WebApp.Services
{
    public class BlogService: IBlogService
    {
        private readonly HttpClient _httpClient;
        public BlogService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<APIResponse<BlogPostGetDTO>> GetAllBlogsAsync()
        {
            var resStr = await _httpClient.GetStringAsync("blogs");
            var apiResult = JsonConvert.DeserializeObject<APIResponse<BlogPostGetDTO>>(resStr);
            if (apiResult != null)
            {
                return apiResult;
            }
            else
            {
                var response = new APIResponse<BlogPostGetDTO> { IsSuccess = false };
                return response;
            }

        }
        public async Task<APIResponse<BlogPostAddDTO>> AddBlogAsync(BlogPostAddDTO userDTO)
        {
            var json = JsonConvert.SerializeObject(userDTO);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var apiResponse = await _httpClient.PostAsync("blogposts/Add", data);
            var result = apiResponse.Content.ReadAsStringAsync().Result;
            var apiResult = JsonConvert.DeserializeObject<APIResponse<BlogPostAddDTO>>(result);

            if (apiResult != null)
            {
                return apiResult;
            }
            else
            {
                var response = new APIResponse<BlogPostAddDTO> { IsSuccess = false };
                return response;
            }
        }

        public async Task<APIResponse<BlogPostGetDTO>> GetBlogByBlogId(int blogId)
        {
            var resStr = await _httpClient.GetStringAsync(string.Format("users/{0}", blogId));
            var apiResult = JsonConvert.DeserializeObject<APIResponse<BlogPostGetDTO>>(resStr);
            if (apiResult != null)
            {
                return apiResult;
            }
            else
            {
                var response = new APIResponse<BlogPostGetDTO> { IsSuccess = false };
                return response;
            }
        }

    }
}

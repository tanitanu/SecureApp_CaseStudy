using DXC.BlogConnect.DTO;
using DXC.BlogConnect.WebAPI.Models.DTO;

namespace DXC.BlogConnect.WebApp.Services.Interfaces
{
    public interface IBlogService
    {
        Task<APIResponse<BlogPostGetDTO>> GetAllBlogsAsync();
        Task<APIResponse<BlogPostAddDTO>> AddBlogAsync(BlogPostAddDTO userDTO);
        Task<APIResponse<BlogPostGetDTO>> GetBlogByBlogId(int blogId);
    }
}

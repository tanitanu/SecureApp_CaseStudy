using DXC.BlogConnect.WebAPI.Models.DTO;
using DXC.BlogConnect.WebAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DXC.BlogConnect.WebAPI.Controllers
{
    /*Created by Prabu Elavarasan*/
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostLikeController : ControllerBase
    {
        private readonly IBlogPostLikeRepository blogPostLikeRepository;

        public BlogPostLikeController(IBlogPostLikeRepository blogPostLikeRepository)
        {
            this.blogPostLikeRepository = blogPostLikeRepository;
        }

        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> AddLike([FromBody] AddBlogPostLikeDTO addBlogPostLikeRequest)
        {
            await blogPostLikeRepository.AddLikeForBlog(addBlogPostLikeRequest.BlogPostId,
                addBlogPostLikeRequest.UserId);

            return Ok();
        }


        [HttpGet]
        [Route("{blogPostId:int}/totalLikes")]
        public async Task<IActionResult> GetTotalLikes([FromRoute] int blogPostId)
        {
            var totalLikes = await blogPostLikeRepository.GetTotalLikesForBlog(blogPostId);

            return Ok(totalLikes);
        }
    }
}

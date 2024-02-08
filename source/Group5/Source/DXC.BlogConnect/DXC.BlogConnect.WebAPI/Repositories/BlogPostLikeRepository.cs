using DXC.BlogConnect.WebAPI.Data;
using DXC.BlogConnect.WebAPI.Models.Domain;
using DXC.BlogConnect.WebAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DXC.BlogConnect.WebAPI.Repositories
{
    /*Created by Prabu Elavarasan*/
    public class BlogPostLikeRepository : IBlogPostLikeRepository
    {
        private readonly BlogConnectDbcontext bloggieDbContext;

        public BlogPostLikeRepository(BlogConnectDbcontext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }

        public async Task AddLikeForBlog(int blogPostId, int userId)
        {
            var like = new Likes
            {
                PostId = blogPostId,
                LikedUserId = userId
            };

            await bloggieDbContext.BlogPostLike.AddAsync(like);
            await bloggieDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Likes>> GetLikesForBlog(int blogPostId)
        {
            return await bloggieDbContext.BlogPostLike.Where(x => x.PostId == blogPostId)
                .ToListAsync();
        }

        public async Task<int> GetTotalLikesForBlog(int blogPostId)
        {
            return await bloggieDbContext.BlogPostLike
                .CountAsync(x => x.PostId == blogPostId);
        }
    }
}

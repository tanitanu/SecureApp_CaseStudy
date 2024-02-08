using DXC.BlogConnect.WebAPI.Data;
using DXC.BlogConnect.WebAPI.Models.Domain;
using DXC.BlogConnect.WebAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DXC.BlogConnect.WebAPI.Repositories
{
    public class BlogPostCommentRepository: IBlogPostCommentRepository
    {
        private readonly BlogConnectDbcontext bloggieDbContext;

        public BlogPostCommentRepository(BlogConnectDbcontext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }

        public async Task<Comments> AddAsync(Comments blogPostComment)
        {
            await bloggieDbContext.BlogPostComment.AddAsync(blogPostComment);
            await bloggieDbContext.SaveChangesAsync();
            return blogPostComment;
        }

        public async Task<IEnumerable<Comments>> GetAllAsync(int blogPostId)
        {
            return await bloggieDbContext.BlogPostComment.Where(x => x.PostId == blogPostId)
                .ToListAsync();
        }
    }
}

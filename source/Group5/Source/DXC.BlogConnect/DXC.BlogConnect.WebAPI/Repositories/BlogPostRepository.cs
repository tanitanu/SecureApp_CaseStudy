using DXC.BlogConnect.WebAPI.Data;
using DXC.BlogConnect.WebAPI.Models.Domain;
using DXC.BlogConnect.WebAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DXC.BlogConnect.WebAPI.Repositories
{
    /*Created by Prabu Elavarasan*/
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly BlogConnectDbcontext bloggieDbContext;

        public BlogPostRepository(BlogConnectDbcontext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }

        public async Task<Post> AddAsync(Post blogPost)
        {
            await bloggieDbContext.BlogPosts.AddAsync(blogPost);
            await bloggieDbContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existingBlog = await bloggieDbContext.BlogPosts.FindAsync(id);

            if (existingBlog != null)
            {
                bloggieDbContext.BlogPosts.Remove(existingBlog);
                await bloggieDbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            return await bloggieDbContext.BlogPosts.Include(nameof(Post.Tags)).ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetAllAsync(string tagName)
        {
            return await (bloggieDbContext.BlogPosts.Include(nameof(Post.Tags))
                .Where(x => x.Tags.Any(x => x.Name == tagName)))
                .ToListAsync();
        }

        public async Task<Post> GetAsync(int id)
        {
            return await bloggieDbContext.BlogPosts.Include(nameof(Post.Tags))
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Post> UpdateAsync(Post blogPost)
        {
            var existingBlogPost = await bloggieDbContext.BlogPosts.Include(nameof(Post.Tags))
                .FirstOrDefaultAsync(x => x.Id == blogPost.Id);

            if (existingBlogPost != null)
            {
                existingBlogPost.Content = blogPost.Content;
                existingBlogPost.ShortDescription = blogPost.ShortDescription;
                existingBlogPost.ThumbnailUrl = blogPost.ThumbnailUrl;
                existingBlogPost.PublishedDate = blogPost.PublishedDate;

                if (blogPost.Tags != null && blogPost.Tags.Any())
                {
                    // Delete the existing tags
                    bloggieDbContext.Tags.RemoveRange(existingBlogPost.Tags);

                    // Add new tags
                    blogPost.Tags.ToList().ForEach(x => x.BlogPostId = existingBlogPost.Id);
                    await bloggieDbContext.Tags.AddRangeAsync(blogPost.Tags);
                }
            }

            await bloggieDbContext.SaveChangesAsync();
            return existingBlogPost;
        }
    }
}

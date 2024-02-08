using DXC.BlogConnect.WebAPI.Models.Domain;
using DXC.BlogConnect.WebAPI.Repositories;
using DXC.BlogConnect.WebAPI.Repositories.Interfaces;
using DXC.BlogConnect.WebAPI.Services.Interfaces;

namespace DXC.BlogConnect.WebAPI.Services
{
    public class BlogPostService: IBlogPostService
    {
        public IUnitOfWork _unitOfWork;
        public IBlogPostRepository _blogPostRepository;

        public BlogPostService(IUnitOfWork unitOfWork, IBlogPostRepository blogPostRepository)
        {
            _unitOfWork = unitOfWork;
            _blogPostRepository = blogPostRepository;
        }

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            var entity = await _blogPostRepository.GetAllAsync();
            return entity;
        }
        public async Task<IEnumerable<Post>> GetAllAsync(string tagName)
        {
            var entity = await _blogPostRepository.GetAllAsync(tagName);
            return entity;
        }
        public async Task<Post> GetAsync(int id)
        {
            var entity = await _blogPostRepository.GetAsync(id);
            return entity;
        }
        public async Task<Post> AddAsync(Post blogPost)
        {
            return await _blogPostRepository.AddAsync(blogPost);
        }
        public async Task<Post> UpdateAsync(Post blogPost)
        {
            return await _blogPostRepository.UpdateAsync(blogPost);
        }
         public async Task<bool> DeleteAsync(int id)
        {
            return await _blogPostRepository.DeleteAsync(id);
        }
    }
}

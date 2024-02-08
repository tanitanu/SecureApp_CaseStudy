using DXC.BlogConnect.WebAPI.Models.Domain;

namespace DXC.BlogConnect.WebAPI.Repositories.Interfaces
{
    /*Created by Prabu Elavarasan*/
    public interface IBlogPostRepository
    {
        Task<IEnumerable<Post>> GetAllAsync();
        Task<IEnumerable<Post>> GetAllAsync(string tagName);
        Task<Post> GetAsync(int id);
        Task<Post> AddAsync(Post blogPost);
        Task<Post> UpdateAsync(Post blogPost);
        Task<bool> DeleteAsync(int id);
    }
}

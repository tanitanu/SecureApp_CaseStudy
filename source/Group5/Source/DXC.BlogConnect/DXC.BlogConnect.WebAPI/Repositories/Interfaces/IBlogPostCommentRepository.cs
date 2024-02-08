using DXC.BlogConnect.WebAPI.Models.Domain;

namespace DXC.BlogConnect.WebAPI.Repositories.Interfaces
{
    public interface IBlogPostCommentRepository
    {
        Task<Comments> AddAsync(Comments blogPostComment);

        Task<IEnumerable<Comments>> GetAllAsync(int blogPostId);
    }
}

using DXC.BlogConnect.WebAPI.Models.Domain;

namespace DXC.BlogConnect.WebAPI.Repositories.Interfaces
{
    public interface IBlogPostLikeRepository
    {
        /*Created by Prabu Elavarasan*/
        Task<int> GetTotalLikesForBlog(int blogPostId);

        Task AddLikeForBlog(int blogPostId, int userId);

        Task<IEnumerable<Likes>> GetLikesForBlog(int blogPostId);
    }
}

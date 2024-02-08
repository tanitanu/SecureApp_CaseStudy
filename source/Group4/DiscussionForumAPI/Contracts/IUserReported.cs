using DiscussionForumAPI.Models;
using DiscussionForumAPI.Models.DTO;

namespace DiscussionForumAPI.Contracts
{
    public interface IUserReported
    {
        Task<List<UserReportedDTO>> GetAllReportedUsersAsync();
        Task<DiscussionForumUserReported?> GetByIdAsync (string reportUserId);

        Task<DiscussionForumUserReported?> CreateUpdateAsync(DiscussionForumUserReported user);
    }
}

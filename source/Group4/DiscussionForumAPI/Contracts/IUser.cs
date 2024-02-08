using DiscussionForumAPI.Models;
using DiscussionForumAPI.Models.DTO;

namespace DiscussionForumAPI.Contracts
{
    public interface IUser
    {
        Task<List<UsersDTO>> GetAllUsersAsync();

        Task<AspNetUser?> DeleteAsync(string userId);

    }
}

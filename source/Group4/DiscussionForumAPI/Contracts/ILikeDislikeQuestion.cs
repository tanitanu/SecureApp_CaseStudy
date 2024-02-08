using DiscussionForumAPI.Models;

namespace DiscussionForumAPI.Contracts
{
    public interface ILikeDislikeQuestion
    {
        Task<DiscussionForumLikesDislike?> GetByIdAsync(string likeDislikeQuestionId);
        Task<DiscussionForumLikesDislike> CreateUpdateAsync(DiscussionForumLikesDislike likeDislikeQuestion);
    }
}

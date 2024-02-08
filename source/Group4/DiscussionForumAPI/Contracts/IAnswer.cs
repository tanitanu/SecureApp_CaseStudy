using DiscussionForumAPI.Models;

namespace DiscussionForumAPI.Contracts
{
    public interface IAnswer
    {
        Task<DiscussionForumAnswer?> GetByIdAsync(string answerId);
        Task<DiscussionForumAnswer?> CreateAsync(DiscussionForumAnswer answer);

        Task<DiscussionForumAnswer?> UpdateAsync(string answerId, DiscussionForumAnswer answer);
        Task<DiscussionForumAnswer?> DeleteAsync(string answerId, string? userId);

        Task<QuestionAnswerDetails?> GetQuesAnsByIdAsync(string questionId, string answerId);
    }
}

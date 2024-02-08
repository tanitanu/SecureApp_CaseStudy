using DiscussionForumAPI.Models;

namespace DiscussionForumAPI.Contracts
{
    public interface IQuestion
    {
        Task<List<QuestionDetails?>> GetAllAsync();
        Task<DiscussionForumQuestion?> GetByIdAsync(string questionId);

        Task<QuestionAnswerDetails> GetQuestionAnswerDetails (string questionId, string userId);
        Task<DiscussionForumQuestion?> CreateAsync(DiscussionForumQuestion question);

        Task<DiscussionForumQuestion?> UpdateAsync(string questionId, DiscussionForumQuestion question);
        Task<DiscussionForumQuestion?> DeleteAsync(string questionId, string? userId);

        Task<List<DiscussionForumCategory>> GetAllCategories();
        Task<DiscussionForumQuestion?> UpdateAsync(string questionId, string? userId);

    }
}

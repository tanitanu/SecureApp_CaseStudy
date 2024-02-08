namespace DiscussionForumAPI.Models.DTO
{
    public class DiscussionForumQuestionDTO
    {
        public int Id { get; set; }

        public string QuestionId { get; set; } = null!;

        public string CategoryId { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string Content { get; set; } = null!;

        public bool? IsDelete { get; set; }

        public string? Status { get; set; }

        public string? CreatedBy { get; set; }

        public string? UpdatedBy { get; set; }

    }
}

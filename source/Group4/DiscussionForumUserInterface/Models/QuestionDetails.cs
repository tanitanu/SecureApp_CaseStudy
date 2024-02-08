namespace DiscussionForumUserInterface.Models
{
    public class QuestionDetails
    {
        public string? QuestionId { get; set; } = null!;

        public string? Title { get; set; } = null!;
        public string? CategoryName { get; set; } = null!;

        public string? Status { get; set; }
        public int LikeCount { get; set; }
        public int DislikeCount { get; set; }
        public bool Delete { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime DateCreation { get; set; }

        public string? CreatedByName { get; set; }

        public string? LoginId { get; set; }

        public string? Role { get; set; }

    }
}

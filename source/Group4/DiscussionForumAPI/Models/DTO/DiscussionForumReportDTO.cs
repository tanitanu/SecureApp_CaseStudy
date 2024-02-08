namespace DiscussionForumAPI.Models.DTO
{
    public class DiscussionForumReportDTO
    {
      public List<QuestionReportDetails> QuestionReportDetails { get; set; }
        public List<TopContributor> TopContributor { get; set; }
    }
    public class QuestionReportDetails
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
        public string? Role { get; set; }

    }

    public class TopContributor
    {
      public string UserName { get; set; }
        public int AnswerCount { get; set; }
    }
}

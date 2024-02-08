namespace DiscussionForumUserInterface.Models
{
    public class AdminReportModel
    {
        public string Status { get; set; }
        public string Type { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public string? FromYearDate { get; set; }
        public string? FromMonthDate { get; set; }
        public string? FromWeekDate { get; set; }
        public List<TopContributor> topContributor { get; set; }
        public List<QuestionReportDetails?> QuestionReportDetails { get; set; }

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

        public string? LoginId { get; set; }

        public string? Role { get; set; }

    }

    public class TopContributor
    {
        public string userName { get; set; }
        public int answerCount { get; set; }
    }

}

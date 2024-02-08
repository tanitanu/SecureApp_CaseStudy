namespace DiscussionForumAPI.Models.DTO
{
    public class DiscussionForumUserReportedDTO
    {
        public int Id { get; set; }

        public string ReportId { get; set; } = null!;

        public string QuestionId { get; set; } = null!;

        public string ReporterUserId { get; set; } = null!;

        public string ReporteeUserId { get; set; } = null!;

        public string? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

    }
}

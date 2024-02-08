namespace DiscussionForumAPI.Models.DTO
{
    public class DiscussionForumAnswerDTO
    {
        public int Id { get; set; }

        public string AnswerId { get; set; } = null!;

        public string QuestionId { get; set; } = null!;

        public string Answer { get; set; } = null!;

        public bool? IsDelete { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}

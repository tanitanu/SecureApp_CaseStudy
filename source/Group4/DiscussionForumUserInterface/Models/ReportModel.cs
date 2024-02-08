using System.ComponentModel.DataAnnotations;

namespace DiscussionForumUserInterface.Models
{
    public class ReportModel
    {
        public string QuestionId { get; set; } = null!;
        public string RespondentUserId { get; set; } = null!;
    }
}

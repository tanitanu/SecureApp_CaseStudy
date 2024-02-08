using System.ComponentModel.DataAnnotations;

namespace DiscussionForumAPI.Models.DTO
{
    public class AddUpdateUserReporterDTO
    {

        [Required]
        public string QuestionId { get; set; } = null!;
        public string? ReporterUserId { get; set; }
        [Required]
        public string RespondentUserId { get; set; } = null!;

    }
}

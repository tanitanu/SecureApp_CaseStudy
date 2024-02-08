using System.ComponentModel.DataAnnotations;

namespace DiscussionForumAPI.Models.DTO
{
    public class UpdateAnswerDTO
    {
        [Required(ErrorMessage = "Answer Id is required")]
        public string? AnswerId { get; set; }

        [Required(ErrorMessage = "Question Id is required")]
        public string QuestionId { get; set; } = null!;

        [Required(ErrorMessage = "Answer is required")]
        public string Answer { get; set; } = null!;

        public bool? IsDelete { get; set; }
    }
}

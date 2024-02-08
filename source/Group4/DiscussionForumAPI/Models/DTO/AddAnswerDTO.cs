using System.ComponentModel.DataAnnotations;

namespace DiscussionForumAPI.Models.DTO
{
    public class AddAnswerDTO
    {
        [Required(ErrorMessage = "Question Id is required")]
        public string QuestionId { get; set; } = null!;

        [Required(ErrorMessage = "Answer is required")]
        public string Answer { get; set; } = null!;

    }
}

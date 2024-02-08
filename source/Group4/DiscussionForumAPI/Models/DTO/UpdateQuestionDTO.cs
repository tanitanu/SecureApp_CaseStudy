using System.ComponentModel.DataAnnotations;

namespace DiscussionForumAPI.Models.DTO
{
    public class UpdateQuestionDTO
    {
        [Required(ErrorMessage = "Question Id is required")]
        public string? QuestionId { get; set; }
        [Required]
        public string CategoryId { get; set; } = null!;

        [Required(ErrorMessage = "Title is required")]
        [MinLength(3, ErrorMessage = "Title has to be a minimum of 3 characters")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Status is required")]
        public string? Status { get; set; }
        [Required(ErrorMessage = "Content is required")]
        public string? Content { get; set; }

        public bool? IsDelete { get; set; }

    }
}

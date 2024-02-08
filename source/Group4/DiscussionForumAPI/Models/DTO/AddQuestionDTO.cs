using System.ComponentModel.DataAnnotations;

namespace DiscussionForumAPI.Models.DTO
{
    public class AddQuestionDTO
    {

        [Required(ErrorMessage = "Category Id is required")]
        public string CategoryId { get; set; } = null!;

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Status is required")]
        public string? Status { get; set; }
        [Required(ErrorMessage = "Content is required")]
        public string? Content { get; set; }

        public bool? IsDelete { get; set; }
    }
}

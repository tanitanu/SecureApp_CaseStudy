using System.ComponentModel.DataAnnotations;

namespace DiscussionForumAPI.Models.DTO
{
    public class AddLikeDislikeQuestionDTO
    {
        [Required(ErrorMessage = "Question Id is required")]
        public string QuestionId { get; set; } = null!;
        [Required(ErrorMessage = "Like is required")]
        public bool? Like { get; set; }
        [Required(ErrorMessage = "Dislike is required")]
        public bool? Dislike { get; set; }

    }
}

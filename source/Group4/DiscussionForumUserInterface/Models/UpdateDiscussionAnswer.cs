using System.ComponentModel.DataAnnotations;

namespace DiscussionForumUserInterface.Models
{
    public class UpdateDiscussionAnswer
    {
        public string? QuestionId { get; set; }

        public string? Title { get; set; } 
        public string? Content { get; set; } 
        public string? CategoryName { get; set; } 

        public string? Status { get; set; }
        public int? LikeCount { get; set; }
        public int? DislikeCount { get; set; }

        public string? QuestionCreatedBy { get; set; }
        public DateTime? QuestionDateCreation { get; set; }

        public string? QuestionCreatedByName { get; set; }
        public bool? QuestionIsDelete { get; set; }

        [Required(ErrorMessage = "Answer is required")]
        public string? Answer { get; set; }

        public string? AnswerID { get; set; }
        public bool? AnswerIsDelete { get; set; }
        public string? AnswerCreatedByName { get; set; }
        public DateTime? AnswerDateCreation { get; set; }
        public string? AnswerCreatedBy { get; set; }
    }
}

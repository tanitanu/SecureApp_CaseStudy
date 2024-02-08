using Microsoft.AspNetCore.Http.HttpResults;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DiscussionForumAPI.Models
{
    public class QuestionAnswerDetails
    {
        public string? QuestionId { get; set; } = null!;

        public string? Title { get; set; } = null!;
        public string? Content { get; set; } = null!;
        public string? CategoryName { get; set; } = null!;

        public string? Status { get; set; }
        public int? LikeCount { get; set; }
        public int? DislikeCount { get; set; }

        public string? QuestionCreatedBy { get; set; }
        public DateTime? QuestionDateCreation { get; set; }

        public string? QuestionCreatedByName { get; set; }
        public bool? QuestionIsDelete { get; set; }
        public bool? Like { get; set; }
        public bool? Dislike { get; set; }


        public List<AnswerDetails?> answerDetails { get; set; }


    }

    public class AnswerDetails
    {
        public string? AnswerID { get; set; }
        public string? Answer { get; set; }      
        public bool? AnswerIsDelete { get; set; }
        public string? AnswerCreatedByName { get; set; }
        public DateTime? AnswerDateCreation { get; set; }
        public string? AnswerCreatedBy { get; set; }
    }
}

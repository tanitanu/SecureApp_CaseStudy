using System.ComponentModel.DataAnnotations;

namespace DiscussionForumUserInterface.Models
{
    public class LikeDislikeModel
    {
        public string QuestionId { get; set; } = null!;
        public bool? Like { get; set; }
        public bool? Dislike { get; set; }
    }
}

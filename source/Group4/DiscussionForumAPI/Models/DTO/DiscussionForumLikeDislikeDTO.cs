namespace DiscussionForumAPI.Models.DTO
{
    public class DiscussionForumLikeDislikeDTO
    {
        public int Id { get; set; }

        public string LikeDislikeId { get; set; } = null!;

        public string QuestionId { get; set; } = null!;

        public bool? Like { get; set; }

        public bool? Dislike { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}

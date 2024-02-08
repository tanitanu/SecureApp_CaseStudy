namespace DiscussionForumAPI.Models.DTO
{
    public class DisussionForumUserTwoFactorCodeDTO
    {
        public int Id { get; set; }

        public string UserTwoFactorCodeId { get; set; } = null!;

        public string UserId { get; set; } = null!;

        public string? Code { get; set; }

        public DateTime? CreatedOn { get; set; }
    }
}

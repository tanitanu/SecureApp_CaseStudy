namespace DiscussionForumAPI.Models.DTO
{
    public class UsersDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
    }
}

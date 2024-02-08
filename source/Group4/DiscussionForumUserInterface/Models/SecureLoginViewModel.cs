using Microsoft.AspNetCore.Identity;

namespace DiscussionForumUserInterface.Models
{
    public class SecureLoginViewModel
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public  DateTime Expiration { get; set; }

        public  string? Email { get; set; }
        public  string? Id { get; set; }
        public  string? Role { get; set; }

        public  string? UserName { get; set; }
    }    
}

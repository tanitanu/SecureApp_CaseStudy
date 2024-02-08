using Microsoft.AspNetCore.Identity;

namespace DiscussionForumAPI.Auth
{
    /// <summary Author = Kirti Garg>
    /// In the below class, we have extended default Identity user with new properties refresh token and refresh token expiry time.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}

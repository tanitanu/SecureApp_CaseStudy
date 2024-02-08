using System.ComponentModel.DataAnnotations;

namespace DiscussionForumAPI.Auth
{
    /// <summary Author = Kirti Garg>
    /// We have created Login Model with necessary fields
    /// </summary>
    public class LoginModel
    {
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string? Code { get; set; }
    }
}

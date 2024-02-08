using System.ComponentModel.DataAnnotations;

namespace DiscussionForumAPI.Auth
{
    /// <summary Author = Kirti Garg>
    /// We have created Register Model with necessary fields.
    /// </summary>
    public class RegisterModel
    {
        [Required(ErrorMessage = "First Name is required")]
        [DataType(DataType.Text)]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

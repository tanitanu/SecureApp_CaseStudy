using System.ComponentModel.DataAnnotations;

namespace SecureApp.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter your email address")]

        [DataType(DataType.EmailAddress)]

        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Email is not in a valid format")]

        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter Password")]

        public string Password { get; set; } = string.Empty;

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}

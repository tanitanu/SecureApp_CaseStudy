using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace DiscussionForumUserInterface.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "First Name is required")]
        [DataType(DataType.Text)]
        public string? Firstname { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
        public string? ConfirmPassword { get; set; }
        
    }
}

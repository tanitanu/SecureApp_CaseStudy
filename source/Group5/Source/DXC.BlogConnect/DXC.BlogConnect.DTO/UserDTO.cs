using System.ComponentModel.DataAnnotations;

namespace DXC.BlogConnect.DTO
{
    /*
* Created By: Kishore
*/
    public class UserDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "User Name is required")]
        [StringLength(10, ErrorMessage = "Must be between 6 and 10 characters", MinimumLength = 6)]
        [Display(Name = "User Name", AutoGenerateFilter = false)]
        public string UserName { get; set; } = null!;
        [Required(ErrorMessage = "First Name is required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = null!;
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage ="Please enter valid email")]
        public string EmailId { get; set; } = null!;

        [Required(ErrorMessage = "Password is required")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [StringLength(10, ErrorMessage = "Must be between 6 and 10 characters", MinimumLength = 6)]
        public string Password { get; set; } = null!;
        [Required(ErrorMessage = "Confirm Password is required")]
        [Display(Name = "Confirm Password")]
        [StringLength(10, ErrorMessage = "Must be between 6 and 10 characters", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; } = null!;
    }
}

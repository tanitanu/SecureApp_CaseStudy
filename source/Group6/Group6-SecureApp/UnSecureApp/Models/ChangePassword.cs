using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace UnSecureApp.Models
{
    [Keyless]
    public class ChangePassword
    {
        [Required]
        [PersonalData]
        [Display(Name = "User Name")]
        public string? UserName { get; set; }

        public virtual User? User { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage =
            "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

    }
}

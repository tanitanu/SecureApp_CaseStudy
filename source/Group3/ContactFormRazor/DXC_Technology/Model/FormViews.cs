using System.ComponentModel.DataAnnotations;

namespace DXC_Technology.Model
{
    public class FormViews
    {
        //Developed by Geetanjali
        public Guid Id { get; set; }

        [Required(ErrorMessage="Please Enter Your Name")]
        [RegularExpression("^((?!^UserName$)[a-zA-Z '])+$", ErrorMessage = "Please enter a valid Name")]
        public string UserName { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Please Enter a valid email ID")]
        public string Email { get; set; }

        public string? Subject { get; set; }
        [Required(ErrorMessage = "Please Enter a message")]
        [MaxLength(1000, ErrorMessage = "Maximum charecters reached")]
        public string Message { get; set; }
    }
}

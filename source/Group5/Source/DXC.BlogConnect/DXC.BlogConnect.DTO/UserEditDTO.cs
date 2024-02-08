using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DXC.BlogConnect.DTO
{
    public class UserEditDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        [Required(ErrorMessage = "First Name is required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = null!;
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Please enter valid email")]
        public string EmailId { get; set; } = null!;

    }
}

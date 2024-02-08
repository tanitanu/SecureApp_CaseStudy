using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingScheduler.Entities
{
    public class UserLogin
    {
        [Required(ErrorMessage = "Please enter your email address")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Email is not in a valid format")]
        public string EmailId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter Password")]
        public string Password { get; set; } = string.Empty;
    }
}

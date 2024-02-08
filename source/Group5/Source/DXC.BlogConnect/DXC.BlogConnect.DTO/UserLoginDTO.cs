using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace DXC.BlogConnect.DTO
{
    /*
* Created By: Kishore
*/
    public class UserLoginDTO
    {
        [Required(ErrorMessage = "Please enter User Name")]
        [StringLength(10, ErrorMessage = "Must be between 6 and 10 characters", MinimumLength = 6)]
        [Display(Name = "User Name", AutoGenerateFilter = false)]
        public string UserName { get; set; } = null!;
        [Required(ErrorMessage = "Please enter Password")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [StringLength(10, ErrorMessage = "Must be between 6 and 10 characters", MinimumLength = 6)]
        public string Password { get; set; } = null!;
    }
}

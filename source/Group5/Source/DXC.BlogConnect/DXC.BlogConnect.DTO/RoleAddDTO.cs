using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DXC.BlogConnect.DTO
{
    public class RoleAddDTO
    {
        [Required(ErrorMessage = "RoleName is required")]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; } = null!;
        public string UserName { get; set; } = null!;
    }
}

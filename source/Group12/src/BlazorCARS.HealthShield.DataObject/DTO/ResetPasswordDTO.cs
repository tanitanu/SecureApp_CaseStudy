using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
  Created by JAYaseelan
 */
namespace BlazorCARS.HealthShield.DataObject.DTO
{
    public class ResetPasswordDTO
    {
        public string UserName { get; set; }
        public int UserRoleId { get; set; }
        public string NewPassword { get; set; }
        public string ChangedBy { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
  Created by JAYaseelan
 */
namespace BlazorCARS.HealthShield.Utility.RequestValidator
{
    public class UserStoreRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int UserRoleId { get; set; }
        public int DiscriminationId { get; set; }
    }
}

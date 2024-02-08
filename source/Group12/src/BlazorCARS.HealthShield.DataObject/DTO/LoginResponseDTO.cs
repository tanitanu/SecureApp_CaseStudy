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
    public class LoginResponseDTO
    {
        public string UserName { get; set; }
        public string UserRole { get; set; }
        public int? DiscriminationId { get; set; }
        public string Token { get; set; }
    }
}

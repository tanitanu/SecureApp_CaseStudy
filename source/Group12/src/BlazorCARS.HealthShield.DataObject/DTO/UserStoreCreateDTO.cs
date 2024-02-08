using BlazorCARS.HealthShield.DataObject.DTO.Base;
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
    public class UserStoreCreateDTO : CreateDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int UserRoleId { get; set; }
        public int? DiscriminationId { get; set; }
    }
}

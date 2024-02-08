using System.ComponentModel.DataAnnotations;
/*
  Created by JAYaseelan
 */
namespace BlazorCARS.HealthShield.WebApp.Model.DTO
{
    public class UserRoleDTO
    {
        [Display(Name = "User Role Id")]
        public int UserRoleId { get; set; }
        [Display(Name = "User Role")]
        public string Name { get; set; }
    }
}

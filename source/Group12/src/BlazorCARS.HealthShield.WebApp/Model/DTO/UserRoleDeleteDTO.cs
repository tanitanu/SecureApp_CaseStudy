using System.ComponentModel.DataAnnotations;
/*
  Created by JAYaseelan
 */
namespace BlazorCARS.HealthShield.WebApp.Model.DTO
{
    public class UserRoleDeleteDTO
    {
        public int UserRoleId { get; set; }
        [Display(Name = "Role")]
        public string Name { get; set; }
        [Display(Name = "Deleted User")]
        public string DeletedUser { get; set; }
    }
}

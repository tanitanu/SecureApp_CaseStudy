using System.ComponentModel.DataAnnotations;
/*
  Created by JAYaseelan
 */
namespace BlazorCARS.HealthShield.WebApp.Model.DTO
{
    public class UserRoleCreateDTO
    {
        [Required(ErrorMessage = "Role must not be empty.")]
        [Display(Name = "Role")]
        public string Name { get; set; }
        [Display(Name = "Created User")]
        public string CreatedUser { get; set; }
    }
}

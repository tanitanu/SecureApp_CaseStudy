using System.ComponentModel.DataAnnotations;
/*
  Created by JAYaseelan
 */
namespace BlazorCARS.HealthShield.WebApp.Model.DTO
{
    public class UserRoleUpdateDTO
    {
        public int UserRoleId { get; set; }
        [Required(ErrorMessage = "Role must not be empty.")]
        [Display(Name = "Role")]
        public string Name { get; set; }
        [Display(Name = "Updated User")]
        public string UpdatedUser { get; set; }
    }
}

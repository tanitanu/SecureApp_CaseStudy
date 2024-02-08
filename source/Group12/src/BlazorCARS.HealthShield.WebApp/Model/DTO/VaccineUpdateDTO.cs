using System.ComponentModel.DataAnnotations;
/*
  Created by JAYaseelan
 */
namespace BlazorCARS.HealthShield.WebApp.Model.DTO
{
    public class VaccineUpdateDTO
    {
        public int VaccineId { get; set; }
        [Required(ErrorMessage = "VaccineID must not be empty.")]
        [Display(Name = "Role")]
        public string Name { get; set; }
        [Display(Name = "Updated Vaccine")]
        public string UpdatedVaccine { get; set; }
    }
}

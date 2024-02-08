using System.ComponentModel.DataAnnotations;
/*
  Created by JAYaseelan
 */
namespace BlazorCARS.HealthShield.WebApp.Model.DTO
{
    public class VaccineCreateDTO
    {
        [Required(ErrorMessage = "VaccineId must not be empty.")]
        //[Display(Name = "Role")]
        public string Name { get; set; }
        [Display(Name = "Created Vaccine")]
        public string CreatedVaccine { get; set; }
    }
}

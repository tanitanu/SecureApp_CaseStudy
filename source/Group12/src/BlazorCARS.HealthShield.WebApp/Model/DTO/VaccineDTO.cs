using System.ComponentModel.DataAnnotations;

namespace BlazorCARS.HealthShield.WebApp.Model.DTO
{
    public class VaccineDTO
    {
        
        [Required(ErrorMessage = "VaccineId is required")]
        [DataType(DataType.Text)]
        public string VaccinId { get; set; }
        
        [Required(ErrorMessage = "Name is required")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Manufacturer is required")]
        [DataType(DataType.Text)]
        public string Manufacturer { get; set; }


        [Required(ErrorMessage ="AgeGroup is required")]
        [DataType(DataType.Text)]
        public string AgeGroup { get; set; }


    }
}

using BlazorCARS.HealthShield.DataObject.DTO;
using System.ComponentModel.DataAnnotations;

namespace BlazorCARS.HealthShield.WebApp.Model.DTO
{
    public class HospitalDTO
    {
        public int HospitalId { get; set; }
        
        [Display(Name = "Hospital Name*")]
        public string Name { get; set; }

        [Display(Name = "License Number*")]
        [MaxLength(20)]
        public string LicenseNo { get; set; }
        
        [Display(Name = "Type*")]
        public string Discrimination { get; set; }
        
        [Display(Name = "Address1*")]
        public string Address1 { get; set; }
        
        [Display(Name = "Address2")]
        public string Address2 { get; set; }
        
        [Display(Name = "Landmark")]
        public string Landmark { get; set; }
        
        [Display(Name = "City*")]
        public string City { get; set; }
        
        [Display(Name = "Postal Code*")]
        [MaxLength(6)]
        public string PostalCode { get; set; }
        
        [Display(Name = "Select State*")]
        public int StateId { get; set; }
        
        [Display(Name = "Select Country*")]
        public int CountryId { get; set; }

        [Display(Name = "Primary Contact*")]
        [MaxLength(10)]
        public string PrimaryContact { get; set; }

        [Display(Name = "Secondary Contact")]
        [MaxLength(10)]
        public string SecondaryContact { get; set; }

        [Display(Name = "Emergency Contact*")]
        [MaxLength(10)]
        public string EmergencyContact { get; set; }

        [Display(Name = "Email Id*")]
        public string EmailId { get; set; }

        [Display(Name = "Created User")]
        public string CreatedUser { get; set; }

        [Display(Name = "State")]
        public List<StateDTO> StateList { get; set; }

        [Display(Name = "Country")]
        public List<CountryDTO> CountryList { get; set; }
    }
}

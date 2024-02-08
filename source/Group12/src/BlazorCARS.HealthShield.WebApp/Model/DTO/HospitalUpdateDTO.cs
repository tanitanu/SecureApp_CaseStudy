using BlazorCARS.HealthShield.DataObject.DTO;
using System.ComponentModel.DataAnnotations;

namespace BlazorCARS.HealthShield.WebApp.Model.DTO
{
    public class HospitalUpdateDTO
    {
        public int HospitalId { get; set; }

        [Required(ErrorMessage = "Hospital name must not be empty.")]
        [Display(Name = "Hospital Name*")]
        public string Name { get; set; }

        [Required(ErrorMessage = "License number must not be empty.")]
        [Display(Name = "License Number*")]
        [MaxLength(20)]
        public string LicenseNo { get; set; }

        [Required(ErrorMessage = "Type must be selected.")]
        [Display(Name = "Type*")]
        public string Discrimination { get; set; }

        [Required(ErrorMessage = "Address1 must not be empty.")]
        [Display(Name = "Address1*")]
        public string Address1 { get; set; }

        [Display(Name = "Address2")]
        public string Address2 { get; set; }

        [Display(Name = "Landmark")]
        public string Landmark { get; set; }

        [Required(ErrorMessage = "City must not be empty.")]
        [Display(Name = "City*")]
        public string City { get; set; }

        [Required(ErrorMessage = "Postal code must not be empty.")]
        [Display(Name = "Postal Code*")]
        [MaxLength(6)]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "State must be selected.")]
        [Display(Name = "Select State*")]
        public int StateId { get; set; }

        [Required(ErrorMessage = "Country must be selected")]
        [Display(Name = "Select Country*")]
        public int CountryId { get; set; }

        [Required(ErrorMessage = "Primary contact must not be empty.")]
        [Display(Name = "Primary Contact*")]
        [MaxLength(10)]
        public string PrimaryContact { get; set; }

        [Display(Name = "Secondary Contact")]
        [MaxLength(10)]
        public string SecondaryContact { get; set; }

        [Required(ErrorMessage = "Emergency contact must not be empty.")]
        [Display(Name = "Emergency Contact*")]
        [MaxLength(10)]
        public string EmergencyContact { get; set; }

        [Required(ErrorMessage = "Email address must not be empty.")]
        [Display(Name = "Email Id*")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid email address")]
        public string EmailId { get; set; }

        public string UpdatedUser { get; set; }

        public List<StateDTO> StateList { get; set; }

        public List<CountryDTO> CountryList { get; set; }

        public string RegistrationStatus { get; set; }
    }
}

using BlazorCARS.HealthShield.DataObject.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BlazorCARS.HealthShield.WebApp.Model.DTO
{
    public class RecipientProfileUpdateDTO
    {   
        public int RecipientId { get; set; }

        [Required(ErrorMessage = "First name must not be empty.")]
        [Display(Name = "First Name*")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name must not be empty.")]
        [Display(Name = "Last Name*")]
        public string LastName { get; set; }
        
        [Required(ErrorMessage = "Aadhar number must not be empty.")]
        [Display(Name = "Aadhaar Number*")]
        [MaxLength(12)]
        public string AadhaarNo { get; set; }
        
        [Required(ErrorMessage = "Date of birth must not be empty.")]
        [Display(Name = "DOB*")]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

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

        [Required(ErrorMessage = "State must be selected")]
        [Display(Name = "Select State*")]
        public int StateId { get; set; }
        
        [Required(ErrorMessage = "Country must be selected")]
        [Display(Name = "Select Country*")]
        public int CountryId { get; set; }
        
        [Required(ErrorMessage = "Primary contact must be selected")]
        [Display(Name = "Primary Contact*")]
        [MaxLength(10)]
        public string PrimaryContact { get; set; }
        
        [Display(Name = "Secondary Contact")]
        [MaxLength(10)]
        public string SecondaryContact { get; set; }
        
        [Display(Name = "Emergency Contact*")]
        [MaxLength(10)]
        public string EmergencyContact { get; set; }
        
        [Display(Name = "Email Id")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid email address")]
        public string EmailId { get; set; }

        public string UpdatedUser { get; set; }

        public List<StateDTO> StateList { get; set; }

        public List<CountryDTO> CountryList { get; set; }
    }
}

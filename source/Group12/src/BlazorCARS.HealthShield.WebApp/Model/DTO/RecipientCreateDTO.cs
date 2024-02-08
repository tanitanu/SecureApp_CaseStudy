using BlazorCARS.HealthShield.DataObject.DTO;
using System.ComponentModel.DataAnnotations;

namespace BlazorCARS.HealthShield.WebApp.Model.DTO
{
    public class RecipientCreateDTO
    {
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

        [Display(Name = "RelationshipType")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Relationship type must be selected")]
        public string RelationshipType { get; set; }

        public int? DependentRecipientId { get; set; }

        public string CreatedUser { get; set; }

    }
}

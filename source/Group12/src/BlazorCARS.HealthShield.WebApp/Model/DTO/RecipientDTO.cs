using BlazorCARS.HealthShield.DataObject.DTO;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlazorCARS.HealthShield.WebApp.Model.DTO
{
    public class RecipientDTO
    {
        public int RecipientId { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Aadhaar Number")]
        public string AadhaarNo { get; set; }

        [Display(Name = "DOB")]
        public DateTime DOB { get; set; }

        [Display(Name = "Relationship Type")]
        public string RelationshipType { get; set; }

        public int? DependentRecipientId { get; set; }

        public string Dose { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        [Display(Name = "Primary Contact")]
        public string PrimaryContact { get; set; }

        [Display(Name = "Secondary Contact")]
        public string SecondaryContact { get; set; }

        [Display(Name = "Emergency Contact")]
        public string EmergencyContact { get; set; }

        [Display(Name = "Email Id")]
        public string EmailId { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string Landmark { get; set; }

        public string City { get; set; }

        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        [Display(Name = "State")]
        public int StateId { get; set; }

        [Display(Name = "Country")]
        public int CountryId { get; set; }
    }
}

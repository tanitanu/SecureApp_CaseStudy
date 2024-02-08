using System.ComponentModel.DataAnnotations;

namespace BlazorCARS.HealthShield.WebApp.Model.DTO
{
    public class VaccineRegistrationCreateDTO
    {
        [Display(Name = "Recipient")]
        public int RecipientId { get; set; }
        public string RecipientName { get; set; }
        [Required(ErrorMessage = "Hospital must be selected")]
        [Display(Name = "Select Hospital*")]
        public string Hospital { get; set; }
        
        public int VaccineScheduleId { get; set; }
        [Required(ErrorMessage = "ScheduleDate must be selected")]
        [Display(Name = "Select Schedule Date*")]
        public DateTime ScheduleDate { get; set; }
        public bool IsVaccinated { get; set; } = false;
        [Required(ErrorMessage = "Timeslot must be selected")]
        [Display(Name = "Select Time slot*")]
        public string TimeSlot { get; set; }
        [Required(ErrorMessage = "Dose must be selected")]
        [Display(Name = "Select Dose*")]
        public string Dose { get; set; }
        [Display(Name ="Dependent Recipient")]
        public int DependantRecipientId { get; set; }
        [Display(Name = "Created User")]
        public string CreatedUser { get; set; }

    }
}

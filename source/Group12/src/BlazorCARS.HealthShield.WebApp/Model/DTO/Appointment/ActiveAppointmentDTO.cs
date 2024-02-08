using System.ComponentModel.DataAnnotations;

namespace BlazorCARS.HealthShield.WebApp.Model.DTO.Appointment
{
    public class ActiveAppointmentDTO
    {
        public int VaccineRegistrationId { get; set; }
        public int RecipientId { get; set; }
        [Display(Name ="Recipient Name")]
        public string RecipientName { get; set; }
        public int VaccineScheduleId { get; set; }
        [Display(Name = "Schedule Date")]
        public DateTime ScheduleDate { get; set; }
        [Display(Name = "Time Slot")]
        public string TimeSlot { get; set; }
        [Display(Name = "Dose")]
        public string Dose { get; set; }
    }
}

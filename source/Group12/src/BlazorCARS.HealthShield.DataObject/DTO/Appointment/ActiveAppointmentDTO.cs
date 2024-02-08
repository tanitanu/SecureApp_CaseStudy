using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorCARS.HealthShield.DataObject.DTO.Appointment
{
    public class ActiveAppointmentDTO
    {
        public int VaccineRegistrationId { get; set; }
        public int RecipientId { get; set; }
        public string RecipientName { get; set; }
        public int VaccineScheduleId { get; set; }
        public DateTime ScheduleDate { get; set; }
        public string TimeSlot { get; set; }
        public string Dose { get; set; }
    }
}

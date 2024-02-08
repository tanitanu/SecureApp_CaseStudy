namespace BlazorCARS.HealthShield.WebApp.Model.DTO
{
    public class VaccineScheduleDTO
    {
        public int VaccineScheduleId { get; set; }
        public int HospitalId { get; set; }
        public int VaccineId { get; set; }
        public DateTime ScheduleDate { get; set; }
        public string TimeSlot { get; set; }
    }
}

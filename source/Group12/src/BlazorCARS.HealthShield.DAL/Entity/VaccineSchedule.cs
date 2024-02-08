using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
  Created by JAYaseelan
 */

namespace BlazorCARS.HealthShield.DAL.Entity
{
    public class VaccineSchedule : BaseDomain
    {
        public int VaccineScheduleId { get; set; }
        public int HospitalId { get; set; }
        public int VaccineId { get; set; }
        public DateTime ScheduleDate { get; set; }
        public string TimeSlot { get; set; }
    }
}

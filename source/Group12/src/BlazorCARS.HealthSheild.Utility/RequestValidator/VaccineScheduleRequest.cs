using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
  Created by JAYaseelan
 */
namespace BlazorCARS.HealthShield.Utility.RequestValidator
{
    public class VaccineScheduleRequest
    {
        public int HospitalId { get; set; }
        public int VaccineId { get; set; }
        public string TimeSlot { get; set; }
    }
}

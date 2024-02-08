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
    public class VaccineRegistrationRequest
    {
        public int RecipientId { get; set; }
        public int VaccineScheduleId { get; set; }
        public string TimeSlot { get; set; }
        public string Dose { get; set; }
    }
}

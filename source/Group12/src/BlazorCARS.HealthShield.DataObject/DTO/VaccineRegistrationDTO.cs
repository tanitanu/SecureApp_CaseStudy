using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
  Created by JAYaseelan
 */

namespace BlazorCARS.HealthShield.DataObject.DTO
{
    public class VaccineRegistrationDTO
    {
        public int VaccineRegistrationId { get; set; }
        public int RecipientId { get; set; }
        public int VaccineScheduleId { get; set; }
        public bool IsVaccinated { get; set; }
        public string TimeSlot { get; set; }
        public string Dose { get; set; }
    }
}

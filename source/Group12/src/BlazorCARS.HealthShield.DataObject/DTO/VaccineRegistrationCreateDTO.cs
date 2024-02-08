using BlazorCARS.HealthShield.DataObject.DTO.Base;
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
    public class VaccineRegistrationCreateDTO : CreateDTO
    {
        public int RecipientId { get; set; }
        public int VaccineScheduleId { get; set; }
        public bool IsVaccinated { get; set; } = false;
        public string TimeSlot { get; set; }
        public string Dose { get; set; }
        public int DependantRecipientId { get; set; }
    }
}

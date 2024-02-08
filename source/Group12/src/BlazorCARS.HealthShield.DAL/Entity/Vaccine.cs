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
    public class Vaccine : BaseDomain
    {
        public int VaccineId { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string AgeGroup { get; set; }
    }
}

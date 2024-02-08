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
    public class VaccineRequest
    {
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string AgeGroup { get; set; }
    }
}

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
    public class InventoryRequest
    {
        public int HospitalId { get; set; }
        public int VaccineId { get; set; }
        public int ReorderLevel { get; set; }
    }
}

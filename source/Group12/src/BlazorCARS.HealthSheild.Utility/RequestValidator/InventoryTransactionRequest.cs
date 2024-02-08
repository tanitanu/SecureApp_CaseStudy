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
    public class InventoryTransactionRequest
    {
        public int HospitalId { get; set; }
        public int VaccineId { get; set; }
        public int ReceivedQty { get; set; }
        public string RefNo { get; set; }
        public DateTime ReceivedOn { get; set; }
    }
}

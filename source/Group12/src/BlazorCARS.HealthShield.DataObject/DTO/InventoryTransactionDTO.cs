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
    public class InventoryTransactionDTO
    {
        public int InventoryTransactionId { get; set; }
        public int HospitalId { get; set; }
        public int VaccineId { get; set; }
        public int ReceivedQty { get; set; }
        public string RefNo { get; set; }
        public DateTime ReceivedOn { get; set; }
    }
}

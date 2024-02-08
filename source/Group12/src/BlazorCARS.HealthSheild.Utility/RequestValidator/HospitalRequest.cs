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
    public class HospitalRequest
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string LicenseNo { get; set; }
        public string Address1 { get; set; }
        public string Landmark { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public int StateId { get; set; }
        public int CountryId { get; set; }
        public string PrimaryContact { get; set; }
        public string EmergencyContact { get; set; }
        public string EmailId { get; set; }
    }
}

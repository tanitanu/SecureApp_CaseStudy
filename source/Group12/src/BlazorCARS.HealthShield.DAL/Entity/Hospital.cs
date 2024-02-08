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
    public class Hospital : AddressBaseDomain
    {
        public int HospitalId { get; set; }
        public string Name { get; set; }
        public string LicenseNo { get; set; }
        public string Discrimination { get; set; }
        public string RegistrationStatus { get; set; }
    }
}

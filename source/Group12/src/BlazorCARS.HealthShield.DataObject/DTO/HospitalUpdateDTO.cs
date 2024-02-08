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
    public class HospitalUpdateDTO : AddressDTO
    {
        public string Name { get; set; }
        public string LicenseNo { get; set; }
        public string Discrimination { get; set; }
        public string RegistrationStatus { get; set; }
        public string UpdatedUser { get; set; }
    }
}

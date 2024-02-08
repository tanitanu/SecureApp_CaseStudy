using BlazorCARS.HealthShield.DataObject.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BlazorCARS.HealthShield.DataObject.DTO
{
    public class HospitalRegistryDTO : AddressDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string LicenseNo { get; set; }
        public string Discrimination { get; set; }
        public string RegistrationStatus { get; set; }
        public string CreatedUser { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
  Created by JAYaseelan
 */

namespace BlazorCARS.HealthShield.DataObject.DTO.Base
{
    public abstract class AddressDTO
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Landmark { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public int StateId { get; set; }
        public int CountryId { get; set; }
        public string PrimaryContact { get; set; }
        public string SecondaryContact { get; set; }
        public string EmergencyContact { get; set; }
        public string EmailId { get; set; }
    }
}

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
    public class RecipientCreateDTO : AddressDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AadhaarNo { get; set; }
        public DateTime DOB { get; set; }
        public string RelationshipType { get; set; }
        public int? DependentRecipientId { get; set; }
        public string Dose { get; set; }
        public string CreatedUser { get; set; }
    }
}

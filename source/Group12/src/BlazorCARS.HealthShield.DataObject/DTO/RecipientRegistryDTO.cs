using BlazorCARS.HealthShield.DataObject.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BlazorCARS.HealthShield.DataObject.DTO
{
    public class RecipientRegistryDTO : AddressDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AadhaarNo { get; set; }
        public DateTime DOB { get; set; }
        public int? DependentRecipientId { get; set; }
        public string CreatedUser { get; set; }
    }
}

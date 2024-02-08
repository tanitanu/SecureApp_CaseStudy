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
    public class Recipient : AddressBaseDomain
    {
        public int RecipientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AadhaarNo { get; set; }
        public DateTime DOB { get; set; }
        public string RelationshipType { get; set; }
        public int? DependentRecipientId { get; set; }
        public string Dose { get; set; }
    }
}

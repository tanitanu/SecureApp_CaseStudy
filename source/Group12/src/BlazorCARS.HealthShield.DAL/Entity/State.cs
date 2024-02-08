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
    public class State : BaseDomain
    {
        public int StateId { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
    }
}

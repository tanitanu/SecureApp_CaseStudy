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
    public abstract class BaseDomain
    {
        public string CreatedUser { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string UpdatedUser { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}

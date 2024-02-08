using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
  Created by JAYaseelan
 */

namespace BlazorCARS.HealthShield.DataObject.Enums
{
    public enum RegistrationStatus
    {
        [Description("Submitted")]
        Submitted,
        [Description("Approved")]
        Approved,
        [Description("Rejected")]
        Rejected
    }
}

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
    public enum UserRole
    {
        [Description("Super Admin")]
        SAdmin = 1,
        [Description("Admin")]
        Admin = 2,
        [Description("User")]
        User = 3
    }
}

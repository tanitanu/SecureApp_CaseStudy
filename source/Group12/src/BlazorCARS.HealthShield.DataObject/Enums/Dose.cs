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
    public enum Dose
    {
        [Description("Dose1")]
        Dose1,
        [Description("Dose2")]
        Dose2,
        [Description("Dose3")]
        Dose3,
        [Description("Booster1")]
        Booster1,
        [Description("Booster2")]
        Booster2,
        [Description("Booster3")]
        Booster3
    }
}

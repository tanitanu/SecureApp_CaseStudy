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
    public enum Relationship
    {
        [Description("Primary")]
        Primary,
        [Description("Grandmother")]
        Grandmother,
        [Description("Grandfather")]
        Grandfather,
        [Description("Mother")]
        Mother,
        [Description("Father")]
        Father,
        [Description("Mother-In-Law")]
        MotherInLaw,
        [Description("Father-In-Law")]
        FatherInLaw,
        [Description("Son")]
        Son,
        [Description("Daughter")]
        Daughter,
        [Description("Spouse")]
        Spouse,
        [Description("Hubby")]
        Habby,
        [Description("Sister")]
        Sister,
        [Description("Brother")]
        Brother
    }
}

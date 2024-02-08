using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DataApiModels
{
    public partial class Login
    {
        [Required]
        [DisplayName("Name")]
        public string? Name { get; set; }


        [DisplayName("Password")]
        public string? Password { get; set; }

    }
}

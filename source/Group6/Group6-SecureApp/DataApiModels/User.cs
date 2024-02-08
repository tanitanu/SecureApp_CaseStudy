using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DataApiModels
{

    public class User
    {

        [DisplayName("ID")]
        public string ID { get; set; }

        [DisplayName("Name")]
        public string? Name { get; set; }

        [DisplayName("Password")]
        public string? Password { get; set; }

    }
}

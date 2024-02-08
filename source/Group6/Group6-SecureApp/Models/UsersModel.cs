using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace DataApiModels
{
    [DataContract]
    public class UsersModel
    {
        [DataMember(Name = "ID")]
        public string ID { get; set; }

        [DataMember(Name = "Name")]
        public string? Name { get; set; }

        [DataMember(Name = "Password")]
        public string? Password { get; set; }

       
    }
}

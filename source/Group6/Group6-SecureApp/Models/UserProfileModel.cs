using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace DataApiModels
{
    [DataContract]
    public class UserProfileModels
    {
        [DataMember(Name = "UserId")]
        public string UserId { get; set; }

        [DataMember(Name = "DateofBirth")]
        public DateTime Dob { get; set; }

        [DataMember(Name = "Aadhar")]
        public long Adhar { get; set; }

        [DataMember(Name = "ID")]
        public string ID { get; set; }

        [DataMember(Name = "User")]
        public UsersModel? User { get; set; }


    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace DataApiModels
{
    public class UserProfile
    {
        [DisplayName("UserId")]
        public string UserId { get; set; }

        [DisplayName("Date of Birth")]
        public DateTime Dob { get; set; }

        [DisplayName("Adhar No.")]
        public long Adhar { get; set; }

        [DisplayName("ID")]
        public string ID { get; set; }

        [DisplayName("User")]
        public User? User { get; set; }
    }
}

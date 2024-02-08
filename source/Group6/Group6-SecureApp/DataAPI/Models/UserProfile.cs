using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAPI.Models;

[Table("UserProfile")]
public class UserProfile
{
    public string UserId { get; set; }

    [Required(ErrorMessage = "Date of Birth is required")]
    public DateTime Dob { get; set; }

    [Required(ErrorMessage = "Adhar is required")]
    public long Adhar { get; set; }

    [ForeignKey("FK_UserProfile_User_UserId")]
    public string ID { get; set; }

    public User? User { get; set; }
}


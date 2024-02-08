using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SecureApp.Models;

public partial class UserProfile
{
    public string Id { get; set; } = null!;

    //[Required]
    [PersonalData]
    [Display(Name = "User ID")]
    public string? UserId { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Date of Birth")]
    public DateTime? Dob { get; set; }

    [Display(Name = "Adhaar Number")]
    [Range(100000000000, 999999999999, ErrorMessage = "Enter Correct Aadhar number")]
    public long? Adhar { get; set; }

    public virtual User? User { get; set; }
}

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SecureApp.Models;

public partial class User
{

    [Required]
    [PersonalData]
    [Key]
    [Display(Name = "ID")]
    public string Id { get; set; } = null!;

    [Display(Name = "Name")]
    public string? Name { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string? Password { get; set; }

  

    public virtual ICollection<UserProfile> UserProfile { get; set; } = new List<UserProfile>();
}

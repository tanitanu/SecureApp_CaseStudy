using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DataAPI.Models;

[Table("User")]
public class User
{

    [Required]
    [PersonalData]
    [Key]
    [Display(Name = "ID")]
    public string ID { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [StringLength(60, ErrorMessage = "Name can't be longer than 60 characters")]
    public string? Name { get; set; }

    [Required]
    [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string? Password { get; set; }

    //public virtual ICollection<UserProfile> UserProfile { get; set; } = new List<UserProfile>();
}

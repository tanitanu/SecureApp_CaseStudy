#region [Using]
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TaskManagement.WebApp.CustomValidationAttribute;
#endregion [Using]

namespace TaskManagement.WebApp.Models
{
    #region [Summary]
    ///<author>sayyad, shaheena</author>
    ///<date>01-Nov-2023</date>
    ///<project>TaskManagement.WebApp</project>
    ///<class>Tasks</class>
    /// <summary>
    /// This is the domain model for Task
    /// </summary>
    #endregion [Summary]
    public class Tasks
    {
        #region [Properties]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        [MinLength(3, ErrorMessage = "Title must be atleast 3 characters.")]
        [Display(Name ="Title *")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Description is required.")]
        [MaxLength(100, ErrorMessage = "Description must be at most 100 characters.")]
        [Display(Name ="Description *")]
        public string Description { get; set; }

        [Required(ErrorMessage ="Due date is required")]
        [FutureDate(ErrorMessage = "Due Date must be a future date.")]
        [Display(Name = "Due Date *")]
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }

        [Required(ErrorMessage = "Assigned To is required.")]
        [Display(Name = "Assigned To *")]
        [EmailValidation(allowedDomain: "dxc.com", ErrorMessage = "Email Domain must be dxc.com")]
        public string AssignedTo { get; set; }

        
        public string? Comments { get; set; }

        [Display(Name ="Status *")]
        [Required(ErrorMessage = "Status is required.")]
        public int StatusId { get; set; }

        // Navigation property
        public Status? Status { get; set; }
        #endregion [Properties]

    }
}

#region [Using]
using System.ComponentModel.DataAnnotations;
using TaskManagement.Api.CustomValidationAttribute;
#endregion [Using]

namespace TaskManagement.Api.Models.DTO
{
    #region [Summary]
    ///<author>Poornima Shanbhag</author>
    ///<date>02-Nov-2023</date>
    ///<project>TaskManagement.Api</project>
    ///<class>AddTaskRequestDto</class>
    /// <summary>
    /// This is the DTO class to add new task
    ///  History
    ///     09-Nov-2023: sayyad, shaheena: Updated Validations
    ///     20-Nov-2023: Ranjna,Devi: Updated Date validations
    /// </summary>
    #endregion [Summary]
    public class AddTaskRequestDto
    {
        #region [Properties]

        [Required(ErrorMessage = "Assigned To is required.")]
        [Display(Name = "Assigned To")]
        [EmailValidation(allowedDomain: "dxc.com", ErrorMessage = "Email Domain must be dxc.com")]

        public string AssignedTo { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [MinLength(3, ErrorMessage = "Title must be atleast 3 characters.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Description is required.")]
        [MaxLength(100, ErrorMessage = "Description must be at most 100 characters.")]
        public string Description { get; set; }

        [FutureDate(ErrorMessage = "Due Date must be a future date.")]
        [Display(Name = "Due Date")]
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }

        public string? Comments { get; set; }
        [Required]
        [Range(1, 5)]
        public int StatusId { get; set; }
        #endregion [Properties]
    }
}

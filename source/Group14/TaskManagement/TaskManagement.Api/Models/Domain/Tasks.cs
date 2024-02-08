#region [Using]
using System.ComponentModel.DataAnnotations;
#endregion [Using]

namespace TaskManagement.Api.Models.Domain
{
    #region [Summary]
    ///<author>sayyad, shaheena</author>
    ///<date>01-Nov-2023</date>
    ///<project>TaskManagement.Api</project>
    ///<class>Tasks</class>
    /// <summary>
    /// This is the domain model for Status
    /// </summary>
    #endregion [Summary]
    public class Tasks
    {
        #region [Properties]
        [Key]
        public Guid Id { get; set; }
        public string AssignedTo { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public string? Comments { get; set; }
        public int StatusId { get; set; }

        // Navigation property
        public Status Status { get; set; }
        #endregion [Properties]

    }
}

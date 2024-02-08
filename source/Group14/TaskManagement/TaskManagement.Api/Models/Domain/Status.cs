#region [Using]
using System.ComponentModel.DataAnnotations;
#endregion [Using]

namespace TaskManagement.Api.Models.Domain
{
    #region [Summary]
    ///<author>sayyad, shaheena</author>
    ///<date>01-Nov-2023</date>
    ///<project>TaskManagement.Api</project>
    ///<class>Status</class>
    /// <summary>
    /// This is the domain model for Status
    /// </summary>
    #endregion [Summary]
    public class Status
    {
        #region [Properties]
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        #endregion [Properties]
    }
}

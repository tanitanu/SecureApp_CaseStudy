#region [Using]
using TaskManagement.Api.Models.Domain;
#endregion [Using]

namespace TaskManagement.Api.Repositories
{
    #region [Summary]
    ///<author>Poornima Shanbhag</author>
    ///<date>02-Nov-2023</date>
    ///<project>TaskManagement.Api</project>
    ///<class>ITaskRepository</class>
    /// <summary>
    /// This is the repository interface for task
    /// /// History
    ///     09-Nov-2023: sayyad, shaheena: Updated Filtering, Sort and Pagination
    ///     10-Nov-2023: Poornima Shanbhag: Added GetAllStatus action method
    /// </summary>
    #endregion [Summary]
    public interface ITaskRepository
    {
        #region [Interface Methods]
        Task<List<Tasks>> GetAllAsync
            (
            string? filterOn = null, 
            string? filterQuery = null, 
            string? sortBy = null,
            bool isAscending = true,
            int PageNumber=1,
            int PageSize=1000
            );
        Task<Tasks?> GetByIdAsync(Guid id);

        Task<Tasks> CreateAsync(Tasks task);

        Task<Tasks?> UpdateAsync(Guid id, Tasks task);

        Task<Tasks?> DeleteAsync(Guid id);

        Task<List<Status>> GetAllStatusAsync(string? filterOn = null,
            string? filterQuery = null);
        #endregion [Interface Methods]
    }
}

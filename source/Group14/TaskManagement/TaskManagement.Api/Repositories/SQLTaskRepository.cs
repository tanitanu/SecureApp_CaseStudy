#region [Using]
using Microsoft.EntityFrameworkCore;
using TaskManagement.Api.Data;
using TaskManagement.Api.Models.Domain;
#endregion [Using]

namespace TaskManagement.Api.Repositories
{
    #region [Summary]
    ///<author>Poornima Shanbhag</author>
    ///<date>02-Nov-2023</date>
    ///<project>TaskManagement.Api</project>
    ///<class>SQLTaskRepository</class>
    /// <summary>
    /// This is the SQL repository class for task
    /// /// History
    ///     09-Nov-2023: sayyad, shaheena: Updated Filtering, Sort and Pagination
    ///     10-Nov-2023: Poornima Shanbhag: Added GetAllStatus action method
    /// </summary>
    #endregion [Summary]
    public class SQLTaskRepository : ITaskRepository
    {
        #region [Private Variables]
        private readonly TaskDBContext dbContext;
        #endregion [Private Variables]

        #region [Constructor]
        public SQLTaskRepository(TaskDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        #endregion [Constructor]

        #region [public Methods]
        public async Task<List<Tasks>> GetAllAsync
            (
            string? filterOn = null, 
            string? filterQuery = null,
            string? sortBy = null,
            bool isAscending = true,
            int PageNumber=1,
            int PageSize=1000
            )
        {
            var tasks= dbContext.Tasks.Include("Status").AsQueryable();

            //Filtering

            if(string.IsNullOrWhiteSpace(filterOn)==false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("AssignedTo", StringComparison.OrdinalIgnoreCase))
                {
                    tasks=tasks.Where(x=>x.AssignedTo.Contains(filterQuery));
                }
            }

            //Sorting

            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("AssignedTo", StringComparison.OrdinalIgnoreCase))
                {
                    tasks = isAscending ? tasks.OrderBy(s => s.AssignedTo) : tasks.OrderByDescending(s => s.AssignedTo);
                }

            }

            //Pagination
            var skipResults=(PageNumber-1)*PageSize;

            return await tasks.Skip(skipResults).Take(PageSize).ToListAsync();
        }

        public async Task<Tasks?> GetByIdAsync(Guid id) => await dbContext.Tasks.Include("Status").FirstOrDefaultAsync(x => x.Id == id);

        public async Task<Tasks> CreateAsync(Tasks task)
        {
            await dbContext.AddAsync(task);
            await dbContext.SaveChangesAsync();
            return task;
        }

        public async Task<Tasks?> UpdateAsync(Guid id, Tasks task)
        {
            var existingTask = await dbContext.Tasks.FirstOrDefaultAsync(x => x.Id == id);

            if (existingTask == null) { return null; }

            existingTask.DueDate = task.DueDate;
            existingTask.Description = task.Description;
            existingTask.StatusId = task.StatusId;
            existingTask.AssignedTo = task.AssignedTo;
            existingTask.Comments = task.Comments;
            existingTask.Title = task.Title;

            await dbContext.SaveChangesAsync();
            return existingTask;

        }

        public async Task<Tasks?> DeleteAsync(Guid id)
        {
            var existingTask = await dbContext.Tasks.FirstOrDefaultAsync(x => x.Id == id);

            if (existingTask == null) { return null; }

            dbContext.Tasks.Remove(existingTask);
            await dbContext.SaveChangesAsync();
            return existingTask;
        }

        public async Task<List<Status>> GetAllStatusAsync(string? filterOn = null,
            string? filterQuery = null)
        {
            var status = this.dbContext.Status.AsQueryable();

            // Filtering
            if (! string.IsNullOrWhiteSpace(filterOn) && ! string.IsNullOrWhiteSpace(filterQuery))
            {
                status = (filterOn.Equals("Id", StringComparison.InvariantCultureIgnoreCase) ?
                status.Where(x => x.Id == Convert.ToInt32(filterQuery)) :
                (filterOn.Equals("Code", StringComparison.InvariantCultureIgnoreCase)?
                status.Where(x => x.Code.ToLower().Equals(filterQuery.ToLower())):
                null));
            }

            if (status == null ) return null;
            return await status.ToListAsync();
        }


        #endregion [public Methods]
    }
}

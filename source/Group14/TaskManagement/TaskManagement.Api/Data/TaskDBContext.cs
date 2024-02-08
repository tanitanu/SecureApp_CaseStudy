#region [Using]
using Microsoft.EntityFrameworkCore;
using TaskManagement.Api.Models.Domain;
#endregion [Using]

namespace TaskManagement.Api.Data
{
    #region [Summary]
    ///<author>sayyad, shaheena</author>
    ///<date>01-Nov-2023</date>
    ///<project>TaskManagement.Api</project>
    ///<class>TaskDBContext</class>
    /// <summary>
    /// This is the DB Context class for Task
    /// </summary>
    #endregion [Summary]
    public class TaskDBContext : DbContext
    {
        #region [Constructor]
        public TaskDBContext(DbContextOptions<TaskDBContext> options) : base(options)
        {

        }
        #endregion [Constructor]

        #region [Properties]
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<Status> Status { get; set; }
        #endregion [Properties]
       
        #region [protected methods]
        /// <summary>
        /// Seed the Status table data
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data for Status
            // NEW, IN-PROGRESS, DONE, ON-HOLD, CANCELLED

            var status = new List<Status>() {
                new Status()
                { Id= 1,
                Code ="NEW"},
                new Status()
                { Id= 2,
                Code ="IN-PROGRESS"},
                 new Status()
                { Id= 3,
                Code ="DONE"},
                  new Status()
                { Id= 4,
                Code ="ON-HOLD"},
                   new Status()
                { Id= 5,
                Code ="CANCELLED"}
            };
            modelBuilder.Entity<Status>().HasData(status);
        }
        #endregion [protected methods]
    }
}

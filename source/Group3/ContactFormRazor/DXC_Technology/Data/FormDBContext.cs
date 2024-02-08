using DXC_Technology.Model;
using Microsoft.EntityFrameworkCore;

namespace DXC_Technology.Data
{

    //Developed By Mounika and Harshitha
    public class FormDBContext : DbContext
    {
        public FormDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<FormViews> formView { get; set; }
    }
}

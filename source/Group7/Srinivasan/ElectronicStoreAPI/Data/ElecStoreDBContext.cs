using ElectronicStoreAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ElectronicStoreAPI.Data
{
    //by Srinivasan
    public class ElecStoreDBContext : DbContext 
    {
        public ElecStoreDBContext(DbContextOptions dbContextOption) : base(dbContextOption)
        {
            
        }
        public DbSet<Brand> BrandMstr { get; set; }
        public DbSet<Category> CategoryMstr { get; set; }
        public DbSet<Product> ProductMstr { get; set; }

    }
}

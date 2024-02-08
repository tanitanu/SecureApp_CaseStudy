using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace SecureApp.Model
{
    public class SecureAppContext : IdentityDbContext<IdentityUser>
    {
        private readonly DbContextOptions _options;
        public SecureAppContext()
        {
            
        }
        public SecureAppContext(DbContextOptions<SecureAppContext> options)
        : base(options)
        {
            _options = options;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.

        //DataSource and DataBase name varies device to device
        => optionsBuilder.UseSqlServer("Data Source=IN-PF2NA6C9;Database=SecureApplication;Trusted_Connection=True;Encrypt=False");
    }
}

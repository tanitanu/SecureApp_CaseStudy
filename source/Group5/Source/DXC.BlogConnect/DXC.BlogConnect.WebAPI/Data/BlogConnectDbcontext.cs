using DXC.BlogConnect.WebAPI.Data.FluentConfig;
using DXC.BlogConnect.WebAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;
/*
 * Created By: Kishore
 */
namespace DXC.BlogConnect.WebAPI.Data
{
    public class BlogConnectDbcontext : DbContext
    {
        public BlogConnectDbcontext(DbContextOptions<BlogConnectDbcontext> options) : base(options) { }
        #region DBSet
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Post> BlogPosts { get; set; }
        public DbSet<Likes> BlogPostLike { get; set; }
        public DbSet<Comments> BlogPostComment { get; set; }
        public DbSet<Tag> Tags { get; set; }
        #endregion
        #region On Model Creating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Register fluent configuration
            modelBuilder.ApplyConfiguration(new RoleFluentConfig());
            modelBuilder.ApplyConfiguration(new UserFluentConfig());

            #endregion
            #region Relationships
            modelBuilder.Entity<Role>()
            .HasMany<User>()
            .WithOne()
            .HasForeignKey(k=> k.RoleId)
            .IsRequired()
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_User_Role");
            #endregion

        }

        #endregion
    }
}

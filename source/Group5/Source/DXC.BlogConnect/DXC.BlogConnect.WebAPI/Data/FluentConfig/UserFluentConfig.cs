using DXC.BlogConnect.WebAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

/*
 * Created By: Kishore
 */

namespace DXC.BlogConnect.WebAPI.Data.FluentConfig
{
    public class UserFluentConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.ToTable("User");
            entity.HasKey(p => p.Id).HasName("UserId");
            entity.Property(x => x.UserName).IsRequired().HasMaxLength(50).IsUnicode(false);
            entity.Property(x => x.FirstName).IsRequired().HasMaxLength(50).IsUnicode(false);
            entity.Property(x => x.LastName).IsRequired().HasMaxLength(50).IsUnicode(false);
            entity.Property(x => x.EmailId).IsRequired().HasMaxLength(50).IsUnicode(false);
            entity.Property(x => x.Password).IsRequired().HasMaxLength(500).IsUnicode(false);
            entity.Property(x => x.RoleId).IsRequired();
            entity.Property(x => x.CreatedBy).IsRequired().HasMaxLength(50).IsUnicode(false);
            entity.Property(x => x.CreatedDate).IsRequired().HasColumnType("datetime");
            entity.Property(x => x.UpdatedBy).IsRequired().HasMaxLength(50).IsUnicode(false);
            entity.Property(x => x.UpdatedDate).IsRequired().HasColumnType("datetime");
            entity.Property(x => x.IsDeleted).HasDefaultValue(false);
            entity.HasIndex(i => new
            {
                i.UserName
            }).HasDatabaseName("UQ_User_Name").IsUnique();
                         
        }
    }
}

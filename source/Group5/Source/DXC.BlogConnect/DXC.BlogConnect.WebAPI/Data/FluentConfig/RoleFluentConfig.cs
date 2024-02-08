using DXC.BlogConnect.WebAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
/*
 * Created By: Kishore
 */
namespace DXC.BlogConnect.WebAPI.Data.FluentConfig
{
    public class RoleFluentConfig:IEntityTypeConfiguration<Role>
    {
      public void Configure(EntityTypeBuilder<Role> entity)
        {
            entity.ToTable("Role");
            entity.HasKey(p => p.Id).HasName("RoleId");
            entity.Property(x => x.Name).IsRequired().HasMaxLength(50).IsUnicode(false);
            entity.Property(x => x.CreatedBy).IsRequired().HasMaxLength(50).IsUnicode(false);
            entity.Property(x => x.CreatedDate).IsRequired().HasColumnType("datetime");
            entity.Property(x => x.UpdatedBy).IsRequired().HasMaxLength(50).IsUnicode(false);
            entity.Property(x => x.UpdatedDate).IsRequired().HasColumnType("datetime");
            entity.Property(x => x.IsDeleted).HasDefaultValue(false);
            entity.HasIndex(i => new
            {
                i.Name
            }).HasDatabaseName("UQ_Role_Name").IsUnique();
        }
    }
}

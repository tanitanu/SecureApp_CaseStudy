using BlazorCARS.HealthShield.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
  Created by JAYaseelan
 */
namespace BlazorCARS.HealthShield.DAL.Data.FluentConfig
{
    public class UserRoleFulentConfig : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UserRole");
            builder.HasKey(k => k.UserRoleId).HasName("PK_RoleId");
            builder.Property(k => k.UserRoleId).HasColumnOrder(1);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(50).IsUnicode(false).HasColumnOrder(2);

            builder.Property(x => x.CreatedUser).IsRequired().HasMaxLength(50).IsUnicode(false).HasColumnOrder(3);
            builder.Property(x => x.CreatedDateTime).IsRequired().HasColumnType("datetime").HasColumnOrder(4);
            builder.Property(x => x.UpdatedUser).IsRequired().HasMaxLength(50).IsUnicode(false).HasColumnOrder(5);
            builder.Property(x => x.UpdatedDateTime).IsRequired().HasColumnType("datetime").HasColumnOrder(6);
            builder.Property(x => x.IsDeleted).HasDefaultValue(false).HasColumnOrder(7);

            //Unique key column(s)
            builder.HasIndex(i => new
            {
                i.Name
            }).HasDatabaseName("UQ_UserRole_Name").IsUnique();
        }
    }
}

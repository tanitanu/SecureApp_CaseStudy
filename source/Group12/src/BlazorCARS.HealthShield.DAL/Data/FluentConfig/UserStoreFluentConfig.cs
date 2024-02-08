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
    public class UserStoreFluentConfig : IEntityTypeConfiguration<UserStore>
    {
        public void Configure(EntityTypeBuilder<UserStore> builder)
        {
            builder.ToTable("UserStore");
            builder.HasKey(k => k.UserStoreId).HasName("PK_UserStorreId");
            builder.Property(k => k.UserStoreId).HasColumnOrder(1);
            builder.Property(p => p.UserName).IsRequired().HasMaxLength(50).IsUnicode(false).HasColumnOrder(2);
            builder.Property(p => p.Password).IsRequired().HasMaxLength(50).IsUnicode(false).HasColumnOrder(3);
            builder.Property(p => p.DiscriminationId).IsRequired(false).HasColumnOrder(4);

            builder.Property(x => x.CreatedUser).IsRequired().HasMaxLength(50).IsUnicode(false).HasColumnOrder(5);
            builder.Property(x => x.CreatedDateTime).IsRequired().HasColumnType("datetime").HasColumnOrder(6);
            builder.Property(x => x.UpdatedUser).IsRequired().HasMaxLength(50).IsUnicode(false).HasColumnOrder(7);
            builder.Property(x => x.UpdatedDateTime).IsRequired().HasColumnType("datetime").HasColumnOrder(8);
            builder.Property(x => x.IsDeleted).HasDefaultValue(false).HasColumnOrder(9);

            //Unique key column(s)
            builder.HasIndex(i => new
            {
                i.UserName
            }).HasDatabaseName("UQ_UserStore_UserName").IsUnique();
        }
    }
}

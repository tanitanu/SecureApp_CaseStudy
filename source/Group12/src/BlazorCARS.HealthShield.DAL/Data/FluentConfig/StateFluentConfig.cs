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
    public class StateFluentConfig : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> builder)
        {
            builder.ToTable("State");
            builder.HasKey(k => k.StateId).HasName("PK_StateId");
            builder.Property(k => k.StateId).HasColumnOrder(1);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(50).IsUnicode(false).HasColumnOrder(2);
            builder.Property(p => p.CountryId).IsRequired().HasColumnOrder(3);

            builder.Property(x => x.CreatedUser).IsRequired().HasMaxLength(50).IsUnicode(false).HasColumnOrder(4);
            builder.Property(x => x.CreatedDateTime).IsRequired().HasColumnType("datetime").HasColumnOrder(5);
            builder.Property(x => x.UpdatedUser).IsRequired().HasMaxLength(50).IsUnicode(false).HasColumnOrder(6);
            builder.Property(x => x.UpdatedDateTime).IsRequired().HasColumnType("datetime").HasColumnOrder(7);
            builder.Property(x => x.IsDeleted).HasDefaultValue(false).HasColumnOrder(8);

            //Unique key column(s)
            builder.HasIndex(i => new
            {
                i.Name
            }).HasDatabaseName("UQ_State_Name").IsUnique();
        }
    }
}

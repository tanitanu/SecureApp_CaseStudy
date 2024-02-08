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
    public class HospitalFluentConfig : IEntityTypeConfiguration<Hospital>
    {
        public void Configure(EntityTypeBuilder<Hospital> builder)
        {
            builder.ToTable("Hospital");
            builder.HasKey(k => k.HospitalId).HasName("PK_HospitalId");
            builder.Property(p => p.HospitalId).HasColumnOrder(1);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100).IsUnicode(false).HasColumnOrder(2);
            builder.Property(p => p.Address1).IsRequired().HasMaxLength(50).IsUnicode(false).HasColumnOrder(3);
            builder.Property(p => p.Address2).IsRequired(false).HasMaxLength(50).IsUnicode(false).HasColumnOrder(4);
            builder.Property(p => p.Landmark).IsRequired(false).HasMaxLength(100).IsUnicode(false).HasColumnOrder(5);
            builder.Property(p => p.City).IsRequired().HasMaxLength(50).IsUnicode(false).HasColumnOrder(6);
            builder.Property(p => p.StateId).IsRequired().HasColumnOrder(7);
            builder.Property(p => p.CountryId).IsRequired().HasColumnOrder(8);
            builder.Property(p => p.PostalCode).IsRequired().HasMaxLength(10).IsUnicode(false).HasColumnOrder(9);
            builder.Property(p => p.PrimaryContact).IsRequired().HasMaxLength(16).IsUnicode(false).HasColumnOrder(10);
            builder.Property(p => p.SecondaryContact).IsRequired(false).HasMaxLength(16).IsUnicode(false).HasColumnOrder(11);
            builder.Property(p => p.EmergencyContact).IsRequired().HasMaxLength(16).IsUnicode(false).HasColumnOrder(12);
            builder.Property(p => p.EmailId).IsRequired().HasMaxLength(100).IsUnicode(false).HasColumnOrder(13);
            builder.Property(p => p.Discrimination).IsRequired(false).HasMaxLength(16).IsUnicode(false).HasColumnOrder(14);
            builder.Property(p => p.RegistrationStatus).IsRequired().HasMaxLength(16).IsUnicode(false).HasColumnOrder(15);

            builder.Property(x => x.CreatedUser).IsRequired().HasMaxLength(50).IsUnicode(false).HasColumnOrder(16);
            builder.Property(x => x.CreatedDateTime).IsRequired().HasColumnType("datetime").HasColumnOrder(17);
            builder.Property(x => x.UpdatedUser).IsRequired().HasMaxLength(50).IsUnicode(false).HasColumnOrder(18);
            builder.Property(x => x.UpdatedDateTime).IsRequired().HasColumnType("datetime").HasColumnOrder(19);
            builder.Property(x => x.IsDeleted).HasDefaultValue(false).HasColumnOrder(20);

            //Unique key column(s)
            builder.HasIndex(i => new
            {
                i.Name
            }).HasDatabaseName("UQ_Inventory_Name").IsUnique();
        }
    }
}

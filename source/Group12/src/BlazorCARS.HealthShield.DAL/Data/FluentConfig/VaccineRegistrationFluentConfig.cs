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
    public class VaccineRegistrationFluentConfig : IEntityTypeConfiguration<VaccineRegistration>
    {
        public void Configure(EntityTypeBuilder<VaccineRegistration> builder)
        {
            builder.ToTable("VaccineRegistration");
            builder.HasKey(k => k.VaccineRegistrationId).HasName("PK_VaccineRegistrationId");
            builder.Property(k => k.VaccineRegistrationId).HasColumnOrder(1);
            builder.Property(p => p.RecipientId).IsRequired().HasColumnOrder(2);
            builder.Property(p => p.VaccineScheduleId).IsRequired().HasColumnOrder(3);
            builder.Property(p => p.IsVaccinated).HasDefaultValue(false).HasColumnOrder(4);
            builder.Property(p => p.TimeSlot).IsRequired().HasMaxLength(16).IsUnicode(false).HasColumnOrder(5);
            builder.Property(p => p.Dose).IsRequired().HasMaxLength(16).IsUnicode(false).HasColumnOrder(6);
            builder.Property(p => p.DependantRecipientId).HasColumnOrder(12);

            builder.Property(x => x.CreatedUser).IsRequired().HasMaxLength(50).IsUnicode(false).HasColumnOrder(7);
            builder.Property(x => x.CreatedDateTime).IsRequired().HasColumnType("datetime").HasColumnOrder(8);
            builder.Property(x => x.UpdatedUser).IsRequired().HasMaxLength(50).IsUnicode(false).HasColumnOrder(9);
            builder.Property(x => x.UpdatedDateTime).IsRequired().HasColumnType("datetime").HasColumnOrder(10);
            builder.Property(x => x.IsDeleted).HasDefaultValue(false).HasColumnOrder(11);

            //Unique key column(s)
            builder.HasIndex(i => new
            {
                i.RecipientId,
                i.Dose
            }).HasDatabaseName("UQ_Inventory_RecipientId_Dose").IsUnique();
        }
    }
}

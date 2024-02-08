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
    public class VaccineScheduleFluentConfig : IEntityTypeConfiguration<VaccineSchedule>
    {
        public void Configure(EntityTypeBuilder<VaccineSchedule> builder)
        {
            builder.ToTable("VaccineSchedule");
            builder.HasKey(k => k.VaccineScheduleId).HasName("PK_VaccineScheduleId");
            builder.Property(k => k.VaccineScheduleId).HasColumnOrder(1);
            builder.Property(p => p.HospitalId).IsRequired().HasColumnOrder(2);
            builder.Property(p => p.VaccineId).IsRequired().HasColumnOrder(3);
            builder.Property(p => p.ScheduleDate).IsRequired().HasColumnType("datetime").HasColumnOrder(4);
            builder.Property(p => p.TimeSlot).IsRequired().HasMaxLength(16).IsUnicode(false).HasColumnOrder(5);

            builder.Property(x => x.CreatedUser).IsRequired().HasMaxLength(50).IsUnicode(false).HasColumnOrder(6);
            builder.Property(x => x.CreatedDateTime).IsRequired().HasColumnType("datetime").HasColumnOrder(7);
            builder.Property(x => x.UpdatedUser).IsRequired().HasMaxLength(50).IsUnicode(false).HasColumnOrder(8);
            builder.Property(x => x.UpdatedDateTime).IsRequired().HasColumnType("datetime").HasColumnOrder(9);
            builder.Property(x => x.IsDeleted).HasDefaultValue(false).HasColumnOrder(10);

            //Unique key column(s)
            builder.HasIndex(i => new
            {
                i.HospitalId,
                i.VaccineId,
                i.ScheduleDate,
                i.TimeSlot
            }).HasDatabaseName("UQ_Inventory_HospitalId_VaccineId_ScheduleDate_TimeSlot").IsUnique();
        }
    }
}

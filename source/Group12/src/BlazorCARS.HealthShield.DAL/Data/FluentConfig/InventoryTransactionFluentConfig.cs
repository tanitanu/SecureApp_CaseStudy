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
    public class InventoryTransactionFluentConfig : IEntityTypeConfiguration<InventoryTransaction>
    {
        public void Configure(EntityTypeBuilder<InventoryTransaction> builder)
        {
            builder.ToTable("InventoryTransaction");
            builder.HasKey(k => k.InventoryTransactionId).HasName("PK_InventoryTransactionId");
            builder.Property(k => k.InventoryTransactionId).HasColumnOrder(1);
            builder.Property(p => p.HospitalId).IsRequired().HasColumnOrder(2);
            builder.Property(p => p.VaccineId).IsRequired().HasColumnOrder(3);
            builder.Property(p => p.ReceivedQty).IsRequired().HasColumnOrder(5);
            builder.Property(p => p.ReceivedOn).IsRequired().HasColumnType("datetime").HasColumnOrder(6);
            builder.Property(p => p.RefNo).IsRequired(false).HasMaxLength(50).IsUnicode(false).HasColumnOrder(7);

            builder.Property(x => x.CreatedUser).IsRequired().HasMaxLength(50).IsUnicode(false).HasColumnOrder(8);
            builder.Property(x => x.CreatedDateTime).IsRequired().HasColumnType("datetime").HasColumnOrder(9);
            builder.Property(x => x.UpdatedUser).IsRequired().HasMaxLength(50).IsUnicode(false).HasColumnOrder(10);
            builder.Property(x => x.UpdatedDateTime).IsRequired().HasColumnType("datetime").HasColumnOrder(11);
            builder.Property(x => x.IsDeleted).HasDefaultValue(false).HasColumnOrder(12);
        }
    }
}

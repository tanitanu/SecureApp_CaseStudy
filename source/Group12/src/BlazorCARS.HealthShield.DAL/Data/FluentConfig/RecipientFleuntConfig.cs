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
    public class RecipientFleuntConfig : IEntityTypeConfiguration<Recipient>
    {
        public void Configure(EntityTypeBuilder<Recipient> builder)
        {
            builder.ToTable("Recipient");
            builder.HasKey(k => k.RecipientId).HasName("PK_RecipientId");
            builder.Property(k => k.RecipientId).HasColumnOrder(1);
            builder.Property(p => p.FirstName).IsRequired().HasMaxLength(50).IsUnicode(false).HasColumnOrder(2);
            builder.Property(p => p.LastName).IsRequired().HasMaxLength(50).IsUnicode(false).HasColumnOrder(3);
            builder.Property(p => p.AadhaarNo).IsRequired().HasMaxLength(16).IsUnicode(false).HasColumnOrder(4);
            builder.Property(x => x.DOB).IsRequired().HasColumnType("datetime").HasColumnOrder(5);
            builder.Property(p => p.Address1).IsRequired(false).HasMaxLength(50).IsUnicode(false).HasColumnOrder(6);
            builder.Property(p => p.Address2).IsRequired(false).HasMaxLength(50).IsUnicode(false).HasColumnOrder(7);
            builder.Property(p => p.Landmark).IsRequired(false).HasMaxLength(100).IsUnicode(false).HasColumnOrder(8);
            builder.Property(p => p.City).IsRequired(false).HasMaxLength(50).IsUnicode(false).HasColumnOrder(9);
            builder.Property(p => p.StateId).IsRequired().HasColumnOrder(10);
            builder.Property(p => p.CountryId).IsRequired().HasColumnOrder(11);
            builder.Property(p => p.PostalCode).IsRequired(false).HasMaxLength(10).IsUnicode(false).HasColumnOrder(12);
            builder.Property(p => p.PrimaryContact).IsRequired().HasMaxLength(16).IsUnicode(false).HasColumnOrder(13);
            builder.Property(p => p.SecondaryContact).IsRequired(false).HasMaxLength(16).IsUnicode(false).HasColumnOrder(14);
            builder.Property(p => p.EmergencyContact).IsRequired(false).HasMaxLength(16).IsUnicode(false).HasColumnOrder(15);
            builder.Property(p => p.EmailId).IsRequired(false).HasMaxLength(100).IsUnicode(false).HasColumnOrder(16);
            builder.Property(p => p.RelationshipType).IsRequired(false).HasMaxLength(50).IsUnicode(false).HasColumnOrder(17);
            builder.Property(p => p.DependentRecipientId).IsRequired(false).HasColumnOrder(18);
            builder.Property(p => p.Dose).IsRequired(false).HasMaxLength(16).IsUnicode(false).HasColumnOrder(19);

            builder.Property(x => x.CreatedUser).IsRequired().HasMaxLength(50).IsUnicode(false).HasColumnOrder(20);
            builder.Property(x => x.CreatedDateTime).IsRequired().HasColumnType("datetime").HasColumnOrder(21);
            builder.Property(x => x.UpdatedUser).IsRequired().HasMaxLength(50).IsUnicode(false).HasColumnOrder(22);
            builder.Property(x => x.UpdatedDateTime).IsRequired().HasColumnType("datetime").HasColumnOrder(23);
            builder.Property(x => x.IsDeleted).HasDefaultValue(false).HasColumnOrder(24);

            //Unique key column(s)
            builder.HasIndex(i => new
            {
                i.AadhaarNo
            }).HasDatabaseName("UQ_Inventory_AadhaarNo").IsUnique();
        }
    }
}

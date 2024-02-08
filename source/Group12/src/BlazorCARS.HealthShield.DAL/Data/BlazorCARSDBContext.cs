using BlazorCARS.HealthShield.DAL.Data.FluentConfig;
using BlazorCARS.HealthShield.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
  Created by JAYaseelan
 */
namespace BlazorCARS.HealthShield.DAL.Data
{
    public class BlazorCARSDBContext : DbContext
    {
        public BlazorCARSDBContext(DbContextOptions<BlazorCARSDBContext> options) : base(options) 
        {
            
        }

        #region
        public DbSet<UserRole> Role { get; set; }
        public DbSet<UserStore> UserStore { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<State> State { get; set; }
        public DbSet<Vaccine> Vaccine { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<InventoryTransaction> InventoryTransaction { get; set; }
        public DbSet<VaccineSchedule> VaccineSchedule { get; set; }
        public DbSet<VaccineRegistration> VaccineRegistration { get; set; }
        public DbSet<Hospital> Hospital { get; set; }
        public DbSet<Recipient> Recipient { get; set; }
        #endregion

        #region Other configurations
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Register fluent configuration
            modelBuilder.ApplyConfiguration(new UserRoleFulentConfig());
            modelBuilder.ApplyConfiguration(new UserStoreFluentConfig());
            modelBuilder.ApplyConfiguration(new CountryFluentConfig());
            modelBuilder.ApplyConfiguration(new StateFluentConfig());
            modelBuilder.ApplyConfiguration(new VaccineFluentConfig());
            modelBuilder.ApplyConfiguration(new InventoryFluentConfig());
            modelBuilder.ApplyConfiguration(new InventoryTransactionFluentConfig());
            modelBuilder.ApplyConfiguration(new VaccineScheduleFluentConfig());
            modelBuilder.ApplyConfiguration(new VaccineRegistrationFluentConfig());
            modelBuilder.ApplyConfiguration(new HospitalFluentConfig());
            modelBuilder.ApplyConfiguration(new RecipientFleuntConfig());
            #endregion

            #region Relationships
            //UserRole - UserStore one to many relationship
            modelBuilder.Entity<UserRole>()
                .HasMany<UserStore>()
                .WithOne()
                .HasForeignKey(fk => fk.UserRoleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRole_UserStore_UserRoleId");
            //Country - State one to many relationship
            modelBuilder.Entity<Country>()
                .HasMany<State>()
                .WithOne()
                .HasForeignKey(fk => fk.CountryId)
                .IsRequired()
                .OnDelete(deleteBehavior: DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Country_State_CountryId");
            //Vaccine - Inventory one to many relationship
            modelBuilder.Entity<Vaccine>()
                .HasMany<Inventory>()
                .WithOne()
                .HasForeignKey(fk => fk.VaccineId)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vaccine_Inventory_VaccineId");
            //Vaccine - InventoryTransation one to many relationship
            modelBuilder.Entity<Vaccine>()
                .HasMany<InventoryTransaction>()
                .WithOne()
                .HasForeignKey(fk => fk.VaccineId)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vaccine_InventoryTransaction_VaccineId");
            //Vaccine - InventorySchedule one to many relationship
            modelBuilder.Entity<Vaccine>()
                .HasMany<VaccineSchedule>()
                .WithOne()
                .HasForeignKey(fk => fk.VaccineId)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vaccine_VaccineSchedule_VaccineId");
            //Vaccine - VaccineSchedule one to many relationship
            modelBuilder.Entity<Vaccine>()
                .HasMany<VaccineSchedule>()
                .WithOne()
                .HasForeignKey(fk => fk.VaccineId)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vaccine_VaccineSchedule_VaccineId");
            //VaccineSchedule - VaccineRegistration one to many relationship
            modelBuilder.Entity<VaccineSchedule>()
                .HasMany<VaccineRegistration>()
                .WithOne()
                .HasForeignKey(fk => fk.VaccineScheduleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VaccineSchedule_VaccineRegistration_VaccineScheduleId");
            //Hospital - UserStore one to many relationship
            //modelBuilder.Entity<Hospital>()
            //    .HasMany<UserStore>()
            //    .WithOne()
            //    .HasForeignKey(fk => fk.DiscriminationId)
            //    .IsRequired()
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_Hospital_UserStore_DiscriminationId");
            //Hospital - Inventory one to many relationship
            modelBuilder.Entity<Hospital>()
                .HasMany<Inventory>()
                .WithOne()
                .HasForeignKey(fk => fk.HospitalId)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Hospital_Inventory_HospitalId");
            modelBuilder.Entity<Hospital>()
                .HasMany<InventoryTransaction>()
                .WithOne()
                .HasForeignKey(fk => fk.HospitalId)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Hospital_InventoryTransaction_HospitalId");
            //Hospital - VaccineSchedule one to many relationship
            modelBuilder.Entity<Hospital>()
                .HasMany<VaccineSchedule>()
                .WithOne()
                .HasForeignKey(fk => fk.HospitalId)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Hospital_VaccineSchedule_HospitalId");
            //Country - Hospital one to many relationship
            modelBuilder.Entity<Country>()
                .HasMany<Hospital>()
                .WithOne()
                .HasForeignKey(fk => fk.CountryId)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Country_Hospital_CountryId");
            //State - Hospital one to many relationship
            modelBuilder.Entity<State>()
                .HasMany<Hospital>()
                .WithOne()
                .HasForeignKey(fk => fk.StateId)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_State_Hospital_StateId");
            //Recipient - UserStore one to many relationship
            //modelBuilder.Entity<Recipient>()
            //    .HasMany<UserStore>()
            //    .WithOne()
            //    .HasForeignKey(fk => fk.DiscriminationId)
            //    .IsRequired()
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_Recipient_UserStore_DiscriminationId");
            //Recipient - VaccineREgistration one to many relationship
            modelBuilder.Entity<Recipient>()
                .HasMany<VaccineRegistration>()
                .WithOne()
                .HasForeignKey(fk => fk.RecipientId)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Recipient_VaccineRegistration_HospitalId");
            //Country - Recipient one to many relationship
            modelBuilder.Entity<Country>()
                .HasMany<Recipient>()
                .WithOne()
                .HasForeignKey(fk => fk.CountryId)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Country_Recipient_CountryId");
            //State - Recipient one to many relationship
            modelBuilder.Entity<State>()
                .HasMany<Recipient>()
                .WithOne()
                .HasForeignKey(fk => fk.StateId)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_State_Recipient_StateId");
            //Recipient self join TODO

            #endregion

            #region Seed Data
            modelBuilder.Entity<Country>().HasData(
                new Country { CountryId = 1, Name = "India", CreatedUser = "Seeded", CreatedDateTime = new DateTime(2023, 10, 1), UpdatedUser = "Seeded", UpdatedDateTime = new DateTime(2023, 10, 1) }
                );
            //Role
            modelBuilder.Entity<UserRole>().HasData(
                new UserRole { UserRoleId = 1, Name = "Super Admin", CreatedUser = "Seeded", CreatedDateTime = new DateTime(2023, 10, 1), UpdatedUser = "Seeded", UpdatedDateTime = new DateTime(2023, 10, 1) },
                new UserRole { UserRoleId = 2, Name = "Admin", CreatedUser = "Seeded", CreatedDateTime = new DateTime(2023, 10, 1), UpdatedUser = "Seeded", UpdatedDateTime = new DateTime(2023, 10, 1) },
                new UserRole { UserRoleId = 3, Name = "User", CreatedUser = "Seeded", CreatedDateTime = new DateTime(2023, 10, 1), UpdatedUser = "Seeded", UpdatedDateTime = new DateTime(2023, 10, 1) }
            );
            #endregion
        }
        #endregion
    }
}

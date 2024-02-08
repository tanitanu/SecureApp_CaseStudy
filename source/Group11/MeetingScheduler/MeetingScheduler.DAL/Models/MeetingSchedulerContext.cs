using System;
using System.Collections.Generic;
using MeetingScheduler.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeetingScheduler.DAL.Models;

public partial class MeetingSchedulerContext : DbContext
{
    public MeetingSchedulerContext()
    {
    }

    public MeetingSchedulerContext(DbContextOptions<MeetingSchedulerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Meeting> Meetings { get; set; }

    public virtual DbSet<Resource> Resources { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost\\SQLExpress;Database=MeetingScheduler;Trusted_Connection=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Meeting>(entity =>
        {
            entity.ToTable("Meeting");

            entity.Property(e => e.MeetingId).ValueGeneratedNever();
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.LastUpdtId)
                .HasMaxLength(50)
                .HasColumnName("Last_Updt_Id");
            entity.Property(e => e.LastUpdtTs)
                .HasColumnType("datetime")
                .HasColumnName("Last_Updt_Ts");
            entity.Property(e => e.Location)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RecurrenceException).HasMaxLength(50);
            entity.Property(e => e.RecurrenceId).HasColumnName("RecurrenceID");
            entity.Property(e => e.RecurrenceRule)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.StartTime).HasColumnType("datetime");
            entity.Property(e => e.Subject).HasMaxLength(50);
        });

        modelBuilder.Entity<Resource>(entity =>
        {
            entity.Property(e => e.ResourceId)
                .ValueGeneratedNever()
                .HasColumnName("Resource_Id");
            entity.Property(e => e.LastUpdtId)
                .HasMaxLength(50)
                .HasColumnName("Last_Updt_Id");
            entity.Property(e => e.LastUpdtTs)
                .HasColumnType("datetime")
                .HasColumnName("Last_Updt_Ts");
            entity.Property(e => e.ResourceEmailId)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.RoleId)
                .ValueGeneratedNever()
                .HasColumnName("Role_Id");
            entity.Property(e => e.LastUpdtTs)
                .HasColumnType("datetime")
                .HasColumnName("Last_Updt_Ts");
            entity.Property(e => e.LastUpdtUserId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Last_Updt_User_id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("Role_Name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.LastUpdtTs)
                .HasColumnType("datetime")
                .HasColumnName("Last_Updt_Ts");
            entity.Property(e => e.LastUpdtUserId)
                .HasMaxLength(50)
                .HasColumnName("Last_Updt_User_id");
            entity.Property(e => e.RoleId).HasColumnName("Role_Id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

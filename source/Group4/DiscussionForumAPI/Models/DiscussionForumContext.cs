using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DiscussionForumAPI.Models;

public partial class DiscussionForumContext : DbContext
{
    public DiscussionForumContext()
    {
    }

    public DiscussionForumContext(DbContextOptions<DiscussionForumContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<DiscussionForumAnswer> DiscussionForumAnswers { get; set; }

    public virtual DbSet<DiscussionForumCategory> DiscussionForumCategories { get; set; }

    public virtual DbSet<DiscussionForumLikesDislike> DiscussionForumLikesDislikes { get; set; }

    public virtual DbSet<DiscussionForumQuestion> DiscussionForumQuestions { get; set; }

    public virtual DbSet<DiscussionForumUserReported> DiscussionForumUserReporteds { get; set; }

    public virtual DbSet<DisussionForumUserTwoFactorCode> DisussionForumUserTwoFactorCodes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=DiscussionForum;Trusted_Connection=True;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<DiscussionForumAnswer>(entity =>
        {
            entity.HasKey(e => e.AnswerId);

            entity.ToTable("DiscussionForum.Answers", tb =>
                {
                    tb.HasTrigger("trg_answer_created_date");
                    tb.HasTrigger("trg_answer_modified_date");
                });

            entity.Property(e => e.CreatedBy).HasMaxLength(450);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");
            entity.Property(e => e.QuestionId).HasMaxLength(450);
            entity.Property(e => e.UpdatedBy).HasMaxLength(450);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.DiscussionForumAnswerCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_DiscussionForum.Answers_DiscussionForum.AspNetUsers1");

            entity.HasOne(d => d.Question).WithMany(p => p.DiscussionForumAnswers)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DiscussionForum.Answers_DiscussionForum.Questions");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.DiscussionForumAnswerUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("FK_DiscussionForum.Answers_AspNetUsers");
        });

        modelBuilder.Entity<DiscussionForumCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId);

            entity.ToTable("DiscussionForum.Categories");

            entity.Property(e => e.Category).HasMaxLength(250);
            entity.Property(e => e.CreatedBy).HasMaxLength(450);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.UpdatedBy).HasMaxLength(450);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.DiscussionForumCategoryCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_DiscussionForum.Categories_AspNetUsers");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.DiscussionForumCategoryUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("FK_DiscussionForum.Categories_AspNetUsers1");
        });

        modelBuilder.Entity<DiscussionForumLikesDislike>(entity =>
        {
            entity.HasKey(e => e.LikeDislikeId);

            entity.ToTable("DiscussionForum.LikesDislikes", tb =>
                {
                    tb.HasTrigger("trg_likedislike_created_date");
                    tb.HasTrigger("trg_likedislike_modified_date");
                });

            entity.Property(e => e.CreatedBy).HasMaxLength(450);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.QuestionId).HasMaxLength(450);
            entity.Property(e => e.UpdatedBy).HasMaxLength(450);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.DiscussionForumLikesDislikeCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_DiscussionForum.LikesDislikes_AspNetUsers");

            entity.HasOne(d => d.Question).WithMany(p => p.DiscussionForumLikesDislikes)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DiscussionForum.LikesDislikes_DiscussionForum.Questions");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.DiscussionForumLikesDislikeUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("FK_DiscussionForum.LikesDislikes_AspNetUsers1");
        });

        modelBuilder.Entity<DiscussionForumQuestion>(entity =>
        {
            entity.HasKey(e => e.QuestionId).HasName("PK_DiscussionForum.Question");

            entity.ToTable("DiscussionForum.Questions", tb =>
                {
                    tb.HasTrigger("trg_created_date");
                    tb.HasTrigger("trg_modified_date");
                });

            entity.Property(e => e.CategoryId).HasMaxLength(450);
            entity.Property(e => e.CreatedBy).HasMaxLength(450);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.Title).HasMaxLength(100);
            entity.Property(e => e.UpdatedBy).HasMaxLength(450);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<DiscussionForumUserReported>(entity =>
        {
            entity.HasKey(e => e.ReportId);

            entity.ToTable("DiscussionForum.UserReported", tb =>
                {
                    tb.HasTrigger("trg_report_created_date");
                    tb.HasTrigger("trg_report_modified_date");
                });

            entity.Property(e => e.CreatedBy).HasMaxLength(450);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");
            entity.Property(e => e.QuestionId).HasMaxLength(450);
            entity.Property(e => e.ReporterUserId).HasMaxLength(450);
            entity.Property(e => e.RespondentUserId).HasMaxLength(450);
            entity.Property(e => e.UpdatedBy).HasMaxLength(450);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.DiscussionForumUserReportedCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_DiscussionForum.UserReported_AspNetUsers2");

            entity.HasOne(d => d.Question).WithMany(p => p.DiscussionForumUserReporteds)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DiscussionForum.UserReported_DiscussionForum.Questions");

            entity.HasOne(d => d.ReporterUser).WithMany(p => p.DiscussionForumUserReportedReporterUsers)
                .HasForeignKey(d => d.ReporterUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DiscussionForum.UserReported_AspNetUsers");

            entity.HasOne(d => d.RespondentUser).WithMany(p => p.DiscussionForumUserReportedRespondentUsers)
                .HasForeignKey(d => d.RespondentUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DiscussionForum.UserReported_AspNetUsers1");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.DiscussionForumUserReportedUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("FK_DiscussionForum.UserReported_AspNetUsers3");
        });

        modelBuilder.Entity<DisussionForumUserTwoFactorCode>(entity =>
        {
            entity.HasKey(e => e.UserTwoFactorCodeId);

            entity.ToTable("DisussionForum.UserTwoFactorCode", tb => tb.HasTrigger("trg_code_created_date"));

            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.UserId).HasMaxLength(450);

            entity.HasOne(d => d.User).WithMany(p => p.DisussionForumUserTwoFactorCodes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DisussionForum.UserTwoFactorCode_AspNetUsers");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

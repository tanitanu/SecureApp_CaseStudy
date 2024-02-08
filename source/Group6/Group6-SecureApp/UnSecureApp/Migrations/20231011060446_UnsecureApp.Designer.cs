﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnSecureApp.Models;

#nullable disable

namespace UnSecureApp.Migrations
{
    [DbContext(typeof(SecureAppContext))]
    [Migration("20231011060446_UnsecureApp")]
    partial class UnsecureApp
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("UnSecureApp.Models.ForgotPassword", b =>
                {
                    b.Property<string>("ConfirmPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NewPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasIndex("UserId");

                    b.ToTable("ForgotPasswords");
                });

            modelBuilder.Entity("UnSecureApp.Models.MasterTable", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("MasterTables");
                });

            modelBuilder.Entity("UnSecureApp.Models.TransactionTable", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<long?>("Adhar")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("Dob")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("TransactionTables");
                });

            modelBuilder.Entity("UnSecureApp.Models.ForgotPassword", b =>
                {
                    b.HasOne("UnSecureApp.Models.MasterTable", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("UnSecureApp.Models.TransactionTable", b =>
                {
                    b.HasOne("UnSecureApp.Models.MasterTable", "User")
                        .WithMany("TransactionTables")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("UnSecureApp.Models.MasterTable", b =>
                {
                    b.Navigation("TransactionTables");
                });
#pragma warning restore 612, 618
        }
    }
}
﻿// <auto-generated />
using System;
using Gym_Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Gym_Api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250304110359_addassignmenttable")]
    partial class addassignmenttable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Gym_Api.Data.Models.Assignment", b =>
                {
                    b.Property<int>("Assignment_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Assignment_ID"));

                    b.Property<DateTime>("Assignment_Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Coach_ID")
                        .HasColumnType("int");

                    b.Property<double>("CompleteRate")
                        .HasColumnType("float");

                    b.Property<int>("Exercise_ID")
                        .HasColumnType("int");

                    b.Property<int>("User_ID")
                        .HasColumnType("int");

                    b.HasKey("Assignment_ID");

                    b.HasIndex("Coach_ID");

                    b.HasIndex("Exercise_ID");

                    b.HasIndex("User_ID");

                    b.ToTable("Assignments");
                });

            modelBuilder.Entity("Gym_Api.Data.Models.Category", b =>
                {
                    b.Property<int>("Category_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Category_ID"));

                    b.Property<string>("Category_Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Category_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Category_ID");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Gym_Api.Data.Models.Coach", b =>
                {
                    b.Property<int>("Coach_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Coach_ID"));

                    b.Property<string>("Availability")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Experience_Years")
                        .HasColumnType("int");

                    b.Property<string>("Fname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Portfolio_Link")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Ratings")
                        .HasColumnType("float");

                    b.HasKey("Coach_ID");

                    b.ToTable("Coaches");
                });

            modelBuilder.Entity("Gym_Api.Data.Models.Exercise", b =>
                {
                    b.Property<int>("Exercise_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Exercise_ID"));

                    b.Property<int>("Calories_Burned")
                        .HasColumnType("int");

                    b.Property<int>("Category_ID")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Difficulty_Level")
                        .HasColumnType("int");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<string>("Exercise_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Target_Muscle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Video_URL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Exercise_ID");

                    b.HasIndex("Category_ID");

                    b.ToTable("Exercises");
                });

            modelBuilder.Entity("Gym_Api.Data.Models.User", b =>
                {
                    b.Property<int>("User_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("User_ID"));

                    b.Property<DateTime>("BDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Fitness_Goal")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Fname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Height")
                        .HasColumnType("float");

                    b.Property<string>("Lname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Payment_History")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Registration_Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Subscription_Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Weight")
                        .HasColumnType("float");

                    b.HasKey("User_ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Gym_Api.Data.Models.Assignment", b =>
                {
                    b.HasOne("Gym_Api.Data.Models.Coach", "Coach")
                        .WithMany("Assignments")
                        .HasForeignKey("Coach_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Gym_Api.Data.Models.Exercise", "Exercise")
                        .WithMany("Assignments")
                        .HasForeignKey("Exercise_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Gym_Api.Data.Models.User", "User")
                        .WithMany("Assignments")
                        .HasForeignKey("User_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Coach");

                    b.Navigation("Exercise");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Gym_Api.Data.Models.Exercise", b =>
                {
                    b.HasOne("Gym_Api.Data.Models.Category", "Category")
                        .WithMany("Exercises")
                        .HasForeignKey("Category_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Gym_Api.Data.Models.Category", b =>
                {
                    b.Navigation("Exercises");
                });

            modelBuilder.Entity("Gym_Api.Data.Models.Coach", b =>
                {
                    b.Navigation("Assignments");
                });

            modelBuilder.Entity("Gym_Api.Data.Models.Exercise", b =>
                {
                    b.Navigation("Assignments");
                });

            modelBuilder.Entity("Gym_Api.Data.Models.User", b =>
                {
                    b.Navigation("Assignments");
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using System;
using Banq.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Banq.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Banq.Authentication.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Banq.Database.Entities.City", b =>
                {
                    b.Property<string>("Code")
                        .HasMaxLength(4)
                        .HasColumnType("varchar(4)");

                    b.Property<Guid>("ConcurrencyStamp")
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.HasKey("Code");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("Banq.Database.Entities.Class", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint unsigned");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<Guid>("ConcurrencyStamp")
                        .HasColumnType("char(36)");

                    b.Property<string>("LessonCode")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("varchar(5)");

                    b.Property<string>("Name")
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Classes");
                });

            modelBuilder.Entity("Banq.Database.Entities.Field", b =>
                {
                    b.Property<string>("Code")
                        .HasMaxLength(2)
                        .HasColumnType("varchar(2)");

                    b.Property<Guid>("ConcurrencyStamp")
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.HasKey("Code");

                    b.ToTable("Fields");
                });

            modelBuilder.Entity("Banq.Database.Entities.FieldOfTeach", b =>
                {
                    b.Property<string>("Code")
                        .HasMaxLength(3)
                        .HasColumnType("varchar(3)");

                    b.Property<Guid>("ConcurrencyStamp")
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Code");

                    b.ToTable("FieldsOfTeach");
                });

            modelBuilder.Entity("Banq.Database.Entities.Lesson", b =>
                {
                    b.Property<string>("Code")
                        .HasMaxLength(5)
                        .HasColumnType("varchar(5)");

                    b.Property<string>("BookCode")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("varchar(6)");

                    b.Property<Guid>("ConcurrencyStamp")
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Code");

                    b.ToTable("Lessons");
                });

            modelBuilder.Entity("Banq.Database.Entities.Manager", b =>
                {
                    b.Property<string>("PersonnelCode")
                        .HasMaxLength(8)
                        .HasColumnType("varchar(8)");

                    b.Property<string>("Biography")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<Guid>("ConcurrencyStamp")
                        .HasColumnType("char(36)");

                    b.Property<string>("Family")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid?>("PictureGuid")
                        .HasColumnType("char(36)");

                    b.HasKey("PersonnelCode");

                    b.ToTable("Managers");
                });

            modelBuilder.Entity("Banq.Database.Entities.Office", b =>
                {
                    b.Property<string>("Code")
                        .HasMaxLength(4)
                        .HasColumnType("varchar(4)");

                    b.Property<Guid>("ConcurrencyStamp")
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.HasKey("Code");

                    b.ToTable("Offices");
                });

            modelBuilder.Entity("Banq.Database.Entities.Province", b =>
                {
                    b.Property<string>("Code")
                        .HasMaxLength(4)
                        .HasColumnType("varchar(4)");

                    b.Property<Guid>("ConcurrencyStamp")
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.HasKey("Code");

                    b.ToTable("Provinces");
                });

            modelBuilder.Entity("Banq.Database.Entities.Question", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint unsigned");

                    b.Property<Guid>("ConcurrencyStamp")
                        .HasColumnType("char(36)");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<DateTime>("ServerTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("Banq.Database.Entities.Relations.CityAndOffice", b =>
                {
                    b.Property<string>("CityCode")
                        .HasMaxLength(4)
                        .HasColumnType("varchar(4)");

                    b.Property<string>("OfficeCode")
                        .HasMaxLength(4)
                        .HasColumnType("varchar(4)");

                    b.Property<Guid>("ConcurrencyStamp")
                        .HasColumnType("char(36)");

                    b.HasKey("CityCode", "OfficeCode");

                    b.ToTable("CityAndOffices");
                });

            modelBuilder.Entity("Banq.Database.Entities.Relations.FieldAndLessonAndGrade", b =>
                {
                    b.Property<string>("FieldCode")
                        .HasMaxLength(2)
                        .HasColumnType("varchar(2)");

                    b.Property<string>("LessonCode")
                        .HasMaxLength(5)
                        .HasColumnType("varchar(5)");

                    b.Property<int>("Grade")
                        .HasColumnType("int");

                    b.Property<Guid>("ConcurrencyStamp")
                        .HasColumnType("char(36)");

                    b.HasKey("FieldCode", "LessonCode", "Grade");

                    b.ToTable("FieldAndLessonsAndGrades");
                });

            modelBuilder.Entity("Banq.Database.Entities.Relations.ManagerAndSchool", b =>
                {
                    b.Property<string>("ManagerPersonnelCode")
                        .HasMaxLength(8)
                        .HasColumnType("varchar(8)");

                    b.Property<string>("SchoolCode")
                        .HasMaxLength(8)
                        .HasColumnType("varchar(8)");

                    b.Property<Guid>("ConcurrencyStamp")
                        .HasColumnType("char(36)");

                    b.HasKey("ManagerPersonnelCode", "SchoolCode");

                    b.ToTable("ManagerAndSchools");
                });

            modelBuilder.Entity("Banq.Database.Entities.Relations.OfficeAndSchool", b =>
                {
                    b.Property<string>("OfficeCode")
                        .HasMaxLength(4)
                        .HasColumnType("varchar(4)");

                    b.Property<string>("SchoolCode")
                        .HasMaxLength(8)
                        .HasColumnType("varchar(8)");

                    b.Property<Guid>("ConcurrencyStamp")
                        .HasColumnType("char(36)");

                    b.HasKey("OfficeCode", "SchoolCode");

                    b.ToTable("OfficeAndSchools");
                });

            modelBuilder.Entity("Banq.Database.Entities.Relations.ProvinceAndCity", b =>
                {
                    b.Property<string>("ProvinceCode")
                        .HasMaxLength(4)
                        .HasColumnType("varchar(4)");

                    b.Property<string>("CityCode")
                        .HasMaxLength(4)
                        .HasColumnType("varchar(4)");

                    b.Property<Guid>("ConcurrencyStamp")
                        .HasColumnType("char(36)");

                    b.HasKey("ProvinceCode", "CityCode");

                    b.ToTable("ProvinceAndCities");
                });

            modelBuilder.Entity("Banq.Database.Entities.Relations.SchoolAndClass", b =>
                {
                    b.Property<string>("SchoolCode")
                        .HasMaxLength(8)
                        .HasColumnType("varchar(8)");

                    b.Property<ulong>("ClassId")
                        .HasColumnType("bigint unsigned");

                    b.Property<Guid>("ConcurrencyStamp")
                        .HasColumnType("char(36)");

                    b.HasKey("SchoolCode", "ClassId");

                    b.ToTable("SchoolAndClasses");
                });

            modelBuilder.Entity("Banq.Database.Entities.Relations.SchoolAndField", b =>
                {
                    b.Property<string>("SchoolCode")
                        .HasMaxLength(8)
                        .HasColumnType("varchar(8)");

                    b.Property<string>("FieldCode")
                        .HasMaxLength(2)
                        .HasColumnType("varchar(2)");

                    b.Property<Guid>("ConcurrencyStamp")
                        .HasColumnType("char(36)");

                    b.HasKey("SchoolCode", "FieldCode");

                    b.ToTable("SchoolsAndFields");
                });

            modelBuilder.Entity("Banq.Database.Entities.Relations.TeacherAndClass", b =>
                {
                    b.Property<string>("TeacherPersonnelCode")
                        .HasMaxLength(8)
                        .HasColumnType("varchar(8)");

                    b.Property<ulong>("ClassId")
                        .HasColumnType("bigint unsigned");

                    b.Property<Guid>("ConcurrencyStamp")
                        .HasColumnType("char(36)");

                    b.HasKey("TeacherPersonnelCode", "ClassId");

                    b.ToTable("TeachersAndClasses");
                });

            modelBuilder.Entity("Banq.Database.Entities.Relations.TeacherAndClassAndQuestion", b =>
                {
                    b.Property<string>("TeacherPersonnelCode")
                        .HasMaxLength(8)
                        .HasColumnType("varchar(8)");

                    b.Property<ulong>("ClassId")
                        .HasColumnType("bigint unsigned");

                    b.Property<ulong>("QuestionId")
                        .HasColumnType("bigint unsigned");

                    b.Property<Guid>("ConcurrencyStamp")
                        .HasColumnType("char(36)");

                    b.HasKey("TeacherPersonnelCode", "ClassId", "QuestionId");

                    b.ToTable("TeacherAndClassesAndQuestions");
                });

            modelBuilder.Entity("Banq.Database.Entities.Relations.TeacherAndFieldOfTeach", b =>
                {
                    b.Property<string>("TeacherPersonnelCode")
                        .HasMaxLength(8)
                        .HasColumnType("varchar(8)");

                    b.Property<string>("FieldOfTeachCode")
                        .HasMaxLength(3)
                        .HasColumnType("varchar(3)");

                    b.Property<Guid>("ConcurrencyStamp")
                        .HasColumnType("char(36)");

                    b.HasKey("TeacherPersonnelCode", "FieldOfTeachCode");

                    b.ToTable("TeachersAndFieldOfTeaches");
                });

            modelBuilder.Entity("Banq.Database.Entities.Relations.TeacherAndSchool", b =>
                {
                    b.Property<string>("SchoolCode")
                        .HasMaxLength(8)
                        .HasColumnType("varchar(8)");

                    b.Property<string>("TeacherPersonnelCode")
                        .HasMaxLength(8)
                        .HasColumnType("varchar(8)");

                    b.Property<Guid>("ConcurrencyStamp")
                        .HasColumnType("char(36)");

                    b.HasKey("SchoolCode", "TeacherPersonnelCode");

                    b.ToTable("TeachersAndSchools");
                });

            modelBuilder.Entity("Banq.Database.Entities.School", b =>
                {
                    b.Property<string>("Code")
                        .HasMaxLength(8)
                        .HasColumnType("varchar(8)");

                    b.Property<Guid>("ConcurrencyStamp")
                        .HasColumnType("char(36)");

                    b.Property<int>("CourseLevel")
                        .HasColumnType("int");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<Guid?>("PictureGuid")
                        .HasColumnType("char(36)");

                    b.HasKey("Code");

                    b.ToTable("Schools");
                });

            modelBuilder.Entity("Banq.Database.Entities.Teacher", b =>
                {
                    b.Property<string>("PersonnelCode")
                        .HasMaxLength(8)
                        .HasColumnType("varchar(8)");

                    b.Property<string>("Biography")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<Guid>("ConcurrencyStamp")
                        .HasColumnType("char(36)");

                    b.Property<string>("Family")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid?>("PictureGuid")
                        .HasColumnType("char(36)");

                    b.Property<bool>("WantsToCheckOtherQuestions")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("PersonnelCode");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Banq.Authentication.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Banq.Authentication.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Banq.Authentication.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Banq.Authentication.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

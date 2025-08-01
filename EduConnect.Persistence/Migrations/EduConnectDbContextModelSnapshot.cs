﻿// <auto-generated />
using System;
using EduConnect.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EduConnect.Persistence.Migrations
{
    [DbContext(typeof(EduConnectDbContext))]
    partial class EduConnectDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EduConnect.Domain.Entities.Class", b =>
                {
                    b.Property<Guid>("ClassId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AcademicYear")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClassName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GradeLevel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("HomeroomTeacherId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ClassId");

                    b.HasIndex("HomeroomTeacherId");

                    b.ToTable("Classes");
                });

            modelBuilder.Entity("EduConnect.Domain.Entities.ClassBehaviorLog", b =>
                {
                    b.Property<Guid>("LogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BehaviorType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ClassSessionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("LogId");

                    b.HasIndex("ClassSessionId");

                    b.ToTable("ClassBehaviorLogs");
                });

            modelBuilder.Entity("EduConnect.Domain.Entities.ClassReport", b =>
                {
                    b.Property<Guid>("ReportId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ClassId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("GeneratedByAI")
                        .HasColumnType("bit");

                    b.Property<DateTime>("GeneratedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SummaryContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("ReportId");

                    b.HasIndex("ClassId");

                    b.ToTable("ClassReports");
                });

            modelBuilder.Entity("EduConnect.Domain.Entities.ClassSession", b =>
                {
                    b.Property<Guid>("ClassSessionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ClassId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeleteAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("GeneralBehaviorNote")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LessonContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("PeriodId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SubjectId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TeacherId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("TotalAbsentStudents")
                        .HasColumnType("int");

                    b.HasKey("ClassSessionId");

                    b.HasIndex("ClassId");

                    b.HasIndex("PeriodId");

                    b.HasIndex("SubjectId");

                    b.HasIndex("TeacherId");

                    b.ToTable("ClassSessions");
                });

            modelBuilder.Entity("EduConnect.Domain.Entities.Message", b =>
                {
                    b.Property<Guid>("MessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AIResponse")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ParentId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("MessageId");

                    b.HasIndex("ParentId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("EduConnect.Domain.Entities.Notification", b =>
                {
                    b.Property<Guid>("NotificationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ClassReportId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsRead")
                        .HasColumnType("bit");

                    b.Property<Guid>("RecipientUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("SentAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("StudentReportId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("NotificationId");

                    b.HasIndex("ClassReportId");

                    b.HasIndex("RecipientUserId");

                    b.HasIndex("StudentReportId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("EduConnect.Domain.Entities.Period", b =>
                {
                    b.Property<Guid>("PeriodId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("time");

                    b.Property<int>("PeriodNumber")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("time");

                    b.HasKey("PeriodId");

                    b.HasIndex("PeriodNumber")
                        .IsUnique();

                    b.ToTable("Periods");
                });

            modelBuilder.Entity("EduConnect.Domain.Entities.Student", b =>
                {
                    b.Property<Guid>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AvatarUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ClassId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ParentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StudentId");

                    b.HasIndex("ClassId");

                    b.HasIndex("ParentId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("EduConnect.Domain.Entities.StudentBehaviorNote", b =>
                {
                    b.Property<Guid>("NoteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BehaviorType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ClassSessionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("NoteId");

                    b.HasIndex("ClassSessionId");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentBehaviorNotes");
                });

            modelBuilder.Entity("EduConnect.Domain.Entities.StudentReport", b =>
                {
                    b.Property<Guid>("ReportId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("GeneratedByAI")
                        .HasColumnType("bit");

                    b.Property<DateTime>("GeneratedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("SummaryContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("ReportId");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentReports");
                });

            modelBuilder.Entity("EduConnect.Domain.Entities.Subject", b =>
                {
                    b.Property<Guid>("SubjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubjectName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SubjectId");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("EduConnect.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeviceToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RefreshTokenExpiryTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("33f41895-b601-4aa1-8dc4-8229a9d07008"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "seed-5",
                            Email = "admin@example.com",
                            EmailConfirmed = true,
                            FullName = "",
                            IsActive = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@EXAMPLE.COM",
                            NormalizedUserName = "ADMIN",
                            PasswordHash = "AQAAAAIAAYagAAAAEArkj30trX83EVbR+GZ0CTJ8DdM4SmvnILd5uNxwo2RQidaD/cyROPGABswA8gZzTw==",
                            PhoneNumberConfirmed = false,
                            RefreshToken = "",
                            RefreshTokenExpiryTime = new DateTime(2025, 7, 22, 15, 13, 12, 267, DateTimeKind.Utc).AddTicks(3711),
                            SecurityStamp = "seed-4",
                            TwoFactorEnabled = false,
                            UserName = "admin"
                        },
                        new
                        {
                            Id = new Guid("09097277-2705-40c2-bce5-51dbd1f4c1e6"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "seed-7",
                            Email = "teacher@example.com",
                            EmailConfirmed = true,
                            FullName = "",
                            IsActive = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "TEACHER@EXAMPLE.COM",
                            NormalizedUserName = "TEACHER",
                            PasswordHash = "AQAAAAIAAYagAAAAEFClHyja3OgEV1bSlR/1lAaWLmcf2+Lf0HqdV1KLDS405R+YuzqUK3CODe9d+kJVyw==",
                            PhoneNumberConfirmed = false,
                            RefreshToken = "",
                            RefreshTokenExpiryTime = new DateTime(2025, 7, 22, 15, 13, 12, 321, DateTimeKind.Utc).AddTicks(1493),
                            SecurityStamp = "seed-6",
                            TwoFactorEnabled = false,
                            UserName = "teacher"
                        },
                        new
                        {
                            Id = new Guid("fe014130-bfb5-443b-9989-9c8f90d1065f"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "seed-9",
                            Email = "parent@example.com",
                            EmailConfirmed = true,
                            FullName = "",
                            IsActive = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "PARENT@EXAMPLE.COM",
                            NormalizedUserName = "PARENT",
                            PasswordHash = "AQAAAAIAAYagAAAAEB2KywlLoo/OLMzfVTT5RA9BFWkYSjyha84NJhwku6YWI8TuSePyYnjbXsEGcHsifw==",
                            PhoneNumberConfirmed = false,
                            RefreshToken = "",
                            RefreshTokenExpiryTime = new DateTime(2025, 7, 22, 15, 13, 12, 375, DateTimeKind.Utc).AddTicks(4326),
                            SecurityStamp = "seed-8",
                            TwoFactorEnabled = false,
                            UserName = "parent"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("3631e38b-60dd-4d1a-af7f-a26f21c2ef82"),
                            ConcurrencyStamp = "seed-1",
                            Name = "admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = new Guid("51ef7e08-ff07-459b-8c55-c7ebac505103"),
                            ConcurrencyStamp = "seed-2",
                            Name = "teacher",
                            NormalizedName = "TEACHER"
                        },
                        new
                        {
                            Id = new Guid("37a7c5df-4898-4fd4-8e5f-d2abd4b57520"),
                            ConcurrencyStamp = "seed-3",
                            Name = "parent",
                            NormalizedName = "PARENT"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = new Guid("33f41895-b601-4aa1-8dc4-8229a9d07008"),
                            RoleId = new Guid("3631e38b-60dd-4d1a-af7f-a26f21c2ef82")
                        },
                        new
                        {
                            UserId = new Guid("09097277-2705-40c2-bce5-51dbd1f4c1e6"),
                            RoleId = new Guid("51ef7e08-ff07-459b-8c55-c7ebac505103")
                        },
                        new
                        {
                            UserId = new Guid("fe014130-bfb5-443b-9989-9c8f90d1065f"),
                            RoleId = new Guid("37a7c5df-4898-4fd4-8e5f-d2abd4b57520")
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("EduConnect.Domain.Entities.Class", b =>
                {
                    b.HasOne("EduConnect.Domain.Entities.User", "HomeroomTeacher")
                        .WithMany("HomeroomClasses")
                        .HasForeignKey("HomeroomTeacherId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("HomeroomTeacher");
                });

            modelBuilder.Entity("EduConnect.Domain.Entities.ClassBehaviorLog", b =>
                {
                    b.HasOne("EduConnect.Domain.Entities.ClassSession", "ClassSession")
                        .WithMany("ClassBehaviorLogs")
                        .HasForeignKey("ClassSessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ClassSession");
                });

            modelBuilder.Entity("EduConnect.Domain.Entities.ClassReport", b =>
                {
                    b.HasOne("EduConnect.Domain.Entities.Class", "Class")
                        .WithMany("ClassReports")
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Class");
                });

            modelBuilder.Entity("EduConnect.Domain.Entities.ClassSession", b =>
                {
                    b.HasOne("EduConnect.Domain.Entities.Class", "Class")
                        .WithMany("ClassSessions")
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EduConnect.Domain.Entities.Period", "Period")
                        .WithMany("ClassSessions")
                        .HasForeignKey("PeriodId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("EduConnect.Domain.Entities.Subject", "Subject")
                        .WithMany("ClassSessions")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("EduConnect.Domain.Entities.User", "Teacher")
                        .WithMany("TeachingSessions")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Class");

                    b.Navigation("Period");

                    b.Navigation("Subject");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("EduConnect.Domain.Entities.Message", b =>
                {
                    b.HasOne("EduConnect.Domain.Entities.User", "Parent")
                        .WithMany("Messages")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("EduConnect.Domain.Entities.Notification", b =>
                {
                    b.HasOne("EduConnect.Domain.Entities.ClassReport", "ClassReport")
                        .WithMany("Notifications")
                        .HasForeignKey("ClassReportId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("EduConnect.Domain.Entities.User", "Recipient")
                        .WithMany("Notifications")
                        .HasForeignKey("RecipientUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("EduConnect.Domain.Entities.StudentReport", "StudentReport")
                        .WithMany("Notifications")
                        .HasForeignKey("StudentReportId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("ClassReport");

                    b.Navigation("Recipient");

                    b.Navigation("StudentReport");
                });

            modelBuilder.Entity("EduConnect.Domain.Entities.Student", b =>
                {
                    b.HasOne("EduConnect.Domain.Entities.Class", "Class")
                        .WithMany("Students")
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("EduConnect.Domain.Entities.User", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Class");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("EduConnect.Domain.Entities.StudentBehaviorNote", b =>
                {
                    b.HasOne("EduConnect.Domain.Entities.ClassSession", "ClassSession")
                        .WithMany("StudentBehaviorNotes")
                        .HasForeignKey("ClassSessionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("EduConnect.Domain.Entities.Student", "Student")
                        .WithMany("BehaviorNotes")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ClassSession");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("EduConnect.Domain.Entities.StudentReport", b =>
                {
                    b.HasOne("EduConnect.Domain.Entities.Student", "Student")
                        .WithMany("StudentReports")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("EduConnect.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("EduConnect.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EduConnect.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("EduConnect.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EduConnect.Domain.Entities.Class", b =>
                {
                    b.Navigation("ClassReports");

                    b.Navigation("ClassSessions");

                    b.Navigation("Students");
                });

            modelBuilder.Entity("EduConnect.Domain.Entities.ClassReport", b =>
                {
                    b.Navigation("Notifications");
                });

            modelBuilder.Entity("EduConnect.Domain.Entities.ClassSession", b =>
                {
                    b.Navigation("ClassBehaviorLogs");

                    b.Navigation("StudentBehaviorNotes");
                });

            modelBuilder.Entity("EduConnect.Domain.Entities.Period", b =>
                {
                    b.Navigation("ClassSessions");
                });

            modelBuilder.Entity("EduConnect.Domain.Entities.Student", b =>
                {
                    b.Navigation("BehaviorNotes");

                    b.Navigation("StudentReports");
                });

            modelBuilder.Entity("EduConnect.Domain.Entities.StudentReport", b =>
                {
                    b.Navigation("Notifications");
                });

            modelBuilder.Entity("EduConnect.Domain.Entities.Subject", b =>
                {
                    b.Navigation("ClassSessions");
                });

            modelBuilder.Entity("EduConnect.Domain.Entities.User", b =>
                {
                    b.Navigation("Children");

                    b.Navigation("HomeroomClasses");

                    b.Navigation("Messages");

                    b.Navigation("Notifications");

                    b.Navigation("TeachingSessions");
                });
#pragma warning restore 612, 618
        }
    }
}

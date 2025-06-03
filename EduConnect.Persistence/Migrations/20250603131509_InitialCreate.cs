using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EduConnect.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    SubjectID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubjectName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.SubjectID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Classrooms",
                columns: table => new
                {
                    ClassID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClassName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HomeroomTeacherID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AcademicYear = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classrooms", x => x.ClassID);
                    table.ForeignKey(
                        name: "FK_Classrooms_AspNetUsers_HomeroomTeacherID",
                        column: x => x.HomeroomTeacherID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    MessageID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ToUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SentTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsFromAI = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.MessageID);
                    table.ForeignKey(
                        name: "FK_Messages_AspNetUsers_FromUserID",
                        column: x => x.FromUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Messages_AspNetUsers_ToUserID",
                        column: x => x.ToUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClassNotebooks",
                columns: table => new
                {
                    NotebookID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClassID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SummaryNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassNotebooks", x => x.NotebookID);
                    table.ForeignKey(
                        name: "FK_ClassNotebooks_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClassNotebooks_Classrooms_ClassID",
                        column: x => x.ClassID,
                        principalTable: "Classrooms",
                        principalColumn: "ClassID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reminders",
                columns: table => new
                {
                    ReminderID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClassID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reminders", x => x.ReminderID);
                    table.ForeignKey(
                        name: "FK_Reminders_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reminders_Classrooms_ClassID",
                        column: x => x.ClassID,
                        principalTable: "Classrooms",
                        principalColumn: "ClassID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    ScheduleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClassID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubjectID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeacherID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Period = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.ScheduleID);
                    table.ForeignKey(
                        name: "FK_Schedules_AspNetUsers_TeacherID",
                        column: x => x.TeacherID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Schedules_Classrooms_ClassID",
                        column: x => x.ClassID,
                        principalTable: "Classrooms",
                        principalColumn: "ClassID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Schedules_Subjects_SubjectID",
                        column: x => x.SubjectID,
                        principalTable: "Subjects",
                        principalColumn: "SubjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClassID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentID);
                    table.ForeignKey(
                        name: "FK_Students_AspNetUsers_ParentID",
                        column: x => x.ParentID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Students_Classrooms_ClassID",
                        column: x => x.ClassID,
                        principalTable: "Classrooms",
                        principalColumn: "ClassID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LessonLogs",
                columns: table => new
                {
                    LessonLogID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NotebookID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScheduleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeacherID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalPresent = table.Column<int>(type: "int", nullable: false),
                    TotalAbsent = table.Column<int>(type: "int", nullable: false),
                    DisciplineNote = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonLogs", x => x.LessonLogID);
                    table.ForeignKey(
                        name: "FK_LessonLogs_AspNetUsers_TeacherID",
                        column: x => x.TeacherID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LessonLogs_ClassNotebooks_NotebookID",
                        column: x => x.NotebookID,
                        principalTable: "ClassNotebooks",
                        principalColumn: "NotebookID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LessonLogs_Schedules_ScheduleID",
                        column: x => x.ScheduleID,
                        principalTable: "Schedules",
                        principalColumn: "ScheduleID");
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    NotificationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SentBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SentDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.NotificationID);
                    table.ForeignKey(
                        name: "FK_Notifications_AspNetUsers_SentBy",
                        column: x => x.SentBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notifications_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentLessonNotes",
                columns: table => new
                {
                    StudentNoteID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LessonLogID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NoteContent = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentLessonNotes", x => x.StudentNoteID);
                    table.ForeignKey(
                        name: "FK_StudentLessonNotes_LessonLogs_LessonLogID",
                        column: x => x.LessonLogID,
                        principalTable: "LessonLogs",
                        principalColumn: "LessonLogID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentLessonNotes_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "StudentID");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("3631e38b-60dd-4d1a-af7f-a26f21c2ef82"), "seed-1", "admin", "ADMIN" },
                    { new Guid("37a7c5df-4898-4fd4-8e5f-d2abd4b57520"), "seed-3", "parent", "PARENT" },
                    { new Guid("51ef7e08-ff07-459b-8c55-c7ebac505103"), "seed-2", "teacher", "TEACHER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "IsActive", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("09097277-2705-40c2-bce5-51dbd1f4c1e6"), 0, "seed-7", "teacher@example.com", true, true, false, null, "TEACHER@EXAMPLE.COM", "TEACHER", "AQAAAAIAAYagAAAAEAPlkdmwriKMYwT/qBXLfHSRtCTg3oUUEm0Ht60m3yO8DFk4TVWqm/toKh30X8EH3g==", null, false, "", new DateTime(2025, 6, 10, 13, 15, 9, 400, DateTimeKind.Utc).AddTicks(5203), "seed-6", false, "teacher" },
                    { new Guid("33f41895-b601-4aa1-8dc4-8229a9d07008"), 0, "seed-5", "admin@example.com", true, true, false, null, "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAIAAYagAAAAEK9VYA1FUEOizjgY1tgHU4U+aod7h3CLiBzEPlqRTvBkE5djrNijGJbsUnAEkMKGNQ==", null, false, "", new DateTime(2025, 6, 10, 13, 15, 9, 349, DateTimeKind.Utc).AddTicks(5945), "seed-4", false, "admin" },
                    { new Guid("fe014130-bfb5-443b-9989-9c8f90d1065f"), 0, "seed-9", "parent@example.com", true, true, false, null, "PARENT@EXAMPLE.COM", "PARENT", "AQAAAAIAAYagAAAAEDcU1XkMO1QdfgVQV8YAwrK1GeJdrXZM+i8W7/4jBaXEX76+wWwdlliAL61H2iaTmQ==", null, false, "", new DateTime(2025, 6, 10, 13, 15, 9, 451, DateTimeKind.Utc).AddTicks(7356), "seed-8", false, "parent" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("51ef7e08-ff07-459b-8c55-c7ebac505103"), new Guid("09097277-2705-40c2-bce5-51dbd1f4c1e6") },
                    { new Guid("3631e38b-60dd-4d1a-af7f-a26f21c2ef82"), new Guid("33f41895-b601-4aa1-8dc4-8229a9d07008") },
                    { new Guid("37a7c5df-4898-4fd4-8e5f-d2abd4b57520"), new Guid("fe014130-bfb5-443b-9989-9c8f90d1065f") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ClassNotebooks_ClassID",
                table: "ClassNotebooks",
                column: "ClassID");

            migrationBuilder.CreateIndex(
                name: "IX_ClassNotebooks_CreatedBy",
                table: "ClassNotebooks",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Classrooms_HomeroomTeacherID",
                table: "Classrooms",
                column: "HomeroomTeacherID");

            migrationBuilder.CreateIndex(
                name: "IX_LessonLogs_NotebookID",
                table: "LessonLogs",
                column: "NotebookID");

            migrationBuilder.CreateIndex(
                name: "IX_LessonLogs_ScheduleID",
                table: "LessonLogs",
                column: "ScheduleID");

            migrationBuilder.CreateIndex(
                name: "IX_LessonLogs_TeacherID",
                table: "LessonLogs",
                column: "TeacherID");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_FromUserID",
                table: "Messages",
                column: "FromUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ToUserID",
                table: "Messages",
                column: "ToUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_SentBy",
                table: "Notifications",
                column: "SentBy");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_StudentID",
                table: "Notifications",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_Reminders_ClassID",
                table: "Reminders",
                column: "ClassID");

            migrationBuilder.CreateIndex(
                name: "IX_Reminders_CreatedBy",
                table: "Reminders",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_ClassID",
                table: "Schedules",
                column: "ClassID");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_SubjectID",
                table: "Schedules",
                column: "SubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_TeacherID",
                table: "Schedules",
                column: "TeacherID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentLessonNotes_LessonLogID",
                table: "StudentLessonNotes",
                column: "LessonLogID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentLessonNotes_StudentID",
                table: "StudentLessonNotes",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_Students_ClassID",
                table: "Students",
                column: "ClassID");

            migrationBuilder.CreateIndex(
                name: "IX_Students_ParentID",
                table: "Students",
                column: "ParentID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Reminders");

            migrationBuilder.DropTable(
                name: "StudentLessonNotes");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "LessonLogs");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "ClassNotebooks");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "Classrooms");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}

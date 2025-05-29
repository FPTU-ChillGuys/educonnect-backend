using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EduConnect.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedingUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "name",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "AspNetUsers",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "phone_number",
                table: "AspNetUsers",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "AspNetUsers",
                newName: "PasswordHash");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3631e38b-60dd-4d1a-af7f-a26f21c2ef82", "3631e38b-60dd-4d1a-af7f-a26f21c2ef82", "admin", "ADMIN" },
                    { "37a7c5df-4898-4fd4-8e5f-d2abd4b57520", "37a7c5df-4898-4fd4-8e5f-d2abd4b57520", "parent", "PARENT" },
                    { "51ef7e08-ff07-459b-8c55-c7ebac505103", "51ef7e08-ff07-459b-8c55-c7ebac505103", "teacher", "TEACHER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "a1b2c3d4-e5f6-7890-abcd-123456789012", 0, "817dc449-7d4d-4d35-8e0f-7371d203017b", "admin@example.com", true, false, null, "USER@EXAMPLE.COM", "ADMIN", "AQAAAAIAAYagAAAAEIToWbthtJRrlbaemrK2/acaNTKg9+2lgfkJPEfExufrxL8JQSXo8yZPV1xo04Pdzg==", null, false, "51f4003c-ea6f-4f57-8640-f2300bdfeced", false, "admin" },
                    { "b2c3d4e5-f6a7-8901-bcde-234567890123", 0, "34665f9e-148e-4877-8e30-6200c69f0ffc", "teacher@example.com", true, false, null, "TEACHER@EXAMPLE.COM", "TEACHER", "AQAAAAIAAYagAAAAEN1zPEeL8za6PKCMh8jHeQDg1Ur7iD+oZy+DlZkKvhWOpNP26LETlYS55uklMb8JKg==", null, false, "acfbaad2-6174-41b4-a7cf-e39711ed4e41", false, "teacher" },
                    { "c3d4e5f6-a789-0123-cdef-345678901234", 0, "1506083b-a24d-45db-9353-f1f55d38e32e", "parent@example.com", true, false, null, "PARENT@EXAMPLE.COM", "PARENT", "AQAAAAIAAYagAAAAEBGyx4zJLWIX1eVl7iKCR1SV12QnoDrfUj4MqLA6oZjPjSmj9yZw/uMX4/rhDOMtSQ==", null, false, "77da91b0-a0ad-47e6-9568-aa35f32114ba", false, "parent" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "3631e38b-60dd-4d1a-af7f-a26f21c2ef82", "a1b2c3d4-e5f6-7890-abcd-123456789012" },
                    { "51ef7e08-ff07-459b-8c55-c7ebac505103", "b2c3d4e5-f6a7-8901-bcde-234567890123" },
                    { "37a7c5df-4898-4fd4-8e5f-d2abd4b57520", "c3d4e5f6-a789-0123-cdef-345678901234" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3631e38b-60dd-4d1a-af7f-a26f21c2ef82", "a1b2c3d4-e5f6-7890-abcd-123456789012" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "51ef7e08-ff07-459b-8c55-c7ebac505103", "b2c3d4e5-f6a7-8901-bcde-234567890123" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "37a7c5df-4898-4fd4-8e5f-d2abd4b57520", "c3d4e5f6-a789-0123-cdef-345678901234" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3631e38b-60dd-4d1a-af7f-a26f21c2ef82");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "37a7c5df-4898-4fd4-8e5f-d2abd4b57520");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "51ef7e08-ff07-459b-8c55-c7ebac505103");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a1b2c3d4-e5f6-7890-abcd-123456789012");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b2c3d4e5-f6a7-8901-bcde-234567890123");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c3d4e5f6-a789-0123-cdef-345678901234");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "AspNetUsers",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "AspNetUsers",
                newName: "phone_number");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "AspNetUsers",
                newName: "password");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "AspNetUsers",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "phone_number",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "password",
                table: "AspNetUsers",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "AspNetUsers",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);
        }
    }
}

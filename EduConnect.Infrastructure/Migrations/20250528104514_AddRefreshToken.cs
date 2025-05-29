using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduConnect.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "RefeshToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefeshTokenExpiryTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a1b2c3d4-e5f6-7890-abcd-123456789012",
                columns: new[] { "ConcurrencyStamp", "IsActive", "PasswordHash", "RefeshToken", "RefeshTokenExpiryTime", "SecurityStamp" },
                values: new object[] { "32781660-8722-4553-882f-01b5c91be6b3", true, "AQAAAAIAAYagAAAAEMKX5mVVXyszeGZt6gq3fyJsJeCG2mTqSxBEDEwVGeKFdX1SafjY4v45Qb3OIYpFBA==", "", new DateTime(2025, 6, 4, 10, 45, 12, 654, DateTimeKind.Utc).AddTicks(2677), "82841fbf-70f6-4ff9-94eb-54a9499871a3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b2c3d4e5-f6a7-8901-bcde-234567890123",
                columns: new[] { "ConcurrencyStamp", "IsActive", "PasswordHash", "RefeshToken", "RefeshTokenExpiryTime", "SecurityStamp" },
                values: new object[] { "18ccb39a-0cbe-442a-bd65-1295250751ba", true, "AQAAAAIAAYagAAAAENZmXAYMaNYHJAbdH9DuTGvuIGA2yW0Wd1xQWIjaJ4mcHCQObBwt9ll7QA8ie8xpaw==", "", new DateTime(2025, 6, 4, 10, 45, 12, 703, DateTimeKind.Utc).AddTicks(9747), "2ba5eb3f-cae5-4a9c-8a5f-5d1c1b92bf68" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c3d4e5f6-a789-0123-cdef-345678901234",
                columns: new[] { "ConcurrencyStamp", "IsActive", "PasswordHash", "RefeshToken", "RefeshTokenExpiryTime", "SecurityStamp" },
                values: new object[] { "52319cfc-76a6-4c2d-9c83-4420f6972e71", true, "AQAAAAIAAYagAAAAEHRjrKaOlEBuzduoHbv1o5jeNooxC8NLB23G7JjD8WYB2uk65JVmgbpf+4iHqvNyyw==", "", new DateTime(2025, 6, 4, 10, 45, 12, 754, DateTimeKind.Utc).AddTicks(4721), "0bdcb82a-f656-4042-a8e2-c589ac84a979" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefeshToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefeshTokenExpiryTime",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a1b2c3d4-e5f6-7890-abcd-123456789012",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "817dc449-7d4d-4d35-8e0f-7371d203017b", "AQAAAAIAAYagAAAAEIToWbthtJRrlbaemrK2/acaNTKg9+2lgfkJPEfExufrxL8JQSXo8yZPV1xo04Pdzg==", "51f4003c-ea6f-4f57-8640-f2300bdfeced" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b2c3d4e5-f6a7-8901-bcde-234567890123",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "34665f9e-148e-4877-8e30-6200c69f0ffc", "AQAAAAIAAYagAAAAEN1zPEeL8za6PKCMh8jHeQDg1Ur7iD+oZy+DlZkKvhWOpNP26LETlYS55uklMb8JKg==", "acfbaad2-6174-41b4-a7cf-e39711ed4e41" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c3d4e5f6-a789-0123-cdef-345678901234",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1506083b-a24d-45db-9353-f1f55d38e32e", "AQAAAAIAAYagAAAAEBGyx4zJLWIX1eVl7iKCR1SV12QnoDrfUj4MqLA6oZjPjSmj9yZw/uMX4/rhDOMtSQ==", "77da91b0-a0ad-47e6-9568-aa35f32114ba" });
        }
    }
}

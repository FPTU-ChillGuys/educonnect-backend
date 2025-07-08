using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduConnect.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Refactor_Entitie_ClassSession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteAt",
                table: "ClassSessions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ClassSessions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("09097277-2705-40c2-bce5-51dbd1f4c1e6"),
                columns: new[] { "PasswordHash", "RefreshTokenExpiryTime" },
                values: new object[] { "AQAAAAIAAYagAAAAEAFaPaW5g9lsDp6e+IN/LYEgMfsLxfqUnE1MB9TfW6q7RoWp9cclE93VhWvEh0bWvQ==", new DateTime(2025, 7, 5, 15, 30, 48, 653, DateTimeKind.Utc).AddTicks(3805) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("33f41895-b601-4aa1-8dc4-8229a9d07008"),
                columns: new[] { "PasswordHash", "RefreshTokenExpiryTime" },
                values: new object[] { "AQAAAAIAAYagAAAAEN9i2r6nodVnTSfEXIyN5r4SzLLDPOJS3uggMav9o6RK7P5/rRvoGz/vEwheovkd4g==", new DateTime(2025, 7, 5, 15, 30, 48, 602, DateTimeKind.Utc).AddTicks(9857) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fe014130-bfb5-443b-9989-9c8f90d1065f"),
                columns: new[] { "PasswordHash", "RefreshTokenExpiryTime" },
                values: new object[] { "AQAAAAIAAYagAAAAEHMF9KGSjjyxPa3mJYtBN90ViK9GhU5GhbeODHyLdZ/4Nj3EnbEUgqokqFqTb4ayyg==", new DateTime(2025, 7, 5, 15, 30, 48, 704, DateTimeKind.Utc).AddTicks(8199) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeleteAt",
                table: "ClassSessions");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ClassSessions");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("09097277-2705-40c2-bce5-51dbd1f4c1e6"),
                columns: new[] { "PasswordHash", "RefreshTokenExpiryTime" },
                values: new object[] { "AQAAAAIAAYagAAAAENtFoAOUOS/sNUg1drgdEyl/yBS4b9cUHvjZrd+jafe/87SBkbsng1Z5m0V9brertw==", new DateTime(2025, 6, 26, 13, 47, 16, 368, DateTimeKind.Utc).AddTicks(4180) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("33f41895-b601-4aa1-8dc4-8229a9d07008"),
                columns: new[] { "PasswordHash", "RefreshTokenExpiryTime" },
                values: new object[] { "AQAAAAIAAYagAAAAEBsO7nIM3v0c6oSp/fDZ08l/PeNUWis1Jw41oCVyd4GMCHUcLkYV2H5MaKbc0WWafg==", new DateTime(2025, 6, 26, 13, 47, 16, 318, DateTimeKind.Utc).AddTicks(8999) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fe014130-bfb5-443b-9989-9c8f90d1065f"),
                columns: new[] { "PasswordHash", "RefreshTokenExpiryTime" },
                values: new object[] { "AQAAAAIAAYagAAAAEDHj8+FVbFU9lrfR0ozDJCPfdRulXnPmVPEUIzgj3u4qQoyhuHhThjEOCeLIaMtt+Q==", new DateTime(2025, 6, 26, 13, 47, 16, 418, DateTimeKind.Utc).AddTicks(7647) });
        }
    }
}

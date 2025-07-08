using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduConnect.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_DeviceToken_Property : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeviceToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("09097277-2705-40c2-bce5-51dbd1f4c1e6"),
                columns: new[] { "DeviceToken", "PasswordHash", "RefreshTokenExpiryTime" },
                values: new object[] { null, "AQAAAAIAAYagAAAAEAhJSLDINyKtcX8VZFp+T/mSGED5iuxgW75fbzPr/aaYZGdEceJF9iH7ZSogmjGL6g==", new DateTime(2025, 7, 9, 14, 9, 53, 429, DateTimeKind.Utc).AddTicks(3618) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("33f41895-b601-4aa1-8dc4-8229a9d07008"),
                columns: new[] { "DeviceToken", "PasswordHash", "RefreshTokenExpiryTime" },
                values: new object[] { null, "AQAAAAIAAYagAAAAEDyeN/ci6ibT6HMMBumq+W4mJH5rIvoXmm2n2pPWuIFt6r8q/VQY1BdqZ7f84f2f7g==", new DateTime(2025, 7, 9, 14, 9, 53, 379, DateTimeKind.Utc).AddTicks(3965) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fe014130-bfb5-443b-9989-9c8f90d1065f"),
                columns: new[] { "DeviceToken", "PasswordHash", "RefreshTokenExpiryTime" },
                values: new object[] { null, "AQAAAAIAAYagAAAAEKKjIFzgu0CHtJYGorct9e+YSKGlv+ie0A2T4QHJga6FKazIYHcLbbGE0ScBtIwwiw==", new DateTime(2025, 7, 9, 14, 9, 53, 479, DateTimeKind.Utc).AddTicks(7957) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeviceToken",
                table: "AspNetUsers");

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
    }
}

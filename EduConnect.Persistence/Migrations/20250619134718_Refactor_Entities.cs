using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduConnect.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Refactor_Entities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StudentCode",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GradeLevel",
                table: "Classes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("09097277-2705-40c2-bce5-51dbd1f4c1e6"),
                columns: new[] { "Address", "PasswordHash", "RefreshTokenExpiryTime" },
                values: new object[] { null, "AQAAAAIAAYagAAAAENtFoAOUOS/sNUg1drgdEyl/yBS4b9cUHvjZrd+jafe/87SBkbsng1Z5m0V9brertw==", new DateTime(2025, 6, 26, 13, 47, 16, 368, DateTimeKind.Utc).AddTicks(4180) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("33f41895-b601-4aa1-8dc4-8229a9d07008"),
                columns: new[] { "Address", "PasswordHash", "RefreshTokenExpiryTime" },
                values: new object[] { null, "AQAAAAIAAYagAAAAEBsO7nIM3v0c6oSp/fDZ08l/PeNUWis1Jw41oCVyd4GMCHUcLkYV2H5MaKbc0WWafg==", new DateTime(2025, 6, 26, 13, 47, 16, 318, DateTimeKind.Utc).AddTicks(8999) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fe014130-bfb5-443b-9989-9c8f90d1065f"),
                columns: new[] { "Address", "PasswordHash", "RefreshTokenExpiryTime" },
                values: new object[] { null, "AQAAAAIAAYagAAAAEDHj8+FVbFU9lrfR0ozDJCPfdRulXnPmVPEUIzgj3u4qQoyhuHhThjEOCeLIaMtt+Q==", new DateTime(2025, 6, 26, 13, 47, 16, 418, DateTimeKind.Utc).AddTicks(7647) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "StudentCode",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "GradeLevel",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("09097277-2705-40c2-bce5-51dbd1f4c1e6"),
                columns: new[] { "PasswordHash", "RefreshTokenExpiryTime" },
                values: new object[] { "AQAAAAIAAYagAAAAEK5c7xvibhC9Fj0xMjlClW9IwB8CqzMgiMy6ZHkgQy8Vxxr5YQtpyOf4WBSurHfzpQ==", new DateTime(2025, 6, 18, 4, 43, 22, 133, DateTimeKind.Utc).AddTicks(596) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("33f41895-b601-4aa1-8dc4-8229a9d07008"),
                columns: new[] { "PasswordHash", "RefreshTokenExpiryTime" },
                values: new object[] { "AQAAAAIAAYagAAAAEM0ccD8R4Fbb8gJIcf2b8IwVGJScNUsi60XOG7Fqu7awtys2bSJjoXlTJvnoBms29Q==", new DateTime(2025, 6, 18, 4, 43, 22, 79, DateTimeKind.Utc).AddTicks(5086) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fe014130-bfb5-443b-9989-9c8f90d1065f"),
                columns: new[] { "PasswordHash", "RefreshTokenExpiryTime" },
                values: new object[] { "AQAAAAIAAYagAAAAEAWRg2ecDKSBvn96uLvkXJgGlKAFtTPMBpb4K1fXPAROBjOs9Xr2PD0dwgoknVNVCQ==", new DateTime(2025, 6, 18, 4, 43, 22, 189, DateTimeKind.Utc).AddTicks(1581) });
        }
    }
}

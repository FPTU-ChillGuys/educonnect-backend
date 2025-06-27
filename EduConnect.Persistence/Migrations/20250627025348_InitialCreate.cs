using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduConnect.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("09097277-2705-40c2-bce5-51dbd1f4c1e6"),
                columns: new[] { "PasswordHash", "RefreshTokenExpiryTime" },
                values: new object[] { "AQAAAAIAAYagAAAAEHPz+wKWvXzAqJopSfVqpyMNwRk7+rO8bHy7L2kBDIvZbbO97gNoj9Vh56SV830vWg==", new DateTime(2025, 7, 4, 2, 53, 47, 973, DateTimeKind.Utc).AddTicks(4489) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("33f41895-b601-4aa1-8dc4-8229a9d07008"),
                columns: new[] { "PasswordHash", "RefreshTokenExpiryTime" },
                values: new object[] { "AQAAAAIAAYagAAAAEPgMQVzMEVZCO5Hrfv52VW5Mn/qvGnBdMHAKmGIaHsbzEQW68TFI9th8NYcIemZzng==", new DateTime(2025, 7, 4, 2, 53, 47, 916, DateTimeKind.Utc).AddTicks(1778) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fe014130-bfb5-443b-9989-9c8f90d1065f"),
                columns: new[] { "PasswordHash", "RefreshTokenExpiryTime" },
                values: new object[] { "AQAAAAIAAYagAAAAEPb84Gm2MCUbhEnTSUv2FWLpa91tOFTbSfOmMSnbs7tsZQRsXC4C+ibSOh9X5LYI2A==", new DateTime(2025, 7, 4, 2, 53, 48, 33, DateTimeKind.Utc).AddTicks(4115) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

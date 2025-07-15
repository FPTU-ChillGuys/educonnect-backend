using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduConnect.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_Period : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PeriodNumber",
                table: "ClassSessions");

            migrationBuilder.AddColumn<Guid>(
                name: "PeriodId",
                table: "ClassSessions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Periods",
                columns: table => new
                {
                    PeriodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PeriodNumber = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Periods", x => x.PeriodId);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("09097277-2705-40c2-bce5-51dbd1f4c1e6"),
                columns: new[] { "FullName", "PasswordHash", "RefreshTokenExpiryTime" },
                values: new object[] { "", "AQAAAAIAAYagAAAAEOE//No3PoxmLncUkG/2O4hJK+huioeDvEVOx+d68krs75H7M8ew+rM/jhxdFDoJeA==", new DateTime(2025, 7, 21, 9, 22, 28, 485, DateTimeKind.Utc).AddTicks(4117) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("33f41895-b601-4aa1-8dc4-8229a9d07008"),
                columns: new[] { "FullName", "PasswordHash", "RefreshTokenExpiryTime" },
                values: new object[] { "", "AQAAAAIAAYagAAAAEJAQZ30KgFYtVIzaC+4CnisqwKXC3XF9VIw+Ns1Xpjk11k0eA678ObfVS2MqbdoZvg==", new DateTime(2025, 7, 21, 9, 22, 28, 436, DateTimeKind.Utc).AddTicks(1805) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fe014130-bfb5-443b-9989-9c8f90d1065f"),
                columns: new[] { "FullName", "PasswordHash", "RefreshTokenExpiryTime" },
                values: new object[] { "", "AQAAAAIAAYagAAAAEKtGJz5KL7PrRC2sf7pqV6Sdfv4/g+vhSKDhXUu/h0ELLvhCLvsrF1aNK9+YAcgupA==", new DateTime(2025, 7, 21, 9, 22, 28, 534, DateTimeKind.Utc).AddTicks(8418) });

            migrationBuilder.CreateIndex(
                name: "IX_ClassSessions_PeriodId",
                table: "ClassSessions",
                column: "PeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_Periods_PeriodNumber",
                table: "Periods",
                column: "PeriodNumber",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassSessions_Periods_PeriodId",
                table: "ClassSessions",
                column: "PeriodId",
                principalTable: "Periods",
                principalColumn: "PeriodId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassSessions_Periods_PeriodId",
                table: "ClassSessions");

            migrationBuilder.DropTable(
                name: "Periods");

            migrationBuilder.DropIndex(
                name: "IX_ClassSessions_PeriodId",
                table: "ClassSessions");

            migrationBuilder.DropColumn(
                name: "PeriodId",
                table: "ClassSessions");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "PeriodNumber",
                table: "ClassSessions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("09097277-2705-40c2-bce5-51dbd1f4c1e6"),
                columns: new[] { "PasswordHash", "RefreshTokenExpiryTime" },
                values: new object[] { "AQAAAAIAAYagAAAAEAhJSLDINyKtcX8VZFp+T/mSGED5iuxgW75fbzPr/aaYZGdEceJF9iH7ZSogmjGL6g==", new DateTime(2025, 7, 9, 14, 9, 53, 429, DateTimeKind.Utc).AddTicks(3618) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("33f41895-b601-4aa1-8dc4-8229a9d07008"),
                columns: new[] { "PasswordHash", "RefreshTokenExpiryTime" },
                values: new object[] { "AQAAAAIAAYagAAAAEDyeN/ci6ibT6HMMBumq+W4mJH5rIvoXmm2n2pPWuIFt6r8q/VQY1BdqZ7f84f2f7g==", new DateTime(2025, 7, 9, 14, 9, 53, 379, DateTimeKind.Utc).AddTicks(3965) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fe014130-bfb5-443b-9989-9c8f90d1065f"),
                columns: new[] { "PasswordHash", "RefreshTokenExpiryTime" },
                values: new object[] { "AQAAAAIAAYagAAAAEKKjIFzgu0CHtJYGorct9e+YSKGlv+ie0A2T4QHJga6FKazIYHcLbbGE0ScBtIwwiw==", new DateTime(2025, 7, 9, 14, 9, 53, 479, DateTimeKind.Utc).AddTicks(7957) });
        }
    }
}

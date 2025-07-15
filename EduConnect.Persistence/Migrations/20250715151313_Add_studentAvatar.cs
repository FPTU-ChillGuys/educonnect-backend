using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduConnect.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_studentAvatar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AvatarUrl",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("09097277-2705-40c2-bce5-51dbd1f4c1e6"),
                columns: new[] { "PasswordHash", "RefreshTokenExpiryTime" },
                values: new object[] { "AQAAAAIAAYagAAAAEFClHyja3OgEV1bSlR/1lAaWLmcf2+Lf0HqdV1KLDS405R+YuzqUK3CODe9d+kJVyw==", new DateTime(2025, 7, 22, 15, 13, 12, 321, DateTimeKind.Utc).AddTicks(1493) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("33f41895-b601-4aa1-8dc4-8229a9d07008"),
                columns: new[] { "PasswordHash", "RefreshTokenExpiryTime" },
                values: new object[] { "AQAAAAIAAYagAAAAEArkj30trX83EVbR+GZ0CTJ8DdM4SmvnILd5uNxwo2RQidaD/cyROPGABswA8gZzTw==", new DateTime(2025, 7, 22, 15, 13, 12, 267, DateTimeKind.Utc).AddTicks(3711) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fe014130-bfb5-443b-9989-9c8f90d1065f"),
                columns: new[] { "PasswordHash", "RefreshTokenExpiryTime" },
                values: new object[] { "AQAAAAIAAYagAAAAEB2KywlLoo/OLMzfVTT5RA9BFWkYSjyha84NJhwku6YWI8TuSePyYnjbXsEGcHsifw==", new DateTime(2025, 7, 22, 15, 13, 12, 375, DateTimeKind.Utc).AddTicks(4326) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvatarUrl",
                table: "Students");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("09097277-2705-40c2-bce5-51dbd1f4c1e6"),
                columns: new[] { "PasswordHash", "RefreshTokenExpiryTime" },
                values: new object[] { "AQAAAAIAAYagAAAAEOE//No3PoxmLncUkG/2O4hJK+huioeDvEVOx+d68krs75H7M8ew+rM/jhxdFDoJeA==", new DateTime(2025, 7, 21, 9, 22, 28, 485, DateTimeKind.Utc).AddTicks(4117) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("33f41895-b601-4aa1-8dc4-8229a9d07008"),
                columns: new[] { "PasswordHash", "RefreshTokenExpiryTime" },
                values: new object[] { "AQAAAAIAAYagAAAAEJAQZ30KgFYtVIzaC+4CnisqwKXC3XF9VIw+Ns1Xpjk11k0eA678ObfVS2MqbdoZvg==", new DateTime(2025, 7, 21, 9, 22, 28, 436, DateTimeKind.Utc).AddTicks(1805) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fe014130-bfb5-443b-9989-9c8f90d1065f"),
                columns: new[] { "PasswordHash", "RefreshTokenExpiryTime" },
                values: new object[] { "AQAAAAIAAYagAAAAEKtGJz5KL7PrRC2sf7pqV6Sdfv4/g+vhSKDhXUu/h0ELLvhCLvsrF1aNK9+YAcgupA==", new DateTime(2025, 7, 21, 9, 22, 28, 534, DateTimeKind.Utc).AddTicks(8418) });
        }
    }
}

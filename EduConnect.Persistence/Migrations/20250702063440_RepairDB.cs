using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduConnect.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RepairDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "ConversationId",
                table: "Messages",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "ParentId",
                table: "Conversations",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("09097277-2705-40c2-bce5-51dbd1f4c1e6"),
                columns: new[] { "PasswordHash", "RefreshTokenExpiryTime" },
                values: new object[] { "AQAAAAIAAYagAAAAENzst2ffLfJoZGtLQok0Qv8Q7hdyi/WJYtyNCOYgt/hIeeh/Fs0uU59PubpkaSlncA==", new DateTime(2025, 7, 9, 6, 34, 39, 992, DateTimeKind.Utc).AddTicks(355) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("33f41895-b601-4aa1-8dc4-8229a9d07008"),
                columns: new[] { "PasswordHash", "RefreshTokenExpiryTime" },
                values: new object[] { "AQAAAAIAAYagAAAAELo/07HqvdStz1z+9l3mBXpCw2yuhuLKGaprp+o6PI/2KKzTp+RoBJAN01h1qE1nqg==", new DateTime(2025, 7, 9, 6, 34, 39, 932, DateTimeKind.Utc).AddTicks(1143) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fe014130-bfb5-443b-9989-9c8f90d1065f"),
                columns: new[] { "PasswordHash", "RefreshTokenExpiryTime" },
                values: new object[] { "AQAAAAIAAYagAAAAEKxXNsl9wnQ1BJaydthqP0bVXyScV6GFi961x4ovLxg1fqeNeqVCEtELlpektV0Q9A==", new DateTime(2025, 7, 9, 6, 34, 40, 60, DateTimeKind.Utc).AddTicks(7755) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "ConversationId",
                table: "Messages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ParentId",
                table: "Conversations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("09097277-2705-40c2-bce5-51dbd1f4c1e6"),
                columns: new[] { "PasswordHash", "RefreshTokenExpiryTime" },
                values: new object[] { "AQAAAAIAAYagAAAAELoWRpUQ1Jt7IQgCSTtj4ze/IySNXpMFhL8R8VwrvDrl2nDrdcGwkVju9kMTvIu/9g==", new DateTime(2025, 7, 5, 17, 19, 34, 918, DateTimeKind.Utc).AddTicks(2334) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("33f41895-b601-4aa1-8dc4-8229a9d07008"),
                columns: new[] { "PasswordHash", "RefreshTokenExpiryTime" },
                values: new object[] { "AQAAAAIAAYagAAAAENcJ4QRSgJlZqjzIjwMYSbsZz0trx13sBHBX2c0srYmLmk03AtUYotA3hqRQUWDFUA==", new DateTime(2025, 7, 5, 17, 19, 34, 855, DateTimeKind.Utc).AddTicks(5815) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fe014130-bfb5-443b-9989-9c8f90d1065f"),
                columns: new[] { "PasswordHash", "RefreshTokenExpiryTime" },
                values: new object[] { "AQAAAAIAAYagAAAAECNIIq5wyj5xtdL0OhWZnrUW7FqGsWDHf/YyeRaO+2DZG8L7efr5izktScUQ+aTpkA==", new DateTime(2025, 7, 5, 17, 19, 34, 981, DateTimeKind.Utc).AddTicks(5748) });
        }
    }
}

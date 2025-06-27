using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduConnect.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDB1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_ParentId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "AIResponse",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "ParentId",
                table: "Messages",
                newName: "ConversationId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_ParentId",
                table: "Messages",
                newName: "IX_Messages_ConversationId");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Conversation",
                columns: table => new
                {
                    ConversationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversation", x => x.ConversationId);
                    table.ForeignKey(
                        name: "FK_Conversation_AspNetUsers_ParentId",
                        column: x => x.ParentId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("09097277-2705-40c2-bce5-51dbd1f4c1e6"),
                columns: new[] { "PasswordHash", "RefreshTokenExpiryTime" },
                values: new object[] { "AQAAAAIAAYagAAAAEJx8HRFllm8mQJ4JvkHIbcWh/qoS8Y/haCG+ifDMPIZq19YKjuhOgm53blWCBnB+tw==", new DateTime(2025, 7, 4, 3, 27, 4, 420, DateTimeKind.Utc).AddTicks(1426) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("33f41895-b601-4aa1-8dc4-8229a9d07008"),
                columns: new[] { "PasswordHash", "RefreshTokenExpiryTime" },
                values: new object[] { "AQAAAAIAAYagAAAAENggqPjUPhBJeTaHu0+S5t2oSs8AVGrgghgaEi5xItS6pDQ5J1penVXJKA6LQxgeQg==", new DateTime(2025, 7, 4, 3, 27, 4, 357, DateTimeKind.Utc).AddTicks(3444) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fe014130-bfb5-443b-9989-9c8f90d1065f"),
                columns: new[] { "PasswordHash", "RefreshTokenExpiryTime" },
                values: new object[] { "AQAAAAIAAYagAAAAEMqHgMwh8nloHHiPowcVarBQUZAe8JuzksX0sQKXS2iMFiyFRJfIntKBtc3D2v+s8Q==", new DateTime(2025, 7, 4, 3, 27, 4, 482, DateTimeKind.Utc).AddTicks(3002) });

            migrationBuilder.CreateIndex(
                name: "IX_Conversation_ParentId",
                table: "Conversation",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Conversation_ConversationId",
                table: "Messages",
                column: "ConversationId",
                principalTable: "Conversation",
                principalColumn: "ConversationId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Conversation_ConversationId",
                table: "Messages");

            migrationBuilder.DropTable(
                name: "Conversation");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "ConversationId",
                table: "Messages",
                newName: "ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_ConversationId",
                table: "Messages",
                newName: "IX_Messages_ParentId");

            migrationBuilder.AddColumn<string>(
                name: "AIResponse",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_ParentId",
                table: "Messages",
                column: "ParentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

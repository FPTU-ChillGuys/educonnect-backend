using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduConnect.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDB2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversation_AspNetUsers_ParentId",
                table: "Conversation");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Conversation_ConversationId",
                table: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Conversation",
                table: "Conversation");

            migrationBuilder.RenameTable(
                name: "Conversation",
                newName: "Conversations");

            migrationBuilder.RenameIndex(
                name: "IX_Conversation_ParentId",
                table: "Conversations",
                newName: "IX_Conversations_ParentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Conversations",
                table: "Conversations",
                column: "ConversationId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_AspNetUsers_ParentId",
                table: "Conversations",
                column: "ParentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Conversations_ConversationId",
                table: "Messages",
                column: "ConversationId",
                principalTable: "Conversations",
                principalColumn: "ConversationId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_AspNetUsers_ParentId",
                table: "Conversations");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Conversations_ConversationId",
                table: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Conversations",
                table: "Conversations");

            migrationBuilder.RenameTable(
                name: "Conversations",
                newName: "Conversation");

            migrationBuilder.RenameIndex(
                name: "IX_Conversations_ParentId",
                table: "Conversation",
                newName: "IX_Conversation_ParentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Conversation",
                table: "Conversation",
                column: "ConversationId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Conversation_AspNetUsers_ParentId",
                table: "Conversation",
                column: "ParentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Conversation_ConversationId",
                table: "Messages",
                column: "ConversationId",
                principalTable: "Conversation",
                principalColumn: "ConversationId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

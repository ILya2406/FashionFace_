using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FashionFace.Repositories.Context.Migrations
{
    /// <inheritdoc />
    public partial class RemovePositionIndexFromChatMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PositionIndex",
                table: "UserToUserChatMessageSendNotificationOutbox");

            migrationBuilder.DropColumn(
                name: "PositionIndex",
                table: "UserToUserChatMessage");

            migrationBuilder.DropColumn(
                name: "LastReadMessagePositionIndex",
                table: "UserToUserChatApplicationUser");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "UserToUserChatMessage",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastReadAt",
                table: "UserToUserChatApplicationUser",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "UserToUserChatMessage");

            migrationBuilder.DropColumn(
                name: "LastReadAt",
                table: "UserToUserChatApplicationUser");

            migrationBuilder.AddColumn<double>(
                name: "PositionIndex",
                table: "UserToUserChatMessageSendNotificationOutbox",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PositionIndex",
                table: "UserToUserChatMessage",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "LastReadMessagePositionIndex",
                table: "UserToUserChatApplicationUser",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}

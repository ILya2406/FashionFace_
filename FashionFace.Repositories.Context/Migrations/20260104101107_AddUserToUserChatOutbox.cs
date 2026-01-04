using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FashionFace.Repositories.Context.Migrations
{
    /// <inheritdoc />
    public partial class AddUserToUserChatOutbox : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "UserToUserChatMessage");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "UserToUserMessage",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "UserToUserChatMessageReadNotificationOutbox",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ChatId = table.Column<Guid>(type: "uuid", nullable: false),
                    InitiatorUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TargetUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    MessageId = table.Column<Guid>(type: "uuid", nullable: false),
                    OutboxStatus = table.Column<int>(type: "integer", nullable: false),
                    AttemptCount = table.Column<int>(type: "integer", nullable: false),
                    ProcessingStartedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToUserChatMessageReadNotificationOutbox", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserToUserChatMessageReadNotificationOutbox_AspNetUsers_Ini~",
                        column: x => x.InitiatorUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserChatMessageReadNotificationOutbox_AspNetUsers_Tar~",
                        column: x => x.TargetUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserChatMessageReadNotificationOutbox_UserToUserChatM~",
                        column: x => x.MessageId,
                        principalTable: "UserToUserChatMessage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserChatMessageReadNotificationOutbox_UserToUserChat_~",
                        column: x => x.ChatId,
                        principalTable: "UserToUserChat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserToUserChatMessageReadOutbox",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ChatId = table.Column<Guid>(type: "uuid", nullable: false),
                    InitiatorUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    MessageId = table.Column<Guid>(type: "uuid", nullable: false),
                    OutboxStatus = table.Column<int>(type: "integer", nullable: false),
                    AttemptCount = table.Column<int>(type: "integer", nullable: false),
                    ProcessingStartedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToUserChatMessageReadOutbox", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserToUserChatMessageReadOutbox_AspNetUsers_InitiatorUserId",
                        column: x => x.InitiatorUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserChatMessageReadOutbox_UserToUserChatMessage_Messa~",
                        column: x => x.MessageId,
                        principalTable: "UserToUserChatMessage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserChatMessageReadOutbox_UserToUserChat_ChatId",
                        column: x => x.ChatId,
                        principalTable: "UserToUserChat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserToUserChatMessageSendNotificationOutbox",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ChatId = table.Column<Guid>(type: "uuid", nullable: false),
                    InitiatorUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TargetUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    MessageId = table.Column<Guid>(type: "uuid", nullable: false),
                    MessageValue = table.Column<string>(type: "text", nullable: false),
                    PositionIndex = table.Column<double>(type: "double precision", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    OutboxStatus = table.Column<int>(type: "integer", nullable: false),
                    AttemptCount = table.Column<int>(type: "integer", nullable: false),
                    ProcessingStartedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToUserChatMessageSendNotificationOutbox", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserToUserChatMessageSendNotificationOutbox_AspNetUsers_Ini~",
                        column: x => x.InitiatorUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserChatMessageSendNotificationOutbox_AspNetUsers_Tar~",
                        column: x => x.TargetUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserChatMessageSendNotificationOutbox_UserToUserChatM~",
                        column: x => x.MessageId,
                        principalTable: "UserToUserChatMessage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserChatMessageSendNotificationOutbox_UserToUserChat_~",
                        column: x => x.ChatId,
                        principalTable: "UserToUserChat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserToUserChatMessageSendOutbox",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ChatId = table.Column<Guid>(type: "uuid", nullable: false),
                    InitiatorUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    MessageId = table.Column<Guid>(type: "uuid", nullable: false),
                    OutboxStatus = table.Column<int>(type: "integer", nullable: false),
                    AttemptCount = table.Column<int>(type: "integer", nullable: false),
                    ProcessingStartedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToUserChatMessageSendOutbox", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserToUserChatMessageSendOutbox_AspNetUsers_InitiatorUserId",
                        column: x => x.InitiatorUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserChatMessageSendOutbox_UserToUserChatMessage_Messa~",
                        column: x => x.MessageId,
                        principalTable: "UserToUserChatMessage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserChatMessageSendOutbox_UserToUserChat_ChatId",
                        column: x => x.ChatId,
                        principalTable: "UserToUserChat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatMessageReadNotificationOutbox_ChatId",
                table: "UserToUserChatMessageReadNotificationOutbox",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatMessageReadNotificationOutbox_InitiatorUserId",
                table: "UserToUserChatMessageReadNotificationOutbox",
                column: "InitiatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatMessageReadNotificationOutbox_MessageId",
                table: "UserToUserChatMessageReadNotificationOutbox",
                column: "MessageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatMessageReadNotificationOutbox_TargetUserId",
                table: "UserToUserChatMessageReadNotificationOutbox",
                column: "TargetUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatMessageReadOutbox_ChatId",
                table: "UserToUserChatMessageReadOutbox",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatMessageReadOutbox_InitiatorUserId",
                table: "UserToUserChatMessageReadOutbox",
                column: "InitiatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatMessageReadOutbox_MessageId",
                table: "UserToUserChatMessageReadOutbox",
                column: "MessageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatMessageSendNotificationOutbox_ChatId",
                table: "UserToUserChatMessageSendNotificationOutbox",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatMessageSendNotificationOutbox_InitiatorUserId",
                table: "UserToUserChatMessageSendNotificationOutbox",
                column: "InitiatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatMessageSendNotificationOutbox_MessageId",
                table: "UserToUserChatMessageSendNotificationOutbox",
                column: "MessageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatMessageSendNotificationOutbox_TargetUserId",
                table: "UserToUserChatMessageSendNotificationOutbox",
                column: "TargetUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatMessageSendOutbox_ChatId",
                table: "UserToUserChatMessageSendOutbox",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatMessageSendOutbox_InitiatorUserId",
                table: "UserToUserChatMessageSendOutbox",
                column: "InitiatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatMessageSendOutbox_MessageId",
                table: "UserToUserChatMessageSendOutbox",
                column: "MessageId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserToUserChatMessageReadNotificationOutbox");

            migrationBuilder.DropTable(
                name: "UserToUserChatMessageReadOutbox");

            migrationBuilder.DropTable(
                name: "UserToUserChatMessageSendNotificationOutbox");

            migrationBuilder.DropTable(
                name: "UserToUserChatMessageSendOutbox");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "UserToUserMessage");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "UserToUserChatMessage",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}

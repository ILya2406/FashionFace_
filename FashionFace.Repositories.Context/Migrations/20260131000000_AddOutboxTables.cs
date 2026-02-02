using System;

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FashionFace.Repositories.Context.Migrations
{
    /// <inheritdoc />
    public partial class AddOutboxTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // UserToUserChatMessageSendOutbox
            migrationBuilder.CreateTable(
                name: "UserToUserChatMessageSendOutbox",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ChatId = table.Column<Guid>(type: "uuid", nullable: false),
                    InitiatorUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    MessageId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CorrelationId = table.Column<Guid>(type: "uuid", nullable: false),
                    OutboxStatus = table.Column<string>(type: "varchar(16)", nullable: false),
                    AttemptCount = table.Column<int>(type: "integer", nullable: false),
                    ClaimedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
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

            // UserToUserChatMessageReadOutbox
            migrationBuilder.CreateTable(
                name: "UserToUserChatMessageReadOutbox",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ChatId = table.Column<Guid>(type: "uuid", nullable: false),
                    InitiatorUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    MessageId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CorrelationId = table.Column<Guid>(type: "uuid", nullable: false),
                    OutboxStatus = table.Column<string>(type: "varchar(16)", nullable: false),
                    AttemptCount = table.Column<int>(type: "integer", nullable: false),
                    ClaimedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
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
                        name: "FK_UserToUserChatMessageReadOutbox_UserToUserChatMessage_Messag~",
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

            // UserToUserChatMessageSendNotificationOutbox
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
                    MessageCreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CorrelationId = table.Column<Guid>(type: "uuid", nullable: false),
                    OutboxStatus = table.Column<string>(type: "varchar(16)", nullable: false),
                    AttemptCount = table.Column<int>(type: "integer", nullable: false),
                    ClaimedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToUserChatMessageSendNotificationOutbox", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserToUserChatMessageSendNotificationOutbox_AspNetUsers_Init~",
                        column: x => x.InitiatorUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserChatMessageSendNotificationOutbox_AspNetUsers_Targ~",
                        column: x => x.TargetUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserChatMessageSendNotificationOutbox_UserToUserChatMe~",
                        column: x => x.MessageId,
                        principalTable: "UserToUserChatMessage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserChatMessageSendNotificationOutbox_UserToUserChat_C~",
                        column: x => x.ChatId,
                        principalTable: "UserToUserChat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatMessageSendNotificationOutbox_TargetUserId",
                table: "UserToUserChatMessageSendNotificationOutbox",
                column: "TargetUserId");

            // UserToUserChatMessageReadNotificationOutbox
            migrationBuilder.CreateTable(
                name: "UserToUserChatMessageReadNotificationOutbox",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ChatId = table.Column<Guid>(type: "uuid", nullable: false),
                    InitiatorUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TargetUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    MessageId = table.Column<Guid>(type: "uuid", nullable: false),
                    MessageValue = table.Column<string>(type: "text", nullable: false),
                    MessageCreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CorrelationId = table.Column<Guid>(type: "uuid", nullable: false),
                    OutboxStatus = table.Column<string>(type: "varchar(16)", nullable: false),
                    AttemptCount = table.Column<int>(type: "integer", nullable: false),
                    ClaimedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToUserChatMessageReadNotificationOutbox", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserToUserChatMessageReadNotificationOutbox_AspNetUsers_Init~",
                        column: x => x.InitiatorUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserChatMessageReadNotificationOutbox_AspNetUsers_Targ~",
                        column: x => x.TargetUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserChatMessageReadNotificationOutbox_UserToUserChatMe~",
                        column: x => x.MessageId,
                        principalTable: "UserToUserChatMessage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserChatMessageReadNotificationOutbox_UserToUserChat_C~",
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
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatMessageReadNotificationOutbox_TargetUserId",
                table: "UserToUserChatMessageReadNotificationOutbox",
                column: "TargetUserId");

            // UserToUserChatInvitationAcceptedOutbox
            migrationBuilder.CreateTable(
                name: "UserToUserChatInvitationAcceptedOutbox",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InvitationId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChatId = table.Column<Guid>(type: "uuid", nullable: false),
                    InitiatorUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TargetUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CorrelationId = table.Column<Guid>(type: "uuid", nullable: false),
                    OutboxStatus = table.Column<string>(type: "varchar(16)", nullable: false),
                    AttemptCount = table.Column<int>(type: "integer", nullable: false),
                    ClaimedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToUserChatInvitationAcceptedOutbox", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserToUserChatInvitationAcceptedOutbox_AspNetUsers_Initiato~",
                        column: x => x.InitiatorUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserChatInvitationAcceptedOutbox_AspNetUsers_TargetUs~",
                        column: x => x.TargetUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserChatInvitationAcceptedOutbox_UserToUserChatInvita~",
                        column: x => x.InvitationId,
                        principalTable: "UserToUserChatInvitation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserChatInvitationAcceptedOutbox_UserToUserChat_ChatI~",
                        column: x => x.ChatId,
                        principalTable: "UserToUserChat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatInvitationAcceptedOutbox_ChatId",
                table: "UserToUserChatInvitationAcceptedOutbox",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatInvitationAcceptedOutbox_InitiatorUserId",
                table: "UserToUserChatInvitationAcceptedOutbox",
                column: "InitiatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatInvitationAcceptedOutbox_InvitationId",
                table: "UserToUserChatInvitationAcceptedOutbox",
                column: "InvitationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatInvitationAcceptedOutbox_TargetUserId",
                table: "UserToUserChatInvitationAcceptedOutbox",
                column: "TargetUserId");

            // UserToUserChatInvitationCanceledOutbox
            migrationBuilder.CreateTable(
                name: "UserToUserChatInvitationCanceledOutbox",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InvitationId = table.Column<Guid>(type: "uuid", nullable: false),
                    InitiatorUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TargetUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CorrelationId = table.Column<Guid>(type: "uuid", nullable: false),
                    OutboxStatus = table.Column<string>(type: "varchar(16)", nullable: false),
                    AttemptCount = table.Column<int>(type: "integer", nullable: false),
                    ClaimedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToUserChatInvitationCanceledOutbox", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserToUserChatInvitationCanceledOutbox_AspNetUsers_Initiato~",
                        column: x => x.InitiatorUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserChatInvitationCanceledOutbox_AspNetUsers_TargetUs~",
                        column: x => x.TargetUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserChatInvitationCanceledOutbox_UserToUserChatInvita~",
                        column: x => x.InvitationId,
                        principalTable: "UserToUserChatInvitation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatInvitationCanceledOutbox_InitiatorUserId",
                table: "UserToUserChatInvitationCanceledOutbox",
                column: "InitiatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatInvitationCanceledOutbox_InvitationId",
                table: "UserToUserChatInvitationCanceledOutbox",
                column: "InvitationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatInvitationCanceledOutbox_TargetUserId",
                table: "UserToUserChatInvitationCanceledOutbox",
                column: "TargetUserId");

            // UserToUserChatInvitationCreatedOutbox
            migrationBuilder.CreateTable(
                name: "UserToUserChatInvitationCreatedOutbox",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InvitationId = table.Column<Guid>(type: "uuid", nullable: false),
                    InitiatorUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TargetUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CorrelationId = table.Column<Guid>(type: "uuid", nullable: false),
                    OutboxStatus = table.Column<string>(type: "varchar(16)", nullable: false),
                    AttemptCount = table.Column<int>(type: "integer", nullable: false),
                    ClaimedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToUserChatInvitationCreatedOutbox", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserToUserChatInvitationCreatedOutbox_AspNetUsers_Initiator~",
                        column: x => x.InitiatorUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserChatInvitationCreatedOutbox_AspNetUsers_TargetUse~",
                        column: x => x.TargetUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserChatInvitationCreatedOutbox_UserToUserChatInvitat~",
                        column: x => x.InvitationId,
                        principalTable: "UserToUserChatInvitation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatInvitationCreatedOutbox_InitiatorUserId",
                table: "UserToUserChatInvitationCreatedOutbox",
                column: "InitiatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatInvitationCreatedOutbox_InvitationId",
                table: "UserToUserChatInvitationCreatedOutbox",
                column: "InvitationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatInvitationCreatedOutbox_TargetUserId",
                table: "UserToUserChatInvitationCreatedOutbox",
                column: "TargetUserId");

            // UserToUserChatInvitationRejectedOutbox
            migrationBuilder.CreateTable(
                name: "UserToUserChatInvitationRejectedOutbox",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InvitationId = table.Column<Guid>(type: "uuid", nullable: false),
                    InitiatorUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TargetUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CorrelationId = table.Column<Guid>(type: "uuid", nullable: false),
                    OutboxStatus = table.Column<string>(type: "varchar(16)", nullable: false),
                    AttemptCount = table.Column<int>(type: "integer", nullable: false),
                    ClaimedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToUserChatInvitationRejectedOutbox", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserToUserChatInvitationRejectedOutbox_AspNetUsers_Initiato~",
                        column: x => x.InitiatorUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserChatInvitationRejectedOutbox_AspNetUsers_TargetUs~",
                        column: x => x.TargetUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserChatInvitationRejectedOutbox_UserToUserChatInvita~",
                        column: x => x.InvitationId,
                        principalTable: "UserToUserChatInvitation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatInvitationRejectedOutbox_InitiatorUserId",
                table: "UserToUserChatInvitationRejectedOutbox",
                column: "InitiatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatInvitationRejectedOutbox_InvitationId",
                table: "UserToUserChatInvitationRejectedOutbox",
                column: "InvitationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatInvitationRejectedOutbox_TargetUserId",
                table: "UserToUserChatInvitationRejectedOutbox",
                column: "TargetUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserToUserChatMessageSendOutbox");

            migrationBuilder.DropTable(
                name: "UserToUserChatMessageReadOutbox");

            migrationBuilder.DropTable(
                name: "UserToUserChatMessageSendNotificationOutbox");

            migrationBuilder.DropTable(
                name: "UserToUserChatMessageReadNotificationOutbox");

            migrationBuilder.DropTable(
                name: "UserToUserChatInvitationAcceptedOutbox");

            migrationBuilder.DropTable(
                name: "UserToUserChatInvitationCanceledOutbox");

            migrationBuilder.DropTable(
                name: "UserToUserChatInvitationCreatedOutbox");

            migrationBuilder.DropTable(
                name: "UserToUserChatInvitationRejectedOutbox");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FashionFace.Repositories.Context.Migrations
{
    /// <inheritdoc />
    public partial class AddNotificationOutboxEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserToUserChatInvitationAcceptedNotificationOutbox",
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
                    table.PrimaryKey("PK_UserToUserChatInvitationAcceptedNotificationOutbox", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserToUserChatInvitationAcceptedNotificationOutbox_AspNetUs~",
                        column: x => x.InitiatorUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserChatInvitationAcceptedNotificationOutbox_AspNetU~1",
                        column: x => x.TargetUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserChatInvitationAcceptedNotificationOutbox_UserToUs~",
                        column: x => x.ChatId,
                        principalTable: "UserToUserChat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserChatInvitationAcceptedNotificationOutbox_UserToU~1",
                        column: x => x.InvitationId,
                        principalTable: "UserToUserChatInvitation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserToUserChatInvitationCanceledNotificationOutbox",
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
                    table.PrimaryKey("PK_UserToUserChatInvitationCanceledNotificationOutbox", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserToUserChatInvitationCanceledNotificationOutbox_AspNetUs~",
                        column: x => x.InitiatorUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserChatInvitationCanceledNotificationOutbox_AspNetU~1",
                        column: x => x.TargetUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserChatInvitationCanceledNotificationOutbox_UserToUs~",
                        column: x => x.InvitationId,
                        principalTable: "UserToUserChatInvitation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserToUserChatInvitationCreatedNotificationOutbox",
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
                    table.PrimaryKey("PK_UserToUserChatInvitationCreatedNotificationOutbox", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserToUserChatInvitationCreatedNotificationOutbox_AspNetUse~",
                        column: x => x.InitiatorUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserChatInvitationCreatedNotificationOutbox_AspNetUs~1",
                        column: x => x.TargetUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserChatInvitationCreatedNotificationOutbox_UserToUse~",
                        column: x => x.InvitationId,
                        principalTable: "UserToUserChatInvitation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserToUserChatInvitationRejectedNotificationOutbox",
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
                    table.PrimaryKey("PK_UserToUserChatInvitationRejectedNotificationOutbox", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserToUserChatInvitationRejectedNotificationOutbox_AspNetUs~",
                        column: x => x.InitiatorUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserChatInvitationRejectedNotificationOutbox_AspNetU~1",
                        column: x => x.TargetUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserChatInvitationRejectedNotificationOutbox_UserToUs~",
                        column: x => x.InvitationId,
                        principalTable: "UserToUserChatInvitation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatInvitationAcceptedNotificationOutbox_ChatId",
                table: "UserToUserChatInvitationAcceptedNotificationOutbox",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatInvitationAcceptedNotificationOutbox_Initiato~",
                table: "UserToUserChatInvitationAcceptedNotificationOutbox",
                column: "InitiatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatInvitationAcceptedNotificationOutbox_Invitati~",
                table: "UserToUserChatInvitationAcceptedNotificationOutbox",
                column: "InvitationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatInvitationAcceptedNotificationOutbox_TargetUs~",
                table: "UserToUserChatInvitationAcceptedNotificationOutbox",
                column: "TargetUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatInvitationCanceledNotificationOutbox_Initiato~",
                table: "UserToUserChatInvitationCanceledNotificationOutbox",
                column: "InitiatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatInvitationCanceledNotificationOutbox_Invitati~",
                table: "UserToUserChatInvitationCanceledNotificationOutbox",
                column: "InvitationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatInvitationCanceledNotificationOutbox_TargetUs~",
                table: "UserToUserChatInvitationCanceledNotificationOutbox",
                column: "TargetUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatInvitationCreatedNotificationOutbox_Initiator~",
                table: "UserToUserChatInvitationCreatedNotificationOutbox",
                column: "InitiatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatInvitationCreatedNotificationOutbox_Invitatio~",
                table: "UserToUserChatInvitationCreatedNotificationOutbox",
                column: "InvitationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatInvitationCreatedNotificationOutbox_TargetUse~",
                table: "UserToUserChatInvitationCreatedNotificationOutbox",
                column: "TargetUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatInvitationRejectedNotificationOutbox_Initiato~",
                table: "UserToUserChatInvitationRejectedNotificationOutbox",
                column: "InitiatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatInvitationRejectedNotificationOutbox_Invitati~",
                table: "UserToUserChatInvitationRejectedNotificationOutbox",
                column: "InvitationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatInvitationRejectedNotificationOutbox_TargetUs~",
                table: "UserToUserChatInvitationRejectedNotificationOutbox",
                column: "TargetUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserToUserChatInvitationAcceptedNotificationOutbox");

            migrationBuilder.DropTable(
                name: "UserToUserChatInvitationCanceledNotificationOutbox");

            migrationBuilder.DropTable(
                name: "UserToUserChatInvitationCreatedNotificationOutbox");

            migrationBuilder.DropTable(
                name: "UserToUserChatInvitationRejectedNotificationOutbox");
        }
    }
}

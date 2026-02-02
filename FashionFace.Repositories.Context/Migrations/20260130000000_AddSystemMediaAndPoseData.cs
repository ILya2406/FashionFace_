using System;

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FashionFace.Repositories.Context.Migrations
{
    /// <inheritdoc />
    public partial class AddSystemMediaAndPoseData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Create SystemMediaFile table
            migrationBuilder.CreateTable(
                name: "SystemMediaFile",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FileResourceId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemMediaFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SystemMediaFile_FileResource_FileResourceId",
                        column: x => x.FileResourceId,
                        principalTable: "FileResource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            // Create SystemMedia table
            migrationBuilder.CreateTable(
                name: "SystemMedia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OriginalFileId = table.Column<Guid>(type: "uuid", nullable: false),
                    OptimizedFileId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemMedia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SystemMedia_SystemMediaFile_OriginalFileId",
                        column: x => x.OriginalFileId,
                        principalTable: "SystemMediaFile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SystemMedia_SystemMediaFile_OptimizedFileId",
                        column: x => x.OptimizedFileId,
                        principalTable: "SystemMediaFile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            // Create SystemMediaAggregate table
            migrationBuilder.CreateTable(
                name: "SystemMediaAggregate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    PreviewMediaId = table.Column<Guid>(type: "uuid", nullable: false),
                    OriginalMediaId = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemMediaAggregate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SystemMediaAggregate_SystemMedia_PreviewMediaId",
                        column: x => x.PreviewMediaId,
                        principalTable: "SystemMedia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SystemMediaAggregate_SystemMedia_OriginalMediaId",
                        column: x => x.OriginalMediaId,
                        principalTable: "SystemMedia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            // Rename MediaAggregateId to SystemMediaAggregateId in PoseReferenceProjection
            migrationBuilder.RenameColumn(
                name: "MediaAggregateId",
                table: "PoseReferenceProjection",
                newName: "SystemMediaAggregateId");

            migrationBuilder.RenameIndex(
                name: "IX_PoseReferenceProjection_MediaAggregateId",
                table: "PoseReferenceProjection",
                newName: "IX_PoseReferenceProjection_SystemMediaAggregateId");

            // Add foreign key to SystemMediaAggregate
            migrationBuilder.DropForeignKey(
                name: "FK_PoseReferenceProjection_MediaAggregate_MediaAggregateId",
                table: "PoseReferenceProjection");

            migrationBuilder.AddForeignKey(
                name: "FK_PoseReferenceProjection_SystemMediaAggregate_SystemMediaAggregateId",
                table: "PoseReferenceProjection",
                column: "SystemMediaAggregateId",
                principalTable: "SystemMediaAggregate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            // Create indexes
            migrationBuilder.CreateIndex(
                name: "IX_SystemMediaFile_FileResourceId",
                table: "SystemMediaFile",
                column: "FileResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemMedia_OriginalFileId",
                table: "SystemMedia",
                column: "OriginalFileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SystemMedia_OptimizedFileId",
                table: "SystemMedia",
                column: "OptimizedFileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SystemMediaAggregate_PreviewMediaId",
                table: "SystemMediaAggregate",
                column: "PreviewMediaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SystemMediaAggregate_OriginalMediaId",
                table: "SystemMediaAggregate",
                column: "OriginalMediaId",
                unique: true);

            // Add pose data using raw SQL
            migrationBuilder.Sql(@"
                -- Preview image FileResource
                INSERT INTO ""FileResource"" (""Id"", ""RelativePath"", ""FileName"", ""SizeBytes"", ""ContentType"", ""CreatedAt"", ""IsDeleted"", ""HashSha256"")
                VALUES ('a1b2c3d4-e5f6-7890-abcd-ef1234567890', '/models/yoga-tree-preview.jpg', 'yoga-tree-preview.jpg', 0, 'image/jpeg', '2026-01-01 00:00:00+00', false, 'placeholder-hash-preview');

                -- 3D model FileResource
                INSERT INTO ""FileResource"" (""Id"", ""RelativePath"", ""FileName"", ""SizeBytes"", ""ContentType"", ""CreatedAt"", ""IsDeleted"", ""HashSha256"")
                VALUES ('c4d5e6f7-a8b9-0123-cdef-456789abcdef', '/models/yoga-tree-pose.obj', 'yoga-tree-pose.obj', 0, 'model/obj', '2026-01-01 00:00:00+00', false, 'placeholder-hash-model');

                -- PoseReference with preview image
                INSERT INTO ""PoseReference"" (""Id"", ""FileResourceId"", ""IsDeleted"", ""CreatedAt"", ""Description"")
                VALUES ('b2c3d4e5-f6a7-8901-bcde-f12345678901', 'a1b2c3d4-e5f6-7890-abcd-ef1234567890', false, '2026-01-01 00:00:00+00', 'Yoga Pose - Tree Position');

                -- SystemMediaFile for 3D model
                INSERT INTO ""SystemMediaFile"" (""Id"", ""FileResourceId"", ""CreatedAt"")
                VALUES ('a8b9c0d1-e2f3-4567-89ab-cdef01234567', 'c4d5e6f7-a8b9-0123-cdef-456789abcdef', '2026-01-01 00:00:00+00');

                -- SystemMedia for 3D model
                INSERT INTO ""SystemMedia"" (""Id"", ""OriginalFileId"", ""OptimizedFileId"", ""IsDeleted"", ""CreatedAt"")
                VALUES ('d5e6f7a8-b9c0-1234-def5-6789abcdef01', 'a8b9c0d1-e2f3-4567-89ab-cdef01234567', 'a8b9c0d1-e2f3-4567-89ab-cdef01234567', false, '2026-01-01 00:00:00+00');

                -- SystemMediaAggregate for 3D model
                INSERT INTO ""SystemMediaAggregate"" (""Id"", ""PreviewMediaId"", ""OriginalMediaId"", ""Description"", ""IsDeleted"", ""CreatedAt"")
                VALUES ('e6f7a8b9-c0d1-2345-ef67-89abcdef0123', 'd5e6f7a8-b9c0-1234-def5-6789abcdef01', 'd5e6f7a8-b9c0-1234-def5-6789abcdef01', 'Yoga Tree Pose 3D Model', false, '2026-01-01 00:00:00+00');

                -- PoseReferenceProjection linking pose to 3D model
                INSERT INTO ""PoseReferenceProjection"" (""Id"", ""PoseReferenceId"", ""SystemMediaAggregateId"")
                VALUES ('f7a8b9c0-d1e2-3456-f789-abcdef012345', 'b2c3d4e5-f6a7-8901-bcde-f12345678901', 'e6f7a8b9-c0d1-2345-ef67-89abcdef0123');
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Delete pose data using raw SQL
            migrationBuilder.Sql(@"
                DELETE FROM ""PoseReferenceProjection"" WHERE ""Id"" = 'f7a8b9c0-d1e2-3456-f789-abcdef012345';
                DELETE FROM ""SystemMediaAggregate"" WHERE ""Id"" = 'e6f7a8b9-c0d1-2345-ef67-89abcdef0123';
                DELETE FROM ""SystemMedia"" WHERE ""Id"" = 'd5e6f7a8-b9c0-1234-def5-6789abcdef01';
                DELETE FROM ""SystemMediaFile"" WHERE ""Id"" = 'a8b9c0d1-e2f3-4567-89ab-cdef01234567';
                DELETE FROM ""PoseReference"" WHERE ""Id"" = 'b2c3d4e5-f6a7-8901-bcde-f12345678901';
                DELETE FROM ""FileResource"" WHERE ""Id"" = 'a1b2c3d4-e5f6-7890-abcd-ef1234567890';
                DELETE FROM ""FileResource"" WHERE ""Id"" = 'c4d5e6f7-a8b9-0123-cdef-456789abcdef';
            ");

            // Rename back SystemMediaAggregateId to MediaAggregateId
            migrationBuilder.DropForeignKey(
                name: "FK_PoseReferenceProjection_SystemMediaAggregate_SystemMediaAggregateId",
                table: "PoseReferenceProjection");

            migrationBuilder.RenameColumn(
                name: "SystemMediaAggregateId",
                table: "PoseReferenceProjection",
                newName: "MediaAggregateId");

            migrationBuilder.RenameIndex(
                name: "IX_PoseReferenceProjection_SystemMediaAggregateId",
                table: "PoseReferenceProjection",
                newName: "IX_PoseReferenceProjection_MediaAggregateId");

            migrationBuilder.AddForeignKey(
                name: "FK_PoseReferenceProjection_MediaAggregate_MediaAggregateId",
                table: "PoseReferenceProjection",
                column: "MediaAggregateId",
                principalTable: "MediaAggregate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            // Drop SystemMedia tables
            migrationBuilder.DropTable(
                name: "SystemMediaAggregate");

            migrationBuilder.DropTable(
                name: "SystemMedia");

            migrationBuilder.DropTable(
                name: "SystemMediaFile");
        }
    }
}

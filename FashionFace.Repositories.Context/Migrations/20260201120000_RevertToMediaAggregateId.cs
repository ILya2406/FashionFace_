using System;

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FashionFace.Repositories.Context.Migrations
{
    /// <inheritdoc />
    public partial class RevertToMediaAggregateId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Delete old PoseReferenceProjection data (linked to SystemMediaAggregate)
            migrationBuilder.Sql(@"
                DELETE FROM ""PoseReferenceProjection"";
            ");

            // 2. Drop FK to SystemMediaAggregate
            migrationBuilder.DropForeignKey(
                name: "FK_PoseReferenceProjection_SystemMediaAggregate_SystemMediaAggr",
                table: "PoseReferenceProjection");

            // 3. Rename column back to MediaAggregateId
            migrationBuilder.RenameColumn(
                name: "SystemMediaAggregateId",
                table: "PoseReferenceProjection",
                newName: "MediaAggregateId");

            migrationBuilder.RenameIndex(
                name: "IX_PoseReferenceProjection_SystemMediaAggregateId",
                table: "PoseReferenceProjection",
                newName: "IX_PoseReferenceProjection_MediaAggregateId");

            // 4. Add FK to MediaAggregate
            migrationBuilder.AddForeignKey(
                name: "FK_PoseReferenceProjection_MediaAggregate_MediaAggregateId",
                table: "PoseReferenceProjection",
                column: "MediaAggregateId",
                principalTable: "MediaAggregate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            // 5. Create MediaAggregate for pose preview (using existing FileResources)
            migrationBuilder.Sql(@"
                -- MediaFile for preview image
                INSERT INTO ""MediaFile"" (""Id"", ""FileResourceId"", ""CreatedAt"")
                VALUES ('11111111-1111-1111-1111-111111111111', 'a1b2c3d4-e5f6-7890-abcd-ef1234567890', '2026-01-01 00:00:00+00');

                -- Media for preview
                INSERT INTO ""Media"" (""Id"", ""OriginalFileId"", ""OptimizedFileId"", ""IsDeleted"")
                VALUES ('22222222-2222-2222-2222-222222222222', '11111111-1111-1111-1111-111111111111', '11111111-1111-1111-1111-111111111111', false);

                -- MediaAggregate for pose
                INSERT INTO ""MediaAggregate"" (""Id"", ""PreviewMediaId"", ""OriginalMediaId"", ""Description"", ""IsDeleted"")
                VALUES ('33333333-3333-3333-3333-333333333333', '22222222-2222-2222-2222-222222222222', '22222222-2222-2222-2222-222222222222', 'Yoga Tree Pose Preview', false);

                -- Re-create PoseReferenceProjection with MediaAggregateId
                INSERT INTO ""PoseReferenceProjection"" (""Id"", ""PoseReferenceId"", ""MediaAggregateId"")
                VALUES ('f7a8b9c0-d1e2-3456-f789-abcdef012345', 'b2c3d4e5-f6a7-8901-bcde-f12345678901', '33333333-3333-3333-3333-333333333333');
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Delete new data
            migrationBuilder.Sql(@"
                DELETE FROM ""PoseReferenceProjection"" WHERE ""Id"" = 'f7a8b9c0-d1e2-3456-f789-abcdef012345';
                DELETE FROM ""MediaAggregate"" WHERE ""Id"" = '33333333-3333-3333-3333-333333333333';
                DELETE FROM ""Media"" WHERE ""Id"" = '22222222-2222-2222-2222-222222222222';
                DELETE FROM ""MediaFile"" WHERE ""Id"" = '11111111-1111-1111-1111-111111111111';
            ");

            // Revert FK
            migrationBuilder.DropForeignKey(
                name: "FK_PoseReferenceProjection_MediaAggregate_MediaAggregateId",
                table: "PoseReferenceProjection");

            // Rename back
            migrationBuilder.RenameColumn(
                name: "MediaAggregateId",
                table: "PoseReferenceProjection",
                newName: "SystemMediaAggregateId");

            migrationBuilder.RenameIndex(
                name: "IX_PoseReferenceProjection_MediaAggregateId",
                table: "PoseReferenceProjection",
                newName: "IX_PoseReferenceProjection_SystemMediaAggregateId");

            // Add FK to SystemMediaAggregate
            migrationBuilder.AddForeignKey(
                name: "FK_PoseReferenceProjection_SystemMediaAggregate_SystemMediaAggr",
                table: "PoseReferenceProjection",
                column: "SystemMediaAggregateId",
                principalTable: "SystemMediaAggregate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            // Re-create old data
            migrationBuilder.Sql(@"
                INSERT INTO ""PoseReferenceProjection"" (""Id"", ""PoseReferenceId"", ""SystemMediaAggregateId"")
                VALUES ('f7a8b9c0-d1e2-3456-f789-abcdef012345', 'b2c3d4e5-f6a7-8901-bcde-f12345678901', 'e6f7a8b9-c0d1-2345-ef67-89abcdef0123');
            ");
        }
    }
}

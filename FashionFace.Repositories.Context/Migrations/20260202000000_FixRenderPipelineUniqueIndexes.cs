using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FashionFace.Repositories.Context.Migrations
{
    /// <inheritdoc />
    public partial class FixRenderPipelineUniqueIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop old UNIQUE indexes (1:1 relationship)
            migrationBuilder.DropIndex(
                name: "IX_RenderPipeline_ApplicationUserId",
                table: "RenderPipeline");

            migrationBuilder.DropIndex(
                name: "IX_RenderPipeline_PoseReferenceId",
                table: "RenderPipeline");

            migrationBuilder.DropIndex(
                name: "IX_RenderPipeline_ProductMediaAggregateId",
                table: "RenderPipeline");

            migrationBuilder.DropIndex(
                name: "IX_RenderPipeline_TalentId",
                table: "RenderPipeline");

            // Create non-unique indexes (Many:1 relationship)
            migrationBuilder.CreateIndex(
                name: "IX_RenderPipeline_ApplicationUserId",
                table: "RenderPipeline",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RenderPipeline_PoseReferenceId",
                table: "RenderPipeline",
                column: "PoseReferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_RenderPipeline_ProductMediaAggregateId",
                table: "RenderPipeline",
                column: "ProductMediaAggregateId");

            migrationBuilder.CreateIndex(
                name: "IX_RenderPipeline_TalentId",
                table: "RenderPipeline",
                column: "TalentId");

            // Create composite unique index (same user can create multiple pipelines with different poses/products)
            migrationBuilder.CreateIndex(
                name: "IX_RenderPipeline_Composite_Unique",
                table: "RenderPipeline",
                columns: new[] { "ApplicationUserId", "TalentId", "PoseReferenceId", "ProductMediaAggregateId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop composite index
            migrationBuilder.DropIndex(
                name: "IX_RenderPipeline_Composite_Unique",
                table: "RenderPipeline");

            // Drop non-unique indexes
            migrationBuilder.DropIndex(
                name: "IX_RenderPipeline_ApplicationUserId",
                table: "RenderPipeline");

            migrationBuilder.DropIndex(
                name: "IX_RenderPipeline_PoseReferenceId",
                table: "RenderPipeline");

            migrationBuilder.DropIndex(
                name: "IX_RenderPipeline_ProductMediaAggregateId",
                table: "RenderPipeline");

            migrationBuilder.DropIndex(
                name: "IX_RenderPipeline_TalentId",
                table: "RenderPipeline");

            // Recreate old UNIQUE indexes
            migrationBuilder.CreateIndex(
                name: "IX_RenderPipeline_ApplicationUserId",
                table: "RenderPipeline",
                column: "ApplicationUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RenderPipeline_PoseReferenceId",
                table: "RenderPipeline",
                column: "PoseReferenceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RenderPipeline_ProductMediaAggregateId",
                table: "RenderPipeline",
                column: "ProductMediaAggregateId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RenderPipeline_TalentId",
                table: "RenderPipeline",
                column: "TalentId",
                unique: true);
        }
    }
}

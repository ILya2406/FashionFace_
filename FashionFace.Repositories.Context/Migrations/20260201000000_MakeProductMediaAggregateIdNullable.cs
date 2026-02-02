using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FashionFace.Repositories.Context.Migrations;

public partial class MakeProductMediaAggregateIdNullable : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql(@"
            ALTER TABLE ""RenderPipeline""
            ALTER COLUMN ""ProductMediaAggregateId"" DROP NOT NULL;
        ");

        migrationBuilder.Sql(@"
            ALTER TABLE ""RenderPipelineAttemptCreateRequestTask""
            ALTER COLUMN ""ProductMediaAggregateId"" DROP NOT NULL;
        ");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql(@"
            ALTER TABLE ""RenderPipeline""
            ALTER COLUMN ""ProductMediaAggregateId"" SET NOT NULL;
        ");

        migrationBuilder.Sql(@"
            ALTER TABLE ""RenderPipelineAttemptCreateRequestTask""
            ALTER COLUMN ""ProductMediaAggregateId"" SET NOT NULL;
        ");
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FashionFace.Repositories.Context.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultTags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Tag",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "11111111-1111-1111-1111-111111111111", "TFP" },
                    { "22222222-2222-2222-2222-222222222222", "Ню" },
                    { "33333333-3333-3333-3333-333333333333", "Коммерция" },
                    { "44444444-4444-4444-4444-444444444444", "Портрет" },
                    { "55555555-5555-5555-5555-555555555555", "Fashion" },
                    { "66666666-6666-6666-6666-666666666666", "Beauty" },
                    { "77777777-7777-7777-7777-777777777777", "Контент" }
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "Name",
                keyValue: "TFP"
            );

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "Name",
                keyValue: "Ню"
            );

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "Name",
                keyValue: "Коммерция"
            );

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "Name",
                keyValue: "Портрет"
            );

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "Name",
                keyValue: "Fashion"
            );

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "Name",
                keyValue: "Beauty"
            );

            migrationBuilder.DeleteData(
                table: "Tag",
                keyColumn: "Name",
                keyValue: "Контент"
            );
        }
    }
}

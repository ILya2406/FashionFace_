using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FashionFace.Repositories.Context.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[] { "7c2c1d6e-8b3a-4f5e-9c6a-3f8d2e6b1a74", "Admin", "ADMIN", "7c2c1d6e-8b3a-4f5e-9c6a-3f8d2e6b1a74" }
            );

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[] { "c91e4a52-1b77-4d0a-b8e3-5a2f9d6c4e18", "User", "USER", "c91e4a52-1b77-4d0a-b8e3-5a2f9d6c4e18" }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Name",
                keyValue: "Admin"
            );

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Name",
                keyValue: "User"
            );
        }
    }
}
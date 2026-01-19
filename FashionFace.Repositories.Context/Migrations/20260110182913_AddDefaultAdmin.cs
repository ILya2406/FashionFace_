using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FashionFace.Repositories.Context.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultAdmin : Migration
    {
         /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[]
                {
                    "Id",
                    "PhoneNumberConfirmed",
                    "TwoFactorEnabled",
                    "LockoutEnabled",
                    "UserName",
                    "NormalizedUserName",
                    "Email",
                    "NormalizedEmail",
                    "EmailConfirmed",
                    "PasswordHash",
                    "SecurityStamp",
                    "ConcurrencyStamp",
                    "AccessFailedCount"
                },
                values: new object[]
                {
                    "f47ac10b-58cc-4372-a567-0e02b2c3d479",
                    false,
                    false,
                    false,
                    "admin",
                    "ADMIN",
                    "admin@ff.ai",
                    "ADMIN@FF.AI",
                    true,
                    "AQAAAAIAAYagAAAAEPeyCmDk3v+hgiAuN1V5NRp9GDbJr++PmQLOJzXHC2WdxQpsoO0vV1i1uy9wEONq3A==",
                    "7c2c1d6e-8b3a-4f5e-9c6a-3f8d2e6b1a74",
                    "c91e4a52-1b77-4d0a-b8e3-5a2f9d6c4e18",
                    0
                }
            );

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[]
                {
                    "UserId",
                    "RoleId"
                },
                values: new object[]
                {
                    "f47ac10b-58cc-4372-a567-0e02b2c3d479",
                    "7c2c1d6e-8b3a-4f5e-9c6a-3f8d2e6b1a74"
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "UserName",
                keyValue: "admin"
            );

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumn: "UserId",
                keyValue: "f47ac10b-58cc-4372-a567-0e02b2c3d479"
            );
        }
    }
}
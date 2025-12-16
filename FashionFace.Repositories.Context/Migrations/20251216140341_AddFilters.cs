using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FashionFace.Repositories.Context.Migrations
{
    /// <inheritdoc />
    public partial class AddFilters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Filter",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    LocationType = table.Column<string>(type: "varchar(32)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filter", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FilterAppearanceTraits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FilterId = table.Column<Guid>(type: "uuid", nullable: false),
                    SexType = table.Column<string>(type: "varchar(32)", nullable: true),
                    FaceType = table.Column<string>(type: "varchar(32)", nullable: true),
                    HairColorType = table.Column<string>(type: "varchar(32)", nullable: true),
                    HairType = table.Column<string>(type: "varchar(32)", nullable: true),
                    HairLengthType = table.Column<string>(type: "varchar(32)", nullable: true),
                    BodyType = table.Column<string>(type: "varchar(32)", nullable: true),
                    SkinToneType = table.Column<string>(type: "varchar(32)", nullable: true),
                    EyeShapeType = table.Column<string>(type: "varchar(32)", nullable: true),
                    EyeColorType = table.Column<string>(type: "varchar(32)", nullable: true),
                    NoseType = table.Column<string>(type: "varchar(32)", nullable: true),
                    JawType = table.Column<string>(type: "varchar(32)", nullable: true),
                    Height = table.Column<int>(type: "integer", nullable: true),
                    ShoeSize = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilterAppearanceTraits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilterAppearanceTraits_Filter_FilterId",
                        column: x => x.FilterId,
                        principalTable: "Filter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FilterLocation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CityId = table.Column<Guid>(type: "uuid", nullable: false),
                    FilterId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlaceId = table.Column<Guid>(type: "uuid", nullable: false),
                    LocationType = table.Column<string>(type: "varchar(32)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilterLocation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilterLocation_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FilterLocation_Filter_FilterId",
                        column: x => x.FilterId,
                        principalTable: "Filter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FilterLocation_Place_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Place",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FilterTag",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FilterMediaAggregateId = table.Column<Guid>(type: "uuid", nullable: false),
                    TagId = table.Column<Guid>(type: "uuid", nullable: false),
                    PositionIndex = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilterTag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilterTag_Filter_FilterMediaAggregateId",
                        column: x => x.FilterMediaAggregateId,
                        principalTable: "Filter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FilterTag_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FilterFemaleTraits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FilterAppearanceTraitsId = table.Column<Guid>(type: "uuid", nullable: false),
                    BustSizeType = table.Column<string>(type: "varchar(32)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilterFemaleTraits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilterFemaleTraits_FilterAppearanceTraits_FilterAppearanceT~",
                        column: x => x.FilterAppearanceTraitsId,
                        principalTable: "FilterAppearanceTraits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FilterMaleTraits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FilterAppearanceTraitsId = table.Column<Guid>(type: "uuid", nullable: false),
                    FacialHairLengthType = table.Column<string>(type: "varchar(32)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilterMaleTraits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilterMaleTraits_FilterAppearanceTraits_FilterAppearanceTra~",
                        column: x => x.FilterAppearanceTraitsId,
                        principalTable: "FilterAppearanceTraits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FilterAppearanceTraits_FilterId",
                table: "FilterAppearanceTraits",
                column: "FilterId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FilterFemaleTraits_FilterAppearanceTraitsId",
                table: "FilterFemaleTraits",
                column: "FilterAppearanceTraitsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FilterLocation_CityId",
                table: "FilterLocation",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_FilterLocation_FilterId",
                table: "FilterLocation",
                column: "FilterId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FilterLocation_PlaceId",
                table: "FilterLocation",
                column: "PlaceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FilterMaleTraits_FilterAppearanceTraitsId",
                table: "FilterMaleTraits",
                column: "FilterAppearanceTraitsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FilterTag_FilterMediaAggregateId",
                table: "FilterTag",
                column: "FilterMediaAggregateId");

            migrationBuilder.CreateIndex(
                name: "IX_FilterTag_TagId",
                table: "FilterTag",
                column: "TagId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FilterFemaleTraits");

            migrationBuilder.DropTable(
                name: "FilterLocation");

            migrationBuilder.DropTable(
                name: "FilterMaleTraits");

            migrationBuilder.DropTable(
                name: "FilterTag");

            migrationBuilder.DropTable(
                name: "FilterAppearanceTraits");

            migrationBuilder.DropTable(
                name: "Filter");
        }
    }
}

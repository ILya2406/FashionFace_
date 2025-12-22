using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FashionFace.Repositories.Context.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Building",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "varchar(128)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Building", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Country = table.Column<string>(type: "varchar(128)", nullable: false),
                    Name = table.Column<string>(type: "varchar(128)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dimension",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "varchar(128)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dimension", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FilterCriteria",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TalentType = table.Column<string>(type: "varchar(32)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilterCriteria", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FilterRangeValue",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Min = table.Column<int>(type: "integer", nullable: false),
                    Max = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilterRangeValue", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Landmark",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "varchar(128)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Landmark", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "varchar(32)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Talent",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Description = table.Column<string>(type: "varchar(1024)", nullable: false),
                    LocationType = table.Column<string>(type: "varchar(32)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Talent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Profile",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ApplicationUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "varchar(32)", nullable: false),
                    Description = table.Column<string>(type: "varchar(1024)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AgeCategoryType = table.Column<string>(type: "varchar(32)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profile_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DimensionValue",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DimensionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "varchar(128)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DimensionValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DimensionValue_Dimension_DimensionId",
                        column: x => x.DimensionId,
                        principalTable: "Dimension",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Filter",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ApplicationUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    FilterCriteriaId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    PositionIndex = table.Column<double>(type: "double precision", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "varchar(128)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Filter_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Filter_FilterCriteria_FilterCriteriaId",
                        column: x => x.FilterCriteriaId,
                        principalTable: "FilterCriteria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FilterCriteriaAppearanceTraits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FilterCriteriaId = table.Column<Guid>(type: "uuid", nullable: false),
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
                    JawType = table.Column<string>(type: "varchar(32)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilterCriteriaAppearanceTraits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilterCriteriaAppearanceTraits_FilterCriteria_FilterCriteri~",
                        column: x => x.FilterCriteriaId,
                        principalTable: "FilterCriteria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Place",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BuildingId = table.Column<Guid>(type: "uuid", nullable: false),
                    LandmarkId = table.Column<Guid>(type: "uuid", nullable: false),
                    Street = table.Column<string>(type: "varchar(128)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Place", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Place_Building_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Building",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Place_Landmark_LandmarkId",
                        column: x => x.LandmarkId,
                        principalTable: "Landmark",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FilterCriteriaTag",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FilterCriteriaId = table.Column<Guid>(type: "uuid", nullable: false),
                    TagId = table.Column<Guid>(type: "uuid", nullable: false),
                    PositionIndex = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilterCriteriaTag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilterCriteriaTag_FilterCriteria_FilterCriteriaId",
                        column: x => x.FilterCriteriaId,
                        principalTable: "FilterCriteria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FilterCriteriaTag_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Portfolio",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TalentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Portfolio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Portfolio_Talent_TalentId",
                        column: x => x.TalentId,
                        principalTable: "Talent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppearanceTraits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProfileId = table.Column<Guid>(type: "uuid", nullable: false),
                    SexType = table.Column<string>(type: "varchar(32)", nullable: false),
                    FaceType = table.Column<string>(type: "varchar(32)", nullable: false),
                    NoseType = table.Column<string>(type: "varchar(32)", nullable: false),
                    JawType = table.Column<string>(type: "varchar(32)", nullable: false),
                    HairColorType = table.Column<string>(type: "varchar(32)", nullable: false),
                    HairType = table.Column<string>(type: "varchar(32)", nullable: false),
                    HairLengthType = table.Column<string>(type: "varchar(32)", nullable: false),
                    BodyType = table.Column<string>(type: "varchar(32)", nullable: false),
                    SkinToneType = table.Column<string>(type: "varchar(32)", nullable: false),
                    EyeShapeType = table.Column<string>(type: "varchar(32)", nullable: false),
                    EyeColorType = table.Column<string>(type: "varchar(32)", nullable: false),
                    Height = table.Column<int>(type: "integer", nullable: false),
                    ShoeSize = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppearanceTraits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppearanceTraits_Profile_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dossier",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProfileId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dossier", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dossier_Profile_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MediaFile",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProfileId = table.Column<Guid>(type: "uuid", nullable: false),
                    RelativePath = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MediaFile_Profile_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfileTalent",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProfileMediaId = table.Column<Guid>(type: "uuid", nullable: false),
                    TalentId = table.Column<Guid>(type: "uuid", nullable: false),
                    PositionIndex = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileTalent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfileTalent_Profile_ProfileMediaId",
                        column: x => x.ProfileMediaId,
                        principalTable: "Profile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfileTalent_Talent_TalentId",
                        column: x => x.TalentId,
                        principalTable: "Talent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppearanceTraitsDimensionValue",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProfileId = table.Column<Guid>(type: "uuid", nullable: false),
                    DimensionValueId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppearanceTraitsDimensionValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppearanceTraitsDimensionValue_DimensionValue_DimensionValu~",
                        column: x => x.DimensionValueId,
                        principalTable: "DimensionValue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppearanceTraitsDimensionValue_Profile_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FilterCriteriaDimension",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FilterCriteriaId = table.Column<Guid>(type: "uuid", nullable: false),
                    DimensionValueId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilterCriteriaDimension", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilterCriteriaDimension_DimensionValue_DimensionValueId",
                        column: x => x.DimensionValueId,
                        principalTable: "DimensionValue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FilterCriteriaDimension_FilterCriteria_FilterCriteriaId",
                        column: x => x.FilterCriteriaId,
                        principalTable: "FilterCriteria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FilterCriteriaFemaleTraits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FilterCriteriaAppearanceTraitsId = table.Column<Guid>(type: "uuid", nullable: false),
                    BustSizeType = table.Column<string>(type: "varchar(32)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilterCriteriaFemaleTraits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilterCriteriaFemaleTraits_FilterCriteriaAppearanceTraits_F~",
                        column: x => x.FilterCriteriaAppearanceTraitsId,
                        principalTable: "FilterCriteriaAppearanceTraits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FilterCriteriaHeight",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FilterRangeValueId = table.Column<Guid>(type: "uuid", nullable: false),
                    FilterCriteriaAppearanceTraitsId = table.Column<Guid>(type: "uuid", nullable: false),
                    FilterCriteriaAppearanceTraitsId1 = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilterCriteriaHeight", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilterCriteriaHeight_FilterCriteriaAppearanceTraits_FilterC~",
                        column: x => x.FilterCriteriaAppearanceTraitsId,
                        principalTable: "FilterCriteriaAppearanceTraits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FilterCriteriaHeight_FilterCriteriaAppearanceTraits_Filter~1",
                        column: x => x.FilterCriteriaAppearanceTraitsId1,
                        principalTable: "FilterCriteriaAppearanceTraits",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FilterCriteriaHeight_FilterRangeValue_FilterRangeValueId",
                        column: x => x.FilterRangeValueId,
                        principalTable: "FilterRangeValue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FilterCriteriaMaleTraits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FilterCriteriaAppearanceTraitsId = table.Column<Guid>(type: "uuid", nullable: false),
                    FacialHairLengthType = table.Column<string>(type: "varchar(32)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilterCriteriaMaleTraits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilterCriteriaMaleTraits_FilterCriteriaAppearanceTraits_Fil~",
                        column: x => x.FilterCriteriaAppearanceTraitsId,
                        principalTable: "FilterCriteriaAppearanceTraits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FilterCriteriaShoeSize",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FilterRangeValueId = table.Column<Guid>(type: "uuid", nullable: false),
                    FilterCriteriaAppearanceTraitsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilterCriteriaShoeSize", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilterCriteriaShoeSize_FilterCriteriaAppearanceTraits_Filte~",
                        column: x => x.FilterCriteriaAppearanceTraitsId,
                        principalTable: "FilterCriteriaAppearanceTraits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FilterCriteriaShoeSize_FilterRangeValue_FilterRangeValueId",
                        column: x => x.FilterRangeValueId,
                        principalTable: "FilterRangeValue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FilterCriteriaLocation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FilterCriteriaId = table.Column<Guid>(type: "uuid", nullable: false),
                    CityId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlaceId = table.Column<Guid>(type: "uuid", nullable: false),
                    LocationType = table.Column<string>(type: "varchar(32)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilterCriteriaLocation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilterCriteriaLocation_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FilterCriteriaLocation_FilterCriteria_FilterCriteriaId",
                        column: x => x.FilterCriteriaId,
                        principalTable: "FilterCriteria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FilterCriteriaLocation_Place_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Place",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TalentId = table.Column<Guid>(type: "uuid", nullable: false),
                    CityId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlaceId = table.Column<Guid>(type: "uuid", nullable: false),
                    LocationType = table.Column<string>(type: "varchar(32)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Location_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Location_Place_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Place",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Location_Talent_TalentId",
                        column: x => x.TalentId,
                        principalTable: "Talent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PortfolioTag",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PortfolioMediaAggregateId = table.Column<Guid>(type: "uuid", nullable: false),
                    TagId = table.Column<Guid>(type: "uuid", nullable: false),
                    PositionIndex = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioTag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PortfolioTag_Portfolio_PortfolioMediaAggregateId",
                        column: x => x.PortfolioMediaAggregateId,
                        principalTable: "Portfolio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PortfolioTag_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FemaleTraits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AppearanceTraitsId = table.Column<Guid>(type: "uuid", nullable: false),
                    BustSizeType = table.Column<string>(type: "varchar(32)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FemaleTraits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FemaleTraits_AppearanceTraits_AppearanceTraitsId",
                        column: x => x.AppearanceTraitsId,
                        principalTable: "AppearanceTraits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaleTraits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AppearanceTraitsId = table.Column<Guid>(type: "uuid", nullable: false),
                    FacialHairLengthType = table.Column<string>(type: "varchar(32)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaleTraits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaleTraits_AppearanceTraits_AppearanceTraitsId",
                        column: x => x.AppearanceTraitsId,
                        principalTable: "AppearanceTraits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Media",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OriginalFileId = table.Column<Guid>(type: "uuid", nullable: false),
                    OptimizedFileId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Media", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Media_MediaFile_OptimizedFileId",
                        column: x => x.OptimizedFileId,
                        principalTable: "MediaFile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Media_MediaFile_OriginalFileId",
                        column: x => x.OriginalFileId,
                        principalTable: "MediaFile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MediaAggregate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    PreviewMediaId = table.Column<Guid>(type: "uuid", nullable: false),
                    OriginalMediaId = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaAggregate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MediaAggregate_Media_OriginalMediaId",
                        column: x => x.OriginalMediaId,
                        principalTable: "Media",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MediaAggregate_Media_PreviewMediaId",
                        column: x => x.PreviewMediaId,
                        principalTable: "Media",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DossierMediaAggregate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DossierId = table.Column<Guid>(type: "uuid", nullable: false),
                    MediaAggregateId = table.Column<Guid>(type: "uuid", nullable: false),
                    PositionIndex = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DossierMediaAggregate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DossierMediaAggregate_Dossier_DossierId",
                        column: x => x.DossierId,
                        principalTable: "Dossier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DossierMediaAggregate_MediaAggregate_MediaAggregateId",
                        column: x => x.MediaAggregateId,
                        principalTable: "MediaAggregate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MediaAggregateTag",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MediaAggregateId = table.Column<Guid>(type: "uuid", nullable: false),
                    TagId = table.Column<Guid>(type: "uuid", nullable: false),
                    PositionIndex = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaAggregateTag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MediaAggregateTag_MediaAggregate_MediaAggregateId",
                        column: x => x.MediaAggregateId,
                        principalTable: "MediaAggregate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MediaAggregateTag_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PortfolioMediaAggregate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PortfolioId = table.Column<Guid>(type: "uuid", nullable: false),
                    MediaAggregateId = table.Column<Guid>(type: "uuid", nullable: false),
                    PositionIndex = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioMediaAggregate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PortfolioMediaAggregate_MediaAggregate_MediaAggregateId",
                        column: x => x.MediaAggregateId,
                        principalTable: "MediaAggregate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PortfolioMediaAggregate_Portfolio_PortfolioId",
                        column: x => x.PortfolioId,
                        principalTable: "Portfolio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfileMediaAggregate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProfileId = table.Column<Guid>(type: "uuid", nullable: false),
                    MediaAggregateId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileMediaAggregate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfileMediaAggregate_MediaAggregate_MediaAggregateId",
                        column: x => x.MediaAggregateId,
                        principalTable: "MediaAggregate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfileMediaAggregate_Profile_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TalentMediaAggregate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TalentId = table.Column<Guid>(type: "uuid", nullable: false),
                    MediaAggregateId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TalentMediaAggregate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TalentMediaAggregate_MediaAggregate_MediaAggregateId",
                        column: x => x.MediaAggregateId,
                        principalTable: "MediaAggregate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TalentMediaAggregate_Talent_TalentId",
                        column: x => x.TalentId,
                        principalTable: "Talent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppearanceTraits_ProfileId",
                table: "AppearanceTraits",
                column: "ProfileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppearanceTraitsDimensionValue_DimensionValueId_ProfileId",
                table: "AppearanceTraitsDimensionValue",
                columns: new[] { "DimensionValueId", "ProfileId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppearanceTraitsDimensionValue_ProfileId",
                table: "AppearanceTraitsDimensionValue",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_City_Country_Name",
                table: "City",
                columns: new[] { "Country", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DimensionValue_DimensionId_Code",
                table: "DimensionValue",
                columns: new[] { "DimensionId", "Code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dossier_ProfileId",
                table: "Dossier",
                column: "ProfileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DossierMediaAggregate_DossierId",
                table: "DossierMediaAggregate",
                column: "DossierId");

            migrationBuilder.CreateIndex(
                name: "IX_DossierMediaAggregate_MediaAggregateId",
                table: "DossierMediaAggregate",
                column: "MediaAggregateId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FemaleTraits_AppearanceTraitsId",
                table: "FemaleTraits",
                column: "AppearanceTraitsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Filter_ApplicationUserId",
                table: "Filter",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Filter_FilterCriteriaId",
                table: "Filter",
                column: "FilterCriteriaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FilterCriteriaAppearanceTraits_FilterCriteriaId",
                table: "FilterCriteriaAppearanceTraits",
                column: "FilterCriteriaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FilterCriteriaDimension_DimensionValueId",
                table: "FilterCriteriaDimension",
                column: "DimensionValueId");

            migrationBuilder.CreateIndex(
                name: "IX_FilterCriteriaDimension_FilterCriteriaId",
                table: "FilterCriteriaDimension",
                column: "FilterCriteriaId");

            migrationBuilder.CreateIndex(
                name: "IX_FilterCriteriaFemaleTraits_FilterCriteriaAppearanceTraitsId",
                table: "FilterCriteriaFemaleTraits",
                column: "FilterCriteriaAppearanceTraitsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FilterCriteriaHeight_FilterCriteriaAppearanceTraitsId",
                table: "FilterCriteriaHeight",
                column: "FilterCriteriaAppearanceTraitsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FilterCriteriaHeight_FilterCriteriaAppearanceTraitsId1",
                table: "FilterCriteriaHeight",
                column: "FilterCriteriaAppearanceTraitsId1",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FilterCriteriaHeight_FilterRangeValueId",
                table: "FilterCriteriaHeight",
                column: "FilterRangeValueId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FilterCriteriaLocation_CityId",
                table: "FilterCriteriaLocation",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_FilterCriteriaLocation_FilterCriteriaId",
                table: "FilterCriteriaLocation",
                column: "FilterCriteriaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FilterCriteriaLocation_PlaceId",
                table: "FilterCriteriaLocation",
                column: "PlaceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FilterCriteriaMaleTraits_FilterCriteriaAppearanceTraitsId",
                table: "FilterCriteriaMaleTraits",
                column: "FilterCriteriaAppearanceTraitsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FilterCriteriaShoeSize_FilterCriteriaAppearanceTraitsId",
                table: "FilterCriteriaShoeSize",
                column: "FilterCriteriaAppearanceTraitsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FilterCriteriaShoeSize_FilterRangeValueId",
                table: "FilterCriteriaShoeSize",
                column: "FilterRangeValueId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FilterCriteriaTag_FilterCriteriaId",
                table: "FilterCriteriaTag",
                column: "FilterCriteriaId");

            migrationBuilder.CreateIndex(
                name: "IX_FilterCriteriaTag_TagId",
                table: "FilterCriteriaTag",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_CityId",
                table: "Location",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_PlaceId",
                table: "Location",
                column: "PlaceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Location_TalentId",
                table: "Location",
                column: "TalentId");

            migrationBuilder.CreateIndex(
                name: "IX_MaleTraits_AppearanceTraitsId",
                table: "MaleTraits",
                column: "AppearanceTraitsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Media_OptimizedFileId",
                table: "Media",
                column: "OptimizedFileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Media_OriginalFileId",
                table: "Media",
                column: "OriginalFileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MediaAggregate_OriginalMediaId",
                table: "MediaAggregate",
                column: "OriginalMediaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MediaAggregate_PreviewMediaId",
                table: "MediaAggregate",
                column: "PreviewMediaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MediaAggregateTag_MediaAggregateId",
                table: "MediaAggregateTag",
                column: "MediaAggregateId");

            migrationBuilder.CreateIndex(
                name: "IX_MediaAggregateTag_TagId",
                table: "MediaAggregateTag",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_MediaFile_ProfileId",
                table: "MediaFile",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Place_BuildingId",
                table: "Place",
                column: "BuildingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Place_LandmarkId",
                table: "Place",
                column: "LandmarkId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Portfolio_TalentId",
                table: "Portfolio",
                column: "TalentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioMediaAggregate_MediaAggregateId",
                table: "PortfolioMediaAggregate",
                column: "MediaAggregateId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioMediaAggregate_PortfolioId",
                table: "PortfolioMediaAggregate",
                column: "PortfolioId");

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioTag_PortfolioMediaAggregateId",
                table: "PortfolioTag",
                column: "PortfolioMediaAggregateId");

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioTag_TagId",
                table: "PortfolioTag",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_Profile_ApplicationUserId",
                table: "Profile",
                column: "ApplicationUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProfileMediaAggregate_MediaAggregateId",
                table: "ProfileMediaAggregate",
                column: "MediaAggregateId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProfileMediaAggregate_ProfileId",
                table: "ProfileMediaAggregate",
                column: "ProfileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProfileTalent_ProfileMediaId",
                table: "ProfileTalent",
                column: "ProfileMediaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileTalent_TalentId",
                table: "ProfileTalent",
                column: "TalentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TalentMediaAggregate_MediaAggregateId",
                table: "TalentMediaAggregate",
                column: "MediaAggregateId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TalentMediaAggregate_TalentId",
                table: "TalentMediaAggregate",
                column: "TalentId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppearanceTraitsDimensionValue");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "DossierMediaAggregate");

            migrationBuilder.DropTable(
                name: "FemaleTraits");

            migrationBuilder.DropTable(
                name: "Filter");

            migrationBuilder.DropTable(
                name: "FilterCriteriaDimension");

            migrationBuilder.DropTable(
                name: "FilterCriteriaFemaleTraits");

            migrationBuilder.DropTable(
                name: "FilterCriteriaHeight");

            migrationBuilder.DropTable(
                name: "FilterCriteriaLocation");

            migrationBuilder.DropTable(
                name: "FilterCriteriaMaleTraits");

            migrationBuilder.DropTable(
                name: "FilterCriteriaShoeSize");

            migrationBuilder.DropTable(
                name: "FilterCriteriaTag");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "MaleTraits");

            migrationBuilder.DropTable(
                name: "MediaAggregateTag");

            migrationBuilder.DropTable(
                name: "PortfolioMediaAggregate");

            migrationBuilder.DropTable(
                name: "PortfolioTag");

            migrationBuilder.DropTable(
                name: "ProfileMediaAggregate");

            migrationBuilder.DropTable(
                name: "ProfileTalent");

            migrationBuilder.DropTable(
                name: "TalentMediaAggregate");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Dossier");

            migrationBuilder.DropTable(
                name: "DimensionValue");

            migrationBuilder.DropTable(
                name: "FilterCriteriaAppearanceTraits");

            migrationBuilder.DropTable(
                name: "FilterRangeValue");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "Place");

            migrationBuilder.DropTable(
                name: "AppearanceTraits");

            migrationBuilder.DropTable(
                name: "Portfolio");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "MediaAggregate");

            migrationBuilder.DropTable(
                name: "Dimension");

            migrationBuilder.DropTable(
                name: "FilterCriteria");

            migrationBuilder.DropTable(
                name: "Building");

            migrationBuilder.DropTable(
                name: "Landmark");

            migrationBuilder.DropTable(
                name: "Talent");

            migrationBuilder.DropTable(
                name: "Media");

            migrationBuilder.DropTable(
                name: "MediaFile");

            migrationBuilder.DropTable(
                name: "Profile");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}

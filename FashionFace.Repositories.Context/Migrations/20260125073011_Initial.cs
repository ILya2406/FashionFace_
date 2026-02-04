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
                    Name = table.Column<string>(type: "varchar(256)", nullable: false)
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
                    Name = table.Column<string>(type: "varchar(256)", nullable: false)
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
                name: "FileResource",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RelativePath = table.Column<string>(type: "text", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: false),
                    SizeBytes = table.Column<long>(type: "bigint", nullable: false),
                    ContentType = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    HashSha256 = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileResource", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FilterCriteria",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TalentType = table.Column<string>(type: "varchar(32)", nullable: true)
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
                    Name = table.Column<string>(type: "varchar(256)", nullable: false)
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
                    Name = table.Column<string>(type: "varchar(256)", nullable: false)
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
                name: "NotificationHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ApplicationUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsRead = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SourceId = table.Column<Guid>(type: "uuid", nullable: false),
                    SourceType = table.Column<string>(type: "varchar(32)", nullable: false),
                    EventType = table.Column<string>(type: "varchar(32)", nullable: false),
                    Payload = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationHistory_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
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
                    Name = table.Column<string>(type: "varchar(256)", nullable: false),
                    City = table.Column<string>(type: "text", nullable: true),
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
                name: "UserToUserChatInvitation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InitiatorUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TargetUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<string>(type: "varchar(32)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToUserChatInvitation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserToUserChatInvitation_AspNetUsers_InitiatorUserId",
                        column: x => x.InitiatorUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserChatInvitation_AspNetUsers_TargetUserId",
                        column: x => x.TargetUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserToUserMessage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ApplicationUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToUserMessage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserToUserMessage_AspNetUsers_ApplicationUserId",
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
                name: "PoseReference",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FileResourceId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Description = table.Column<string>(type: "varchar(1024)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoseReference", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PoseReference_FileResource_FileResourceId",
                        column: x => x.FileResourceId,
                        principalTable: "FileResource",
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
                    Name = table.Column<string>(type: "varchar(256)", nullable: false)
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
                    FileResourceId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MediaFile_FileResource_FileResourceId",
                        column: x => x.FileResourceId,
                        principalTable: "FileResource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "UserToUserChatInvitationCanceledNotificationTask",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InvitationId = table.Column<Guid>(type: "uuid", nullable: false),
                    InitiatorUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TargetUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CorrelationId = table.Column<Guid>(type: "uuid", nullable: false),
                    TaskStatus = table.Column<string>(type: "varchar(16)", nullable: false),
                    AttemptCount = table.Column<int>(type: "integer", nullable: false),
                    ClaimedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToUserChatInvitationCanceledNotificationTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserToUserChatInvitationCanceledNotificationTask_AspNetUser~",
                        column: x => x.InitiatorUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserChatInvitationCanceledNotificationTask_AspNetUse~1",
                        column: x => x.TargetUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserChatInvitationCanceledNotificationTask_UserToUser~",
                        column: x => x.InvitationId,
                        principalTable: "UserToUserChatInvitation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserToUserChatInvitationCanceledTask",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InvitationId = table.Column<Guid>(type: "uuid", nullable: false),
                    InitiatorUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TargetUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CorrelationId = table.Column<Guid>(type: "uuid", nullable: false),
                    TaskStatus = table.Column<string>(type: "varchar(16)", nullable: false),
                    AttemptCount = table.Column<int>(type: "integer", nullable: false),
                    ClaimedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToUserChatInvitationCanceledTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserToUserChatInvitationCanceledTask_AspNetUsers_InitiatorU~",
                        column: x => x.InitiatorUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserChatInvitationCanceledTask_AspNetUsers_TargetUser~",
                        column: x => x.TargetUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserChatInvitationCanceledTask_UserToUserChatInvitati~",
                        column: x => x.InvitationId,
                        principalTable: "UserToUserChatInvitation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserToUserChatInvitationCreatedNotificationTask",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InvitationId = table.Column<Guid>(type: "uuid", nullable: false),
                    InitiatorUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TargetUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CorrelationId = table.Column<Guid>(type: "uuid", nullable: false),
                    TaskStatus = table.Column<string>(type: "varchar(16)", nullable: false),
                    AttemptCount = table.Column<int>(type: "integer", nullable: false),
                    ClaimedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToUserChatInvitationCreatedNotificationTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserToUserChatInvitationCreatedNotificationTask_AspNetUsers~",
                        column: x => x.InitiatorUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserChatInvitationCreatedNotificationTask_AspNetUser~1",
                        column: x => x.TargetUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserChatInvitationCreatedNotificationTask_UserToUserC~",
                        column: x => x.InvitationId,
                        principalTable: "UserToUserChatInvitation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserToUserChatInvitationCreatedTask",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InvitationId = table.Column<Guid>(type: "uuid", nullable: false),
                    InitiatorUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TargetUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CorrelationId = table.Column<Guid>(type: "uuid", nullable: false),
                    TaskStatus = table.Column<string>(type: "varchar(16)", nullable: false),
                    AttemptCount = table.Column<int>(type: "integer", nullable: false),
                    ClaimedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToUserChatInvitationCreatedTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserToUserChatInvitationCreatedTask_AspNetUsers_InitiatorUs~",
                        column: x => x.InitiatorUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserChatInvitationCreatedTask_AspNetUsers_TargetUserId",
                        column: x => x.TargetUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserChatInvitationCreatedTask_UserToUserChatInvitatio~",
                        column: x => x.InvitationId,
                        principalTable: "UserToUserChatInvitation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserToUserChatInvitationRejectedNotificationTask",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InvitationId = table.Column<Guid>(type: "uuid", nullable: false),
                    InitiatorUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TargetUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CorrelationId = table.Column<Guid>(type: "uuid", nullable: false),
                    TaskStatus = table.Column<string>(type: "varchar(16)", nullable: false),
                    AttemptCount = table.Column<int>(type: "integer", nullable: false),
                    ClaimedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToUserChatInvitationRejectedNotificationTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserToUserChatInvitationRejectedNotificationTask_AspNetUser~",
                        column: x => x.InitiatorUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserChatInvitationRejectedNotificationTask_AspNetUse~1",
                        column: x => x.TargetUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserChatInvitationRejectedNotificationTask_UserToUser~",
                        column: x => x.InvitationId,
                        principalTable: "UserToUserChatInvitation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserToUserChatInvitationRejectedTask",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InvitationId = table.Column<Guid>(type: "uuid", nullable: false),
                    InitiatorUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TargetUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CorrelationId = table.Column<Guid>(type: "uuid", nullable: false),
                    TaskStatus = table.Column<string>(type: "varchar(16)", nullable: false),
                    AttemptCount = table.Column<int>(type: "integer", nullable: false),
                    ClaimedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToUserChatInvitationRejectedTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserToUserChatInvitationRejectedTask_AspNetUsers_InitiatorU~",
                        column: x => x.InitiatorUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserChatInvitationRejectedTask_AspNetUsers_TargetUser~",
                        column: x => x.TargetUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserChatInvitationRejectedTask_UserToUserChatInvitati~",
                        column: x => x.InvitationId,
                        principalTable: "UserToUserChatInvitation",
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
                    FilterCriteriaAppearanceTraitsId = table.Column<Guid>(type: "uuid", nullable: false)
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
                name: "PoseReferenceMediaAggregate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PoseReferenceId = table.Column<Guid>(type: "uuid", nullable: false),
                    MediaAggregateId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoseReferenceMediaAggregate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PoseReferenceMediaAggregate_MediaAggregate_MediaAggregateId",
                        column: x => x.MediaAggregateId,
                        principalTable: "MediaAggregate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PoseReferenceMediaAggregate_PoseReference_PoseReferenceId",
                        column: x => x.PoseReferenceId,
                        principalTable: "PoseReference",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PoseReferenceProjection",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PoseReferenceId = table.Column<Guid>(type: "uuid", nullable: false),
                    MediaAggregateId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoseReferenceProjection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PoseReferenceProjection_MediaAggregate_MediaAggregateId",
                        column: x => x.MediaAggregateId,
                        principalTable: "MediaAggregate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PoseReferenceProjection_PoseReference_PoseReferenceId",
                        column: x => x.PoseReferenceId,
                        principalTable: "PoseReference",
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
                name: "RenderPipeline",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ApplicationUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TalentId = table.Column<Guid>(type: "uuid", nullable: false),
                    PoseReferenceId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductMediaAggregateId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Name = table.Column<string>(type: "varchar(256)", nullable: false),
                    Description = table.Column<string>(type: "varchar(1024)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RenderPipeline", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RenderPipeline_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RenderPipeline_MediaAggregate_ProductMediaAggregateId",
                        column: x => x.ProductMediaAggregateId,
                        principalTable: "MediaAggregate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RenderPipeline_PoseReference_PoseReferenceId",
                        column: x => x.PoseReferenceId,
                        principalTable: "PoseReference",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RenderPipeline_Talent_TalentId",
                        column: x => x.TalentId,
                        principalTable: "Talent",
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

            migrationBuilder.CreateTable(
                name: "RenderPipelineAttemptSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PoseReferenceProjectionId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserPrompt = table.Column<string>(type: "text", nullable: false),
                    UserPromptHash = table.Column<int>(type: "integer", nullable: false),
                    Temperature = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RenderPipelineAttemptSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RenderPipelineAttemptSettings_PoseReferenceProjection_PoseR~",
                        column: x => x.PoseReferenceProjectionId,
                        principalTable: "PoseReferenceProjection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RenderPipelineAttempt",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ApplicationUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RenderPipelineId = table.Column<Guid>(type: "uuid", nullable: false),
                    RenderAttemptSettingsId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    FinishedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<string>(type: "varchar(32)", nullable: false),
                    RenderSucceededResultId = table.Column<Guid>(type: "uuid", nullable: true),
                    RenderFailedResultId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RenderPipelineAttempt", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RenderPipelineAttempt_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RenderPipelineAttempt_RenderPipelineAttemptSettings_RenderA~",
                        column: x => x.RenderAttemptSettingsId,
                        principalTable: "RenderPipelineAttemptSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RenderPipelineAttempt_RenderPipeline_RenderPipelineId",
                        column: x => x.RenderPipelineId,
                        principalTable: "RenderPipeline",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RenderPipelineAttemptCreateRequestTask",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InitiatorUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RenderPipelineAttemptId = table.Column<Guid>(type: "uuid", nullable: false),
                    ModelMediaAggregateId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductMediaAggregateId = table.Column<Guid>(type: "uuid", nullable: false),
                    PoseReferenceMediaAggregateId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserPrompt = table.Column<string>(type: "text", nullable: false),
                    Temperature = table.Column<double>(type: "double precision", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CorrelationId = table.Column<Guid>(type: "uuid", nullable: false),
                    TaskStatus = table.Column<string>(type: "varchar(16)", nullable: false),
                    AttemptCount = table.Column<int>(type: "integer", nullable: false),
                    ClaimedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RenderPipelineAttemptCreateRequestTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RenderPipelineAttemptCreateRequestTask_AspNetUsers_Initiato~",
                        column: x => x.InitiatorUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RenderPipelineAttemptCreateRequestTask_MediaAggregate_Model~",
                        column: x => x.ModelMediaAggregateId,
                        principalTable: "MediaAggregate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RenderPipelineAttemptCreateRequestTask_MediaAggregate_PoseR~",
                        column: x => x.PoseReferenceMediaAggregateId,
                        principalTable: "MediaAggregate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RenderPipelineAttemptCreateRequestTask_MediaAggregate_Produ~",
                        column: x => x.ProductMediaAggregateId,
                        principalTable: "MediaAggregate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RenderPipelineAttemptCreateRequestTask_RenderPipelineAttemp~",
                        column: x => x.RenderPipelineAttemptId,
                        principalTable: "RenderPipelineAttempt",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RenderPipelineAttemptCreateTask",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InitiatorUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RenderPipelineAttemptId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CorrelationId = table.Column<Guid>(type: "uuid", nullable: false),
                    TaskStatus = table.Column<string>(type: "varchar(16)", nullable: false),
                    AttemptCount = table.Column<int>(type: "integer", nullable: false),
                    ClaimedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RenderPipelineAttemptCreateTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RenderPipelineAttemptCreateTask_AspNetUsers_InitiatorUserId",
                        column: x => x.InitiatorUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RenderPipelineAttemptCreateTask_RenderPipelineAttempt_Rende~",
                        column: x => x.RenderPipelineAttemptId,
                        principalTable: "RenderPipelineAttempt",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RenderPipelineAttemptFailedResult",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RenderAttemptId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Description = table.Column<string>(type: "varchar(1024)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RenderPipelineAttemptFailedResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RenderPipelineAttemptFailedResult_RenderPipelineAttempt_Ren~",
                        column: x => x.RenderAttemptId,
                        principalTable: "RenderPipelineAttempt",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RenderPipelineAttemptSucceededResult",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MediaAggregateId = table.Column<Guid>(type: "uuid", nullable: false),
                    RenderAttemptId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RenderPipelineAttemptSucceededResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RenderPipelineAttemptSucceededResult_MediaAggregate_MediaAg~",
                        column: x => x.MediaAggregateId,
                        principalTable: "MediaAggregate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RenderPipelineAttemptSucceededResult_RenderPipelineAttempt_~",
                        column: x => x.RenderAttemptId,
                        principalTable: "RenderPipelineAttempt",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserToUserChat",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SettingsId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToUserChat", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserToUserChatApplicationUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ApplicationUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChatId = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "varchar(32)", nullable: false),
                    Status = table.Column<string>(type: "varchar(32)", nullable: false),
                    LastReadAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserToUserChatId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToUserChatApplicationUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserToUserChatApplicationUser_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserChatApplicationUser_UserToUserChat_ChatId",
                        column: x => x.ChatId,
                        principalTable: "UserToUserChat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserChatApplicationUser_UserToUserChat_UserToUserChat~",
                        column: x => x.UserToUserChatId,
                        principalTable: "UserToUserChat",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserToUserChatInvitationAcceptedNotificationTask",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InvitationId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChatId = table.Column<Guid>(type: "uuid", nullable: false),
                    InitiatorUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TargetUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CorrelationId = table.Column<Guid>(type: "uuid", nullable: false),
                    TaskStatus = table.Column<string>(type: "varchar(16)", nullable: false),
                    AttemptCount = table.Column<int>(type: "integer", nullable: false),
                    ClaimedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToUserChatInvitationAcceptedNotificationTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserToUserChatInvitationAcceptedNotificationTask_AspNetUser~",
                        column: x => x.InitiatorUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserChatInvitationAcceptedNotificationTask_AspNetUse~1",
                        column: x => x.TargetUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserChatInvitationAcceptedNotificationTask_UserToUser~",
                        column: x => x.ChatId,
                        principalTable: "UserToUserChat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserChatInvitationAcceptedNotificationTask_UserToUse~1",
                        column: x => x.InvitationId,
                        principalTable: "UserToUserChatInvitation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserToUserChatInvitationAcceptedTask",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InvitationId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChatId = table.Column<Guid>(type: "uuid", nullable: false),
                    InitiatorUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TargetUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CorrelationId = table.Column<Guid>(type: "uuid", nullable: false),
                    TaskStatus = table.Column<string>(type: "varchar(16)", nullable: false),
                    AttemptCount = table.Column<int>(type: "integer", nullable: false),
                    ClaimedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToUserChatInvitationAcceptedTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserToUserChatInvitationAcceptedTask_AspNetUsers_InitiatorU~",
                        column: x => x.InitiatorUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserChatInvitationAcceptedTask_AspNetUsers_TargetUser~",
                        column: x => x.TargetUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserChatInvitationAcceptedTask_UserToUserChatInvitati~",
                        column: x => x.InvitationId,
                        principalTable: "UserToUserChatInvitation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserChatInvitationAcceptedTask_UserToUserChat_ChatId",
                        column: x => x.ChatId,
                        principalTable: "UserToUserChat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserToUserChatMessage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MessageId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChatId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserToUserChatId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToUserChatMessage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserToUserChatMessage_UserToUserChat_ChatId",
                        column: x => x.ChatId,
                        principalTable: "UserToUserChat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserChatMessage_UserToUserChat_UserToUserChatId",
                        column: x => x.UserToUserChatId,
                        principalTable: "UserToUserChat",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserToUserChatMessage_UserToUserMessage_MessageId",
                        column: x => x.MessageId,
                        principalTable: "UserToUserMessage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserToUserChatSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ChatId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChatType = table.Column<string>(type: "varchar(32)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToUserChatSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserToUserChatSettings_UserToUserChat_ChatId",
                        column: x => x.ChatId,
                        principalTable: "UserToUserChat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserToUserChatMessageReadNotificationTask",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ChatId = table.Column<Guid>(type: "uuid", nullable: false),
                    InitiatorUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TargetUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    MessageId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CorrelationId = table.Column<Guid>(type: "uuid", nullable: false),
                    TaskStatus = table.Column<string>(type: "varchar(16)", nullable: false),
                    AttemptCount = table.Column<int>(type: "integer", nullable: false),
                    ClaimedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToUserChatMessageReadNotificationTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserToUserChatMessageReadNotificationTask_AspNetUsers_Initi~",
                        column: x => x.InitiatorUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserChatMessageReadNotificationTask_AspNetUsers_Targe~",
                        column: x => x.TargetUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserChatMessageReadNotificationTask_UserToUserChatMes~",
                        column: x => x.MessageId,
                        principalTable: "UserToUserChatMessage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserChatMessageReadNotificationTask_UserToUserChat_Ch~",
                        column: x => x.ChatId,
                        principalTable: "UserToUserChat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserToUserChatMessageReadTask",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ChatId = table.Column<Guid>(type: "uuid", nullable: false),
                    InitiatorUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    MessageId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CorrelationId = table.Column<Guid>(type: "uuid", nullable: false),
                    TaskStatus = table.Column<string>(type: "varchar(16)", nullable: false),
                    AttemptCount = table.Column<int>(type: "integer", nullable: false),
                    ClaimedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToUserChatMessageReadTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserToUserChatMessageReadTask_AspNetUsers_InitiatorUserId",
                        column: x => x.InitiatorUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserChatMessageReadTask_UserToUserChatMessage_Message~",
                        column: x => x.MessageId,
                        principalTable: "UserToUserChatMessage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserChatMessageReadTask_UserToUserChat_ChatId",
                        column: x => x.ChatId,
                        principalTable: "UserToUserChat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserToUserChatMessageSendNotificationTask",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ChatId = table.Column<Guid>(type: "uuid", nullable: false),
                    InitiatorUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TargetUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    MessageId = table.Column<Guid>(type: "uuid", nullable: false),
                    MessageValue = table.Column<string>(type: "text", nullable: false),
                    MessageCreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CorrelationId = table.Column<Guid>(type: "uuid", nullable: false),
                    TaskStatus = table.Column<string>(type: "varchar(16)", nullable: false),
                    AttemptCount = table.Column<int>(type: "integer", nullable: false),
                    ClaimedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToUserChatMessageSendNotificationTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserToUserChatMessageSendNotificationTask_AspNetUsers_Initi~",
                        column: x => x.InitiatorUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserChatMessageSendNotificationTask_AspNetUsers_Targe~",
                        column: x => x.TargetUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserChatMessageSendNotificationTask_UserToUserChatMes~",
                        column: x => x.MessageId,
                        principalTable: "UserToUserChatMessage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserChatMessageSendNotificationTask_UserToUserChat_Ch~",
                        column: x => x.ChatId,
                        principalTable: "UserToUserChat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserToUserChatMessageSendTask",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ChatId = table.Column<Guid>(type: "uuid", nullable: false),
                    InitiatorUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    MessageId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CorrelationId = table.Column<Guid>(type: "uuid", nullable: false),
                    TaskStatus = table.Column<string>(type: "varchar(16)", nullable: false),
                    AttemptCount = table.Column<int>(type: "integer", nullable: false),
                    ClaimedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToUserChatMessageSendTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserToUserChatMessageSendTask_AspNetUsers_InitiatorUserId",
                        column: x => x.InitiatorUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserChatMessageSendTask_UserToUserChatMessage_Message~",
                        column: x => x.MessageId,
                        principalTable: "UserToUserChatMessage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToUserChatMessageSendTask_UserToUserChat_ChatId",
                        column: x => x.ChatId,
                        principalTable: "UserToUserChat",
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
                name: "IX_MediaFile_FileResourceId",
                table: "MediaFile",
                column: "FileResourceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MediaFile_ProfileId",
                table: "MediaFile",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationHistory_ApplicationUserId",
                table: "NotificationHistory",
                column: "ApplicationUserId",
                unique: true);

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
                name: "IX_PoseReference_FileResourceId",
                table: "PoseReference",
                column: "FileResourceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PoseReferenceMediaAggregate_MediaAggregateId",
                table: "PoseReferenceMediaAggregate",
                column: "MediaAggregateId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PoseReferenceMediaAggregate_PoseReferenceId",
                table: "PoseReferenceMediaAggregate",
                column: "PoseReferenceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PoseReferenceProjection_MediaAggregateId",
                table: "PoseReferenceProjection",
                column: "MediaAggregateId");

            migrationBuilder.CreateIndex(
                name: "IX_PoseReferenceProjection_PoseReferenceId",
                table: "PoseReferenceProjection",
                column: "PoseReferenceId");

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

            migrationBuilder.CreateIndex(
                name: "IX_RenderPipeline_Composite_Unique",
                table: "RenderPipeline",
                columns: new[] { "ApplicationUserId", "TalentId", "PoseReferenceId", "ProductMediaAggregateId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RenderPipelineAttempt_ApplicationUserId",
                table: "RenderPipelineAttempt",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RenderPipelineAttempt_RenderAttemptSettingsId",
                table: "RenderPipelineAttempt",
                column: "RenderAttemptSettingsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RenderPipelineAttempt_RenderFailedResultId",
                table: "RenderPipelineAttempt",
                column: "RenderFailedResultId");

            migrationBuilder.CreateIndex(
                name: "IX_RenderPipelineAttempt_RenderPipelineId",
                table: "RenderPipelineAttempt",
                column: "RenderPipelineId");

            migrationBuilder.CreateIndex(
                name: "IX_RenderPipelineAttempt_RenderSucceededResultId",
                table: "RenderPipelineAttempt",
                column: "RenderSucceededResultId");

            migrationBuilder.CreateIndex(
                name: "IX_RenderPipelineAttemptCreateRequestTask_InitiatorUserId",
                table: "RenderPipelineAttemptCreateRequestTask",
                column: "InitiatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RenderPipelineAttemptCreateRequestTask_ModelMediaAggregateId",
                table: "RenderPipelineAttemptCreateRequestTask",
                column: "ModelMediaAggregateId");

            migrationBuilder.CreateIndex(
                name: "IX_RenderPipelineAttemptCreateRequestTask_PoseReferenceMediaAg~",
                table: "RenderPipelineAttemptCreateRequestTask",
                column: "PoseReferenceMediaAggregateId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RenderPipelineAttemptCreateRequestTask_ProductMediaAggregat~",
                table: "RenderPipelineAttemptCreateRequestTask",
                column: "ProductMediaAggregateId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RenderPipelineAttemptCreateRequestTask_RenderPipelineAttemp~",
                table: "RenderPipelineAttemptCreateRequestTask",
                column: "RenderPipelineAttemptId");

            migrationBuilder.CreateIndex(
                name: "IX_RenderPipelineAttemptCreateTask_InitiatorUserId",
                table: "RenderPipelineAttemptCreateTask",
                column: "InitiatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RenderPipelineAttemptCreateTask_RenderPipelineAttemptId",
                table: "RenderPipelineAttemptCreateTask",
                column: "RenderPipelineAttemptId");

            migrationBuilder.CreateIndex(
                name: "IX_RenderPipelineAttemptFailedResult_RenderAttemptId",
                table: "RenderPipelineAttemptFailedResult",
                column: "RenderAttemptId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RenderPipelineAttemptSettings_PoseReferenceProjectionId",
                table: "RenderPipelineAttemptSettings",
                column: "PoseReferenceProjectionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RenderPipelineAttemptSucceededResult_MediaAggregateId",
                table: "RenderPipelineAttemptSucceededResult",
                column: "MediaAggregateId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RenderPipelineAttemptSucceededResult_RenderAttemptId",
                table: "RenderPipelineAttemptSucceededResult",
                column: "RenderAttemptId",
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

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChat_SettingsId",
                table: "UserToUserChat",
                column: "SettingsId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatApplicationUser_ApplicationUserId",
                table: "UserToUserChatApplicationUser",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatApplicationUser_ChatId",
                table: "UserToUserChatApplicationUser",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatApplicationUser_UserToUserChatId",
                table: "UserToUserChatApplicationUser",
                column: "UserToUserChatId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatInvitation_InitiatorUserId",
                table: "UserToUserChatInvitation",
                column: "InitiatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatInvitation_TargetUserId",
                table: "UserToUserChatInvitation",
                column: "TargetUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatInvitationAcceptedNotificationTask_ChatId",
                table: "UserToUserChatInvitationAcceptedNotificationTask",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatInvitationAcceptedNotificationTask_InitiatorU~",
                table: "UserToUserChatInvitationAcceptedNotificationTask",
                column: "InitiatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatInvitationAcceptedNotificationTask_Invitation~",
                table: "UserToUserChatInvitationAcceptedNotificationTask",
                column: "InvitationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatInvitationAcceptedNotificationTask_TargetUser~",
                table: "UserToUserChatInvitationAcceptedNotificationTask",
                column: "TargetUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatInvitationAcceptedTask_ChatId",
                table: "UserToUserChatInvitationAcceptedTask",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatInvitationAcceptedTask_InitiatorUserId",
                table: "UserToUserChatInvitationAcceptedTask",
                column: "InitiatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatInvitationAcceptedTask_InvitationId",
                table: "UserToUserChatInvitationAcceptedTask",
                column: "InvitationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatInvitationAcceptedTask_TargetUserId",
                table: "UserToUserChatInvitationAcceptedTask",
                column: "TargetUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatInvitationCanceledNotificationTask_InitiatorU~",
                table: "UserToUserChatInvitationCanceledNotificationTask",
                column: "InitiatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatInvitationCanceledNotificationTask_Invitation~",
                table: "UserToUserChatInvitationCanceledNotificationTask",
                column: "InvitationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatInvitationCanceledNotificationTask_TargetUser~",
                table: "UserToUserChatInvitationCanceledNotificationTask",
                column: "TargetUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatInvitationCanceledTask_InitiatorUserId",
                table: "UserToUserChatInvitationCanceledTask",
                column: "InitiatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatInvitationCanceledTask_InvitationId",
                table: "UserToUserChatInvitationCanceledTask",
                column: "InvitationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatInvitationCanceledTask_TargetUserId",
                table: "UserToUserChatInvitationCanceledTask",
                column: "TargetUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatInvitationCreatedNotificationTask_InitiatorUs~",
                table: "UserToUserChatInvitationCreatedNotificationTask",
                column: "InitiatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatInvitationCreatedNotificationTask_InvitationId",
                table: "UserToUserChatInvitationCreatedNotificationTask",
                column: "InvitationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatInvitationCreatedNotificationTask_TargetUserId",
                table: "UserToUserChatInvitationCreatedNotificationTask",
                column: "TargetUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatInvitationCreatedTask_InitiatorUserId",
                table: "UserToUserChatInvitationCreatedTask",
                column: "InitiatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatInvitationCreatedTask_InvitationId",
                table: "UserToUserChatInvitationCreatedTask",
                column: "InvitationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatInvitationCreatedTask_TargetUserId",
                table: "UserToUserChatInvitationCreatedTask",
                column: "TargetUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatInvitationRejectedNotificationTask_InitiatorU~",
                table: "UserToUserChatInvitationRejectedNotificationTask",
                column: "InitiatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatInvitationRejectedNotificationTask_Invitation~",
                table: "UserToUserChatInvitationRejectedNotificationTask",
                column: "InvitationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatInvitationRejectedNotificationTask_TargetUser~",
                table: "UserToUserChatInvitationRejectedNotificationTask",
                column: "TargetUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatInvitationRejectedTask_InitiatorUserId",
                table: "UserToUserChatInvitationRejectedTask",
                column: "InitiatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatInvitationRejectedTask_InvitationId",
                table: "UserToUserChatInvitationRejectedTask",
                column: "InvitationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatInvitationRejectedTask_TargetUserId",
                table: "UserToUserChatInvitationRejectedTask",
                column: "TargetUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatMessage_ChatId",
                table: "UserToUserChatMessage",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatMessage_MessageId",
                table: "UserToUserChatMessage",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatMessage_UserToUserChatId",
                table: "UserToUserChatMessage",
                column: "UserToUserChatId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatMessageReadNotificationTask_ChatId",
                table: "UserToUserChatMessageReadNotificationTask",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatMessageReadNotificationTask_InitiatorUserId",
                table: "UserToUserChatMessageReadNotificationTask",
                column: "InitiatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatMessageReadNotificationTask_MessageId",
                table: "UserToUserChatMessageReadNotificationTask",
                column: "MessageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatMessageReadNotificationTask_TargetUserId",
                table: "UserToUserChatMessageReadNotificationTask",
                column: "TargetUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatMessageReadTask_ChatId",
                table: "UserToUserChatMessageReadTask",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatMessageReadTask_InitiatorUserId",
                table: "UserToUserChatMessageReadTask",
                column: "InitiatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatMessageReadTask_MessageId",
                table: "UserToUserChatMessageReadTask",
                column: "MessageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatMessageSendNotificationTask_ChatId",
                table: "UserToUserChatMessageSendNotificationTask",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatMessageSendNotificationTask_InitiatorUserId",
                table: "UserToUserChatMessageSendNotificationTask",
                column: "InitiatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatMessageSendNotificationTask_MessageId",
                table: "UserToUserChatMessageSendNotificationTask",
                column: "MessageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatMessageSendNotificationTask_TargetUserId",
                table: "UserToUserChatMessageSendNotificationTask",
                column: "TargetUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatMessageSendTask_ChatId",
                table: "UserToUserChatMessageSendTask",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatMessageSendTask_InitiatorUserId",
                table: "UserToUserChatMessageSendTask",
                column: "InitiatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatMessageSendTask_MessageId",
                table: "UserToUserChatMessageSendTask",
                column: "MessageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserChatSettings_ChatId",
                table: "UserToUserChatSettings",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToUserMessage_ApplicationUserId",
                table: "UserToUserMessage",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RenderPipelineAttempt_RenderPipelineAttemptFailedResult_Ren~",
                table: "RenderPipelineAttempt",
                column: "RenderFailedResultId",
                principalTable: "RenderPipelineAttemptFailedResult",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RenderPipelineAttempt_RenderPipelineAttemptSucceededResult_~",
                table: "RenderPipelineAttempt",
                column: "RenderSucceededResultId",
                principalTable: "RenderPipelineAttemptSucceededResult",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserToUserChat_UserToUserChatSettings_SettingsId",
                table: "UserToUserChat",
                column: "SettingsId",
                principalTable: "UserToUserChatSettings",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MediaFile_Profile_ProfileId",
                table: "MediaFile");

            migrationBuilder.DropForeignKey(
                name: "FK_RenderPipeline_AspNetUsers_ApplicationUserId",
                table: "RenderPipeline");

            migrationBuilder.DropForeignKey(
                name: "FK_RenderPipelineAttempt_AspNetUsers_ApplicationUserId",
                table: "RenderPipelineAttempt");

            migrationBuilder.DropForeignKey(
                name: "FK_PoseReferenceProjection_MediaAggregate_MediaAggregateId",
                table: "PoseReferenceProjection");

            migrationBuilder.DropForeignKey(
                name: "FK_RenderPipeline_MediaAggregate_ProductMediaAggregateId",
                table: "RenderPipeline");

            migrationBuilder.DropForeignKey(
                name: "FK_RenderPipelineAttemptSucceededResult_MediaAggregate_MediaAg~",
                table: "RenderPipelineAttemptSucceededResult");

            migrationBuilder.DropForeignKey(
                name: "FK_RenderPipeline_Talent_TalentId",
                table: "RenderPipeline");

            migrationBuilder.DropForeignKey(
                name: "FK_PoseReference_FileResource_FileResourceId",
                table: "PoseReference");

            migrationBuilder.DropForeignKey(
                name: "FK_PoseReferenceProjection_PoseReference_PoseReferenceId",
                table: "PoseReferenceProjection");

            migrationBuilder.DropForeignKey(
                name: "FK_RenderPipeline_PoseReference_PoseReferenceId",
                table: "RenderPipeline");

            migrationBuilder.DropForeignKey(
                name: "FK_RenderPipelineAttempt_RenderPipelineAttemptFailedResult_Ren~",
                table: "RenderPipelineAttempt");

            migrationBuilder.DropForeignKey(
                name: "FK_RenderPipelineAttempt_RenderPipelineAttemptSettings_RenderA~",
                table: "RenderPipelineAttempt");

            migrationBuilder.DropForeignKey(
                name: "FK_RenderPipelineAttempt_RenderPipelineAttemptSucceededResult_~",
                table: "RenderPipelineAttempt");

            migrationBuilder.DropForeignKey(
                name: "FK_UserToUserChat_UserToUserChatSettings_SettingsId",
                table: "UserToUserChat");

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
                name: "NotificationHistory");

            migrationBuilder.DropTable(
                name: "PortfolioMediaAggregate");

            migrationBuilder.DropTable(
                name: "PortfolioTag");

            migrationBuilder.DropTable(
                name: "PoseReferenceMediaAggregate");

            migrationBuilder.DropTable(
                name: "ProfileMediaAggregate");

            migrationBuilder.DropTable(
                name: "ProfileTalent");

            migrationBuilder.DropTable(
                name: "RenderPipelineAttemptCreateRequestTask");

            migrationBuilder.DropTable(
                name: "RenderPipelineAttemptCreateTask");

            migrationBuilder.DropTable(
                name: "TalentMediaAggregate");

            migrationBuilder.DropTable(
                name: "UserToUserChatApplicationUser");

            migrationBuilder.DropTable(
                name: "UserToUserChatInvitationAcceptedNotificationTask");

            migrationBuilder.DropTable(
                name: "UserToUserChatInvitationAcceptedTask");

            migrationBuilder.DropTable(
                name: "UserToUserChatInvitationCanceledNotificationTask");

            migrationBuilder.DropTable(
                name: "UserToUserChatInvitationCanceledTask");

            migrationBuilder.DropTable(
                name: "UserToUserChatInvitationCreatedNotificationTask");

            migrationBuilder.DropTable(
                name: "UserToUserChatInvitationCreatedTask");

            migrationBuilder.DropTable(
                name: "UserToUserChatInvitationRejectedNotificationTask");

            migrationBuilder.DropTable(
                name: "UserToUserChatInvitationRejectedTask");

            migrationBuilder.DropTable(
                name: "UserToUserChatMessageReadNotificationTask");

            migrationBuilder.DropTable(
                name: "UserToUserChatMessageReadTask");

            migrationBuilder.DropTable(
                name: "UserToUserChatMessageSendNotificationTask");

            migrationBuilder.DropTable(
                name: "UserToUserChatMessageSendTask");

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
                name: "UserToUserChatInvitation");

            migrationBuilder.DropTable(
                name: "UserToUserChatMessage");

            migrationBuilder.DropTable(
                name: "Dimension");

            migrationBuilder.DropTable(
                name: "FilterCriteria");

            migrationBuilder.DropTable(
                name: "Building");

            migrationBuilder.DropTable(
                name: "Landmark");

            migrationBuilder.DropTable(
                name: "UserToUserMessage");

            migrationBuilder.DropTable(
                name: "Profile");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "MediaAggregate");

            migrationBuilder.DropTable(
                name: "Media");

            migrationBuilder.DropTable(
                name: "MediaFile");

            migrationBuilder.DropTable(
                name: "Talent");

            migrationBuilder.DropTable(
                name: "FileResource");

            migrationBuilder.DropTable(
                name: "PoseReference");

            migrationBuilder.DropTable(
                name: "RenderPipelineAttemptFailedResult");

            migrationBuilder.DropTable(
                name: "RenderPipelineAttemptSettings");

            migrationBuilder.DropTable(
                name: "PoseReferenceProjection");

            migrationBuilder.DropTable(
                name: "RenderPipelineAttemptSucceededResult");

            migrationBuilder.DropTable(
                name: "RenderPipelineAttempt");

            migrationBuilder.DropTable(
                name: "RenderPipeline");

            migrationBuilder.DropTable(
                name: "UserToUserChatSettings");

            migrationBuilder.DropTable(
                name: "UserToUserChat");
        }
    }
}

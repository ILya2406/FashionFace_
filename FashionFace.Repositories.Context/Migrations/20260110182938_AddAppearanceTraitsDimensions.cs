using FashionFace.Common.Constants.Constants;

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FashionFace.Repositories.Context.Migrations
{
    /// <inheritdoc />
    public partial class AddAppearanceTraitsDimensions : Migration
    {
       /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var SexTypeId = "362782d4-f58b-490e-88a2-91203ba5e118";
            var FaceTypeId = "695a959d-c36f-43f4-ae8c-8f880fbbe223";
            var HairColorTypeId = "bbff6200-a676-42a4-8751-e70663f45b42";
            var HairTypeId = "68e0a80c-f0cf-4aa5-a7bd-ad00f4d70bbd";
            var HairLengthTypeId = "e22e98a4-6b7d-409c-932e-e32a409b66bd";
            var BodyTypeId = "607e310f-e8cc-472a-8a8b-fe208abe5133";
            var SkinToneTypeId = "992038ef-1f32-42f2-affe-04ea8d8c5984";
            var EyeShapeTypeId = "b447167c-b922-4c55-ad92-7468ee54c73f";
            var EyeColorTypeId = "9548f4e3-38f5-4666-9a30-92ad6dc31e93";
            var NoseTypeId = "229254e3-c9ba-479f-b1a3-0300c84820f3";
            var JawTypeId = "1cc0065b-0605-4635-8986-99861df629fe";
            var FaceHairLengthTypeId = "86bca668-2897-4339-9a89-e66374e5ba5f";
            var BustSizeTypeId = "89fd5e77-ff84-4583-98d6-3d9125bb209b";

            migrationBuilder.InsertData(
                table: "Dimension",
                columns: new[]
                {
                    "Id",
                    "Code"
                },
                values: new object[,]
                {
                    { SexTypeId, AppearanceTraitsDimensionConstants.SexType },
                    { FaceTypeId, AppearanceTraitsDimensionConstants.FaceType },
                    { HairColorTypeId, AppearanceTraitsDimensionConstants.HairColorType },
                    { HairTypeId, AppearanceTraitsDimensionConstants.HairType },
                    { HairLengthTypeId, AppearanceTraitsDimensionConstants.HairLengthType },
                    { BodyTypeId, AppearanceTraitsDimensionConstants.BodyType },
                    { SkinToneTypeId, AppearanceTraitsDimensionConstants.SkinToneType },
                    { EyeShapeTypeId, AppearanceTraitsDimensionConstants.EyeShapeType },
                    { EyeColorTypeId, AppearanceTraitsDimensionConstants.EyeColorType },
                    { NoseTypeId, AppearanceTraitsDimensionConstants.NoseType },
                    { JawTypeId, AppearanceTraitsDimensionConstants.JawType },
                    { FaceHairLengthTypeId, AppearanceTraitsDimensionConstants.FaceHairLengthType },
                    { BustSizeTypeId, AppearanceTraitsDimensionConstants.BustSizeType },
                }
            );

            migrationBuilder.InsertData(
                table: "DimensionValue",
                columns: new[]
                {
                    "Id",
                    "DimensionId",
                    "Code"
                },
                values: new object[,]
                {
                    { "89fd5e77-ff84-4583-98d6-3d9125bb209b", SexTypeId, AppearanceTraitsDimensionValuesConstants.Male },
                    { "ccefed57-6bc4-4520-80da-5a802ce3f304", SexTypeId, AppearanceTraitsDimensionValuesConstants.Female },
                }
            );

            migrationBuilder.InsertData(
                table: "DimensionValue",
                columns: new[]
                {
                    "Id",
                    "DimensionId",
                    "Code"
                },
                values: new object[,]
                {
                    { "d4ee72f7-c0fc-4df4-b0c6-3a7aa33304c2", FaceTypeId, AppearanceTraitsDimensionValuesConstants.Oval },
                    { "b8539afa-6229-4627-a9c1-90ffc5f156b1", FaceTypeId, AppearanceTraitsDimensionValuesConstants.Round },
                    { "5c5507ea-e53c-4533-b2bc-0e905cdf8a7d", FaceTypeId, AppearanceTraitsDimensionValuesConstants.Square },
                    { "6c4f0695-784e-4ac1-ab4d-fd9669a549e0", FaceTypeId, AppearanceTraitsDimensionValuesConstants.Rectangle },
                    { "634a57d9-01d0-4e2a-a152-f58ff04cac43", FaceTypeId, AppearanceTraitsDimensionValuesConstants.Heart },
                    { "8da24c10-27ef-4a35-84db-91d149ed2562", FaceTypeId, AppearanceTraitsDimensionValuesConstants.Diamond },
                    { "ec7fb276-4c05-4fa6-bc0b-af58255a0159", FaceTypeId, AppearanceTraitsDimensionValuesConstants.Triangle },
                }
            );

            migrationBuilder.InsertData(
                table: "DimensionValue",
                columns: new[]
                {
                    "Id",
                    "DimensionId",
                    "Code"
                },
                values: new object[,]
                {
                    { "cb4d2fae-d5ea-427e-b7f7-2c75ee42b8fb", HairColorTypeId, AppearanceTraitsDimensionValuesConstants.Black },
                    { "6f0bdb3d-9fdf-4f3d-a9d9-73b354437cd9", HairColorTypeId, AppearanceTraitsDimensionValuesConstants.DarkBrown },
                    { "9085182a-614e-4fcc-8c9c-5bdaa162b550", HairColorTypeId, AppearanceTraitsDimensionValuesConstants.Brown },
                    { "e0ea854e-24c1-4249-b1a7-6305c163b833", HairColorTypeId, AppearanceTraitsDimensionValuesConstants.LightBrown },
                    { "7c3068ef-b9f5-4bb2-9ca1-af915769b484", HairColorTypeId, AppearanceTraitsDimensionValuesConstants.Blonde },
                    { "c8d995f0-1eef-4b5d-808e-dfebb43149bd", HairColorTypeId, AppearanceTraitsDimensionValuesConstants.PlatinumBlonde },
                    { "553e9146-776e-4d5c-9fa8-84a1a5a13672", HairColorTypeId, AppearanceTraitsDimensionValuesConstants.Red },
                    { "c2688c1b-7731-49b9-b1db-c9be1c31e0ec", HairColorTypeId, AppearanceTraitsDimensionValuesConstants.Auburn },
                    { "e1b546f3-19a6-4528-ae11-a13032d262a8", HairColorTypeId, AppearanceTraitsDimensionValuesConstants.Gray },
                    { "1ea5638f-95f2-420c-bca9-8d4050b42799", HairColorTypeId, AppearanceTraitsDimensionValuesConstants.White },
                }
            );

            migrationBuilder.InsertData(
                table: "DimensionValue",
                columns: new[]
                {
                    "Id",
                    "DimensionId",
                    "Code"
                },
                values: new object[,]
                {
                    { "06dc9aca-379d-4556-a3ca-2de05146c158", HairTypeId, AppearanceTraitsDimensionValuesConstants.Straight },
                    { "0b6cf5ec-2c6b-4b1b-93a6-5f18c2734b87", HairTypeId, AppearanceTraitsDimensionValuesConstants.Wavy },
                    { "5cf5bd8a-741a-4f5b-8180-b9bceddedc80", HairTypeId, AppearanceTraitsDimensionValuesConstants.Curly },
                    { "3f19bd11-6296-433c-ae08-02d2a2406ed5", HairTypeId, AppearanceTraitsDimensionValuesConstants.Coily },
                }
            );

            migrationBuilder.InsertData(
                table: "DimensionValue",
                columns: new[]
                {
                    "Id",
                    "DimensionId",
                    "Code"
                },
                values: new object[,]
                {
                    { "cc612efa-c97b-4cd8-9141-555bee6c02a2", HairLengthTypeId, AppearanceTraitsDimensionValuesConstants.Bald },
                    { "ca8f958a-40c8-4167-9d97-6dc969cd69db", HairLengthTypeId, AppearanceTraitsDimensionValuesConstants.VeryShort },
                    { "d077dbcd-d903-4f7e-a3f9-607046644973", HairLengthTypeId, AppearanceTraitsDimensionValuesConstants.Short },
                    { "f89d0c90-9d94-4ee1-85a0-4e3d7a4a275a", HairLengthTypeId, AppearanceTraitsDimensionValuesConstants.Medium },
                    { "4f9b44b3-c85b-478d-8ed3-f2300f96fd98", HairLengthTypeId, AppearanceTraitsDimensionValuesConstants.Long },
                    { "2c5e6572-4451-4f05-93f3-ec9f30c205db", HairLengthTypeId, AppearanceTraitsDimensionValuesConstants.VeryLong },
                }
            );

            migrationBuilder.InsertData(
                table: "DimensionValue",
                columns: new[]
                {
                    "Id",
                    "DimensionId",
                    "Code"
                },
                values: new object[,]
                {
                    { "4431da8c-fd8b-43ae-ad0f-0345359e658f", BodyTypeId, AppearanceTraitsDimensionValuesConstants.Slim },
                    { "2663db27-5c2d-441f-9137-4edaac722903", BodyTypeId, AppearanceTraitsDimensionValuesConstants.Average },
                    { "045a9c72-588d-4228-8e37-3244427002c4", BodyTypeId, AppearanceTraitsDimensionValuesConstants.Athletic },
                    { "1fb0ff57-a9c5-47f8-8b17-a248b749c45b", BodyTypeId, AppearanceTraitsDimensionValuesConstants.Muscular },
                    { "5a7fe370-468b-4b3a-bc21-b73303b657ba", BodyTypeId, AppearanceTraitsDimensionValuesConstants.Heavy },
                    { "69a630a0-5cf9-40c5-8345-a54ec1cf1c1b", BodyTypeId, AppearanceTraitsDimensionValuesConstants.Obese },
                }
            );

            migrationBuilder.InsertData(
                table: "DimensionValue",
                columns: new[]
                {
                    "Id",
                    "DimensionId",
                    "Code"
                },
                values: new object[,]
                {
                    { "9b5f8c89-bbdd-4f6a-9f16-5852ef2a5a08", SkinToneTypeId, AppearanceTraitsDimensionValuesConstants.VeryFair },
                    { "61e524f8-ffd8-406f-96ed-1b7c7680ad07", SkinToneTypeId, AppearanceTraitsDimensionValuesConstants.Fair },
                    { "b16a2a07-821a-40de-8b9e-76bba39025c8", SkinToneTypeId, AppearanceTraitsDimensionValuesConstants.Light },
                    { "5f984b77-2d0b-472a-9135-68015bb39ca1", SkinToneTypeId, AppearanceTraitsDimensionValuesConstants.Medium },
                    { "78298192-2bf2-4995-85f7-200297a68947", SkinToneTypeId, AppearanceTraitsDimensionValuesConstants.Tan },
                    { "ca3f66e7-ed14-4d4f-8c47-da9e546876a9", SkinToneTypeId, AppearanceTraitsDimensionValuesConstants.Deep },
                    { "d34acbe1-1b5c-4cfc-822d-535614bb7dcd", SkinToneTypeId, AppearanceTraitsDimensionValuesConstants.Dark },
                }
            );

            migrationBuilder.InsertData(
                table: "DimensionValue",
                columns: new[]
                {
                    "Id",
                    "DimensionId",
                    "Code"
                },
                values: new object[,]
                {
                    { "6d72a5fa-89fd-4dc6-8e15-9d3c448e0e01", EyeShapeTypeId, AppearanceTraitsDimensionValuesConstants.Round },
                    { "bef25df8-09a9-43b6-b0b4-f8b3eccf4ec8", EyeShapeTypeId, AppearanceTraitsDimensionValuesConstants.Almond },
                    { "322b565e-2f4d-4015-884d-ef8942321566", EyeShapeTypeId, AppearanceTraitsDimensionValuesConstants.Monolid },
                    { "1b396092-a58a-4069-b8e7-3b45a16f51ae", EyeShapeTypeId, AppearanceTraitsDimensionValuesConstants.Hooded },
                    { "2bd7a5da-3247-4c0b-bf12-2530ff29e0d9", EyeShapeTypeId, AppearanceTraitsDimensionValuesConstants.Downturned },
                    { "21ee7658-bbc9-47f8-8a7f-c6f07662383e", EyeShapeTypeId, AppearanceTraitsDimensionValuesConstants.Upturned },
                    { "95d8c27f-ba38-484f-9c0a-1532ee5b4a4a", EyeShapeTypeId, AppearanceTraitsDimensionValuesConstants.DeepSet },
                    { "942acb2a-1b6c-403f-a134-604d4f225da7", EyeShapeTypeId, AppearanceTraitsDimensionValuesConstants.Protruding },
                    { "db2e936c-35c6-42d3-b41e-3045bc1c9a86", EyeShapeTypeId, AppearanceTraitsDimensionValuesConstants.WideSet },
                    { "bb813b4f-3b88-403e-a952-7f9edca31932", EyeShapeTypeId, AppearanceTraitsDimensionValuesConstants.CloseSet },
                }
            );

            migrationBuilder.InsertData(
                table: "DimensionValue",
                columns: new[]
                {
                    "Id",
                    "DimensionId",
                    "Code"
                },
                values: new object[,]
                {
                    { "f5e34401-9821-40ce-82eb-73aa286f36bb", EyeColorTypeId, AppearanceTraitsDimensionValuesConstants.Brown },
                    { "81be33ac-6cab-477d-812e-2f60c9c4376b", EyeColorTypeId, AppearanceTraitsDimensionValuesConstants.DarkBrown },
                    { "dfbbb1bd-4f5b-4a6e-92e7-409beda44bc2", EyeColorTypeId, AppearanceTraitsDimensionValuesConstants.LightBrown },
                    { "dfdb9845-8d98-46b9-bd75-c19ad1f9eea1", EyeColorTypeId, AppearanceTraitsDimensionValuesConstants.Hazel },
                    { "6655bfaa-ffc3-467c-8473-61bfb66b3dc8", EyeColorTypeId, AppearanceTraitsDimensionValuesConstants.Amber },
                    { "c13197e5-0415-46c5-a716-92d997632601", EyeColorTypeId, AppearanceTraitsDimensionValuesConstants.Green },
                    { "879c0963-893b-4ae8-9383-f7ceca4e7d09", EyeColorTypeId, AppearanceTraitsDimensionValuesConstants.Blue },
                    { "70789127-118a-47a0-83b7-5fadc775eb70", EyeColorTypeId, AppearanceTraitsDimensionValuesConstants.Gray },
                    { "7e8ce659-77b7-4298-86a4-006d602ac215", EyeColorTypeId, AppearanceTraitsDimensionValuesConstants.Violet },
                }
            );

            migrationBuilder.InsertData(
                table: "DimensionValue",
                columns: new[]
                {
                    "Id",
                    "DimensionId",
                    "Code"
                },
                values: new object[,]
                {
                    { "a060b8e9-b9c6-4083-afa7-cf4b6844ba8b", NoseTypeId, AppearanceTraitsDimensionValuesConstants.Straight },
                    { "7c8cf236-3cc0-41e9-b3d3-47267455232f", NoseTypeId, AppearanceTraitsDimensionValuesConstants.Aquiline },
                    { "f3f7a5f2-daff-4eaa-93b6-20253fa80dfe", NoseTypeId, AppearanceTraitsDimensionValuesConstants.Button },
                    { "7fef7a20-a7f3-4004-86a8-9aca7689dedd", NoseTypeId, AppearanceTraitsDimensionValuesConstants.Nubian },
                    { "b022b533-5f71-4ef7-aa33-90ca54a6e65a", NoseTypeId, AppearanceTraitsDimensionValuesConstants.Snub },
                    { "4c572889-2ac4-4e92-9e70-061fcdaa19f7", NoseTypeId, AppearanceTraitsDimensionValuesConstants.Hawk },
                    { "a2cfb317-3ced-4fa9-a8cd-2c1422f5fbad", NoseTypeId, AppearanceTraitsDimensionValuesConstants.Flat },
                    { "53dbd8e0-dd14-4e98-b6c6-f86096d17823", NoseTypeId, AppearanceTraitsDimensionValuesConstants.Bulbous },
                }
            );

            migrationBuilder.InsertData(
                table: "DimensionValue",
                columns: new[]
                {
                    "Id",
                    "DimensionId",
                    "Code"
                },
                values: new object[,]
                {
                    { "a3071197-efc9-4949-ae91-f4caac69df86", JawTypeId, AppearanceTraitsDimensionValuesConstants.Square },
                    { "be7bd295-9cae-4c0f-8593-98fd2d1479af", JawTypeId, AppearanceTraitsDimensionValuesConstants.Round },
                    { "2d0c24e8-0157-45b9-ae53-713472273519", JawTypeId, AppearanceTraitsDimensionValuesConstants.Pointed },
                    { "dca8a1c4-c592-4b49-859d-a2f3acdc0409", JawTypeId, AppearanceTraitsDimensionValuesConstants.Strong },
                    { "458ed668-bea1-43c3-b196-536504008b65", JawTypeId, AppearanceTraitsDimensionValuesConstants.Weak },
                    { "363f83f9-f2e7-4c42-9377-b755e5c114c5", JawTypeId, AppearanceTraitsDimensionValuesConstants.Wide },
                    { "8de10a92-0ecb-417b-8308-4341b8dd7e06", JawTypeId, AppearanceTraitsDimensionValuesConstants.Narrow },
                }
            );

            migrationBuilder.InsertData(
                table: "DimensionValue",
                columns: new[]
                {
                    "Id",
                    "DimensionId",
                    "Code"
                },
                values: new object[,]
                {
                    { "c9e64945-f1e2-412b-92c3-82ae88892846", FaceHairLengthTypeId, AppearanceTraitsDimensionValuesConstants.Bald },
                    { "c00a36e6-e167-446c-8be9-a4f0d7e82817", FaceHairLengthTypeId, AppearanceTraitsDimensionValuesConstants.VeryShort },
                    { "8c29ffb5-c30d-4efa-941c-d84fb20e38e0", FaceHairLengthTypeId, AppearanceTraitsDimensionValuesConstants.Short },
                    { "aed405b0-b076-4a76-8b19-ca82524ca2a6", FaceHairLengthTypeId, AppearanceTraitsDimensionValuesConstants.Medium },
                    { "683cddb0-7002-49e5-92c7-0719900cf7fb", FaceHairLengthTypeId, AppearanceTraitsDimensionValuesConstants.Long },
                    { "85e795f9-4cbf-40d5-a79d-26881d1a5029", FaceHairLengthTypeId, AppearanceTraitsDimensionValuesConstants.VeryLong },
                }
            );

            migrationBuilder.InsertData(
                table: "DimensionValue",
                columns: new[]
                {
                    "Id",
                    "DimensionId",
                    "Code"
                },
                values: new object[,]
                {
                    { "f5e6e83c-b3e7-4373-b0a7-b769e4afbdb4", BustSizeTypeId, AppearanceTraitsDimensionValuesConstants.ExtraSmall },
                    { "7ebd443d-24c5-45af-a59b-8194095e3d4e", BustSizeTypeId, AppearanceTraitsDimensionValuesConstants.Small },
                    { "4fd39b94-9517-494e-8a86-07b780e0ab9e", BustSizeTypeId, AppearanceTraitsDimensionValuesConstants.Medium },
                    { "f18536e0-1032-4d5b-b4c3-d5f7f9865567", BustSizeTypeId, AppearanceTraitsDimensionValuesConstants.Large },
                    { "71fdfbfc-d950-43d3-bbcd-b10d45e0b7aa", BustSizeTypeId, AppearanceTraitsDimensionValuesConstants.ExtraLarge },
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM \"Dimension\";");
            migrationBuilder.Sql("DELETE FROM \"DimensionValue\";");
        }
    }
}
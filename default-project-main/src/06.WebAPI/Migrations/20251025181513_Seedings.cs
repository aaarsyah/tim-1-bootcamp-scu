using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyApp.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class Seedings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 1,
                column: "RefId",
                value: new Guid("d82147bb-77de-4d6c-a3e6-c498e935f845"));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 2,
                column: "RefId",
                value: new Guid("c05424c6-a029-43a8-96d4-8771c73273a8"));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 3,
                column: "RefId",
                value: new Guid("19c0d800-d37a-4f4c-a6fe-d1f348b6a9a0"));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "RefId" },
                values: new object[] { new DateTime(2025, 10, 25, 18, 14, 4, 836, DateTimeKind.Utc).AddTicks(6208), new Guid("0cd40508-3532-474f-8e3f-f7a484dd7363") });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 5,
                column: "RefId",
                value: new Guid("baad6d8e-de4c-482a-beca-7de41f137c58"));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "RefId" },
                values: new object[] { new DateTime(2025, 10, 25, 18, 14, 4, 836, DateTimeKind.Utc).AddTicks(6242), new Guid("7e048c1f-819c-4540-9ac7-30f59ff5a4ac") });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "RefId" },
                values: new object[] { new DateTime(2025, 10, 25, 18, 14, 4, 836, DateTimeKind.Utc).AddTicks(6249), new Guid("be21d147-ec1c-4382-af9c-4d1821b7bc40") });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "RefId" },
                values: new object[] { new DateTime(2025, 10, 25, 18, 14, 4, 836, DateTimeKind.Utc).AddTicks(6256), new Guid("0c3b4181-6f42-4549-9a9a-18d1ce3d2dc1") });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 9,
                column: "RefId",
                value: new Guid("4739d2df-845c-4c94-8a0e-cbced108e679"));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "RefId" },
                values: new object[] { new DateTime(2025, 10, 25, 18, 14, 4, 836, DateTimeKind.Utc).AddTicks(6275), new Guid("17254622-5ea0-4d74-8214-692271b5ec79") });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "RefId" },
                values: new object[] { new DateTime(2025, 10, 25, 18, 14, 4, 836, DateTimeKind.Utc).AddTicks(6292), new Guid("ba2a9a0a-4dff-48fb-acd4-2ed481e1ecc3") });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "RefId" },
                values: new object[] { new DateTime(2025, 10, 25, 18, 14, 4, 836, DateTimeKind.Utc).AddTicks(6500), new Guid("49b20eeb-51a5-40c1-a00d-f55f3fbd123a") });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "RefId" },
                values: new object[] { new DateTime(2025, 10, 25, 18, 14, 4, 836, DateTimeKind.Utc).AddTicks(6512), new Guid("f1979b95-b850-49ed-b731-51d50b7c5656") });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "RefId" },
                values: new object[] { new DateTime(2025, 10, 25, 18, 14, 4, 836, DateTimeKind.Utc).AddTicks(6521), new Guid("b35f24f7-5b9f-4d06-b18f-68ce93a5c752") });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 15,
                column: "RefId",
                value: new Guid("a9465a2e-bb72-47a8-8773-7ff55f112215"));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "RefId" },
                values: new object[] { new DateTime(2025, 10, 25, 18, 14, 4, 836, DateTimeKind.Utc).AddTicks(6535), new Guid("0a5159f1-8c09-4623-8bce-127b8af05a5e") });

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 25,
                column: "RefId",
                value: new Guid("cd09a608-0de8-4040-814b-b76f4d867bb9"));

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 26,
                column: "RefId",
                value: new Guid("e41991ea-d4de-4aba-bf1e-c18267f852cd"));

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 27,
                column: "RefId",
                value: new Guid("8b0ced9e-7318-433a-bef3-ab323f483257"));

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 28,
                column: "RefId",
                value: new Guid("f49850a1-9474-46d7-8c4c-4d9d231ff048"));

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 29,
                column: "RefId",
                value: new Guid("9356fae0-2970-4339-9e32-5f4a9abf96c3"));

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 30,
                column: "RefId",
                value: new Guid("62975883-8c3a-48e3-b459-932890f6a9e0"));

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 31,
                column: "RefId",
                value: new Guid("b53cc852-3865-4993-99e8-cb1f3c393097"));

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 32,
                column: "RefId",
                value: new Guid("596271aa-d60a-4ddd-9364-c657c70fba8f"));

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 33,
                column: "RefId",
                value: new Guid("7412853d-cea8-417f-be15-be81edce2414"));

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 34,
                column: "RefId",
                value: new Guid("7cb70e43-575e-45b9-a702-40ea781319b0"));

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 35,
                column: "RefId",
                value: new Guid("5c7ca819-e7c0-46d6-81d9-192141a392a9"));

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 36,
                column: "RefId",
                value: new Guid("497ae372-71be-4a90-8237-07e6205e4cd0"));

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 37,
                column: "RefId",
                value: new Guid("65885db1-8945-4e20-9330-4c185165a870"));

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 38,
                column: "RefId",
                value: new Guid("5611958c-03ad-4be9-b14e-98b0182540ff"));

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 39,
                column: "RefId",
                value: new Guid("aa5cf13e-1ae8-4994-b631-a8636b6dc57f"));

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 40,
                column: "RefId",
                value: new Guid("66e193f9-9815-4c64-b57a-6b417eba3c29"));

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 41,
                column: "RefId",
                value: new Guid("57ee0290-4e62-43cd-8b07-5835740f2553"));

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 42,
                column: "RefId",
                value: new Guid("7b4bf368-5a1d-4a1f-a5eb-e2b48f9d3e09"));

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 43,
                column: "RefId",
                value: new Guid("ae2b2961-da79-4c94-93d3-6b00a28fd1bb"));

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 44,
                column: "RefId",
                value: new Guid("03e9d8ff-90e2-4508-8f9b-ba0967bd4c44"));

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 45,
                column: "RefId",
                value: new Guid("0e5c4c1f-5a10-4080-8247-ba5cdb9467a6"));

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 46,
                column: "RefId",
                value: new Guid("622a59be-4b26-4f02-858a-2dac9381bf08"));

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 47,
                column: "RefId",
                value: new Guid("930fb151-2ef3-4796-813f-0e0048fb3626"));

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 48,
                column: "RefId",
                value: new Guid("933f9ae0-686b-47a8-8704-2691454d708d"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 1,
                column: "RefId",
                value: new Guid("2467c3ca-ea46-4720-b590-aeb81ff50ea1"));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 2,
                column: "RefId",
                value: new Guid("17bdb4a1-c77f-4625-a00f-30620c7f3928"));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 3,
                column: "RefId",
                value: new Guid("eccb915d-a7dd-4762-9732-8aeb7f2bcdd9"));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "RefId" },
                values: new object[] { new DateTime(2025, 10, 25, 17, 21, 9, 114, DateTimeKind.Utc).AddTicks(6579), new Guid("f2b0c9d3-3d6c-4b02-9e9f-123456789003") });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 5,
                column: "RefId",
                value: new Guid("d38adc18-7b11-46a1-9417-a2f64b2adcd2"));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "RefId" },
                values: new object[] { new DateTime(2025, 10, 25, 17, 21, 9, 114, DateTimeKind.Utc).AddTicks(6600), new Guid("f3c1d0e4-4e7d-4b03-9f9f-123456789005") });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "RefId" },
                values: new object[] { new DateTime(2025, 10, 25, 17, 21, 9, 114, DateTimeKind.Utc).AddTicks(6609), new Guid("f4a1b2c3-1234-4d56-9876-123456789011") });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "RefId" },
                values: new object[] { new DateTime(2025, 10, 25, 17, 21, 9, 114, DateTimeKind.Utc).AddTicks(6693), new Guid("f4a1b2c3-1234-4d56-9876-123456789012") });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 9,
                column: "RefId",
                value: new Guid("b298d7f3-5f95-403a-bbc5-60f6d4124dd1"));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "RefId" },
                values: new object[] { new DateTime(2025, 10, 25, 17, 21, 9, 114, DateTimeKind.Utc).AddTicks(6713), new Guid("f5d1e0f5-5f8e-4b05-9f9f-123456789007") });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "RefId" },
                values: new object[] { new DateTime(2025, 10, 25, 17, 21, 9, 114, DateTimeKind.Utc).AddTicks(6735), new Guid("4a2910a8-9d25-4b1a-a977-f0c24a6d92e2") });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "RefId" },
                values: new object[] { new DateTime(2025, 10, 25, 17, 21, 9, 114, DateTimeKind.Utc).AddTicks(6743), new Guid("7ed19dc5-56e5-4bc5-a3a7-517fd2c28c71") });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "RefId" },
                values: new object[] { new DateTime(2025, 10, 25, 17, 21, 9, 114, DateTimeKind.Utc).AddTicks(6762), new Guid("97baa9ee-57fa-4766-973d-44676428a881") });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "RefId" },
                values: new object[] { new DateTime(2025, 10, 25, 17, 21, 9, 114, DateTimeKind.Utc).AddTicks(6772), new Guid("f7a1b2c3-1234-4d56-9876-123456789016") });

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 15,
                column: "RefId",
                value: new Guid("2c47a426-06f2-4f9f-bfee-45438d7c46ee"));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "RefId" },
                values: new object[] { new DateTime(2025, 10, 25, 17, 21, 9, 114, DateTimeKind.Utc).AddTicks(6788), new Guid("f8d1e0f5-5f8e-4b08-9f9f-123456789009") });

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 25,
                column: "RefId",
                value: new Guid("3f3e8753-fe57-4a15-9972-df1d655a7fa8"));

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 26,
                column: "RefId",
                value: new Guid("3a52c4bf-5444-4d76-8132-b9abe412a1ce"));

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 27,
                column: "RefId",
                value: new Guid("d0b35d19-dc29-4221-9deb-0b8f8778dcf8"));

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 28,
                column: "RefId",
                value: new Guid("3f19c774-6538-45dc-9588-8030acc118c7"));

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 29,
                column: "RefId",
                value: new Guid("19798df3-0ff8-42a5-84b2-d1577aa59f81"));

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 30,
                column: "RefId",
                value: new Guid("2569f72e-6232-4fbe-8210-7c2bc0980c71"));

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 31,
                column: "RefId",
                value: new Guid("a8450b4a-1b1a-4f06-af44-268c53574a03"));

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 32,
                column: "RefId",
                value: new Guid("ac35f7b7-0c1d-4468-826d-4cbd8540e9d6"));

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 33,
                column: "RefId",
                value: new Guid("3946d563-0a42-484f-93de-f8282fd080c5"));

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 34,
                column: "RefId",
                value: new Guid("d3f1421a-91fe-4199-8e0e-9f978b4737c6"));

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 35,
                column: "RefId",
                value: new Guid("5d7c1acb-6e7d-4a3f-9e11-1eca776bed6c"));

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 36,
                column: "RefId",
                value: new Guid("127e2799-99ae-43fb-af46-7b1ef5e60a9d"));

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 37,
                column: "RefId",
                value: new Guid("f49ec541-c930-4d7c-86f6-13d7cc8298b6"));

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 38,
                column: "RefId",
                value: new Guid("0289a170-28a9-4181-9537-c7dafd2ef459"));

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 39,
                column: "RefId",
                value: new Guid("9a8b3da3-e2c9-4f85-9b50-baaeb47daa19"));

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 40,
                column: "RefId",
                value: new Guid("66317d74-df02-4ddc-9ad3-2acc04d2d637"));

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 41,
                column: "RefId",
                value: new Guid("05d76c24-10b4-4051-a224-2acddcbf50da"));

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 42,
                column: "RefId",
                value: new Guid("e5ae65f1-1f38-4cc7-9b53-d4356fa585de"));

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 43,
                column: "RefId",
                value: new Guid("7c752838-4596-4b71-8ce1-285551d49bc6"));

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 44,
                column: "RefId",
                value: new Guid("704f9a24-7ca6-4571-8551-6536505928a3"));

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 45,
                column: "RefId",
                value: new Guid("40bab7c1-22b8-48ff-86f3-c9f0c1661322"));

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 46,
                column: "RefId",
                value: new Guid("4492784d-bc90-43af-a8ec-52b0395839b1"));

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 47,
                column: "RefId",
                value: new Guid("23830fda-9e81-40c7-bfd5-b3d92ebb939c"));

            migrationBuilder.UpdateData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 48,
                column: "RefId",
                value: new Guid("56fb565b-82c8-46d9-98e1-b075fe994a69"));
        }
    }
}

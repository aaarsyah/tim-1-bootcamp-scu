using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyApp.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class authsecmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "e875bbb0-1dd8-4ed0-b6a7-9f0e6db7134d");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "1b728d7b-d3f9-4552-ab8e-55ed7cf26518");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "258a3806-1c44-4b21-96e2-2702ffad8869");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "37702938-cc5d-41f4-84f9-e8910b94b912");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                column: "ConcurrencyStamp",
                value: "2afac984-6d9a-4311-94cb-b901c2f8b4e0");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6,
                column: "ConcurrencyStamp",
                value: "cf704c0e-9448-4b19-9a19-ce496141fad8");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "7f520b27-049d-411a-b99c-87fbe16068e8");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "dd92ffef-028b-40d5-8c1e-fee28a425eae");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "b0da0459-a6d9-431a-8c19-e72eceb39bcb");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "8006b0b0-9d66-4d8c-9762-393623c0842b");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                column: "ConcurrencyStamp",
                value: "709f3997-22ff-4297-bb8e-7d425d667c5b");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6,
                column: "ConcurrencyStamp",
                value: "98cff2b3-9436-428d-bd3f-9aa67754dfbc");
        }
    }
}

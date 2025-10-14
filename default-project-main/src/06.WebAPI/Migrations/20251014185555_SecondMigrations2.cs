using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyApp.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigrations2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "Name" },
                values: new object[] { "90502986-c010-4719-b3e7-85cc1a910198", "" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "Name" },
                values: new object[] { "88c83be4-dd37-42cf-b7ac-190506324c40", "" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "Name" },
                values: new object[] { "fa9498e5-34e4-47bb-a28a-91ee32b098d9", "" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "Name" },
                values: new object[] { "63193ef7-de64-4042-916f-3fe1556465cc", "" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ConcurrencyStamp", "Name" },
                values: new object[] { "1874e1a6-0cbc-4560-a0cd-a5e9e050bed6", "" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ConcurrencyStamp", "Name" },
                values: new object[] { "316ac85f-d6fa-438a-8556-6d2dc7387874", "" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "f92b61fe-2fe1-4e8d-9af8-b8f8f730c2d8");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "114854e6-6917-40c2-860d-d71bba5a7d4f");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "fbacc9bb-56eb-410d-9fe3-1dcbf8d7ba4d");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "9b171829-53f2-4ebd-89a1-b67bd567f864");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                column: "ConcurrencyStamp",
                value: "03ad3e68-984b-4e12-a7b8-66c28726a999");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6,
                column: "ConcurrencyStamp",
                value: "a79252dc-6c52-4cba-81cd-2262d179204c");
        }
    }
}

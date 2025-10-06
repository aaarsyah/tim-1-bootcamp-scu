using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyApp.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddBasicDataSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Description", "ImageUrl", "IsActive", "LongName", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 10, 18, 0, 0, 0, 0, DateTimeKind.Utc), "", "img/ListMenuBanner.svg", true, "Drummer class", "Drum", new DateTime(2024, 10, 18, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, new DateTime(2022, 10, 18, 0, 0, 0, 0, DateTimeKind.Utc), "", "img/ListMenuBanner.svg", true, "Pianist class", "Piano", new DateTime(2024, 10, 18, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, new DateTime(2022, 10, 18, 0, 0, 0, 0, DateTimeKind.Utc), "", "img/ListMenuBanner.svg", true, "Guitarist class", "Gitar", new DateTime(2024, 10, 18, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, new DateTime(2022, 10, 18, 0, 0, 0, 0, DateTimeKind.Utc), "", "img/ListMenuBanner.svg", true, "Bassist class", "Bass", new DateTime(2024, 10, 18, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, new DateTime(2022, 10, 18, 0, 0, 0, 0, DateTimeKind.Utc), "", "img/ListMenuBanner.svg", true, "Violinist class", "Biola", new DateTime(2024, 10, 18, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 6, new DateTime(2022, 10, 18, 0, 0, 0, 0, DateTimeKind.Utc), "", "img/ListMenuBanner.svg", true, "Singer class", "Menyangi", new DateTime(2024, 10, 18, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 7, new DateTime(2022, 10, 18, 0, 0, 0, 0, DateTimeKind.Utc), "", "img/ListMenuBanner.svg", true, "Flutist class", "Flute", new DateTime(2024, 10, 18, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 8, new DateTime(2022, 10, 18, 0, 0, 0, 0, DateTimeKind.Utc), "", "img/ListMenuBanner.svg", true, "Saxophonist class", "Saxophone", new DateTime(2024, 10, 18, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Description", "ImageUrl", "IsActive", "Name", "Price", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2022, 10, 25, 0, 0, 0, 0, DateTimeKind.Utc), "", "img/Landing1.svg", true, "Kursus Drummer Special Coach (Eno Netral)", 8500000m, new DateTime(2024, 10, 25, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, 3, new DateTime(2022, 10, 25, 0, 0, 0, 0, DateTimeKind.Utc), "", "img/Landing2.svg", true, "[Beginner] Guitar class for kids", 1600000m, new DateTime(2024, 10, 25, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, 5, new DateTime(2022, 10, 25, 0, 0, 0, 0, DateTimeKind.Utc), "", "img/Landing3.svg", true, "Biola Mid-Level Course", 3000000m, new DateTime(2024, 10, 25, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, 1, new DateTime(2022, 10, 25, 0, 0, 0, 0, DateTimeKind.Utc), "", "img/Landing4.svg", true, "Drummer for kids (Level Basic/1)", 2200000m, new DateTime(2024, 10, 25, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, 2, new DateTime(2022, 10, 25, 0, 0, 0, 0, DateTimeKind.Utc), "", "img/Landing5.svg", true, "Kursu Piano : From Zero to Pro (Full Package)", 11650000m, new DateTime(2024, 10, 25, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 6, 8, new DateTime(2022, 10, 25, 0, 0, 0, 0, DateTimeKind.Utc), "", "img/Landing6.svg", true, "Expert Level Saxophone", 7350000m, new DateTime(2024, 10, 25, 0, 0, 0, 0, DateTimeKind.Utc) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8);
        }
    }
}

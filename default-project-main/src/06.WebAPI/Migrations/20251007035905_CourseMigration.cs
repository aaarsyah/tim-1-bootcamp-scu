using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyApp.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class CourseMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Category name - must be unique"),
                    LongName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false, comment: "Optional category description"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true, comment: "Indicates if category is active"),
                    ImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "URL path to Course image"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()", comment: "Timestamp when category was created"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()", comment: "Timestamp when category was last updated")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false, comment: "Course name"),
                    Description = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: false, comment: "Detailed Course description"),
                    ImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "URL path to Course image"),
                    Price = table.Column<decimal>(type: "numeric(10,0)", nullable: false, comment: "Course price in numeric format"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true, comment: "Indicates if Course is active and available"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()", comment: "Timestamp when Course was created"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()", comment: "Timestamp when Course was last updated"),
                    CategoryId = table.Column<int>(type: "int", nullable: false, comment: "Foreign key reference to Categories table")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Course_Categories",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Schedule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedule_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Description", "ImageUrl", "IsActive", "LongName", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Kelas yang mengajarkan teknik dasar hingga lanjutan bermain drum, termasuk ritme, tempo, dan koordinasi tangan.", "/images/drum.svg", true, "Drummer class", "Drum", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Pelajari dasar bermain piano, membaca notasi musik, harmoni, dan teknik permainan untuk semua level.", "/images/piano.svg", true, "Pianist class", "Piano", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Kursus gitar akustik dan elektrik untuk mempelajari chord, melodi, improvisasi, dan teknik fingerstyle.", "/images/guitar.svg", true, "Guitarist class", "Gitar", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Kelas bass untuk memahami peran ritmis dan harmonis dalam musik serta teknik slap dan groove.", "/images/bass.svg", true, "Bassist class", "Bass", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Pelatihan bermain biola dari dasar hingga mahir, mencakup teknik bowing, fingering, dan intonasi.", "/images/violin.svg", true, "Violinist class", "Biola", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 6, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Kelas vokal untuk melatih teknik pernapasan, intonasi, artikulasi, dan ekspresi dalam bernyanyi.", "/images/singing.svg", true, "Singer class", "Menyanyi", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 7, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Kursus flute untuk memahami embouchure, pernapasan, teknik fingering, dan interpretasi musik klasik maupun modern.", "/images/flute.svg", true, "Flutist class", "Flute", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 8, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Kelas saxophone untuk mempelajari teknik embouchure, improvisasi jazz, serta kontrol nada dan dinamika.", "/images/saxophone.svg", true, "Saxophone class", "Saxophone", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Description", "ImageUrl", "IsActive", "Name", "Price", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc), "High-performance gaming laptop with RTX graphics, 16GB RAM, and 1TB SSD. Perfect for gaming and professional work.", "/images/Class1.svg", true, "Kursus Drummer Special Coach (Eno Netral)", 8500000m, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, 3, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Latest smartphone with 5G connectivity, triple camera system, and all-day battery life. Available in multiple colors.", "/images/Class2.svg", true, "[Beginner] Guitar class for kids", 1600000m, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, 5, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Premium noise-cancelling wireless headphones with 30-hour battery life and superior sound quality.", "/images/Class3.svg", true, "Biola Mid-Level Course", 3000000m, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, 1, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Comfortable 100% organic cotton t-shirt with modern fit. Available in multiple sizes and colors.", "/images/Class4.svg", true, "Drummer for kids (Level Basic/1)", 2200000m, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, 2, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Classic fit denim jeans made from durable, comfortable fabric. Perfect for casual and semi-formal occasions.", "/images/Class5.svg", true, "Kursus Piano : From Zero to Pro (Full Package)", 11650000m, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 6, 8, new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Comprehensive guide to C# programming with practical examples, best practices, and real-world projects.", "/images/Class6.svg", true, "Expert Level Saxophone", 7350000m, new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Active_Created",
                table: "Categories",
                columns: new[] { "IsActive", "CreatedAt" },
                filter: "[IsActive] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_IsActive",
                table: "Categories",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true,
                filter: "[IsActive] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_Course_Active_Category",
                table: "Courses",
                columns: new[] { "IsActive", "CategoryId" },
                filter: "[IsActive] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_Course_Active_Name_Price",
                table: "Courses",
                columns: new[] { "IsActive", "Name", "Price" },
                filter: "[IsActive] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_Course_CategoryId",
                table: "Courses",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Course_Name",
                table: "Courses",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Course_Price",
                table: "Courses",
                column: "Price");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_CourseId",
                table: "Schedule",
                column: "CourseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Schedule");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}

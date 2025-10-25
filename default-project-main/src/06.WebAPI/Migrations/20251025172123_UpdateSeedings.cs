using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyApp.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeedings : Migration
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
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LongName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    RefId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LogoUrl = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RefId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    RefId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false, defaultValue: "System"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RefreshTokenExpiry = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmailConfirmationToken = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EmailConfirmationTokenExpiry = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PasswordResetToken = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PasswordResetTokenExpiry = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FailedLoginAttempts = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    LockoutEnd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastLoginAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RefId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false, defaultValue: "System"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.CheckConstraint("CK_Email", "[Email] LIKE '%@%.%'");
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Price = table.Column<decimal>(type: "numeric(10,0)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    RefId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.CheckConstraint("CK_Price", "[Price] >= 0");
                    table.ForeignKey(
                        name: "FK_Courses_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false, defaultValue: "System"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Action = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    TimeOfAction = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    EntityName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    EntityId = table.Column<int>(type: "int", nullable: true),
                    OldValues = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    NewValues = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    IpAddress = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    UserAgent = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    RefId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditLogs_Users_EntityId",
                        column: x => x.EntityId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RefCode = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    PaymentMethodId = table.Column<int>(type: "int", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "numeric(10,0)", nullable: false, defaultValue: 0m),
                    TotalCourse = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    RefId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_PaymentMethods_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Invoices_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false, defaultValue: "System"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    RefId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false, defaultValue: "System"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "CONVERT (DATE, GETUTCDATE())"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    RefId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedules_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ScheduleId = table.Column<int>(type: "int", nullable: false),
                    RefId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ScheduleId = table.Column<int>(type: "int", nullable: false),
                    RefId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceDetail_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InvoiceDetail_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MyClasses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ScheduleId = table.Column<int>(type: "int", nullable: false),
                    RefId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyClasses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MyClasses_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MyClasses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Description", "ImageUrl", "IsActive", "LongName", "Name", "RefId", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 10, 18, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Pelajari teknik bermain drum dari dasar hingga mahir, termasuk ritme, koordinasi tangan-kaki, dan improvisasi.", "img/Class1.svg", true, "Drummer class", "Drum", new Guid("a2f2a74c-9819-4051-852e-93e859c54661"), null },
                    { 2, new DateTime(2022, 10, 18, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Kuasai piano dari dasar sampai teknik lanjutan, termasuk membaca not, improvisasi, dan interpretasi musik.", "img/Class2.svg", true, "Pianist class", "Piano", new Guid("407c4bf0-7f0c-44fc-b3ad-9a4f18a75f29"), null },
                    { 3, new DateTime(2022, 10, 18, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Pelajari gitar akustik dan elektrik, teknik petikan, chord, solo, dan improvisasi kreatif.", "img/Class3.svg", true, "Guitarist class", "Gitar", new Guid("90aa4952-165f-4225-98e4-5a569b83aa8c"), null },
                    { 4, new DateTime(2022, 10, 18, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Belajar bass untuk menciptakan groove yang solid, memahami teknik slap, fingerstyle, dan improvisasi musik.", "img/Class4.svg", true, "Bassist class", "Bass", new Guid("3b470c4e-d847-43b1-9784-a8cc2082cf9e"), null },
                    { 5, new DateTime(2022, 10, 18, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Pelajari biola dari teknik dasar, membaca not, hingga ekspresi musik klasik dan modern.", "img/Class5.svg", true, "Violinist class", "Biola", new Guid("9b648b45-327b-44ce-853a-c4ce17b7fd20"), null },
                    { 6, new DateTime(2022, 10, 18, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Kembangkan kemampuan vokal, teknik pernapasan, kontrol nada, serta ekspresi dan interpretasi lagu.", "img/Class6.svg", true, "Singer class", "Menyanyi", new Guid("e4ea4121-bf90-4ba9-bc87-2c106c6acbbe"), null },
                    { 7, new DateTime(2022, 10, 18, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Belajar flute dari teknik dasar embouchure, fingering, hingga memainkan melodi klasik dan kontemporer.", "img/Class7.svg", true, "Flutist class", "Flute", new Guid("595b8600-4d24-44dc-9e40-6dfd87892cfa"), null },
                    { 8, new DateTime(2022, 10, 18, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Kuasai saxophone dengan belajar teknik embouchure, breath control, improvisasi jazz, dan interpretasi musik modern.", "img/Class8.svg", true, "Saxophonist class", "Saxophone", new Guid("e17ec48c-b705-4448-b916-9867d292e517"), null }
                });

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "IsActive", "LogoUrl", "Name", "RefId", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 10, 18, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, "img/Payment1.svg", "Gopay", new Guid("43380776-ac70-4350-a64b-82050eb436c7"), null },
                    { 2, new DateTime(2022, 10, 18, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, "img/Payment2.svg", "OVO", new Guid("17604b46-fd7f-41fd-8a5b-9281a3de15b1"), null },
                    { 3, new DateTime(2022, 10, 18, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, "img/Payment3.svg", "DANA", new Guid("4aa1dd7f-8c22-446d-a3f5-a25548068daf"), null },
                    { 4, new DateTime(2022, 10, 18, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, "img/Payment4.svg", "Mandiri", new Guid("11816d5a-aa8d-4363-95dc-2edcabc66fd5"), null },
                    { 5, new DateTime(2022, 10, 18, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, "img/Payment5.svg", "BCA", new Guid("e4788b84-999f-43ee-b6fe-dead0c41c189"), null },
                    { 6, new DateTime(2022, 10, 18, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, "img/Payment6.svg", "BNI", new Guid("6a63902e-c624-41b7-b47a-a57c14514efb"), null }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Description", "Name", "RefId", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 10, 18, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Standard user with basic access", "User", new Guid("e7b86411-acc4-4e6f-b132-8349974d973b"), null, null },
                    { 2, new DateTime(2022, 10, 18, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Administrator with management access", "Admin", new Guid("c444bd50-1a9d-4a33-a0d9-b9b375e81a68"), null, null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Email", "EmailConfirmationToken", "EmailConfirmationTokenExpiry", "EmailConfirmed", "LastLoginAt", "LockoutEnd", "Name", "PasswordHash", "PasswordResetToken", "PasswordResetTokenExpiry", "RefId", "RefreshToken", "RefreshTokenExpiry", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 10, 18, 0, 0, 0, 0, DateTimeKind.Utc), "System", "admin@applemusic.com", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, null, "Super Admin", "", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f37e30ef-bacd-4023-be66-da243fc25964"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null },
                    { 2, new DateTime(2022, 10, 18, 0, 0, 0, 0, DateTimeKind.Utc), "System", "nurimamiskandar@gmail.com", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, null, "Nur Imam Iskandar", "", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("5c24001d-62ba-45cf-ad61-b91f38fea0bc"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null },
                    { 3, new DateTime(2022, 10, 18, 0, 0, 0, 0, DateTimeKind.Utc), "System", "imam.stmik15@gmail.com", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, null, "Iskandar", "", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("17039ada-1855-41f1-9bec-15c24acada86"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Email", "EmailConfirmationToken", "EmailConfirmationTokenExpiry", "LastLoginAt", "LockoutEnd", "Name", "PasswordHash", "PasswordResetToken", "PasswordResetTokenExpiry", "RefId", "RefreshToken", "RefreshTokenExpiry", "UpdatedAt", "UpdatedBy" },
                values: new object[] { 4, new DateTime(2022, 10, 18, 0, 0, 0, 0, DateTimeKind.Utc), "System", "iniemaildummysaya@gmail.com", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Dummy User", "", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e33a410d-c70e-4fd7-91bd-e629911c929f"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Email", "EmailConfirmationToken", "EmailConfirmationTokenExpiry", "EmailConfirmed", "LastLoginAt", "LockoutEnd", "Name", "PasswordHash", "PasswordResetToken", "PasswordResetTokenExpiry", "RefId", "RefreshToken", "RefreshTokenExpiry", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 5, new DateTime(2022, 10, 18, 0, 0, 0, 0, DateTimeKind.Utc), "System", "yusrisahrul.works@gmail.com", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, null, "yusri sahrul", "", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("55edc09e-db51-49da-98fb-7f2f25ddc2b8"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null },
                    { 6, new DateTime(2022, 10, 18, 0, 0, 0, 0, DateTimeKind.Utc), "System", "yusribootcamp@gmail.com", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, null, "yusri sahrul test", "", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c8097c3e-ab7f-48fb-95d4-01a912451575"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "CreatedBy", "Description", "ImageUrl", "IsActive", "Name", "Price", "RefId", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2022, 10, 25, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Kursus drum eksklusif dengan mentor profesional Eno Netral, fokus pada teknik, groove, dan improvisasi.", "img/Landing1.svg", true, "Kursus Drummer Special Coach (Eno Netral)", 8500000m, new Guid("2467c3ca-ea46-4720-b590-aeb81ff50ea1"), null },
                    { 2, 1, new DateTime(2022, 10, 25, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Kursus drum untuk anak-anak, belajar ritme dasar, koordinasi tangan-kaki, dan bermain lagu sederhana.", "img/Landing4.svg", true, "Drummer for kids (Level Basic/1)", 2200000m, new Guid("17bdb4a1-c77f-4625-a00f-30620c7f3928"), null },
                    { 3, 2, new DateTime(2022, 10, 25, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Belajar piano dari dasar hingga mahir, termasuk teknik, sight-reading, dan repertoar klasik & modern.", "img/Landing5.svg", true, "Kursus Piano : From Zero to Pro (Full Package)", 11650000m, new Guid("eccb915d-a7dd-4762-9732-8aeb7f2bcdd9"), null },
                    { 4, 2, new DateTime(2025, 10, 25, 17, 21, 9, 114, DateTimeKind.Utc).AddTicks(6579), "System", "Kuasai improvisasi piano jazz, belajar chord voicing, scales, dan bermain bersama band.", "img/Landing22.svg", true, "Piano Jazz Improvisation", 7000000m, new Guid("f2b0c9d3-3d6c-4b02-9e9f-123456789003"), null },
                    { 5, 3, new DateTime(2022, 10, 25, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Kursus gitar pemula untuk anak-anak, fokus belajar chord dasar, petikan, dan lagu sederhana.Kursus gitar pemula untuk anak-anak, fokus belajar chord dasar, petikan, dan lagu sederhana.", "img/Landing2.svg", true, "[Beginner] Guitar class for kids", 1600000m, new Guid("d38adc18-7b11-46a1-9417-a2f64b2adcd2"), null },
                    { 6, 3, new DateTime(2025, 10, 25, 17, 21, 9, 114, DateTimeKind.Utc).AddTicks(6600), "System", "Belajar teknik gitar rock, solo, power chord, dan riff untuk pemula hingga menengah.", "img/Landing33.svg", true, "Guitar Rock Techniques", 3500000m, new Guid("f3c1d0e4-4e7d-4b03-9f9f-123456789005"), null },
                    { 7, 4, new DateTime(2025, 10, 25, 17, 21, 9, 114, DateTimeKind.Utc).AddTicks(6609), "System", "Kursus bass untuk pemula, belajar teknik dasar, groove, dan memainkan lagu sederhana.", "img/Landing44.svg", true, "Bass Fundamentals for Beginners", 2000000m, new Guid("f4a1b2c3-1234-4d56-9876-123456789011"), null },
                    { 8, 4, new DateTime(2025, 10, 25, 17, 21, 9, 114, DateTimeKind.Utc).AddTicks(6693), "System", "Pelajari teknik lanjutan bass, termasuk slap, tapping, dan improvisasi untuk berbagai genre musik.", "img/Landing45.svg", true, "Advanced Bass Techniques", 4500000m, new Guid("f4a1b2c3-1234-4d56-9876-123456789012"), null },
                    { 9, 5, new DateTime(2022, 10, 25, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Kursus biola level menengah, belajar teknik bowing, vibrato, dan ekspresi musikal.", "img/Landing3.svg", true, "Biola Mid-Level Course", 3000000m, new Guid("b298d7f3-5f95-403a-bbc5-60f6d4124dd1"), null },
                    { 10, 5, new DateTime(2025, 10, 25, 17, 21, 9, 114, DateTimeKind.Utc).AddTicks(6713), "System", "Belajar teknik lanjutan biola, interpretasi musik klasik, dan persiapan tampil di konser.", "img/Landing55.svg", true, "Violin Advanced Performance", 6000000m, new Guid("f5d1e0f5-5f8e-4b05-9f9f-123456789007"), null },
                    { 11, 6, new DateTime(2025, 10, 25, 17, 21, 9, 114, DateTimeKind.Utc).AddTicks(6735), "System", "Kursus menyanyi untuk pemula, fokus pada teknik pernapasan, kontrol nada, dan penguatan suara.", "img/Landing66.svg", true, "Vocal Training for Beginners", 2500000m, new Guid("4a2910a8-9d25-4b1a-a977-f0c24a6d92e2"), null },
                    { 12, 6, new DateTime(2025, 10, 25, 17, 21, 9, 114, DateTimeKind.Utc).AddTicks(6743), "System", "Kuasai teknik vokal lanjutan, ekspresi musik, vibrato, dan performa panggung profesional.", "img/Landing66.svg", true, "Advanced Vocal Mastery", 5500000m, new Guid("7ed19dc5-56e5-4bc5-a3a7-517fd2c28c71"), null },
                    { 13, 7, new DateTime(2025, 10, 25, 17, 21, 9, 114, DateTimeKind.Utc).AddTicks(6762), "System", "Kursus flute pemula, belajar teknik embouchure, fingering, dan memainkan melodi sederhana.", "img/Landing67.svg", true, "Flute Basics for Beginners", 2200000m, new Guid("97baa9ee-57fa-4766-973d-44676428a881"), null },
                    { 14, 7, new DateTime(2025, 10, 25, 17, 21, 9, 114, DateTimeKind.Utc).AddTicks(6772), "System", "Pelajari teknik lanjutan flute, interpretasi musik, dinamika, dan ekspresi panggung.", "img/Landing77.svg", true, "Flute Performance & Expression", 4800000m, new Guid("f7a1b2c3-1234-4d56-9876-123456789016"), null },
                    { 15, 8, new DateTime(2022, 10, 25, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Kursus saxophone level expert, belajar improvisasi jazz, teknik embouchure, dan performa profesional.", "img/Landing6.svg", true, "Expert Level Saxophone", 7350000m, new Guid("2c47a426-06f2-4f9f-bfee-45438d7c46ee"), null },
                    { 16, 8, new DateTime(2025, 10, 25, 17, 21, 9, 114, DateTimeKind.Utc).AddTicks(6788), "System", "Belajar teknik dasar hingga improvisasi jazz di saxophone untuk pemula hingga menengah.", "img/Landing88.svg", true, "Saxophone Jazz Essentials", 4500000m, new Guid("f8d1e0f5-5f8e-4b08-9f9f-123456789009"), null }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "RefId", "RoleId", "UpdatedAt", "UpdatedBy", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 10, 18, 0, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("40d61f76-7458-4f51-b7be-f665eaaf53f3"), 2, null, null, 1 },
                    { 2, new DateTime(2022, 10, 18, 0, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("58536f91-0d39-4144-9092-2a587203054b"), 1, null, null, 2 },
                    { 3, new DateTime(2022, 10, 18, 0, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("bee22858-a299-4adc-9349-d0d27146b2aa"), 1, null, null, 3 },
                    { 4, new DateTime(2022, 10, 18, 0, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("12a54832-934c-4a98-96a7-3d0343f87568"), 1, null, null, 4 },
                    { 5, new DateTime(2022, 10, 18, 0, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("435bcec6-301f-48c0-aeb5-72e275dc500a"), 1, null, null, 5 },
                    { 6, new DateTime(2022, 10, 18, 0, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("d493a9b6-1f7e-45a7-8482-32636583e8f3"), 2, null, null, 6 }
                });

            migrationBuilder.InsertData(
                table: "Schedules",
                columns: new[] { "Id", "CourseId", "CreatedAt", "Date", "RefId", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2022, 10, 25), new Guid("7e0ea9d3-10e6-4762-a4b9-5569e398f03b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2022, 10, 26), new Guid("692dfbe2-4ed6-4eb8-9cc8-395820d3ad05"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2022, 10, 27), new Guid("332ffdfb-b8d6-4e06-823c-ec2111c4afa9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2022, 10, 25), new Guid("9be1b178-ada5-48a6-a580-13a606b8a3c1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2022, 10, 26), new Guid("abf7194c-d954-407d-af3b-8ebfb946d8f3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2022, 10, 27), new Guid("b2bc0584-af24-4896-a5cc-7642cabff8d9"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2022, 10, 25), new Guid("336e590c-a8ec-49ac-af12-6f08c837e93d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2022, 10, 26), new Guid("392a3fce-9a3b-41f3-8d2d-5a1547a0b337"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2022, 10, 27), new Guid("d59b88dc-9880-412f-8702-fa36ab470805"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2022, 10, 25), new Guid("80dd8f4a-6d80-4f37-b201-0855f20a5620"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2022, 10, 26), new Guid("06c3d0ff-45e6-46f5-94b7-f76b5aed1a23"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2022, 10, 27), new Guid("ebd1b0d8-72a7-4a17-ae78-65b3d8f54db1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 13, 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2022, 10, 25), new Guid("797150ca-baa3-400b-b88f-e3d11b086a76"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 14, 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2022, 10, 26), new Guid("d3f704ec-3ba8-4cfa-95c2-d99ce4a71c15"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 15, 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2022, 10, 27), new Guid("e1f2d3c4-5678-4f9a-b123-abcdef123456"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 16, 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2022, 10, 25), new Guid("f1a2b3c4-5678-4a9b-cdef-123456abcdef"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 17, 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2022, 10, 26), new Guid("f2b3c4d5-6789-4b0c-def1-234567abcdef"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 18, 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2022, 10, 27), new Guid("f3c4d5e6-7890-4c1d-ef12-345678abcdef"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 19, 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2022, 10, 25), new Guid("f4d5e6f7-8901-4d2e-f123-456789abcdef"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 20, 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2022, 10, 26), new Guid("a7b8c9d0-9012-4e3f-8123-56789abcdef0"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 21, 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2022, 10, 27), new Guid("b8c9d0e1-0123-4f4a-9234-6789abcdef01"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 22, 8, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2022, 10, 25), new Guid("c9d0e1f2-1234-4a5b-a345-789abcdef012"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 23, 8, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2022, 10, 26), new Guid("d0e1f2a3-2345-4b6c-b456-89abcdef0123"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 24, 8, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2022, 10, 27), new Guid("e1f2a3b4-3456-4c7d-c567-9abcdef01234"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 25, 9, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2022, 10, 25), new Guid("3f3e8753-fe57-4a15-9972-df1d655a7fa8"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 26, 9, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2022, 10, 26), new Guid("3a52c4bf-5444-4d76-8132-b9abe412a1ce"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 27, 9, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2022, 10, 27), new Guid("d0b35d19-dc29-4221-9deb-0b8f8778dcf8"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 28, 10, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2022, 10, 25), new Guid("3f19c774-6538-45dc-9588-8030acc118c7"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 29, 10, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2022, 10, 26), new Guid("19798df3-0ff8-42a5-84b2-d1577aa59f81"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 30, 10, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2022, 10, 27), new Guid("2569f72e-6232-4fbe-8210-7c2bc0980c71"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 31, 11, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2022, 10, 25), new Guid("a8450b4a-1b1a-4f06-af44-268c53574a03"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 32, 11, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2022, 10, 26), new Guid("ac35f7b7-0c1d-4468-826d-4cbd8540e9d6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 33, 11, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2022, 10, 27), new Guid("3946d563-0a42-484f-93de-f8282fd080c5"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 34, 12, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2022, 10, 25), new Guid("d3f1421a-91fe-4199-8e0e-9f978b4737c6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 35, 12, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2022, 10, 26), new Guid("5d7c1acb-6e7d-4a3f-9e11-1eca776bed6c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 36, 12, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2022, 10, 27), new Guid("127e2799-99ae-43fb-af46-7b1ef5e60a9d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 37, 13, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2022, 10, 25), new Guid("f49ec541-c930-4d7c-86f6-13d7cc8298b6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 38, 13, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2022, 10, 26), new Guid("0289a170-28a9-4181-9537-c7dafd2ef459"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 39, 13, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2022, 10, 27), new Guid("9a8b3da3-e2c9-4f85-9b50-baaeb47daa19"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 40, 14, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2022, 10, 25), new Guid("66317d74-df02-4ddc-9ad3-2acc04d2d637"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 41, 14, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2022, 10, 26), new Guid("05d76c24-10b4-4051-a224-2acddcbf50da"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 42, 14, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2022, 10, 27), new Guid("e5ae65f1-1f38-4cc7-9b53-d4356fa585de"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 43, 15, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2022, 10, 25), new Guid("7c752838-4596-4b71-8ce1-285551d49bc6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 44, 15, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2022, 10, 26), new Guid("704f9a24-7ca6-4571-8551-6536505928a3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 45, 15, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2022, 10, 27), new Guid("40bab7c1-22b8-48ff-86f3-c9f0c1661322"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 46, 16, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2022, 10, 25), new Guid("4492784d-bc90-43af-a8ec-52b0395839b1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 47, 16, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2022, 10, 26), new Guid("23830fda-9e81-40c7-bfd5-b3d92ebb939c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 48, 16, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2022, 10, 27), new Guid("56fb565b-82c8-46d9-98e1-b075fe994a69"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_EntityId",
                table: "AuditLogs",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_RefId",
                table: "AuditLogs",
                column: "RefId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_RefId",
                table: "CartItems",
                column: "RefId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ScheduleId",
                table: "CartItems",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_UserId",
                table: "CartItems",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_RefId",
                table: "Categories",
                column: "RefId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CategoryId",
                table: "Courses",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_RefId",
                table: "Courses",
                column: "RefId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDetail_InvoiceId",
                table: "InvoiceDetail",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDetail_ScheduleId",
                table: "InvoiceDetail",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_PaymentMethodId",
                table: "Invoices",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_RefCode",
                table: "Invoices",
                column: "RefCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_UserId",
                table: "Invoices",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MyClasses_RefId",
                table: "MyClasses",
                column: "RefId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MyClasses_ScheduleId",
                table: "MyClasses",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_MyClasses_UserId",
                table: "MyClasses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethods_RefId",
                table: "PaymentMethods",
                column: "RefId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RefId",
                table: "RoleClaims",
                column: "RefId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_RefId",
                table: "Roles",
                column: "RefId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_CourseId",
                table: "Schedules",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_RefId",
                table: "Schedules",
                column: "RefId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_RefId",
                table: "UserClaims",
                column: "RefId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RefId",
                table: "UserRoles",
                column: "RefId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RefId",
                table: "Users",
                column: "RefId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "InvoiceDetail");

            migrationBuilder.DropTable(
                name: "MyClasses");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "PaymentMethods");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}

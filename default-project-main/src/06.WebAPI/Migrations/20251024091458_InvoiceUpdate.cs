using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyApp.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class InvoiceUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_InvoiceDetail_InvoiceId_ScheduleId",
                table: "InvoiceDetail");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDetail_InvoiceId",
                table: "InvoiceDetail",
                column: "InvoiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_InvoiceDetail_InvoiceId",
                table: "InvoiceDetail");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDetail_InvoiceId_ScheduleId",
                table: "InvoiceDetail",
                columns: new[] { "InvoiceId", "ScheduleId" },
                unique: true);
        }
    }
}

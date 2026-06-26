using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthcareTicketingSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddReportedBy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReportedBy",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReportedBy",
                table: "Tickets");
        }
    }
}

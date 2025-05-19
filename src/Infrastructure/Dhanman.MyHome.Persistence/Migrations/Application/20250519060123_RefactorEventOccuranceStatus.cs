using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dhanman.MyHome.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RefactorEventOccuranceStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "event_occurrences");

            migrationBuilder.AddColumn<int>(
                name: "EventOccurrenceStatusId",
                table: "event_occurrences",
                type: "integer",
                nullable: false,
                defaultValue: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventOccurrenceStatusId",
                table: "event_occurrences");

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "event_occurrences",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}

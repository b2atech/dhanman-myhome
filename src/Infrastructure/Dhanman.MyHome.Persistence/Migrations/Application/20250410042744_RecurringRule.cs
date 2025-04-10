using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dhanman.MyHome.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RecurringRule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "recurrence_rule",
                table: "events");

            migrationBuilder.AddColumn<int>(
                name: "recurrence_rule_id",
                table: "events",
                type: "integer",
                nullable: true,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "recurrence_rule_id",
                table: "events");

            migrationBuilder.AddColumn<string>(
                name: "recurrence_rule",
                table: "events",
                type: "text",
                nullable: true,
                defaultValue: "");
        }
    }
}

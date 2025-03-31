using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dhanman.MyHome.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class refcatorVisitorTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "entry_time",
                table: "visitors");

            migrationBuilder.DropColumn(
                name: "exit_time",
                table: "visitors");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "entry_time",
                table: "visitors",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "exit_time",
                table: "visitors",
                type: "timestamp without time zone",
                nullable: true);
        }
    }
}

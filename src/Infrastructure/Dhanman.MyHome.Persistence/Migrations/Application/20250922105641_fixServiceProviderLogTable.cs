using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dhanman.MyHome.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class fixServiceProviderLogTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "visit_purpose_id",
                table: "service_provider_logs");

            migrationBuilder.DropColumn(
                name: "visiting_unit_id",
                table: "service_provider_logs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "visit_purpose_id",
                table: "service_provider_logs",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "visiting_unit_id",
                table: "service_provider_logs",
                type: "integer",
                nullable: true);
        }
    }
}

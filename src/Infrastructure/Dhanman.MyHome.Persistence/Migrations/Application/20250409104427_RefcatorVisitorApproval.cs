using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dhanman.MyHome.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RefcatorVisitorApproval : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "company_name",
                table: "visitor_approvals",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "vehicle_number",
                table: "visitor_approvals",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "company_name",
                table: "visitor_approvals");

            migrationBuilder.DropColumn(
                name: "vehicle_number",
                table: "visitor_approvals");
        }
    }
}

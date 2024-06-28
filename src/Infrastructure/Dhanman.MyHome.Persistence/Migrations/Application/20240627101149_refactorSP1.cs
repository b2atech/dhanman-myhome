using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dhanman.MyHome.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class refactorSP1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
            name: "pin_code",
            table: "service_providers",
            type: "character(1)",
            nullable: false,
            defaultValue: "A");

            // Altering the column type
            migrationBuilder.AlterColumn<string>(
                name: "pin_code",
                table: "service_providers",
                type: "text",
                nullable: false,
                oldClrType: typeof(char),
                oldType: "character(1)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<char>(
             name: "pin_code",
             table: "service_providers",
             type: "character(1)",
             nullable: false,
             oldClrType: typeof(string),
             oldType: "text");

            migrationBuilder.DropColumn(
                name: "pin_code",
                table: "service_providers");
        }
    }
}

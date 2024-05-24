using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dhanman.MyHome.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class changeSSubTypeTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "service_provider_sub_type",
                newName: "service_provider_sub_types");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_service_provider_sub_types",
                table: "service_provider_sub_types");

            migrationBuilder.RenameTable(
                name: "service_provider_sub_type",
                newName: "service_provider_sub_types");
        }
    }
}

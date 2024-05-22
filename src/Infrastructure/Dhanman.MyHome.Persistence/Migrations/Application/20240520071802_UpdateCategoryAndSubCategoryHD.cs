using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dhanman.MyHome.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCategoryAndSubCategoryHD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "sub_category_type",
                table: "categories");

            migrationBuilder.AddColumn<int>(
                name: "category_id",
                table: "sub_categories",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "category_id",
                table: "sub_categories");

            migrationBuilder.AddColumn<int>(
                name: "sub_category_type",
                table: "categories",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}

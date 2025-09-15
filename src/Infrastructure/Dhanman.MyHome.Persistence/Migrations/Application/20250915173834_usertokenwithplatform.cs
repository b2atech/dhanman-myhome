using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dhanman.MyHome.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class usertokenwithplatform : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "platform",
                table: "user_fcm_tokens",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "platform",
                table: "user_fcm_tokens");
        }
    }
}

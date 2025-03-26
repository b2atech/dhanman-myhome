using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dhanman.MyHome.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class member_additional_details : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "community_user_details");

            migrationBuilder.CreateTable(
                name: "member_additional_details",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    member_type = table.Column<string>(type: "text", nullable: true),
                    user_name = table.Column<string>(type: "text", nullable: true),
                    password = table.Column<string>(type: "text", nullable: true),
                    company_name = table.Column<string>(type: "text", nullable: true),
                    designation = table.Column<string>(type: "text", nullable: true),
                    hatty_id = table.Column<Guid>(type: "uuid", nullable: false),
                    date_of_birth = table.Column<DateTime>(type: "timestamp", nullable: false),
                    gender = table.Column<char>(type: "char", nullable: false),
                    marital_status = table.Column<string>(type: "text", nullable: true),
                    about_yourself = table.Column<string>(type: "text", nullable: true),
                    spouse_name = table.Column<string>(type: "text", nullable: true),
                    spouse_hatty_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_on_utc = table.Column<DateTime>(type: "timestamp", nullable: false),
                    modified_on_utc = table.Column<DateTime>(type: "timestamp", nullable: true),
                    deleted_on_utc = table.Column<DateTime>(type: "timestamp", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    modified_by = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_member_additional_details", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "member_additional_details");

            migrationBuilder.CreateTable(
                name: "community_user_details",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    about_yourself = table.Column<string>(type: "text", nullable: true),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    created_on_utc = table.Column<DateTime>(type: "timestamp", nullable: false),
                    current_address_id = table.Column<Guid>(type: "uuid", nullable: false),
                    date_of_birth = table.Column<DateTime>(type: "timestamp", nullable: false),
                    deleted_on_utc = table.Column<DateTime>(type: "timestamp", nullable: true),
                    designation = table.Column<string>(type: "text", nullable: true),
                    gender = table.Column<char>(type: "char", nullable: false),
                    hatty_id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    marital_status = table.Column<string>(type: "text", nullable: true),
                    member_type = table.Column<string>(type: "text", nullable: true),
                    modified_by = table.Column<Guid>(type: "uuid", nullable: true),
                    modified_on_utc = table.Column<DateTime>(type: "timestamp", nullable: true),
                    resident_id = table.Column<int>(type: "integer", nullable: false),
                    spouse_hatty_id = table.Column<Guid>(type: "uuid", nullable: true),
                    spouse_name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_community_user_details", x => x.id);
                });
        }
    }
}

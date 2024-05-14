using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dhanman.MyHome.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class refactorServiceProviderEndpoint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_frequent_visitor",
                table: "service_providers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_hireable",
                table: "service_providers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_visible",
                table: "service_providers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "policeverification_status",
                table: "service_providers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "validity_date",
                table: "service_providers",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_frequent_visitor",
                table: "service_providers");

            migrationBuilder.DropColumn(
                name: "is_hireable",
                table: "service_providers");

            migrationBuilder.DropColumn(
                name: "is_visible",
                table: "service_providers");

            migrationBuilder.DropColumn(
                name: "policeverification_status",
                table: "service_providers");

            migrationBuilder.DropColumn(
                name: "validity_date",
                table: "service_providers");
        }
    }
}

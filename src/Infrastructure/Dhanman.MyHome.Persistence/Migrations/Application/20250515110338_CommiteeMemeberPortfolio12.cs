using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dhanman.MyHome.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CommiteeMemeberPortfolio12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created_by",
                table: "portfolios");

            migrationBuilder.DropColumn(
                name: "created_on_utc",
                table: "portfolios");

            migrationBuilder.DropColumn(
                name: "deleted_on_utc",
                table: "portfolios");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "portfolios");

            migrationBuilder.DropColumn(
                name: "modified_by",
                table: "portfolios");

            migrationBuilder.DropColumn(
                name: "modified_on_utc",
                table: "portfolios");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "created_by",
                table: "portfolios",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "created_on_utc",
                table: "portfolios",
                type: "timestamp",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "deleted_on_utc",
                table: "portfolios",
                type: "timestamp",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "portfolios",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "modified_by",
                table: "portfolios",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "modified_on_utc",
                table: "portfolios",
                type: "timestamp",
                nullable: true);
        }
    }
}

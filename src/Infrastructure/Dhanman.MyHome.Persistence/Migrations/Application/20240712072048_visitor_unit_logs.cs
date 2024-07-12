using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Dhanman.MyHome.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class visitor_unit_logs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "visit_purpose_id",
                table: "visitor_logs",
                newName: "visitor_type_id");

            migrationBuilder.CreateTable(
                name: "visitor_unit_logs",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    visitor_log_id = table.Column<int>(type: "integer", nullable: false),
                    unit_id = table.Column<int>(type: "integer", nullable: false),
                    deleted_on_utc = table.Column<DateTime>(type: "timestamp", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_visitor_unit_logs", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "visitor_unit_logs");

            migrationBuilder.RenameColumn(
                name: "visitor_type_id",
                table: "visitor_logs",
                newName: "visit_purpose_id");
            
        }
    }
}

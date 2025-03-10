using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Dhanman.MyHome.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ticketWorkflowTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ticket_workflow",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    apartment_id = table.Column<Guid>(type: "uuid", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    next_status = table.Column<int>(type: "integer", nullable: false),
                    previous_status = table.Column<int>(type: "integer", nullable: false),
                    is_initial = table.Column<bool>(type: "boolean", nullable: false),
                    created_on_utc = table.Column<DateTime>(type: "timestamp", nullable: false),
                    modified_on_utc = table.Column<DateTime>(type: "timestamp", nullable: true),
                    deleted_on_utc = table.Column<DateTime>(type: "timestamp", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    modified_by = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ticket_workflow", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ticket_workflow");
        }
    }
}

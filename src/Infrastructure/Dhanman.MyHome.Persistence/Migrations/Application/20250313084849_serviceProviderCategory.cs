using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Dhanman.MyHome.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class serviceProviderCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "service_provider_ticket_categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    service_provider_id = table.Column<int>(type: "integer", nullable: false),
                    ticket_category_id = table.Column<int>(type: "integer", nullable: false),
                    deleted_on_utc = table.Column<DateTime>(type: "timestamp", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_service_provider_ticket_categories", x => x.id);
                    table.ForeignKey(
                        name: "fk_service_provider_ticket_categories_service_providers_servic",
                        column: x => x.service_provider_id,
                        principalTable: "service_providers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_service_provider_ticket_categories_ticket_category_ticket_c",
                        column: x => x.ticket_category_id,
                        principalTable: "ticket_categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_service_provider_ticket_categories_service_provider_id",
                table: "service_provider_ticket_categories",
                column: "service_provider_id");

            migrationBuilder.CreateIndex(
                name: "ix_service_provider_ticket_categories_ticket_category_id",
                table: "service_provider_ticket_categories",
                column: "ticket_category_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "service_provider_ticket_categories");
        }
    }
}

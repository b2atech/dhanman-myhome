using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dhanman.MyHome.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class TicketServiceProviderOtps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "exit_time",
                table: "visitors",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp");

            migrationBuilder.AlterColumn<DateTime>(
                name: "entry_time",
                table: "visitors",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp");

            migrationBuilder.CreateTable(
                name: "ticket_service_provider_otps",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    otp = table.Column<string>(type: "text", nullable: false),
                    expiration_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ticket_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ticket_service_provider_otps", x => x.id);
                    table.ForeignKey(
                        name: "fk_ticket_service_provider_otps_tickets_ticket_id",
                        column: x => x.ticket_id,
                        principalTable: "tickets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_ticket_service_provider_otps_ticket_id",
                table: "ticket_service_provider_otps",
                column: "ticket_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ticket_service_provider_otps");

            migrationBuilder.AlterColumn<DateTime>(
                name: "exit_time",
                table: "visitors",
                type: "timestamp",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "entry_time",
                table: "visitors",
                type: "timestamp",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");
        }
    }
}

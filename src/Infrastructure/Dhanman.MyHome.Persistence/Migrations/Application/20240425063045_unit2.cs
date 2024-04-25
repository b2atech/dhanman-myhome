using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Dhanman.MyHome.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class unit2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "units",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    building_id = table.Column<int>(type: "integer", nullable: false),
                    floor_id = table.Column<int>(type: "integer", nullable: false),
                    unit_type_id = table.Column<int>(type: "integer", nullable: false),
                    account_id = table.Column<Guid>(type: "uuid", nullable: false),
                    area = table.Column<float>(type: "real", nullable: false),
                    bhk_type = table.Column<float>(type: "real", nullable: false),
                    occupant_type_id = table.Column<int>(type: "integer", nullable: false),
                    occupancy_type_id = table.Column<int>(type: "integer", nullable: false),
                    phone_extention = table.Column<string>(type: "text", nullable: false),
                    e_intercom = table.Column<string>(type: "text", nullable: false),
                    latitude = table.Column<string>(type: "text", nullable: false),
                    longitude = table.Column<string>(type: "text", nullable: false),
                    created_on_utc = table.Column<DateTime>(type: "timestamp", nullable: false),
                    modified_on_utc = table.Column<DateTime>(type: "timestamp", nullable: true),
                    deleted_on_utc = table.Column<DateTime>(type: "timestamp", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    modified_by = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_units", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "units");
        }
    }
}

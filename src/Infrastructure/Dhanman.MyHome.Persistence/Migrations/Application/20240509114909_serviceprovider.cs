using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Dhanman.MyHome.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class serviceprovider : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "service_provider_verifications",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    service_Provider_id = table.Column<int>(type: "integer", nullable: false),
                    verification_type_id = table.Column<int>(type: "integer", nullable: false),
                    comments = table.Column<string>(type: "text", nullable: false),
                    is_verified = table.Column<bool>(type: "boolean", nullable: false),
                    created_on_utc = table.Column<DateTime>(type: "timestamp", nullable: false),
                    modified_on_utc = table.Column<DateTime>(type: "timestamp", nullable: true),
                    deleted_on_utc = table.Column<DateTime>(type: "timestamp", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    modified_by = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_service_provider_verifications", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "service_providers",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    last_name = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    visiting_from = table.Column<string>(type: "text", nullable: false),
                    contact_number = table.Column<string>(type: "text", nullable: false),
                    permanent_address_id = table.Column<Guid>(type: "uuid", nullable: false),
                    present_address_id = table.Column<Guid>(type: "uuid", nullable: false),
                    service_provider_type_id = table.Column<int>(type: "integer", nullable: false),
                    service_provider_sub_type_id = table.Column<int>(type: "integer", nullable: false),
                    vehicle_number = table.Column<string>(type: "text", nullable: false),
                    identity_type_id = table.Column<int>(type: "integer", nullable: false),
                    identity_number = table.Column<string>(type: "text", nullable: false),
                    identity_image = table.Column<byte[]>(type: "bytea", nullable: true),
                    created_on_utc = table.Column<DateTime>(type: "timestamp", nullable: false),
                    modified_on_utc = table.Column<DateTime>(type: "timestamp", nullable: true),
                    deleted_on_utc = table.Column<DateTime>(type: "timestamp", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    modified_by = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_service_providers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "verification_types",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    deleted_on_utc = table.Column<DateTime>(type: "timestamp", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_verification_types", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "service_provider_verifications");

            migrationBuilder.DropTable(
                name: "service_providers");

            migrationBuilder.DropTable(
                name: "verification_types");
        }
    }
}

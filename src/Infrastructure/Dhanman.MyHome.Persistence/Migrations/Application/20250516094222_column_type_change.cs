using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dhanman.MyHome.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class column_type_change : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // For meeting_participants table
            migrationBuilder.Sql(@"
                ALTER TABLE meeting_participants
                ALTER COLUMN occurrence_id TYPE integer USING occurrence_id::text::integer;
            ");

            // For meeting_agenda_items table
            migrationBuilder.Sql(@"
                ALTER TABLE meeting_agenda_items
                ALTER COLUMN occurrence_id TYPE integer USING occurrence_id::text::integer;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {           
            migrationBuilder.Sql(@"
                ALTER TABLE meeting_participants
                ALTER COLUMN occurrence_id TYPE uuid USING occurrence_id::text::uuid;
            ");

            migrationBuilder.Sql(@"
                ALTER TABLE meeting_agenda_items
                ALTER COLUMN occurrence_id TYPE uuid USING occurrence_id::text::uuid;
            ");
        }
    }
}

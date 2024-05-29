using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dhanman.MyHome.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updateUnit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE units ALTER COLUMN phone_extention TYPE integer USING (NULLIF(phone_extention, '')::integer);");
            migrationBuilder.Sql("ALTER TABLE units ALTER COLUMN e_intercom TYPE integer USING (NULLIF(e_intercom, '')::integer);");
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dhanman.MyHome.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class refactorApprovedUserDateType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "start_date",
                table: "approved_visitors",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "exit_time",
                table: "approved_visitors",
                type: "time",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "interval",
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "entry_time",
                table: "approved_visitors",
                type: "time",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "interval",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "end_date",
                table: "approved_visitors",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "start_date",
                table: "approved_visitors",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "exit_time",
                table: "approved_visitors",
                type: "interval",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "time",
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "entry_time",
                table: "approved_visitors",
                type: "interval",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "time",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "end_date",
                table: "approved_visitors",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");
        }
    }
}

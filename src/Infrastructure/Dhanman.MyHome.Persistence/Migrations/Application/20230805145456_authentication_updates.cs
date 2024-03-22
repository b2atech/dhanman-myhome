using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dhanman.MyHome.Persistence.Migrations.Application
{
    /// <inheritdoc />
    public partial class authentication_updates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_user_to_role_mapping",
                table: "UserToRoleMapping");

            migrationBuilder.DropPrimaryKey(
                name: "pk_user_to_paid_modules_mapping",
                table: "UserToPaidModulesMapping");

            migrationBuilder.RenameTable(
                name: "UserToRoleMapping",
                newName: "user_paid_modules");

            migrationBuilder.RenameTable(
                name: "UserToPaidModulesMapping",
                newName: "user_roles");

            migrationBuilder.RenameTable(
                name: "invoiceStatuses",
                newName: "invoice_statuses");

            migrationBuilder.RenameTable(
                name: "InvoicePayments",
                newName: "invoice_payments");

            migrationBuilder.AddPrimaryKey(
                name: "pk_user_paid_modules",
                table: "user_paid_modules",
                columns: new[] { "user_id", "role_name" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_user_roles",
                table: "user_roles",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_user_roles",
                table: "user_roles");

            migrationBuilder.DropPrimaryKey(
                name: "pk_user_paid_modules",
                table: "user_paid_modules");

            migrationBuilder.RenameTable(
                name: "user_roles",
                newName: "UserToPaidModulesMapping");

            migrationBuilder.RenameTable(
                name: "user_paid_modules",
                newName: "UserToRoleMapping");

            migrationBuilder.RenameTable(
                name: "invoice_statuses",
                newName: "invoiceStatuses");

            migrationBuilder.RenameTable(
                name: "invoice_payments",
                newName: "InvoicePayments");

            migrationBuilder.AddPrimaryKey(
                name: "pk_user_to_paid_modules_mapping",
                table: "UserToPaidModulesMapping",
                column: "user_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_user_to_role_mapping",
                table: "UserToRoleMapping",
                columns: new[] { "user_id", "role_name" });
        }
    }
}

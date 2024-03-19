using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sanchime.Identity.Migrations
{
    /// <inheritdoc />
    public partial class Add_IsEnabled : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AddColumn<bool>(
                name: "IsEnabled",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "UserGroups",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AddColumn<bool>(
                name: "IsEnabled",
                table: "UserGroups",
                type: "boolean",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Roles",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AddColumn<bool>(
                name: "IsEnabled",
                table: "Roles",
                type: "boolean",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Permissions",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AddColumn<bool>(
                name: "IsEnabled",
                table: "Permissions",
                type: "boolean",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Accounts",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AddColumn<bool>(
                name: "IsEnabled",
                table: "Accounts",
                type: "boolean",
                nullable: false,
                defaultValue: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_IsEnabled",
                table: "Users",
                column: "IsEnabled");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroups_IsEnabled",
                table: "UserGroups",
                column: "IsEnabled");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_IsEnabled",
                table: "Roles",
                column: "IsEnabled");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_IsEnabled",
                table: "Permissions",
                column: "IsEnabled");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_IsEnabled",
                table: "Accounts",
                column: "IsEnabled");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_IsEnabled",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_UserGroups_IsEnabled",
                table: "UserGroups");

            migrationBuilder.DropIndex(
                name: "IX_Roles_IsEnabled",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_IsEnabled",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_IsEnabled",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "IsEnabled",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsEnabled",
                table: "UserGroups");

            migrationBuilder.DropColumn(
                name: "IsEnabled",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "IsEnabled",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "IsEnabled",
                table: "Accounts");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Users",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "UserGroups",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Roles",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Permissions",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Accounts",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: false);
        }
    }
}

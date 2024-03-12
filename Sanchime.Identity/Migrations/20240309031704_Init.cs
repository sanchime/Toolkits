using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sanchime.Identity.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "IdentityEntitySequence");

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "nextval('\"IdentityEntitySequence\"')"),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedUser = table.Column<long>(type: "bigint", nullable: false),
                    CreatedUserName = table.Column<string>(type: "text", nullable: false),
                    ModifiedUser = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedUserName = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ModifiedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LoginName = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    RefreshToken = table.Column<string>(type: "text", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "nextval('\"IdentityEntitySequence\"')"),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedUser = table.Column<long>(type: "bigint", nullable: false),
                    CreatedUserName = table.Column<string>(type: "text", nullable: false),
                    ModifiedUser = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedUserName = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ModifiedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ParentId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Roles_Roles_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserGroups",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "nextval('\"IdentityEntitySequence\"')"),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedUser = table.Column<long>(type: "bigint", nullable: false),
                    CreatedUserName = table.Column<string>(type: "text", nullable: false),
                    ModifiedUser = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedUserName = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ModifiedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ParentId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserGroups_UserGroups_ParentId",
                        column: x => x.ParentId,
                        principalTable: "UserGroups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "nextval('\"IdentityEntitySequence\"')"),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedUser = table.Column<long>(type: "bigint", nullable: false),
                    CreatedUserName = table.Column<string>(type: "text", nullable: false),
                    ModifiedUser = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedUserName = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ModifiedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    RoleId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permissions_Permissions_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Permissions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Permissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "nextval('\"IdentityEntitySequence\"')"),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedUser = table.Column<long>(type: "bigint", nullable: false),
                    CreatedUserName = table.Column<string>(type: "text", nullable: false),
                    ModifiedUser = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedUserName = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ModifiedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    Avatar = table.Column<string>(type: "text", nullable: true),
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    UserGroupId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_UserGroups_UserGroupId",
                        column: x => x.UserGroupId,
                        principalTable: "UserGroups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RoleUser",
                columns: table => new
                {
                    RolesId = table.Column<long>(type: "bigint", nullable: false),
                    UsersId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleUser", x => new { x.RolesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_RoleUser_Roles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CreatedDate_CreatedUser_CreatedUserName_ModifiedDa~",
                table: "Accounts",
                columns: new[] { "CreatedDate", "CreatedUser", "CreatedUserName", "ModifiedDate", "ModifiedUser", "ModifiedUserName" },
                descending: new bool[0]);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_IsDeleted",
                table: "Accounts",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_LoginName",
                table: "Accounts",
                column: "LoginName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_CreatedDate_CreatedUser_CreatedUserName_Modifie~",
                table: "Permissions",
                columns: new[] { "CreatedDate", "CreatedUser", "CreatedUserName", "ModifiedDate", "ModifiedUser", "ModifiedUserName" },
                descending: new bool[0]);

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_IsDeleted",
                table: "Permissions",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_ParentId",
                table: "Permissions",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_RoleId",
                table: "Permissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_CreatedDate_CreatedUser_CreatedUserName_ModifiedDate_~",
                table: "Roles",
                columns: new[] { "CreatedDate", "CreatedUser", "CreatedUserName", "ModifiedDate", "ModifiedUser", "ModifiedUserName" },
                descending: new bool[0]);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_IsDeleted",
                table: "Roles",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_ParentId",
                table: "Roles",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleUser_UsersId",
                table: "RoleUser",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroups_CreatedDate_CreatedUser_CreatedUserName_Modified~",
                table: "UserGroups",
                columns: new[] { "CreatedDate", "CreatedUser", "CreatedUserName", "ModifiedDate", "ModifiedUser", "ModifiedUserName" },
                descending: new bool[0]);

            migrationBuilder.CreateIndex(
                name: "IX_UserGroups_IsDeleted",
                table: "UserGroups",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroups_ParentId",
                table: "UserGroups",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AccountId",
                table: "Users",
                column: "AccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_CreatedDate_CreatedUser_CreatedUserName_ModifiedDate_~",
                table: "Users",
                columns: new[] { "CreatedDate", "CreatedUser", "CreatedUserName", "ModifiedDate", "ModifiedUser", "ModifiedUserName" },
                descending: new bool[0]);

            migrationBuilder.CreateIndex(
                name: "IX_Users_IsDeleted",
                table: "Users",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserGroupId",
                table: "Users",
                column: "UserGroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "RoleUser");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "UserGroups");

            migrationBuilder.DropSequence(
                name: "IdentityEntitySequence");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace SVU.Database.Migrations
{
    public partial class FixedSchemaNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HealthUserRoles_HealthRoles_RoleId",
                schema: "Health",
                table: "HealthUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_HealthUserRoles_Users_UserId",
                schema: "Health",
                table: "HealthUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HealthUserRoles",
                schema: "Health",
                table: "HealthUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HealthRoles",
                table: "HealthRoles");

            migrationBuilder.RenameTable(
                name: "HealthUserRoles",
                schema: "Health",
                newName: "UserRoles",
                newSchema: "Health");

            migrationBuilder.RenameTable(
                name: "HealthRoles",
                newName: "Roles",
                newSchema: "Health");

            migrationBuilder.RenameIndex(
                name: "IX_HealthUserRoles_UserId",
                schema: "Health",
                table: "UserRoles",
                newName: "IX_UserRoles_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_HealthUserRoles_RoleId",
                schema: "Health",
                table: "UserRoles",
                newName: "IX_UserRoles_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoles",
                schema: "Health",
                table: "UserRoles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                schema: "Health",
                table: "Roles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                schema: "Health",
                table: "UserRoles",
                column: "RoleId",
                principalSchema: "Health",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Users_UserId",
                schema: "Health",
                table: "UserRoles",
                column: "UserId",
                principalSchema: "Health",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                schema: "Health",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Users_UserId",
                schema: "Health",
                table: "UserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoles",
                schema: "Health",
                table: "UserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                schema: "Health",
                table: "Roles");

            migrationBuilder.RenameTable(
                name: "UserRoles",
                schema: "Health",
                newName: "HealthUserRoles",
                newSchema: "Health");

            migrationBuilder.RenameTable(
                name: "Roles",
                schema: "Health",
                newName: "HealthRoles");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoles_UserId",
                schema: "Health",
                table: "HealthUserRoles",
                newName: "IX_HealthUserRoles_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoles_RoleId",
                schema: "Health",
                table: "HealthUserRoles",
                newName: "IX_HealthUserRoles_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HealthUserRoles",
                schema: "Health",
                table: "HealthUserRoles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HealthRoles",
                table: "HealthRoles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HealthUserRoles_HealthRoles_RoleId",
                schema: "Health",
                table: "HealthUserRoles",
                column: "RoleId",
                principalTable: "HealthRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HealthUserRoles_Users_UserId",
                schema: "Health",
                table: "HealthUserRoles",
                column: "UserId",
                principalSchema: "Health",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

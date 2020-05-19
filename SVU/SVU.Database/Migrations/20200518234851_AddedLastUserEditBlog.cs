using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SVU.Database.Migrations
{
    public partial class AddedLastUserEditBlog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Users_AutherId",
                schema: "Application",
                table: "Blogs");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                schema: "Application",
                table: "Blogs",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                schema: "Application",
                table: "Blogs",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "AutherId",
                schema: "Application",
                table: "Blogs",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<Guid>(
                name: "LastEditUserId",
                schema: "Application",
                table: "Blogs",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_LastEditUserId",
                schema: "Application",
                table: "Blogs",
                column: "LastEditUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Users_AutherId",
                schema: "Application",
                table: "Blogs",
                column: "AutherId",
                principalSchema: "Health",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Users_LastEditUserId",
                schema: "Application",
                table: "Blogs",
                column: "LastEditUserId",
                principalSchema: "Health",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Users_AutherId",
                schema: "Application",
                table: "Blogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Users_LastEditUserId",
                schema: "Application",
                table: "Blogs");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_LastEditUserId",
                schema: "Application",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "LastEditUserId",
                schema: "Application",
                table: "Blogs");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                schema: "Application",
                table: "Blogs",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                schema: "Application",
                table: "Blogs",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<Guid>(
                name: "AutherId",
                schema: "Application",
                table: "Blogs",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Users_AutherId",
                schema: "Application",
                table: "Blogs",
                column: "AutherId",
                principalSchema: "Health",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

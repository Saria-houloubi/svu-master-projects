using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SVU.Database.Migrations
{
    public partial class RemovedExtraDataInHealthReplies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestReplies_Users_UserId",
                schema: "Health",
                table: "RequestReplies");

            migrationBuilder.DropColumn(
                name: "IsRequesterSide",
                schema: "Health",
                table: "RequestReplies");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                schema: "Health",
                table: "RequestReplies",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                schema: "Health",
                table: "RequestReplies",
                maxLength: 400,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestReplies_Users_UserId",
                schema: "Health",
                table: "RequestReplies",
                column: "UserId",
                principalSchema: "Health",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestReplies_Users_UserId",
                schema: "Health",
                table: "RequestReplies");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                schema: "Health",
                table: "RequestReplies",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                schema: "Health",
                table: "RequestReplies",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 400);

            migrationBuilder.AddColumn<bool>(
                name: "IsRequesterSide",
                schema: "Health",
                table: "RequestReplies",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestReplies_Users_UserId",
                schema: "Health",
                table: "RequestReplies",
                column: "UserId",
                principalSchema: "Health",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

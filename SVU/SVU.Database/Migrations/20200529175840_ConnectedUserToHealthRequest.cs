using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SVU.Database.Migrations
{
    public partial class ConnectedUserToHealthRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "HealthUserId",
                schema: "Health",
                table: "Requests",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Requests_HealthUserId",
                schema: "Health",
                table: "Requests",
                column: "HealthUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Users_HealthUserId",
                schema: "Health",
                table: "Requests",
                column: "HealthUserId",
                principalSchema: "Health",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Users_HealthUserId",
                schema: "Health",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_HealthUserId",
                schema: "Health",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "HealthUserId",
                schema: "Health",
                table: "Requests");
        }
    }
}

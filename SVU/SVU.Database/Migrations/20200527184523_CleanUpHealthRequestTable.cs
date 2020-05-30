using Microsoft.EntityFrameworkCore.Migrations;

namespace SVU.Database.Migrations
{
    public partial class CleanUpHealthRequestTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                schema: "Health",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "Age",
                schema: "Health",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "Email",
                schema: "Health",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "Gender",
                schema: "Health",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "MedicalHistory",
                schema: "Health",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "Health",
                table: "Requests");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                schema: "Health",
                table: "Requests",
                newName: "Subject");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Subject",
                schema: "Health",
                table: "Requests",
                newName: "PhoneNumber");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                schema: "Health",
                table: "Requests",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Age",
                schema: "Health",
                table: "Requests",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "Health",
                table: "Requests",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                schema: "Health",
                table: "Requests",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MedicalHistory",
                schema: "Health",
                table: "Requests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "Health",
                table: "Requests",
                nullable: true);
        }
    }
}

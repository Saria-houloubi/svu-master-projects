using Microsoft.EntityFrameworkCore.Migrations;

namespace SVU.Database.Migrations
{
    public partial class AddedPreviewContentToBlogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PreviewContent",
                schema: "Application",
                table: "Blogs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreviewContent",
                schema: "Application",
                table: "Blogs");
        }
    }
}

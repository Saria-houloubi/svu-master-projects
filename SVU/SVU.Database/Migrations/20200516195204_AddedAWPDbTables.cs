using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SVU.Database.Migrations
{
    public partial class AddedAWPDbTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Health");

            migrationBuilder.CreateTable(
                name: "HealthRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Requests",
                schema: "Health",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Age = table.Column<int>(nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    MedicalHistory = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "Health",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Blogs",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Thumbnail = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    VisitCout = table.Column<int>(nullable: false),
                    AutherId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blogs_Users_AutherId",
                        column: x => x.AutherId,
                        principalSchema: "Health",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HealthUserRoles",
                schema: "Health",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthUserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HealthUserRoles_HealthRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "HealthRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HealthUserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Health",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequestReplies",
                schema: "Health",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    IsRequesterSide = table.Column<bool>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true),
                    RequestId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestReplies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestReplies_Requests_RequestId",
                        column: x => x.RequestId,
                        principalSchema: "Health",
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequestReplies_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Health",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BlogTags",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    BlogId = table.Column<Guid>(nullable: false),
                    TagId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogTags_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalSchema: "Application",
                        principalTable: "Blogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogTags_Tags_TagId",
                        column: x => x.TagId,
                        principalSchema: "Application",
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_AutherId",
                schema: "Application",
                table: "Blogs",
                column: "AutherId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogTags_BlogId",
                schema: "Application",
                table: "BlogTags",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogTags_TagId",
                schema: "Application",
                table: "BlogTags",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthUserRoles_RoleId",
                schema: "Health",
                table: "HealthUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthUserRoles_UserId",
                schema: "Health",
                table: "HealthUserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestReplies_RequestId",
                schema: "Health",
                table: "RequestReplies",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestReplies_UserId",
                schema: "Health",
                table: "RequestReplies",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogTags",
                schema: "Application");

            migrationBuilder.DropTable(
                name: "HealthUserRoles",
                schema: "Health");

            migrationBuilder.DropTable(
                name: "RequestReplies",
                schema: "Health");

            migrationBuilder.DropTable(
                name: "Blogs",
                schema: "Application");

            migrationBuilder.DropTable(
                name: "Tags",
                schema: "Application");

            migrationBuilder.DropTable(
                name: "HealthRoles");

            migrationBuilder.DropTable(
                name: "Requests",
                schema: "Health");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "Health");
        }
    }
}

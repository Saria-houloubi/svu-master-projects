using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SVU.Database.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "SVU");

            migrationBuilder.EnsureSchema(
                name: "Application");

            migrationBuilder.EnsureSchema(
                name: "SVUDataSet");

            migrationBuilder.EnsureSchema(
                name: "Student");

            migrationBuilder.CreateTable(
                name: "Programs",
                schema: "SVU",
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
                    table.PrimaryKey("PK_Programs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HeartDisease",
                schema: "SVUDataSet",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    Age = table.Column<double>(nullable: false),
                    MaxHeartRate = table.Column<double>(nullable: false),
                    RestBloodPressure = table.Column<double>(nullable: false),
                    BloodSugar = table.Column<bool>(nullable: false),
                    ExerciceAngina = table.Column<bool>(nullable: false),
                    Disease = table.Column<bool>(nullable: false),
                    RestElectro = table.Column<string>(nullable: true),
                    ChestPainType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeartDisease", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tennis",
                schema: "SVUDataSet",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    Outlook = table.Column<string>(nullable: true),
                    Temp = table.Column<string>(nullable: true),
                    Wind = table.Column<string>(nullable: true),
                    Humidity = table.Column<string>(nullable: true),
                    Play = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tennis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                schema: "SVU",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ShortName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    ProgramId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_Programs_ProgramId",
                        column: x => x.ProgramId,
                        principalSchema: "SVU",
                        principalTable: "Programs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Homeworks",
                schema: "Student",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    CourseId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Homeworks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Homeworks_Courses_CourseId",
                        column: x => x.CourseId,
                        principalSchema: "SVU",
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                schema: "SVU",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    CourseId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sessions_Courses_CourseId",
                        column: x => x.CourseId,
                        principalSchema: "SVU",
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExternalLinks",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    ContentType = table.Column<string>(nullable: true),
                    CourseId = table.Column<Guid>(nullable: true),
                    HomeworkId = table.Column<Guid>(nullable: true),
                    ProgramId = table.Column<Guid>(nullable: true),
                    SessionId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalLinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExternalLinks_Courses_CourseId",
                        column: x => x.CourseId,
                        principalSchema: "SVU",
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExternalLinks_Homeworks_HomeworkId",
                        column: x => x.HomeworkId,
                        principalSchema: "Student",
                        principalTable: "Homeworks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExternalLinks_Programs_ProgramId",
                        column: x => x.ProgramId,
                        principalSchema: "SVU",
                        principalTable: "Programs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExternalLinks_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalSchema: "SVU",
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExternalLinks_CourseId",
                schema: "Application",
                table: "ExternalLinks",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalLinks_HomeworkId",
                schema: "Application",
                table: "ExternalLinks",
                column: "HomeworkId");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalLinks_ProgramId",
                schema: "Application",
                table: "ExternalLinks",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalLinks_SessionId",
                schema: "Application",
                table: "ExternalLinks",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Homeworks_CourseId",
                schema: "Student",
                table: "Homeworks",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_ProgramId",
                schema: "SVU",
                table: "Courses",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_CourseId",
                schema: "SVU",
                table: "Sessions",
                column: "CourseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExternalLinks",
                schema: "Application");

            migrationBuilder.DropTable(
                name: "HeartDisease",
                schema: "SVUDataSet");

            migrationBuilder.DropTable(
                name: "Tennis",
                schema: "SVUDataSet");

            migrationBuilder.DropTable(
                name: "Homeworks",
                schema: "Student");

            migrationBuilder.DropTable(
                name: "Sessions",
                schema: "SVU");

            migrationBuilder.DropTable(
                name: "Courses",
                schema: "SVU");

            migrationBuilder.DropTable(
                name: "Programs",
                schema: "SVU");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KorepetycjeNaJuz.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LessonLevels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LevelName = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LessonStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LessonSubjects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonSubjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(maxLength: 255, nullable: true),
                    LastName = table.Column<string>(maxLength: 255, nullable: true),
                    Email = table.Column<string>(maxLength: 255, nullable: true),
                    Telephone = table.Column<string>(maxLength: 15, nullable: true),
                    IsCoach = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: true),
                    Avatar = table.Column<byte[]>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CoachAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CoachId = table.Column<int>(nullable: false),
                    Latitude = table.Column<double>(nullable: true),
                    Longitude = table.Column<double>(nullable: true),
                    Address = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoachAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoachAddresses_Users_CoachId",
                        column: x => x.CoachId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CoachLessons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CoachId = table.Column<int>(nullable: false),
                    LessonSubjectId = table.Column<int>(nullable: false),
                    SubjectId = table.Column<int>(nullable: true),
                    LessonLevelId = table.Column<int>(nullable: false),
                    RatePerHour = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoachLessons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoachLessons_Users_CoachId",
                        column: x => x.CoachId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoachLessons_LessonLevels_LessonLevelId",
                        column: x => x.LessonLevelId,
                        principalTable: "LessonLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoachLessons_LessonSubjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "LessonSubjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OwnerId = table.Column<int>(nullable: false),
                    RecipientId = table.Column<int>(nullable: false),
                    Message = table.Column<string>(maxLength: 1000, nullable: true),
                    DateOfSending = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Messages_Users_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Lessons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LessonStatusId = table.Column<int>(nullable: false),
                    CoachLessonId = table.Column<int>(nullable: false),
                    StudentId = table.Column<int>(nullable: false),
                    CoachAddressId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    NumberOfHours = table.Column<int>(nullable: false),
                    RatingOfStudent = table.Column<byte>(nullable: true),
                    OpinionOfStudent = table.Column<string>(nullable: true),
                    RatingOfCoach = table.Column<byte>(nullable: true),
                    OpinionOfCoach = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lessons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lessons_CoachAddresses_CoachAddressId",
                        column: x => x.CoachAddressId,
                        principalTable: "CoachAddresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Lessons_CoachLessons_CoachLessonId",
                        column: x => x.CoachLessonId,
                        principalTable: "CoachLessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Lessons_LessonStatuses_LessonStatusId",
                        column: x => x.LessonStatusId,
                        principalTable: "LessonStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Lessons_Users_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoachAddresses_CoachId",
                table: "CoachAddresses",
                column: "CoachId");

            migrationBuilder.CreateIndex(
                name: "IX_CoachLessons_CoachId",
                table: "CoachLessons",
                column: "CoachId");

            migrationBuilder.CreateIndex(
                name: "IX_CoachLessons_LessonLevelId",
                table: "CoachLessons",
                column: "LessonLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_CoachLessons_SubjectId",
                table: "CoachLessons",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_CoachAddressId",
                table: "Lessons",
                column: "CoachAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_CoachLessonId",
                table: "Lessons",
                column: "CoachLessonId");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_LessonStatusId",
                table: "Lessons",
                column: "LessonStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_StudentId",
                table: "Lessons",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_OwnerId",
                table: "Messages",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_RecipientId",
                table: "Messages",
                column: "RecipientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lessons");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "CoachAddresses");

            migrationBuilder.DropTable(
                name: "CoachLessons");

            migrationBuilder.DropTable(
                name: "LessonStatuses");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "LessonLevels");

            migrationBuilder.DropTable(
                name: "LessonSubjects");
        }
    }
}

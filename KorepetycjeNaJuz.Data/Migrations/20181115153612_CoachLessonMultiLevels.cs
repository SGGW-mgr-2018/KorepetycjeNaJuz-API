using Microsoft.EntityFrameworkCore.Migrations;

namespace KorepetycjeNaJuz.Data.Migrations
{
    public partial class CoachLessonMultiLevels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoachLessons_LessonLevels_LessonLevelId",
                table: "CoachLessons");

            migrationBuilder.DropIndex(
                name: "IX_CoachLessons_LessonLevelId",
                table: "CoachLessons");

            migrationBuilder.DropColumn(
                name: "LessonLevelId",
                table: "CoachLessons");

            migrationBuilder.CreateTable(
                name: "CoachLessonLevels",
                columns: table => new
                {
                    CoachLessonId = table.Column<int>(nullable: false),
                    LessonLevelId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoachLessonLevels", x => new { x.CoachLessonId, x.LessonLevelId });
                    table.ForeignKey(
                        name: "FK_CoachLessonLevels_CoachLessons_CoachLessonId",
                        column: x => x.CoachLessonId,
                        principalTable: "CoachLessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoachLessonLevels_LessonLevels_LessonLevelId",
                        column: x => x.LessonLevelId,
                        principalTable: "LessonLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoachLessonLevels_LessonLevelId",
                table: "CoachLessonLevels",
                column: "LessonLevelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoachLessonLevels");

            migrationBuilder.AddColumn<int>(
                name: "LessonLevelId",
                table: "CoachLessons",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CoachLessons_LessonLevelId",
                table: "CoachLessons",
                column: "LessonLevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_CoachLessons_LessonLevels_LessonLevelId",
                table: "CoachLessons",
                column: "LessonLevelId",
                principalTable: "LessonLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

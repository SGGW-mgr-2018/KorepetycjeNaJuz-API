using Microsoft.EntityFrameworkCore.Migrations;

namespace KorepetycjeNaJuz.Data.Migrations
{
    public partial class SingleNamesOfEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoachLessons_LessonSubjects_SubjectId",
                table: "CoachLessons");

            migrationBuilder.DropIndex(
                name: "IX_CoachLessons_SubjectId",
                table: "CoachLessons");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "CoachLessons");

            migrationBuilder.RenameColumn(
                name: "Message",
                table: "Messages",
                newName: "Content");

            migrationBuilder.CreateIndex(
                name: "IX_CoachLessons_LessonSubjectId",
                table: "CoachLessons",
                column: "LessonSubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_CoachLessons_LessonSubjects_LessonSubjectId",
                table: "CoachLessons",
                column: "LessonSubjectId",
                principalTable: "LessonSubjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoachLessons_LessonSubjects_LessonSubjectId",
                table: "CoachLessons");

            migrationBuilder.DropIndex(
                name: "IX_CoachLessons_LessonSubjectId",
                table: "CoachLessons");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Messages",
                newName: "Message");

            migrationBuilder.AddColumn<int>(
                name: "SubjectId",
                table: "CoachLessons",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CoachLessons_SubjectId",
                table: "CoachLessons",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_CoachLessons_LessonSubjects_SubjectId",
                table: "CoachLessons",
                column: "SubjectId",
                principalTable: "LessonSubjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

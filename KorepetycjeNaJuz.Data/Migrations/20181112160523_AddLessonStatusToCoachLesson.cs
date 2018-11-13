using Microsoft.EntityFrameworkCore.Migrations;

namespace KorepetycjeNaJuz.Data.Migrations
{
    public partial class AddLessonStatusToCoachLesson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LessonStatusId",
                table: "CoachLessons",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Street",
                table: "Addresses",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Addresses",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CoachLessons_LessonStatusId",
                table: "CoachLessons",
                column: "LessonStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_CoachLessons_LessonStatuses_LessonStatusId",
                table: "CoachLessons",
                column: "LessonStatusId",
                principalTable: "LessonStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoachLessons_LessonStatuses_LessonStatusId",
                table: "CoachLessons");

            migrationBuilder.DropIndex(
                name: "IX_CoachLessons_LessonStatusId",
                table: "CoachLessons");

            migrationBuilder.DropColumn(
                name: "LessonStatusId",
                table: "CoachLessons");

            migrationBuilder.AlterColumn<string>(
                name: "Street",
                table: "Addresses",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Addresses",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);
        }
    }
}

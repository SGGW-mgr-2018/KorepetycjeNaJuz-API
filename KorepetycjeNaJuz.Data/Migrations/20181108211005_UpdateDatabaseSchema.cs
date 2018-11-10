using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KorepetycjeNaJuz.Data.Migrations
{
    public partial class UpdateDatabaseSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int?>(
                name: "CoachAddressId",
                table: "Lessons",
                nullable: true,
                defaultValue: false);

            migrationBuilder.Sql("UPDATE dbo.Lessons SET CoachAddressId = NULL");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_CoachAddresses_CoachAddressId",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_CoachAddressId",
                table: "Lessons");

            migrationBuilder.DropTable(
                name: "CoachAddresses");

            migrationBuilder.DropColumn(
                name: "CoachAddressId",
                table: "Lessons");

            migrationBuilder.RenameColumn(
                name: "IsCoach",
                table: "Users",
                newName: "RodoConfirmed");

            migrationBuilder.AddColumn<bool>(
                name: "CookiesConfirmed",
                table: "Users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PrivacyPolicesConfirmed",
                table: "Users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "OpinionOfStudent",
                table: "Lessons",
                maxLength: 2000,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OpinionOfCoach",
                table: "Lessons",
                maxLength: 2000,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "NumberOfHours",
                table: "Lessons",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "CoachLessons",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateEnd",
                table: "CoachLessons",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateStart",
                table: "CoachLessons",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CoachId = table.Column<int>(nullable: false),
                    Latitude = table.Column<double>(nullable: true),
                    Longitude = table.Column<double>(nullable: true),
                    City = table.Column<string>(maxLength: 1000, nullable: true),
                    Street = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Users_CoachId",
                        column: x => x.CoachId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoachLessons_AddressId",
                table: "CoachLessons",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CoachId",
                table: "Addresses",
                column: "CoachId");

            migrationBuilder.AddForeignKey(
                name: "FK_CoachLessons_Addresses_AddressId",
                table: "CoachLessons",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoachLessons_Addresses_AddressId",
                table: "CoachLessons");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_CoachLessons_AddressId",
                table: "CoachLessons");

            migrationBuilder.DropColumn(
                name: "CookiesConfirmed",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PrivacyPolicesConfirmed",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "CoachLessons");

            migrationBuilder.DropColumn(
                name: "DateEnd",
                table: "CoachLessons");

            migrationBuilder.DropColumn(
                name: "DateStart",
                table: "CoachLessons");

            migrationBuilder.RenameColumn(
                name: "RodoConfirmed",
                table: "Users",
                newName: "IsCoach");

            migrationBuilder.AlterColumn<string>(
                name: "OpinionOfStudent",
                table: "Lessons",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 2000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OpinionOfCoach",
                table: "Lessons",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 2000,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NumberOfHours",
                table: "Lessons",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AddColumn<int>(
                name: "CoachAddressId",
                table: "Lessons",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CoachAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(maxLength: 1000, nullable: true),
                    CoachId = table.Column<int>(nullable: false),
                    Latitude = table.Column<double>(nullable: true),
                    Longitude = table.Column<double>(nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_CoachAddressId",
                table: "Lessons",
                column: "CoachAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_CoachAddresses_CoachId",
                table: "CoachAddresses",
                column: "CoachId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_CoachAddresses_CoachAddressId",
                table: "Lessons",
                column: "CoachAddressId",
                principalTable: "CoachAddresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

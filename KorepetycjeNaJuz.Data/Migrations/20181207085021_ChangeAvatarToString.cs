using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KorepetycjeNaJuz.Data.Migrations
{
    public partial class ChangeAvatarToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Avatar",
                table: "Users",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldMaxLength: 1000,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Avatar",
                table: "Users",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}

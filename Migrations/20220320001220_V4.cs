using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Projekat.Migrations
{
    public partial class V4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DatumVreme",
                table: "Utakmice");

            migrationBuilder.RenameColumn(
                name: "KonacanRezultat",
                table: "Utakmice",
                newName: "Info");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Info",
                table: "Utakmice",
                newName: "KonacanRezultat");

            migrationBuilder.AddColumn<DateTime>(
                name: "DatumVreme",
                table: "Utakmice",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}

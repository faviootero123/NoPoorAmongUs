using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations;

public partial class AddingFileGuid : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "FileGUID",
            table: "StudentDocs",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "FileGUID",
            table: "StudentDocs");
    }
}

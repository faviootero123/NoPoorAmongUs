using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class addedCourseLevels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EnglishLevel",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ITLevel",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CourseLevel",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnglishLevel",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ITLevel",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "CourseLevel",
                table: "Courses");
        }
    }
}

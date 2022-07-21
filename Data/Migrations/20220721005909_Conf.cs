using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class Conf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assessments_Courses_CourseId",
                table: "Assessments");

            migrationBuilder.DropForeignKey(
                name: "FK_Assessments_Sessions_SessionId",
                table: "Assessments");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Sessions_SessionId",
                table: "Attendances");

            migrationBuilder.DropIndex(
                name: "IX_Assessments_SessionId",
                table: "Assessments");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "Assessments");

            migrationBuilder.RenameColumn(
                name: "SessionId",
                table: "Attendances",
                newName: "StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Attendances_SessionId",
                table: "Attendances",
                newName: "IX_Attendances_StudentId");

            migrationBuilder.AddColumn<int>(
                name: "SessionDateId",
                table: "Attendances",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "Assessments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "SessionDates",
                columns: table => new
                {
                    SessionDateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SessionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionDates", x => x.SessionDateId);
                    table.ForeignKey(
                        name: "FK_SessionDates_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "SessionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_SessionDateId",
                table: "Attendances",
                column: "SessionDateId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionDates_SessionId",
                table: "SessionDates",
                column: "SessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assessments_Courses_CourseId",
                table: "Assessments",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_SessionDates_SessionDateId",
                table: "Attendances",
                column: "SessionDateId",
                principalTable: "SessionDates",
                principalColumn: "SessionDateId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Students_StudentId",
                table: "Attendances",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assessments_Courses_CourseId",
                table: "Assessments");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_SessionDates_SessionDateId",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Students_StudentId",
                table: "Attendances");

            migrationBuilder.DropTable(
                name: "SessionDates");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_SessionDateId",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "SessionDateId",
                table: "Attendances");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "Attendances",
                newName: "SessionId");

            migrationBuilder.RenameIndex(
                name: "IX_Attendances_StudentId",
                table: "Attendances",
                newName: "IX_Attendances_SessionId");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Attendances",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "Assessments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SessionId",
                table: "Assessments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Assessments_SessionId",
                table: "Assessments",
                column: "SessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assessments_Courses_CourseId",
                table: "Assessments",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Assessments_Sessions_SessionId",
                table: "Assessments",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "SessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Sessions_SessionId",
                table: "Attendances",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "SessionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class ChangedAssessment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assessments_Grades_GradeId",
                table: "Assessments");

            migrationBuilder.DropForeignKey(
                name: "FK_Assessments_Sessions_SessionId",
                table: "Assessments");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Students_StudentId",
                table: "Ratings");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "Ratings",
                newName: "StudentID");

            migrationBuilder.RenameIndex(
                name: "IX_Ratings_StudentId",
                table: "Ratings",
                newName: "IX_Ratings_StudentID");

            migrationBuilder.AlterColumn<int>(
                name: "SessionId",
                table: "Assessments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "GradeId",
                table: "Assessments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "Assessments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Assessments_CourseId",
                table: "Assessments",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assessments_Courses_CourseId",
                table: "Assessments",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Assessments_Grades_GradeId",
                table: "Assessments",
                column: "GradeId",
                principalTable: "Grades",
                principalColumn: "GradeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assessments_Sessions_SessionId",
                table: "Assessments",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "SessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Students_StudentID",
                table: "Ratings",
                column: "StudentID",
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
                name: "FK_Assessments_Grades_GradeId",
                table: "Assessments");

            migrationBuilder.DropForeignKey(
                name: "FK_Assessments_Sessions_SessionId",
                table: "Assessments");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Students_StudentID",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Assessments_CourseId",
                table: "Assessments");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Assessments");

            migrationBuilder.RenameColumn(
                name: "StudentID",
                table: "Ratings",
                newName: "StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Ratings_StudentID",
                table: "Ratings",
                newName: "IX_Ratings_StudentId");

            migrationBuilder.AlterColumn<int>(
                name: "SessionId",
                table: "Assessments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GradeId",
                table: "Assessments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Assessments_Grades_GradeId",
                table: "Assessments",
                column: "GradeId",
                principalTable: "Grades",
                principalColumn: "GradeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Assessments_Sessions_SessionId",
                table: "Assessments",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "SessionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Students_StudentId",
                table: "Ratings",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class AddingAssesmentStudent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assessments_Grades_GradeId",
                table: "Assessments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Grades_GradeId",
                table: "Enrollments");

            migrationBuilder.DropTable(
                name: "Grades");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_GradeId",
                table: "Enrollments");

            migrationBuilder.DropIndex(
                name: "IX_Assessments_GradeId",
                table: "Assessments");

            migrationBuilder.DropColumn(
                name: "GradeId",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "GradeId",
                table: "Assessments");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "Assessments");

            migrationBuilder.AlterColumn<decimal>(
                name: "FinalGrade",
                table: "Enrollments",
                type: "decimal(5,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)");

            migrationBuilder.CreateTable(
                name: "AssessmentStudents",
                columns: table => new
                {
                    AssessmentStudentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Score = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    AssessmentId = table.Column<int>(type: "int", nullable: false),
                    EnrollmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssessmentStudents", x => x.AssessmentStudentId);
                    table.ForeignKey(
                        name: "FK_AssessmentStudents_Assessments_AssessmentId",
                        column: x => x.AssessmentId,
                        principalTable: "Assessments",
                        principalColumn: "AssessmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssessmentStudents_Enrollments_EnrollmentId",
                        column: x => x.EnrollmentId,
                        principalTable: "Enrollments",
                        principalColumn: "EnrollmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentStudents_AssessmentId",
                table: "AssessmentStudents",
                column: "AssessmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentStudents_EnrollmentId",
                table: "AssessmentStudents",
                column: "EnrollmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssessmentStudents");

            migrationBuilder.AlterColumn<decimal>(
                name: "FinalGrade",
                table: "Enrollments",
                type: "decimal(5,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GradeId",
                table: "Enrollments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GradeId",
                table: "Assessments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Score",
                table: "Assessments",
                type: "decimal(5,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "Grades",
                columns: table => new
                {
                    GradeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssessmentGrade = table.Column<string>(type: "char(2)", nullable: false),
                    BeginningRange = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    EndingRange = table.Column<decimal>(type: "decimal(5,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grades", x => x.GradeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_GradeId",
                table: "Enrollments",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_Assessments_GradeId",
                table: "Assessments",
                column: "GradeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assessments_Grades_GradeId",
                table: "Assessments",
                column: "GradeId",
                principalTable: "Grades",
                principalColumn: "GradeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Grades_GradeId",
                table: "Enrollments",
                column: "GradeId",
                principalTable: "Grades",
                principalColumn: "GradeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

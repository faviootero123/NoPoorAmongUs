using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class DidSomeMoreChangesToModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Criteria_Ratings_RatingId",
                table: "Criteria");

            migrationBuilder.DropForeignKey(
                name: "FK_Notes_NoteTypes_NoteTypeId",
                table: "Notes");

            migrationBuilder.DropTable(
                name: "SessionAssessments");

            migrationBuilder.DropIndex(
                name: "IX_Criteria_RatingId",
                table: "Criteria");

            migrationBuilder.DropColumn(
                name: "RatingId",
                table: "Criteria");

            migrationBuilder.DropColumn(
                name: "CourseName",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "SchoolName",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Subject",
                table: "Courses");

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "StudentDocs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CriterionId",
                table: "Ratings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Topic",
                table: "Notes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "FinalGrade",
                table: "Enrollments",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "GradeId",
                table: "Enrollments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SchoolId",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SubjectId",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GradeId",
                table: "Assessments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AccessType",
                columns: table => new
                {
                    AccessTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessType", x => x.AccessTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Grade",
                columns: table => new
                {
                    GradeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssessmentGrade = table.Column<string>(type: "char(2)", nullable: false),
                    BeginningRange = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EndingRange = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grade", x => x.GradeId);
                });

            migrationBuilder.CreateTable(
                name: "School",
                columns: table => new
                {
                    SchoolId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SchoolName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_School", x => x.SchoolId);
                });

            migrationBuilder.CreateTable(
                name: "Subject",
                columns: table => new
                {
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => x.SubjectId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentDocs_TypeId",
                table: "StudentDocs",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_CriterionId",
                table: "Ratings",
                column: "CriterionId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_GradeId",
                table: "Enrollments",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_SchoolId",
                table: "Courses",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_SubjectId",
                table: "Courses",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Assessments_GradeId",
                table: "Assessments",
                column: "GradeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assessments_Grade_GradeId",
                table: "Assessments",
                column: "GradeId",
                principalTable: "Grade",
                principalColumn: "GradeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_School_SchoolId",
                table: "Courses",
                column: "SchoolId",
                principalTable: "School",
                principalColumn: "SchoolId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Subject_SubjectId",
                table: "Courses",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "SubjectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Grade_GradeId",
                table: "Enrollments",
                column: "GradeId",
                principalTable: "Grade",
                principalColumn: "GradeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_AccessType_NoteTypeId",
                table: "Notes",
                column: "NoteTypeId",
                principalTable: "AccessType",
                principalColumn: "AccessTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Criteria_CriterionId",
                table: "Ratings",
                column: "CriterionId",
                principalTable: "Criteria",
                principalColumn: "CriterionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentDocs_AccessType_TypeId",
                table: "StudentDocs",
                column: "TypeId",
                principalTable: "AccessType",
                principalColumn: "AccessTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assessments_Grade_GradeId",
                table: "Assessments");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_School_SchoolId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Subject_SubjectId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Grade_GradeId",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Notes_AccessType_NoteTypeId",
                table: "Notes");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Criteria_CriterionId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentDocs_AccessType_TypeId",
                table: "StudentDocs");

            migrationBuilder.DropTable(
                name: "AccessType");

            migrationBuilder.DropTable(
                name: "Grade");

            migrationBuilder.DropTable(
                name: "School");

            migrationBuilder.DropTable(
                name: "Subject");

            migrationBuilder.DropIndex(
                name: "IX_StudentDocs_TypeId",
                table: "StudentDocs");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_CriterionId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_GradeId",
                table: "Enrollments");

            migrationBuilder.DropIndex(
                name: "IX_Courses_SchoolId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_SubjectId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Assessments_GradeId",
                table: "Assessments");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "StudentDocs");

            migrationBuilder.DropColumn(
                name: "CriterionId",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "Topic",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "GradeId",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "SchoolId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "GradeId",
                table: "Assessments");

            migrationBuilder.AlterColumn<int>(
                name: "FinalGrade",
                table: "Enrollments",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<int>(
                name: "RatingId",
                table: "Criteria",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CourseName",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SchoolName",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "SessionAssessments",
                columns: table => new
                {
                    SessionAssessmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssessmentId = table.Column<int>(type: "int", nullable: false),
                    SessionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionAssessments", x => x.SessionAssessmentId);
                    table.ForeignKey(
                        name: "FK_SessionAssessments_Assessments_AssessmentId",
                        column: x => x.AssessmentId,
                        principalTable: "Assessments",
                        principalColumn: "AssessmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SessionAssessments_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "SessionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Criteria_RatingId",
                table: "Criteria",
                column: "RatingId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionAssessments_AssessmentId",
                table: "SessionAssessments",
                column: "AssessmentId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionAssessments_SessionId",
                table: "SessionAssessments",
                column: "SessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Criteria_Ratings_RatingId",
                table: "Criteria",
                column: "RatingId",
                principalTable: "Ratings",
                principalColumn: "RatingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_NoteTypes_NoteTypeId",
                table: "Notes",
                column: "NoteTypeId",
                principalTable: "NoteTypes",
                principalColumn: "NoteTypeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class NewBaseLine2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SessionAssessments_Sessions_SessionId",
                table: "SessionAssessments");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Employees_EmployeeId",
                table: "Sessions");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Schools_SchoolId",
                table: "Subjects");

            migrationBuilder.DropTable(
                name: "Grades");

            migrationBuilder.DropTable(
                name: "Assesment");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_SchoolId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_SessionAssessments_SessionId",
                table: "SessionAssessments");

            migrationBuilder.DropColumn(
                name: "SchoolId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "SessionAssessments");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "StudentDocs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "StudentDocs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "StudentDocs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "Sessions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SessionAssessmentId",
                table: "Sessions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Topic",
                table: "Notes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "FacultyMembers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Extension",
                table: "DocTypes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_SessionAssessmentId",
                table: "Sessions",
                column: "SessionAssessmentId");

            migrationBuilder.CreateIndex(
                name: "IX_FacultyMembers_ApplicationUserId",
                table: "FacultyMembers",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FacultyMembers_AspNetUsers_ApplicationUserId",
                table: "FacultyMembers",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Employees_EmployeeId",
                table: "Sessions",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_SessionAssessments_SessionAssessmentId",
                table: "Sessions",
                column: "SessionAssessmentId",
                principalTable: "SessionAssessments",
                principalColumn: "SessionAssessmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FacultyMembers_AspNetUsers_ApplicationUserId",
                table: "FacultyMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Employees_EmployeeId",
                table: "Sessions");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_SessionAssessments_SessionAssessmentId",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_Sessions_SessionAssessmentId",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_FacultyMembers_ApplicationUserId",
                table: "FacultyMembers");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "StudentDocs");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "StudentDocs");

            migrationBuilder.DropColumn(
                name: "Path",
                table: "StudentDocs");

            migrationBuilder.DropColumn(
                name: "SessionAssessmentId",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "Topic",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "FacultyMembers");

            migrationBuilder.DropColumn(
                name: "Extension",
                table: "DocTypes");

            migrationBuilder.AddColumn<int>(
                name: "SchoolId",
                table: "Subjects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "Sessions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "SessionId",
                table: "SessionAssessments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Assesment",
                columns: table => new
                {
                    AssesmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assesment", x => x.AssesmentId);
                });

            migrationBuilder.CreateTable(
                name: "Grades",
                columns: table => new
                {
                    GradeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssementAssesmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grades", x => x.GradeId);
                    table.ForeignKey(
                        name: "FK_Grades_Assesment_AssementAssesmentId",
                        column: x => x.AssementAssesmentId,
                        principalTable: "Assesment",
                        principalColumn: "AssesmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_SchoolId",
                table: "Subjects",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionAssessments_SessionId",
                table: "SessionAssessments",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_AssementAssesmentId",
                table: "Grades",
                column: "AssementAssesmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_SessionAssessments_Sessions_SessionId",
                table: "SessionAssessments",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "SessionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Employees_EmployeeId",
                table: "Sessions",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Schools_SchoolId",
                table: "Subjects",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "SchoolId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

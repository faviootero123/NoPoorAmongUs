using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class RemoveFacultyMember : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_FacultyMembers_FacultyMemberId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Notes_FacultyMembers_FacultyMemberId",
                table: "Notes");

            migrationBuilder.DropTable(
                name: "FacultyMembers");

            migrationBuilder.DropIndex(
                name: "IX_Notes_FacultyMemberId",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Courses_FacultyMemberId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "FacultyMemberId",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "FacultyMemberId",
                table: "Courses");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Notes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Courses",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_ApplicationUserId",
                table: "Notes",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_ApplicationUserId",
                table: "Courses",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_AspNetUsers_ApplicationUserId",
                table: "Courses",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_AspNetUsers_ApplicationUserId",
                table: "Notes",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_AspNetUsers_ApplicationUserId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Notes_AspNetUsers_ApplicationUserId",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_ApplicationUserId",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Courses_ApplicationUserId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "FacultyMemberId",
                table: "Notes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FacultyMemberId",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "FacultyMembers",
                columns: table => new
                {
                    FacultyMemberId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    IsInstructor = table.Column<bool>(type: "bit", nullable: false),
                    IsRater = table.Column<bool>(type: "bit", nullable: false),
                    IsSocialWorker = table.Column<bool>(type: "bit", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacultyMembers", x => x.FacultyMemberId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notes_FacultyMemberId",
                table: "Notes",
                column: "FacultyMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_FacultyMemberId",
                table: "Courses",
                column: "FacultyMemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_FacultyMembers_FacultyMemberId",
                table: "Courses",
                column: "FacultyMemberId",
                principalTable: "FacultyMembers",
                principalColumn: "FacultyMemberId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_FacultyMembers_FacultyMemberId",
                table: "Notes",
                column: "FacultyMemberId",
                principalTable: "FacultyMembers",
                principalColumn: "FacultyMemberId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

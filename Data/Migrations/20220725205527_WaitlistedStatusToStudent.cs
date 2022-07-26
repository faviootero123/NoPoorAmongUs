using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class WaitlistedStatusToStudent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SubjectName",
                table: "Subjects",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Terms_TermId_IsActive",
                table: "Terms",
                columns: new[] { "TermId", "IsActive" });

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_SubjectId_SubjectName",
                table: "Subjects",
                columns: new[] { "SubjectId", "SubjectName" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Terms_TermId_IsActive",
                table: "Terms");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_SubjectId_SubjectName",
                table: "Subjects");

            migrationBuilder.AlterColumn<string>(
                name: "SubjectName",
                table: "Subjects",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}

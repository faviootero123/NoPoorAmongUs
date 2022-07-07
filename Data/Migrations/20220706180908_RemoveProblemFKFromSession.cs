using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class RemoveProblemFKFromSession : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Attendances_AttendanceId",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_Sessions_AttendanceId",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "AttendanceId",
                table: "Sessions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AttendanceId",
                table: "Sessions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_AttendanceId",
                table: "Sessions",
                column: "AttendanceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Attendances_AttendanceId",
                table: "Sessions",
                column: "AttendanceId",
                principalTable: "Attendances",
                principalColumn: "AttendanceId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

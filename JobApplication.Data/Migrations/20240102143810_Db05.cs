using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobApplication.Data.Migrations
{
    /// <inheritdoc />
    public partial class Db05 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_JobSeekers_JobSeekerProfileId",
                table: "Skills");

            migrationBuilder.RenameColumn(
                name: "JobSeekerProfileId",
                table: "Skills",
                newName: "JobSeekerId");

            migrationBuilder.RenameIndex(
                name: "IX_Skills_JobSeekerProfileId",
                table: "Skills",
                newName: "IX_Skills_JobSeekerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_JobSeekers_JobSeekerId",
                table: "Skills",
                column: "JobSeekerId",
                principalTable: "JobSeekers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_JobSeekers_JobSeekerId",
                table: "Skills");

            migrationBuilder.RenameColumn(
                name: "JobSeekerId",
                table: "Skills",
                newName: "JobSeekerProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_Skills_JobSeekerId",
                table: "Skills",
                newName: "IX_Skills_JobSeekerProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_JobSeekers_JobSeekerProfileId",
                table: "Skills",
                column: "JobSeekerProfileId",
                principalTable: "JobSeekers",
                principalColumn: "Id");
        }
    }
}

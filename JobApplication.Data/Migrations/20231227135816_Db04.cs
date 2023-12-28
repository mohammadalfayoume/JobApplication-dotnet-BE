using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobApplication.Data.Migrations
{
    /// <inheritdoc />
    public partial class Db04 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_JobSeekers_ProfilePictureFileId",
                table: "JobSeekers");

            migrationBuilder.DropIndex(
                name: "IX_JobSeekers_ResumeFileId",
                table: "JobSeekers");

            migrationBuilder.DropIndex(
                name: "IX_Companies_ProfilePictureFileId",
                table: "Companies");

            migrationBuilder.AlterColumn<int>(
                name: "ResumeFileId",
                table: "JobSeekers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ProfilePictureFileId",
                table: "JobSeekers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "JobSeekers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "JobSeekers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ProfilePictureFileId",
                table: "Companies",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "Companies",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "Companies",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_JobSeekers_ProfilePictureFileId",
                table: "JobSeekers",
                column: "ProfilePictureFileId",
                unique: true,
                filter: "[ProfilePictureFileId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_JobSeekers_ResumeFileId",
                table: "JobSeekers",
                column: "ResumeFileId",
                unique: true,
                filter: "[ResumeFileId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_ProfilePictureFileId",
                table: "Companies",
                column: "ProfilePictureFileId",
                unique: true,
                filter: "[ProfilePictureFileId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_JobSeekers_ProfilePictureFileId",
                table: "JobSeekers");

            migrationBuilder.DropIndex(
                name: "IX_JobSeekers_ResumeFileId",
                table: "JobSeekers");

            migrationBuilder.DropIndex(
                name: "IX_Companies_ProfilePictureFileId",
                table: "Companies");

            migrationBuilder.AlterColumn<int>(
                name: "ResumeFileId",
                table: "JobSeekers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProfilePictureFileId",
                table: "JobSeekers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "JobSeekers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "JobSeekers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProfilePictureFileId",
                table: "Companies",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "Companies",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "Companies",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobSeekers_ProfilePictureFileId",
                table: "JobSeekers",
                column: "ProfilePictureFileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobSeekers_ResumeFileId",
                table: "JobSeekers",
                column: "ResumeFileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_ProfilePictureFileId",
                table: "Companies",
                column: "ProfilePictureFileId",
                unique: true);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobApplication.Data.Migrations
{
    /// <inheritdoc />
    public partial class Db03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_JobSeekers_CityId",
                table: "JobSeekers");

            migrationBuilder.DropIndex(
                name: "IX_JobSeekers_CountryId",
                table: "JobSeekers");

            migrationBuilder.DropIndex(
                name: "IX_Companies_CityId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_CountryId",
                table: "Companies");

            migrationBuilder.CreateIndex(
                name: "IX_JobSeekers_CityId",
                table: "JobSeekers",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_JobSeekers_CountryId",
                table: "JobSeekers",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CityId",
                table: "Companies",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CountryId",
                table: "Companies",
                column: "CountryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_JobSeekers_CityId",
                table: "JobSeekers");

            migrationBuilder.DropIndex(
                name: "IX_JobSeekers_CountryId",
                table: "JobSeekers");

            migrationBuilder.DropIndex(
                name: "IX_Companies_CityId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_CountryId",
                table: "Companies");

            migrationBuilder.CreateIndex(
                name: "IX_JobSeekers_CityId",
                table: "JobSeekers",
                column: "CityId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobSeekers_CountryId",
                table: "JobSeekers",
                column: "CountryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CityId",
                table: "Companies",
                column: "CityId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CountryId",
                table: "Companies",
                column: "CountryId",
                unique: true);
        }
    }
}

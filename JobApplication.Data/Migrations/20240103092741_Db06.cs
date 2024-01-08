using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobApplication.Data.Migrations
{
    /// <inheritdoc />
    public partial class Db06 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_JobSeekers_JobSeekerId",
                table: "Skills");

            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Jobs_JobId",
                table: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_Skills_JobId",
                table: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_Skills_JobSeekerId",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "JobId",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "JobSeekerId",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Skills");

            migrationBuilder.CreateTable(
                name: "JobSeekerSkills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobSeekerId = table.Column<int>(type: "int", nullable: false),
                    SkillId = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    UpdatedById = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobSeekerSkills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobSeekerSkills_JobSeekers_JobSeekerId",
                        column: x => x.JobSeekerId,
                        principalTable: "JobSeekers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JobSeekerSkills_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobSkills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobId = table.Column<int>(type: "int", nullable: true),
                    SkillId = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    UpdatedById = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobSkills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobSkills_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JobSkills_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobSeekerSkills_JobSeekerId",
                table: "JobSeekerSkills",
                column: "JobSeekerId");

            migrationBuilder.CreateIndex(
                name: "IX_JobSeekerSkills_SkillId",
                table: "JobSeekerSkills",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_JobSkills_JobId",
                table: "JobSkills",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_JobSkills_SkillId",
                table: "JobSkills",
                column: "SkillId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobSeekerSkills");

            migrationBuilder.DropTable(
                name: "JobSkills");

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Skills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Skills",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "JobId",
                table: "Skills",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "JobSeekerId",
                table: "Skills",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedById",
                table: "Skills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Skills",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Skills_JobId",
                table: "Skills",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_JobSeekerId",
                table: "Skills",
                column: "JobSeekerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_JobSeekers_JobSeekerId",
                table: "Skills",
                column: "JobSeekerId",
                principalTable: "JobSeekers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Jobs_JobId",
                table: "Skills",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id");
        }
    }
}

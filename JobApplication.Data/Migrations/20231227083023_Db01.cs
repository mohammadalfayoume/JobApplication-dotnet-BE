using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobApplication.Data.Migrations
{
    /// <inheritdoc />
    public partial class Db01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_JobTypeLookup_JobTypeLookupId",
                table: "Jobs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobTypeLookup",
                table: "JobTypeLookup");

            migrationBuilder.RenameTable(
                name: "JobTypeLookup",
                newName: "JobTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobTypes",
                table: "JobTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_JobTypes_JobTypeLookupId",
                table: "Jobs",
                column: "JobTypeLookupId",
                principalTable: "JobTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_JobTypes_JobTypeLookupId",
                table: "Jobs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobTypes",
                table: "JobTypes");

            migrationBuilder.RenameTable(
                name: "JobTypes",
                newName: "JobTypeLookup");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobTypeLookup",
                table: "JobTypeLookup",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_JobTypeLookup_JobTypeLookupId",
                table: "Jobs",
                column: "JobTypeLookupId",
                principalTable: "JobTypeLookup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

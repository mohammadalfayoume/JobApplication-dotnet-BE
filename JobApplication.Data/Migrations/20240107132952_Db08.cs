using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobApplication.Data.Migrations
{
    /// <inheritdoc />
    public partial class Db08 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Files");

            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "Files",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "Files");

            migrationBuilder.AddColumn<byte[]>(
                name: "Content",
                table: "Files",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}

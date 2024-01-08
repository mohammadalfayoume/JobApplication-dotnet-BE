using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobApplication.Data.Migrations
{
    /// <inheritdoc />
    public partial class Db07 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Content",
                table: "Files",
                type: "varbinary(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Files");
        }
    }
}

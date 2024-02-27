using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Banq.Migrations
{
    /// <inheritdoc />
    public partial class file_link_for_question : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileLink",
                table: "Questions",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileLink",
                table: "Questions");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Banq.Migrations
{
    /// <inheritdoc />
    public partial class field_and_lesson_added_to_question : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FieldCode",
                table: "Questions",
                type: "varchar(2)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "Grade",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LessonCode",
                table: "Questions",
                type: "varchar(5)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_FieldCode",
                table: "Questions",
                column: "FieldCode");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_LessonCode",
                table: "Questions",
                column: "LessonCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Fields_FieldCode",
                table: "Questions",
                column: "FieldCode",
                principalTable: "Fields",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Lessons_LessonCode",
                table: "Questions",
                column: "LessonCode",
                principalTable: "Lessons",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Fields_FieldCode",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Lessons_LessonCode",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_FieldCode",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_LessonCode",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "FieldCode",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "Grade",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "LessonCode",
                table: "Questions");
        }
    }
}

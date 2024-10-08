using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnglishTutorAI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStudyTopicToTheDocumentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudyTopic",
                table: "Documents",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudyTopic",
                table: "Documents");
        }
    }
}

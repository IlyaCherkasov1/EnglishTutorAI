using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnglishTutorAI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTranslatedTextToLinguaFixTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConversationRole",
                table: "LinguaFixMessages");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "LinguaFixMessages",
                newName: "TranslatedText");

            migrationBuilder.AddColumn<string>(
                name: "CorrectedText",
                table: "LinguaFixMessages",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CorrectedText",
                table: "LinguaFixMessages");

            migrationBuilder.RenameColumn(
                name: "TranslatedText",
                table: "LinguaFixMessages",
                newName: "Content");

            migrationBuilder.AddColumn<int>(
                name: "ConversationRole",
                table: "LinguaFixMessages",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}

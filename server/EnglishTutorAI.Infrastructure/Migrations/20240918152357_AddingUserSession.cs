using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnglishTutorAI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingUserSession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Token",
                table: "RefreshTokens",
                newName: "RefreshToken");

            migrationBuilder.AddColumn<bool>(
                name: "IsValid",
                table: "RefreshTokens",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsValid",
                table: "RefreshTokens");

            migrationBuilder.RenameColumn(
                name: "RefreshToken",
                table: "RefreshTokens",
                newName: "Token");
        }
    }
}

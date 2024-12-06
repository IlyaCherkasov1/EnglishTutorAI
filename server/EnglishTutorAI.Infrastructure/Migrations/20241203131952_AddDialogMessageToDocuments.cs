using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnglishTutorAI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDialogMessageToDocuments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DocumentId",
                table: "DialogMessages",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_DialogMessages_DocumentId",
                table: "DialogMessages",
                column: "DocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_DialogMessages_Documents_DocumentId",
                table: "DialogMessages",
                column: "DocumentId",
                principalTable: "Documents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DialogMessages_Documents_DocumentId",
                table: "DialogMessages");

            migrationBuilder.DropIndex(
                name: "IX_DialogMessages_DocumentId",
                table: "DialogMessages");

            migrationBuilder.DropColumn(
                name: "DocumentId",
                table: "DialogMessages");
        }
    }
}

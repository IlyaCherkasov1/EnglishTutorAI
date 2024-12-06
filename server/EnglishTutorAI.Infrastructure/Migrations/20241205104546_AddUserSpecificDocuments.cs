using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnglishTutorAI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserSpecificDocuments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DialogMessages_Documents_DocumentId",
                table: "DialogMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_LinguaFixMessages_DocumentSessions_DocumentSessionId",
                table: "LinguaFixMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_LinguaFixMessages_Documents_DocumentId",
                table: "LinguaFixMessages");

            migrationBuilder.DropTable(
                name: "DocumentSessions");

            migrationBuilder.DropIndex(
                name: "IX_LinguaFixMessages_DocumentId",
                table: "LinguaFixMessages");

            migrationBuilder.DropColumn(
                name: "DocumentId",
                table: "LinguaFixMessages");

            migrationBuilder.DropColumn(
                name: "ThreadId",
                table: "LinguaFixMessages");

            migrationBuilder.DropColumn(
                name: "CurrentLine",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "ThreadId",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "ThreadId",
                table: "DialogMessages");

            migrationBuilder.RenameColumn(
                name: "DocumentId",
                table: "UserDocumentCompletions",
                newName: "UserDocumentId");

            migrationBuilder.RenameColumn(
                name: "DocumentSessionId",
                table: "LinguaFixMessages",
                newName: "UserDocumentId");

            migrationBuilder.RenameIndex(
                name: "IX_LinguaFixMessages_DocumentSessionId",
                table: "LinguaFixMessages",
                newName: "IX_LinguaFixMessages_UserDocumentId");

            migrationBuilder.RenameColumn(
                name: "DocumentId",
                table: "DialogMessages",
                newName: "UserDocumentId");

            migrationBuilder.RenameIndex(
                name: "IX_DialogMessages_DocumentId",
                table: "DialogMessages",
                newName: "IX_DialogMessages_UserDocumentId");

            migrationBuilder.CreateTable(
                name: "UserDocument",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CurrentLine = table.Column<int>(type: "integer", nullable: false),
                    ThreadId = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsCompleted = table.Column<bool>(type: "boolean", nullable: false),
                    SessionId = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserDocument_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserDocument_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserDocument_DocumentId",
                table: "UserDocument",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDocument_UserId",
                table: "UserDocument",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DialogMessages_UserDocument_UserDocumentId",
                table: "DialogMessages",
                column: "UserDocumentId",
                principalTable: "UserDocument",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LinguaFixMessages_UserDocument_UserDocumentId",
                table: "LinguaFixMessages",
                column: "UserDocumentId",
                principalTable: "UserDocument",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DialogMessages_UserDocument_UserDocumentId",
                table: "DialogMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_LinguaFixMessages_UserDocument_UserDocumentId",
                table: "LinguaFixMessages");

            migrationBuilder.DropTable(
                name: "UserDocument");

            migrationBuilder.RenameColumn(
                name: "UserDocumentId",
                table: "UserDocumentCompletions",
                newName: "DocumentId");

            migrationBuilder.RenameColumn(
                name: "UserDocumentId",
                table: "LinguaFixMessages",
                newName: "DocumentSessionId");

            migrationBuilder.RenameIndex(
                name: "IX_LinguaFixMessages_UserDocumentId",
                table: "LinguaFixMessages",
                newName: "IX_LinguaFixMessages_DocumentSessionId");

            migrationBuilder.RenameColumn(
                name: "UserDocumentId",
                table: "DialogMessages",
                newName: "DocumentId");

            migrationBuilder.RenameIndex(
                name: "IX_DialogMessages_UserDocumentId",
                table: "DialogMessages",
                newName: "IX_DialogMessages_DocumentId");

            migrationBuilder.AddColumn<Guid>(
                name: "DocumentId",
                table: "LinguaFixMessages",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "ThreadId",
                table: "LinguaFixMessages",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CurrentLine",
                table: "Documents",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ThreadId",
                table: "Documents",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ThreadId",
                table: "DialogMessages",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "DocumentSessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentSessions_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LinguaFixMessages_DocumentId",
                table: "LinguaFixMessages",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentSessions_DocumentId",
                table: "DocumentSessions",
                column: "DocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_DialogMessages_Documents_DocumentId",
                table: "DialogMessages",
                column: "DocumentId",
                principalTable: "Documents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LinguaFixMessages_DocumentSessions_DocumentSessionId",
                table: "LinguaFixMessages",
                column: "DocumentSessionId",
                principalTable: "DocumentSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LinguaFixMessages_Documents_DocumentId",
                table: "LinguaFixMessages",
                column: "DocumentId",
                principalTable: "Documents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

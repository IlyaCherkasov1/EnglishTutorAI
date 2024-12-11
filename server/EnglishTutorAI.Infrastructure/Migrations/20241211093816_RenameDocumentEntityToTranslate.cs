using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnglishTutorAI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameDocumentEntityToTranslate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DialogMessages_UserDocument_UserDocumentId",
                table: "DialogMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_LinguaFixMessages_UserDocument_UserDocumentId",
                table: "LinguaFixMessages");

            migrationBuilder.DropTable(
                name: "DocumentSentence");

            migrationBuilder.DropTable(
                name: "UserDocument");

            migrationBuilder.DropTable(
                name: "UserDocumentCompletions");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.RenameColumn(
                name: "UserDocumentId",
                table: "LinguaFixMessages",
                newName: "UserTranslateId");

            migrationBuilder.RenameIndex(
                name: "IX_LinguaFixMessages_UserDocumentId",
                table: "LinguaFixMessages",
                newName: "IX_LinguaFixMessages_UserTranslateId");

            migrationBuilder.RenameColumn(
                name: "UserDocumentId",
                table: "DialogMessages",
                newName: "UserTranslateId");

            migrationBuilder.RenameIndex(
                name: "IX_DialogMessages_UserDocumentId",
                table: "DialogMessages",
                newName: "IX_DialogMessages_UserTranslateId");

            migrationBuilder.CreateTable(
                name: "Translates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    StudyTopic = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTranslateCompletions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserTranslateId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTranslateCompletions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTranslateCompletions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TranslateSentence",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    Position = table.Column<int>(type: "integer", nullable: false),
                    TranslatesId = table.Column<Guid>(type: "uuid", nullable: false),
                    TranslateId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TranslateSentence", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TranslateSentence_Translates_TranslateId",
                        column: x => x.TranslateId,
                        principalTable: "Translates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserTranslate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CurrentLine = table.Column<int>(type: "integer", nullable: false),
                    ThreadId = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsCompleted = table.Column<bool>(type: "boolean", nullable: false),
                    CompletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SessionId = table.Column<Guid>(type: "uuid", nullable: false),
                    TranslateId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTranslate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTranslate_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTranslate_Translates_TranslateId",
                        column: x => x.TranslateId,
                        principalTable: "Translates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TranslateSentence_TranslateId",
                table: "TranslateSentence",
                column: "TranslateId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTranslate_TranslateId",
                table: "UserTranslate",
                column: "TranslateId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTranslate_UserId",
                table: "UserTranslate",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTranslateCompletions_UserId",
                table: "UserTranslateCompletions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DialogMessages_UserTranslate_UserTranslateId",
                table: "DialogMessages",
                column: "UserTranslateId",
                principalTable: "UserTranslate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LinguaFixMessages_UserTranslate_UserTranslateId",
                table: "LinguaFixMessages",
                column: "UserTranslateId",
                principalTable: "UserTranslate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DialogMessages_UserTranslate_UserTranslateId",
                table: "DialogMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_LinguaFixMessages_UserTranslate_UserTranslateId",
                table: "LinguaFixMessages");

            migrationBuilder.DropTable(
                name: "TranslateSentence");

            migrationBuilder.DropTable(
                name: "UserTranslate");

            migrationBuilder.DropTable(
                name: "UserTranslateCompletions");

            migrationBuilder.DropTable(
                name: "Translates");

            migrationBuilder.RenameColumn(
                name: "UserTranslateId",
                table: "LinguaFixMessages",
                newName: "UserDocumentId");

            migrationBuilder.RenameIndex(
                name: "IX_LinguaFixMessages_UserTranslateId",
                table: "LinguaFixMessages",
                newName: "IX_LinguaFixMessages_UserDocumentId");

            migrationBuilder.RenameColumn(
                name: "UserTranslateId",
                table: "DialogMessages",
                newName: "UserDocumentId");

            migrationBuilder.RenameIndex(
                name: "IX_DialogMessages_UserTranslateId",
                table: "DialogMessages",
                newName: "IX_DialogMessages_UserDocumentId");

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    StudyTopic = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserDocumentCompletions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserDocumentId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDocumentCompletions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserDocumentCompletions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocumentSentence",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Position = table.Column<int>(type: "integer", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentSentence", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentSentence_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserDocument",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CurrentLine = table.Column<int>(type: "integer", nullable: false),
                    IsCompleted = table.Column<bool>(type: "boolean", nullable: false),
                    SessionId = table.Column<Guid>(type: "uuid", nullable: false),
                    ThreadId = table.Column<string>(type: "text", nullable: false)
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
                name: "IX_DocumentSentence_DocumentId",
                table: "DocumentSentence",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDocument_DocumentId",
                table: "UserDocument",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDocument_UserId",
                table: "UserDocument",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDocumentCompletions_UserId",
                table: "UserDocumentCompletions",
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
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnglishTutorAI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFkConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("9e2c2f8b-410e-422f-a472-dae5bf8f7ee7"), new Guid("4931e704-6fba-419f-921c-a39840ceee3a") });

            migrationBuilder.RenameColumn(
                name: "SessionId",
                table: "LinguaFixMessages",
                newName: "DocumentSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAchievements_UserId",
                table: "UserAchievements",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LinguaFixMessages_DocumentSessionId",
                table: "LinguaFixMessages",
                column: "DocumentSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentSessions_DocumentId",
                table: "DocumentSessions",
                column: "DocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentSessions_Documents_DocumentId",
                table: "DocumentSessions",
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
                name: "FK_UserAchievements_AspNetUsers_UserId",
                table: "UserAchievements",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentSessions_Documents_DocumentId",
                table: "DocumentSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_LinguaFixMessages_DocumentSessions_DocumentSessionId",
                table: "LinguaFixMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAchievements_AspNetUsers_UserId",
                table: "UserAchievements");

            migrationBuilder.DropIndex(
                name: "IX_UserAchievements_UserId",
                table: "UserAchievements");

            migrationBuilder.DropIndex(
                name: "IX_LinguaFixMessages_DocumentSessionId",
                table: "LinguaFixMessages");

            migrationBuilder.DropIndex(
                name: "IX_DocumentSessions_DocumentId",
                table: "DocumentSessions");

            migrationBuilder.RenameColumn(
                name: "DocumentSessionId",
                table: "LinguaFixMessages",
                newName: "SessionId");

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("9e2c2f8b-410e-422f-a472-dae5bf8f7ee7"), new Guid("4931e704-6fba-419f-921c-a39840ceee3a") });
        }
    }
}

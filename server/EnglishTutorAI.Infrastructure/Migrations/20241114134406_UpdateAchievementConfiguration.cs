using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EnglishTutorAI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAchievementConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserStatistics_UserId",
                table: "UserStatistics");

            migrationBuilder.DeleteData(
                table: "AchievementLevels",
                keyColumn: "Id",
                keyValue: new Guid("2443ba90-2d79-4645-8329-5e68d07dea12"));

            migrationBuilder.DeleteData(
                table: "AchievementLevels",
                keyColumn: "Id",
                keyValue: new Guid("321ab4e0-b331-4560-906e-46873e66dbad"));

            migrationBuilder.DeleteData(
                table: "AchievementLevels",
                keyColumn: "Id",
                keyValue: new Guid("a56c2eb0-f02d-46da-a1cb-892097b673b2"));

            migrationBuilder.DeleteData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: new Guid("e5ceb7fe-c164-4dcb-9153-1427cc9e1225"));

            migrationBuilder.UpdateData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: new Guid("684f3efb-bf12-42aa-8ff4-17705f81d447"),
                columns: new[] { "Description", "Name" },
                values: new object[] { "achievements.noviceTranslator.description", "achievements.noviceTranslator.name" });

            migrationBuilder.CreateIndex(
                name: "IX_UserStatistics_UserId",
                table: "UserStatistics",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserStatistics_UserId",
                table: "UserStatistics");

            migrationBuilder.UpdateData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: new Guid("684f3efb-bf12-42aa-8ff4-17705f81d447"),
                columns: new[] { "Description", "Name" },
                values: new object[] { "achievements.flawlessTranslator.description", "achievements.flawlessTranslator.name" });

            migrationBuilder.InsertData(
                table: "Achievements",
                columns: new[] { "Id", "Description", "IconFileName", "IsCompleted", "Name" },
                values: new object[] { new Guid("e5ceb7fe-c164-4dcb-9153-1427cc9e1225"), "achievements.perfectPassage.description", "perfect_passage_icon.png", false, "achievements.perfectPassage.name" });

            migrationBuilder.InsertData(
                table: "AchievementLevels",
                columns: new[] { "Id", "AchievementId", "Goal" },
                values: new object[,]
                {
                    { new Guid("2443ba90-2d79-4645-8329-5e68d07dea12"), new Guid("e5ceb7fe-c164-4dcb-9153-1427cc9e1225"), 20 },
                    { new Guid("321ab4e0-b331-4560-906e-46873e66dbad"), new Guid("e5ceb7fe-c164-4dcb-9153-1427cc9e1225"), 3 },
                    { new Guid("a56c2eb0-f02d-46da-a1cb-892097b673b2"), new Guid("e5ceb7fe-c164-4dcb-9153-1427cc9e1225"), 10 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserStatistics_UserId",
                table: "UserStatistics",
                column: "UserId");
        }
    }
}

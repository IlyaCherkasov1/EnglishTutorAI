using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnglishTutorAI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNewUserAchievementsLevels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AchievementLevels",
                keyColumn: "Id",
                keyValue: new Guid("4300546d-7eb5-461b-b800-def078028ae4"),
                column: "Goal",
                value: 30);

            migrationBuilder.UpdateData(
                table: "AchievementLevels",
                keyColumn: "Id",
                keyValue: new Guid("72a62120-25e8-4a23-b639-694c73a1010c"),
                column: "Goal",
                value: 50);

            migrationBuilder.UpdateData(
                table: "AchievementLevels",
                keyColumn: "Id",
                keyValue: new Guid("87621b97-bd01-4db1-b308-0547c9a09559"),
                column: "Goal",
                value: 5);

            migrationBuilder.UpdateData(
                table: "AchievementLevels",
                keyColumn: "Id",
                keyValue: new Guid("9ebfd7f8-5309-4838-8dfd-64dc7f5741ca"),
                column: "Goal",
                value: 30);

            migrationBuilder.UpdateData(
                table: "AchievementLevels",
                keyColumn: "Id",
                keyValue: new Guid("a4c81451-3aec-43a4-990c-a70a0d1e2522"),
                column: "Goal",
                value: 50);

            migrationBuilder.UpdateData(
                table: "AchievementLevels",
                keyColumn: "Id",
                keyValue: new Guid("c32f0b45-1c31-420c-ac47-a9c2692838c0"),
                column: "Goal",
                value: 10);

            migrationBuilder.UpdateData(
                table: "AchievementLevels",
                keyColumn: "Id",
                keyValue: new Guid("ee5e587d-1050-445d-bee1-c0c74419c273"),
                column: "Goal",
                value: 10);

            migrationBuilder.InsertData(
                table: "AchievementLevels",
                columns: new[] { "Id", "AchievementId", "Goal" },
                values: new object[] { new Guid("99645f7f-cefe-4931-8170-81bb0322c667"), new Guid("684f3efb-bf12-42aa-8ff4-17705f81d447"), 100 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AchievementLevels",
                keyColumn: "Id",
                keyValue: new Guid("99645f7f-cefe-4931-8170-81bb0322c667"));

            migrationBuilder.UpdateData(
                table: "AchievementLevels",
                keyColumn: "Id",
                keyValue: new Guid("4300546d-7eb5-461b-b800-def078028ae4"),
                column: "Goal",
                value: 10);

            migrationBuilder.UpdateData(
                table: "AchievementLevels",
                keyColumn: "Id",
                keyValue: new Guid("72a62120-25e8-4a23-b639-694c73a1010c"),
                column: "Goal",
                value: 100);

            migrationBuilder.UpdateData(
                table: "AchievementLevels",
                keyColumn: "Id",
                keyValue: new Guid("87621b97-bd01-4db1-b308-0547c9a09559"),
                column: "Goal",
                value: 7);

            migrationBuilder.UpdateData(
                table: "AchievementLevels",
                keyColumn: "Id",
                keyValue: new Guid("9ebfd7f8-5309-4838-8dfd-64dc7f5741ca"),
                column: "Goal",
                value: 100);

            migrationBuilder.UpdateData(
                table: "AchievementLevels",
                keyColumn: "Id",
                keyValue: new Guid("a4c81451-3aec-43a4-990c-a70a0d1e2522"),
                column: "Goal",
                value: 30);

            migrationBuilder.UpdateData(
                table: "AchievementLevels",
                keyColumn: "Id",
                keyValue: new Guid("c32f0b45-1c31-420c-ac47-a9c2692838c0"),
                column: "Goal",
                value: 30);

            migrationBuilder.UpdateData(
                table: "AchievementLevels",
                keyColumn: "Id",
                keyValue: new Guid("ee5e587d-1050-445d-bee1-c0c74419c273"),
                column: "Goal",
                value: 5);
        }
    }
}

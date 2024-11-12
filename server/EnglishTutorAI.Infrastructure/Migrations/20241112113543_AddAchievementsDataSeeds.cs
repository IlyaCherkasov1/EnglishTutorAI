﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EnglishTutorAI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAchievementsDataSeeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IconFileName",
                table: "Achievements",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Achievements",
                columns: new[] { "Id", "Description", "IconFileName", "IsCompleted", "Name" },
                values: new object[,]
                {
                    { new Guid("34ff3dda-dcfa-4f35-bb1b-0b13344cbf70"), "Translate sentences without making any mistakes.", "flawless_translator_icon.png", false, "Flawless Translator" },
                    { new Guid("684f3efb-bf12-42aa-8ff4-17705f81d447"), "Translate sentences", "novice_translator_icon.png", false, "Novice Translator" },
                    { new Guid("b4631aaf-f4e1-419f-a073-8da3b86fb6b5"), "Complete translates", "dedicated_translator_icon.png", false, "Dedicated Translator" },
                    { new Guid("e2665643-f566-4cbf-90b8-e85f0906d8bb"), "Correct grammar mistakes", "grammar_perfectionist_icon.png", false, "Grammar Perfectionist" },
                    { new Guid("e5ceb7fe-c164-4dcb-9153-1427cc9e1225"), "Complete entire translate without any mistakes.", "perfect_passage_icon.png", false, "Perfect Passage" }
                });

            migrationBuilder.InsertData(
                table: "AchievementLevels",
                columns: new[] { "Id", "AchievementId", "Goal" },
                values: new object[,]
                {
                    { new Guid("214d3c6f-2e12-47cb-923f-92b33f27f2c8"), new Guid("34ff3dda-dcfa-4f35-bb1b-0b13344cbf70"), 10 },
                    { new Guid("2443ba90-2d79-4645-8329-5e68d07dea12"), new Guid("e5ceb7fe-c164-4dcb-9153-1427cc9e1225"), 20 },
                    { new Guid("321ab4e0-b331-4560-906e-46873e66dbad"), new Guid("e5ceb7fe-c164-4dcb-9153-1427cc9e1225"), 3 },
                    { new Guid("35fb57b1-985c-40da-acc3-9ffcf8362fec"), new Guid("e2665643-f566-4cbf-90b8-e85f0906d8bb"), 20 },
                    { new Guid("3ea4a320-2805-406d-86d4-fd59d93a41a0"), new Guid("e2665643-f566-4cbf-90b8-e85f0906d8bb"), 10 },
                    { new Guid("4300546d-7eb5-461b-b800-def078028ae4"), new Guid("684f3efb-bf12-42aa-8ff4-17705f81d447"), 10 },
                    { new Guid("6afe17c0-1d63-4802-9750-050b42e6835d"), new Guid("e2665643-f566-4cbf-90b8-e85f0906d8bb"), 3 },
                    { new Guid("72a62120-25e8-4a23-b639-694c73a1010c"), new Guid("34ff3dda-dcfa-4f35-bb1b-0b13344cbf70"), 100 },
                    { new Guid("87621b97-bd01-4db1-b308-0547c9a09559"), new Guid("b4631aaf-f4e1-419f-a073-8da3b86fb6b5"), 7 },
                    { new Guid("971e2141-689d-44eb-ad16-b26dd26c7d8e"), new Guid("e2665643-f566-4cbf-90b8-e85f0906d8bb"), 20 },
                    { new Guid("9ebfd7f8-5309-4838-8dfd-64dc7f5741ca"), new Guid("b4631aaf-f4e1-419f-a073-8da3b86fb6b5"), 100 },
                    { new Guid("a4c81451-3aec-43a4-990c-a70a0d1e2522"), new Guid("684f3efb-bf12-42aa-8ff4-17705f81d447"), 30 },
                    { new Guid("a56c2eb0-f02d-46da-a1cb-892097b673b2"), new Guid("e5ceb7fe-c164-4dcb-9153-1427cc9e1225"), 10 },
                    { new Guid("aeb666eb-5192-4dd7-9d55-2e00dbaea370"), new Guid("34ff3dda-dcfa-4f35-bb1b-0b13344cbf70"), 30 },
                    { new Guid("c32f0b45-1c31-420c-ac47-a9c2692838c0"), new Guid("b4631aaf-f4e1-419f-a073-8da3b86fb6b5"), 30 },
                    { new Guid("ee5e587d-1050-445d-bee1-c0c74419c273"), new Guid("684f3efb-bf12-42aa-8ff4-17705f81d447"), 5 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AchievementLevels",
                keyColumn: "Id",
                keyValue: new Guid("214d3c6f-2e12-47cb-923f-92b33f27f2c8"));

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
                keyValue: new Guid("35fb57b1-985c-40da-acc3-9ffcf8362fec"));

            migrationBuilder.DeleteData(
                table: "AchievementLevels",
                keyColumn: "Id",
                keyValue: new Guid("3ea4a320-2805-406d-86d4-fd59d93a41a0"));

            migrationBuilder.DeleteData(
                table: "AchievementLevels",
                keyColumn: "Id",
                keyValue: new Guid("4300546d-7eb5-461b-b800-def078028ae4"));

            migrationBuilder.DeleteData(
                table: "AchievementLevels",
                keyColumn: "Id",
                keyValue: new Guid("6afe17c0-1d63-4802-9750-050b42e6835d"));

            migrationBuilder.DeleteData(
                table: "AchievementLevels",
                keyColumn: "Id",
                keyValue: new Guid("72a62120-25e8-4a23-b639-694c73a1010c"));

            migrationBuilder.DeleteData(
                table: "AchievementLevels",
                keyColumn: "Id",
                keyValue: new Guid("87621b97-bd01-4db1-b308-0547c9a09559"));

            migrationBuilder.DeleteData(
                table: "AchievementLevels",
                keyColumn: "Id",
                keyValue: new Guid("971e2141-689d-44eb-ad16-b26dd26c7d8e"));

            migrationBuilder.DeleteData(
                table: "AchievementLevels",
                keyColumn: "Id",
                keyValue: new Guid("9ebfd7f8-5309-4838-8dfd-64dc7f5741ca"));

            migrationBuilder.DeleteData(
                table: "AchievementLevels",
                keyColumn: "Id",
                keyValue: new Guid("a4c81451-3aec-43a4-990c-a70a0d1e2522"));

            migrationBuilder.DeleteData(
                table: "AchievementLevels",
                keyColumn: "Id",
                keyValue: new Guid("a56c2eb0-f02d-46da-a1cb-892097b673b2"));

            migrationBuilder.DeleteData(
                table: "AchievementLevels",
                keyColumn: "Id",
                keyValue: new Guid("aeb666eb-5192-4dd7-9d55-2e00dbaea370"));

            migrationBuilder.DeleteData(
                table: "AchievementLevels",
                keyColumn: "Id",
                keyValue: new Guid("c32f0b45-1c31-420c-ac47-a9c2692838c0"));

            migrationBuilder.DeleteData(
                table: "AchievementLevels",
                keyColumn: "Id",
                keyValue: new Guid("ee5e587d-1050-445d-bee1-c0c74419c273"));

            migrationBuilder.DeleteData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: new Guid("34ff3dda-dcfa-4f35-bb1b-0b13344cbf70"));

            migrationBuilder.DeleteData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: new Guid("684f3efb-bf12-42aa-8ff4-17705f81d447"));

            migrationBuilder.DeleteData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: new Guid("b4631aaf-f4e1-419f-a073-8da3b86fb6b5"));

            migrationBuilder.DeleteData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: new Guid("e2665643-f566-4cbf-90b8-e85f0906d8bb"));

            migrationBuilder.DeleteData(
                table: "Achievements",
                keyColumn: "Id",
                keyValue: new Guid("e5ceb7fe-c164-4dcb-9153-1427cc9e1225"));

            migrationBuilder.DropColumn(
                name: "IconFileName",
                table: "Achievements");
        }
    }
}

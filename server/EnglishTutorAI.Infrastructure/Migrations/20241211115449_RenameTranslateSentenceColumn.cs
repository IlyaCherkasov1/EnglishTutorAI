using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnglishTutorAI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameTranslateSentenceColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TranslateSentence_Translates_TranslateId",
                table: "TranslateSentence");

            migrationBuilder.DropColumn(
                name: "TranslatesId",
                table: "TranslateSentence");

            migrationBuilder.AlterColumn<Guid>(
                name: "TranslateId",
                table: "TranslateSentence",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TranslateSentence_Translates_TranslateId",
                table: "TranslateSentence",
                column: "TranslateId",
                principalTable: "Translates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TranslateSentence_Translates_TranslateId",
                table: "TranslateSentence");

            migrationBuilder.AlterColumn<Guid>(
                name: "TranslateId",
                table: "TranslateSentence",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "TranslatesId",
                table: "TranslateSentence",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_TranslateSentence_Translates_TranslateId",
                table: "TranslateSentence",
                column: "TranslateId",
                principalTable: "Translates",
                principalColumn: "Id");
        }
    }
}

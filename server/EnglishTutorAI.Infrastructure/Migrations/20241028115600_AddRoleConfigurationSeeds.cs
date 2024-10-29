using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EnglishTutorAI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRoleConfigurationSeeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("17b372ae-9678-49f6-bbe6-a90ae8e3c6ab"), null, "User", "USER" },
                    { new Guid("9e2c2f8b-410e-422f-a472-dae5bf8f7ee7"), null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("9e2c2f8b-410e-422f-a472-dae5bf8f7ee7"), new Guid("4931e704-6fba-419f-921c-a39840ceee3a") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("17b372ae-9678-49f6-bbe6-a90ae8e3c6ab"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("9e2c2f8b-410e-422f-a472-dae5bf8f7ee7"), new Guid("4931e704-6fba-419f-921c-a39840ceee3a") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9e2c2f8b-410e-422f-a472-dae5bf8f7ee7"));
        }
    }
}

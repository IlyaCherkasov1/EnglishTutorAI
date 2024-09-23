using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnglishTutorAI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUserIdType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "ALTER TABLE \"AspNetUserTokens\" DROP CONSTRAINT \"FK_AspNetUserTokens_AspNetUsers_UserId\";");
            migrationBuilder.Sql(
                "ALTER TABLE \"AspNetUserRoles\" DROP CONSTRAINT \"FK_AspNetUserRoles_AspNetUsers_UserId\";");
            migrationBuilder.Sql(
                "ALTER TABLE \"AspNetUserRoles\" DROP CONSTRAINT \"FK_AspNetUserRoles_AspNetRoles_RoleId\";");
            migrationBuilder.Sql(
                "ALTER TABLE \"AspNetUserLogins\" DROP CONSTRAINT \"FK_AspNetUserLogins_AspNetUsers_UserId\";");
            migrationBuilder.Sql(
                "ALTER TABLE \"AspNetUserClaims\" DROP CONSTRAINT \"FK_AspNetUserClaims_AspNetUsers_UserId\";");
            migrationBuilder.Sql(
                "ALTER TABLE \"AspNetRoleClaims\" DROP CONSTRAINT \"FK_AspNetRoleClaims_AspNetRoles_RoleId\";");

            migrationBuilder.Sql("ALTER TABLE \"AspNetUsers\" ALTER COLUMN \"Id\" TYPE uuid USING \"Id\"::uuid;");
            migrationBuilder.Sql(
                "ALTER TABLE \"AspNetUserTokens\" ALTER COLUMN \"UserId\" TYPE uuid USING \"UserId\"::uuid;");
            migrationBuilder.Sql(
                "ALTER TABLE \"AspNetUserRoles\" ALTER COLUMN \"UserId\" TYPE uuid USING \"UserId\"::uuid;");
            migrationBuilder.Sql(
                "ALTER TABLE \"AspNetUserRoles\" ALTER COLUMN \"RoleId\" TYPE uuid USING \"RoleId\"::uuid;");
            migrationBuilder.Sql(
                "ALTER TABLE \"AspNetUserLogins\" ALTER COLUMN \"UserId\" TYPE uuid USING \"UserId\"::uuid;");
            migrationBuilder.Sql(
                "ALTER TABLE \"AspNetUserClaims\" ALTER COLUMN \"UserId\" TYPE uuid USING \"UserId\"::uuid;");
            migrationBuilder.Sql("ALTER TABLE \"AspNetRoles\" ALTER COLUMN \"Id\" TYPE uuid USING \"Id\"::uuid;");
            migrationBuilder.Sql(
                "ALTER TABLE \"AspNetRoleClaims\" ALTER COLUMN \"RoleId\" TYPE uuid USING \"RoleId\"::uuid;");

            migrationBuilder.Sql(
                "ALTER TABLE \"AspNetUserTokens\" ADD CONSTRAINT \"FK_AspNetUserTokens_AspNetUsers_UserId\" FOREIGN KEY (\"UserId\") REFERENCES \"AspNetUsers\" (\"Id\") ON DELETE CASCADE;");
            migrationBuilder.Sql(
                "ALTER TABLE \"AspNetUserRoles\" ADD CONSTRAINT \"FK_AspNetUserRoles_AspNetUsers_UserId\" FOREIGN KEY (\"UserId\") REFERENCES \"AspNetUsers\" (\"Id\") ON DELETE CASCADE;");
            migrationBuilder.Sql(
                "ALTER TABLE \"AspNetUserRoles\" ADD CONSTRAINT \"FK_AspNetUserRoles_AspNetRoles_RoleId\" FOREIGN KEY (\"RoleId\") REFERENCES \"AspNetRoles\" (\"Id\") ON DELETE CASCADE;");
            migrationBuilder.Sql(
                "ALTER TABLE \"AspNetUserLogins\" ADD CONSTRAINT \"FK_AspNetUserLogins_AspNetUsers_UserId\" FOREIGN KEY (\"UserId\") REFERENCES \"AspNetUsers\" (\"Id\") ON DELETE CASCADE;");
            migrationBuilder.Sql(
                "ALTER TABLE \"AspNetUserClaims\" ADD CONSTRAINT \"FK_AspNetUserClaims_AspNetUsers_UserId\" FOREIGN KEY (\"UserId\") REFERENCES \"AspNetUsers\" (\"Id\") ON DELETE CASCADE;");
            migrationBuilder.Sql(
                "ALTER TABLE \"AspNetRoleClaims\" ADD CONSTRAINT \"FK_AspNetRoleClaims_AspNetRoles_RoleId\" FOREIGN KEY (\"RoleId\") REFERENCES \"AspNetRoles\" (\"Id\") ON DELETE CASCADE;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "ALTER TABLE \"AspNetUserTokens\" DROP CONSTRAINT \"FK_AspNetUserTokens_AspNetUsers_UserId\";");
            migrationBuilder.Sql(
                "ALTER TABLE \"AspNetUserRoles\" DROP CONSTRAINT \"FK_AspNetUserRoles_AspNetUsers_UserId\";");
            migrationBuilder.Sql(
                "ALTER TABLE \"AspNetUserRoles\" DROP CONSTRAINT \"FK_AspNetUserRoles_AspNetRoles_RoleId\";");
            migrationBuilder.Sql(
                "ALTER TABLE \"AspNetUserLogins\" DROP CONSTRAINT \"FK_AspNetUserLogins_AspNetUsers_UserId\";");
            migrationBuilder.Sql(
                "ALTER TABLE \"AspNetUserClaims\" DROP CONSTRAINT \"FK_AspNetUserClaims_AspNetUsers_UserId\";");
            migrationBuilder.Sql(
                "ALTER TABLE \"AspNetRoleClaims\" DROP CONSTRAINT \"FK_AspNetRoleClaims_AspNetRoles_RoleId\";");

            migrationBuilder.Sql("ALTER TABLE \"AspNetUsers\" ALTER COLUMN \"Id\" TYPE text USING \"Id\"::text;");
            migrationBuilder.Sql(
                "ALTER TABLE \"AspNetUserTokens\" ALTER COLUMN \"UserId\" TYPE text USING \"UserId\"::text;");
            migrationBuilder.Sql(
                "ALTER TABLE \"AspNetUserRoles\" ALTER COLUMN \"UserId\" TYPE text USING \"UserId\"::text;");
            migrationBuilder.Sql(
                "ALTER TABLE \"AspNetUserRoles\" ALTER COLUMN \"RoleId\" TYPE text USING \"RoleId\"::text;");
            migrationBuilder.Sql(
                "ALTER TABLE \"AspNetUserLogins\" ALTER COLUMN \"UserId\" TYPE text USING \"UserId\"::text;");
            migrationBuilder.Sql(
                "ALTER TABLE \"AspNetUserClaims\" ALTER COLUMN \"UserId\" TYPE text USING \"UserId\"::text;");
            migrationBuilder.Sql("ALTER TABLE \"AspNetRoles\" ALTER COLUMN \"Id\" TYPE text USING \"Id\"::text;");
            migrationBuilder.Sql(
                "ALTER TABLE \"AspNetRoleClaims\" ALTER COLUMN \"RoleId\" TYPE text USING \"RoleId\"::text;");

            migrationBuilder.Sql(
                "ALTER TABLE \"AspNetUserTokens\" ADD CONSTRAINT \"FK_AspNetUserTokens_AspNetUsers_UserId\" FOREIGN KEY (\"UserId\") REFERENCES \"AspNetUsers\" (\"Id\") ON DELETE CASCADE;");
            migrationBuilder.Sql(
                "ALTER TABLE \"AspNetUserRoles\" ADD CONSTRAINT \"FK_AspNetUserRoles_AspNetUsers_UserId\" FOREIGN KEY (\"UserId\") REFERENCES \"AspNetUsers\" (\"Id\") ON DELETE CASCADE;");
            migrationBuilder.Sql(
                "ALTER TABLE \"AspNetUserRoles\" ADD CONSTRAINT \"FK_AspNetUserRoles_AspNetRoles_RoleId\" FOREIGN KEY (\"RoleId\") REFERENCES \"AspNetRoles\" (\"Id\") ON DELETE CASCADE;");
            migrationBuilder.Sql(
                "ALTER TABLE \"AspNetUserLogins\" ADD CONSTRAINT \"FK_AspNetUserLogins_AspNetUsers_UserId\" FOREIGN KEY (\"UserId\") REFERENCES \"AspNetUsers\" (\"Id\") ON DELETE CASCADE;");
            migrationBuilder.Sql(
                "ALTER TABLE \"AspNetUserClaims\" ADD CONSTRAINT \"FK_AspNetUserClaims_AspNetUsers_UserId\" FOREIGN KEY (\"UserId\") REFERENCES \"AspNetUsers\" (\"Id\") ON DELETE CASCADE;");
            migrationBuilder.Sql(
                "ALTER TABLE \"AspNetRoleClaims\" ADD CONSTRAINT \"FK_AspNetRoleClaims_AspNetRoles_RoleId\" FOREIGN KEY (\"RoleId\") REFERENCES \"AspNetRoles\" (\"Id\") ON DELETE CASCADE;");
        }
    }
}
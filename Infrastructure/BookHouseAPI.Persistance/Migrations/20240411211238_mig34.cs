using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookHouseAPI.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class mig34 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e66ab09e-32a0-45e8-8156-a0b47f525884");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "332f5b18-b874-456e-b201-8c8c8d27eec2", "966c93ba-fd7a-496d-9c98-fab695f12f63" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "332f5b18-b874-456e-b201-8c8c8d27eec2");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "966c93ba-fd7a-496d-9c98-fab695f12f63");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUserRoles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUserRoles",
                type: "nvarchar(34)",
                maxLength: 34,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "332f5b18-b874-456e-b201-8c8c8d27eec2", null, "Admin", "ADMIN" },
                    { "e66ab09e-32a0-45e8-8156-a0b47f525884", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BirthDate", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenEndTime", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "966c93ba-fd7a-496d-9c98-fab695f12f63", 0, new DateTime(2024, 4, 11, 21, 11, 11, 167, DateTimeKind.Utc).AddTicks(5835), "eaa38451-76af-453f-a644-da5ae4d5433a", "admin@example.com", true, "default", "default", true, null, "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAIAAYagAAAAEFyii2+5RNoWBZGOJjdIV7SMIZQtKz4lX4e8wIZpJONg5z/6FABQT8DXSXH+tHPXQA==", null, false, null, null, "8459e1db-007b-458f-a4c3-ec3d80c32e4d", false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId", "Discriminator" },
                values: new object[] { "332f5b18-b874-456e-b201-8c8c8d27eec2", "966c93ba-fd7a-496d-9c98-fab695f12f63", "AppUserRoles" });
        }
    }
}

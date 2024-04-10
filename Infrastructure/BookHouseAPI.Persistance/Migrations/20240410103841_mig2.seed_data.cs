using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookHouseAPI.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class mig2seed_data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "01deb434-b67e-41fe-ba4e-aa68810bdb17", null, "User", "USER" },
                    { "b7225315-174f-4131-a73d-441ebad6a793", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BirthDate", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenEndTime", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "c6279997-579f-469a-bc71-67ccbd633269", 0, new DateTime(2024, 4, 10, 10, 38, 41, 140, DateTimeKind.Utc).AddTicks(5287), "cb6f80fb-06ac-4ca4-951e-46115e33e344", "admin@example.com", true, "default", "default", true, null, "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAIAAYagAAAAEGHn/RnUXvWblgl+po59kW8cQ2A4BmrTB3GLwIrpJLqhAkrAyrfz9Q3wgJt0oP1ZJQ==", null, false, null, null, "87df8a76-eb2b-4a50-a27f-dbb0dc1ca216", false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId", "Discriminator" },
                values: new object[] { "b7225315-174f-4131-a73d-441ebad6a793", "c6279997-579f-469a-bc71-67ccbd633269", "AppUserRoles" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01deb434-b67e-41fe-ba4e-aa68810bdb17");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "b7225315-174f-4131-a73d-441ebad6a793", "c6279997-579f-469a-bc71-67ccbd633269" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b7225315-174f-4131-a73d-441ebad6a793");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c6279997-579f-469a-bc71-67ccbd633269");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUserRoles");
        }
    }
}

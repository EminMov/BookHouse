using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookHouseAPI.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class mig3one_to_many_Book_Reviews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "ReviewId",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "37721fa8-1623-4f0b-8c6c-3375b5de7fee", null, "Admin", "ADMIN" },
                    { "c29c003d-9279-45ff-a2c3-624de432f00f", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BirthDate", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenEndTime", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "a919aaf6-90ae-433a-9a32-9d08e5fe7a12", 0, new DateTime(2024, 4, 10, 10, 53, 16, 766, DateTimeKind.Utc).AddTicks(6237), "a8e23a9b-d79b-4a39-9277-9a36f517a720", "admin@example.com", true, "default", "default", true, null, "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAIAAYagAAAAECMuTm/9LgDxAuJkcJ/QWmxA+ax4LF1PS0DMNo/D/QpogSRvwhuRjniNq1JrbfF+Dw==", null, false, null, null, "ca82cedb-253a-454b-9ce3-37ad52f88c24", false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId", "Discriminator" },
                values: new object[] { "37721fa8-1623-4f0b-8c6c-3375b5de7fee", "a919aaf6-90ae-433a-9a32-9d08e5fe7a12", "AppUserRoles" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c29c003d-9279-45ff-a2c3-624de432f00f");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "37721fa8-1623-4f0b-8c6c-3375b5de7fee", "a919aaf6-90ae-433a-9a32-9d08e5fe7a12" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "37721fa8-1623-4f0b-8c6c-3375b5de7fee");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a919aaf6-90ae-433a-9a32-9d08e5fe7a12");

            migrationBuilder.DropColumn(
                name: "ReviewId",
                table: "Books");

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
    }
}

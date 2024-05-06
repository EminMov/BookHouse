using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookHouseAPI.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class mig33 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Baskets_Orders_OrderID",
                table: "Baskets");

            migrationBuilder.DropIndex(
                name: "IX_Baskets_OrderID",
                table: "Baskets");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "194c5bfe-4bb6-431c-8fd3-ff198ebecb03");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "ad424099-32fa-4b19-b90b-5a8f6d52f9dc", "19aef1ed-60e3-49f3-9431-881afa485fcc" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad424099-32fa-4b19-b90b-5a8f6d52f9dc");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "19aef1ed-60e3-49f3-9431-881afa485fcc");

            migrationBuilder.AddColumn<int>(
                name: "BasketId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_Orders_BasketId",
                table: "Orders",
                column: "BasketId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Baskets_BasketId",
                table: "Orders",
                column: "BasketId",
                principalTable: "Baskets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Baskets_BasketId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_BasketId",
                table: "Orders");

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
                name: "BasketId",
                table: "Orders");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "194c5bfe-4bb6-431c-8fd3-ff198ebecb03", null, "User", "USER" },
                    { "ad424099-32fa-4b19-b90b-5a8f6d52f9dc", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BirthDate", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenEndTime", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "19aef1ed-60e3-49f3-9431-881afa485fcc", 0, new DateTime(2024, 4, 11, 21, 2, 44, 522, DateTimeKind.Utc).AddTicks(2595), "c1f013f7-b955-42a1-b9d9-2a1e7fed2a4d", "admin@example.com", true, "default", "default", true, null, "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAIAAYagAAAAEHDMS1EDgCm+sX3W+rW7eMHLHY05vPQrXj5LdqUAaKgRUXAO+AQgrHlbUhFEKW69SA==", null, false, null, null, "53a720a7-5999-4711-9db8-9e1b7b7c53f7", false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId", "Discriminator" },
                values: new object[] { "ad424099-32fa-4b19-b90b-5a8f6d52f9dc", "19aef1ed-60e3-49f3-9431-881afa485fcc", "AppUserRoles" });

            migrationBuilder.CreateIndex(
                name: "IX_Baskets_OrderID",
                table: "Baskets",
                column: "OrderID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Baskets_Orders_OrderID",
                table: "Baskets",
                column: "OrderID",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

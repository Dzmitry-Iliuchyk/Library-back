using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Library.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Addadmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "UserName" },
                values: new object[] { new Guid("b0469193-2a81-42ea-b5e0-0d5b68b6e22d"), "admin@admin.com", "AQAAAAIAAYagAAAAEJFvcG6islmlQVEEWJJ2dCaIIXNuBRCo+GVZm2KESq0jYS6vsMnRBku6TEuD8hIVnw==", "admin" });

            migrationBuilder.InsertData(
                table: "UserAccessGroup",
                columns: new[] { "GroupId", "UserId" },
                values: new object[,]
                {
                    { 1, new Guid("b0469193-2a81-42ea-b5e0-0d5b68b6e22d") },
                    { 2, new Guid("b0469193-2a81-42ea-b5e0-0d5b68b6e22d") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserAccessGroup",
                keyColumns: new[] { "GroupId", "UserId" },
                keyValues: new object[] { 1, new Guid("b0469193-2a81-42ea-b5e0-0d5b68b6e22d") });

            migrationBuilder.DeleteData(
                table: "UserAccessGroup",
                keyColumns: new[] { "GroupId", "UserId" },
                keyValues: new object[] { 2, new Guid("b0469193-2a81-42ea-b5e0-0d5b68b6e22d") });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b0469193-2a81-42ea-b5e0-0d5b68b6e22d"));
        }
    }
}

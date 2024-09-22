using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CRMSystem.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Seeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, "00000000-0000-0000-0000-000000000000", "Admin", "ADMIN" },
                    { 2, "00000000-0000-0000-0000-000000000000", "Client", "CLIENT" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 1, 0, "4464c0e9-1e28-4711-8434-2981f3f35b29", "admin@email.com", false, false, null, "Admin", "ADMIN@EMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEG7X1qubAd+j5SS6cIeAd128DKreObNsnwnsxr8LedgOEb8RsfxUwyddS1D9Yo62aw==", null, false, null, false, "admin" },
                    { 2, 0, "60d92892-8681-4dcc-9d45-e13a51bf5720", "client@email.com", false, false, null, "Client", "CLIENT@EMAIL.COM", "CLIENT", "AQAAAAIAAYagAAAAEMRfBUHeH62z24/Wg+ZsgASza85TlI5yJFX5Zh/5fQhQpQDNv2MuMIgm3STpSxJsaw==", null, false, null, false, "client" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "Deadline", "Description", "Status", "Title", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test Description", 1, "Test Admin Project", 1 },
                    { 2, new DateTime(2024, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test Description", 3, "Test Client Project", 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}

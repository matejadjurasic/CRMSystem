using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRMSystemAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "1", "ADMIN" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { "2", "CLIENT" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash" },
                values: new object[] { "a792d650-a91f-4552-b68c-95d4d5c1a655", "ADMIN@EMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEOCTK0o3oXDrUbPbUTwIVJ2C8EW7WiRZ7d0tEgP8s655rucoTVlS93XEgvCPxJB+IA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "NormalizedName" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash" },
                values: new object[] { "51686b26-01e2-4d43-9ff3-ff4a6a0214b0", null, null, "AQAAAAIAAYagAAAAEDKIDuic5PzrvJQVxdACMXFj8fSc3Sl9rVbhc6F/BIhSP/Si8YPv3n/tmWgYDcsrVw==" });
        }
    }
}

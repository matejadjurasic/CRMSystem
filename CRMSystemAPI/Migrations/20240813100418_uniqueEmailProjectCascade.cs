using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRMSystemAPI.Migrations
{
    /// <inheritdoc />
    public partial class uniqueEmailProjectCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d2946184-d85a-4b6b-acfa-cab6461e2610", "AQAAAAIAAYagAAAAEDRSvUUlWEOiseQQKp8s6/NPqiFeortxNhOs0tA+7JonPI9OznMJvy8+ivQnKjjg4g==" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Email",
                table: "AspNetUsers",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Email",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0e3ec1ba-e4cc-4538-a4a4-640a04ef79dc", "AQAAAAIAAYagAAAAEDFpTaK0iYQXvGgokDW/EoCM2MdDHBrJrflq6JY3lgnHu13y9YNGlybzaaJvpqSecQ==" });
        }
    }
}

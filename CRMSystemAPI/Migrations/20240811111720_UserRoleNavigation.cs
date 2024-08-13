using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRMSystemAPI.Migrations
{
    /// <inheritdoc />
    public partial class UserRoleNavigation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0e3ec1ba-e4cc-4538-a4a4-640a04ef79dc", "AQAAAAIAAYagAAAAEDFpTaK0iYQXvGgokDW/EoCM2MdDHBrJrflq6JY3lgnHu13y9YNGlybzaaJvpqSecQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins",
                columns: new[] { "LoginProvider", "ProviderKey", "UserId" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a792d650-a91f-4552-b68c-95d4d5c1a655", "AQAAAAIAAYagAAAAEOCTK0o3oXDrUbPbUTwIVJ2C8EW7WiRZ7d0tEgP8s655rucoTVlS93XEgvCPxJB+IA==" });
        }
    }
}

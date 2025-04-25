using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace prayer.Migrations
{
    /// <inheritdoc />
    public partial class RBACRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "16ae4beb-8d06-4c33-85a2-13dcb060cb7d", null, "user", null },
                    { "caa8d3e9-2481-45cf-873d-af213c1faedc", null, "admin", "user" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "16ae4beb-8d06-4c33-85a2-13dcb060cb7d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "caa8d3e9-2481-45cf-873d-af213c1faedc");
        }
    }
}

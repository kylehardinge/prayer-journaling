using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace prayer.Migrations
{
    /// <inheritdoc />
    public partial class ChangeIDName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c0925717-d574-4773-ba19-9e226eda3e5d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e2837126-9edd-419d-9f81-a2ea97498bbd");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "Group",
                newName: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "52411c12-335e-4a77-9609-81b1849814e0", null, "user", null },
                    { "d21ee3e6-48b3-4974-8013-7fbc8db9cc75", null, "admin", "user" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "52411c12-335e-4a77-9609-81b1849814e0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d21ee3e6-48b3-4974-8013-7fbc8db9cc75");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Group",
                newName: "GroupId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "c0925717-d574-4773-ba19-9e226eda3e5d", null, "user", null },
                    { "e2837126-9edd-419d-9f81-a2ea97498bbd", null, "admin", "user" }
                });
        }
    }
}

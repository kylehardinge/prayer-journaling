using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace prayer.Migrations
{
    /// <inheritdoc />
    public partial class AddEnums : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "08f5ddab-5e6a-43e9-920c-93391ebef5e1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ae2527a1-ab19-4b93-bc02-a07234476c3e");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Prayer",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9c85b341-8340-41c5-bff7-9a58559d0dbf", null, "admin", "user" },
                    { "b9d21c34-489e-444b-a591-837b96edc887", null, "user", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9c85b341-8340-41c5-bff7-9a58559d0dbf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b9d21c34-489e-444b-a591-837b96edc887");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Prayer",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "08f5ddab-5e6a-43e9-920c-93391ebef5e1", null, "admin", "user" },
                    { "ae2527a1-ab19-4b93-bc02-a07234476c3e", null, "user", null }
                });
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace prayer.Migrations
{
    /// <inheritdoc />
    public partial class AddMigrationEnrollment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "52411c12-335e-4a77-9609-81b1849814e0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d21ee3e6-48b3-4974-8013-7fbc8db9cc75");

            migrationBuilder.AddColumn<DateTime>(
                name: "Enrolled",
                table: "Membership",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "08f5ddab-5e6a-43e9-920c-93391ebef5e1", null, "admin", "user" },
                    { "ae2527a1-ab19-4b93-bc02-a07234476c3e", null, "user", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "08f5ddab-5e6a-43e9-920c-93391ebef5e1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ae2527a1-ab19-4b93-bc02-a07234476c3e");

            migrationBuilder.DropColumn(
                name: "Enrolled",
                table: "Membership");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "52411c12-335e-4a77-9609-81b1849814e0", null, "user", null },
                    { "d21ee3e6-48b3-4974-8013-7fbc8db9cc75", null, "admin", "user" }
                });
        }
    }
}

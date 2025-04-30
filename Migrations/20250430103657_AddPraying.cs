using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace prayer.Migrations
{
    /// <inheritdoc />
    public partial class AddPraying : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrayerSession");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9c85b341-8340-41c5-bff7-9a58559d0dbf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b9d21c34-489e-444b-a591-837b96edc887");

            migrationBuilder.CreateTable(
                name: "Praying",
                columns: table => new
                {
                    SessionId = table.Column<int>(type: "int", nullable: false),
                    PrayerId = table.Column<int>(type: "int", nullable: false),
                    Added = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Praying", x => new { x.SessionId, x.PrayerId });
                    table.ForeignKey(
                        name: "FK_Praying_Prayer_PrayerId",
                        column: x => x.PrayerId,
                        principalTable: "Prayer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Praying_Session_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Session",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "013b42b9-5a72-4396-9c1f-da0eb18fb93c", null, "admin", "user" },
                    { "47e6553c-0ecc-422f-becb-dfe6270b33e2", null, "user", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Praying_PrayerId",
                table: "Praying",
                column: "PrayerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Praying");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "013b42b9-5a72-4396-9c1f-da0eb18fb93c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "47e6553c-0ecc-422f-becb-dfe6270b33e2");

            migrationBuilder.CreateTable(
                name: "PrayerSession",
                columns: table => new
                {
                    PrayersId = table.Column<int>(type: "int", nullable: false),
                    SessionsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrayerSession", x => new { x.PrayersId, x.SessionsId });
                    table.ForeignKey(
                        name: "FK_PrayerSession_Prayer_PrayersId",
                        column: x => x.PrayersId,
                        principalTable: "Prayer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrayerSession_Session_SessionsId",
                        column: x => x.SessionsId,
                        principalTable: "Session",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9c85b341-8340-41c5-bff7-9a58559d0dbf", null, "admin", "user" },
                    { "b9d21c34-489e-444b-a591-837b96edc887", null, "user", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrayerSession_SessionsId",
                table: "PrayerSession",
                column: "SessionsId");
        }
    }
}

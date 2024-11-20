using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LaboratoriumASPNET.Migrations
{
    /// <inheritdoc />
    public partial class mig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "contacts",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2024, 11, 17, 16, 7, 57, 763, DateTimeKind.Local).AddTicks(7019));

            migrationBuilder.UpdateData(
                table: "contacts",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2024, 11, 17, 16, 7, 57, 765, DateTimeKind.Local).AddTicks(4486));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "contacts",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2024, 11, 12, 13, 38, 12, 376, DateTimeKind.Local).AddTicks(6567));

            migrationBuilder.UpdateData(
                table: "contacts",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2024, 11, 12, 13, 38, 12, 376, DateTimeKind.Local).AddTicks(6655));
        }
    }
}

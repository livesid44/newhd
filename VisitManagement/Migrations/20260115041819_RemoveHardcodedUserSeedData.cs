using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VisitManagement.Migrations
{
    /// <inheritdoc />
    public partial class RemoveHardcodedUserSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "VisitUsers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "VisitUsers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "VisitUsers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Visits",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2026, 1, 15, 4, 18, 19, 384, DateTimeKind.Local).AddTicks(294), new DateTime(2026, 1, 15, 4, 18, 19, 384, DateTimeKind.Local).AddTicks(295) });

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2026, 1, 15, 4, 18, 19, 384, DateTimeKind.Local).AddTicks(298), new DateTime(2026, 1, 15, 4, 18, 19, 384, DateTimeKind.Local).AddTicks(298) });

            migrationBuilder.UpdateData(
                table: "Visits",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 15, 4, 18, 19, 384, DateTimeKind.Local).AddTicks(141));

            migrationBuilder.UpdateData(
                table: "Visits",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 15, 4, 18, 19, 384, DateTimeKind.Local).AddTicks(147));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Visits",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2025, 12, 31, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 12, 31, 12, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2025, 12, 31, 12, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 12, 31, 12, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.InsertData(
                table: "VisitUsers",
                columns: new[] { "Id", "AuthType", "CreatedDate", "Email", "FullName", "IsActive", "LdapUserId", "PhoneNumber", "Role" },
                values: new object[,]
                {
                    { 1, 0, new DateTime(2025, 12, 31, 12, 0, 0, 0, DateTimeKind.Local), "john.doe@example.com", "John Doe", true, null, "+1-555-0100", "Administrator" },
                    { 2, 0, new DateTime(2025, 12, 31, 12, 0, 0, 0, DateTimeKind.Local), "jane.smith@example.com", "Jane Smith", true, null, "+1-555-0101", "Sales Manager" },
                    { 3, 0, new DateTime(2025, 12, 31, 12, 0, 0, 0, DateTimeKind.Local), "bob.johnson@example.com", "Bob Johnson", true, null, "+1-555-0102", "Team Lead" }
                });

            migrationBuilder.UpdateData(
                table: "Visits",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 31, 12, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Visits",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 31, 12, 0, 0, 0, DateTimeKind.Local));
        }
    }
}

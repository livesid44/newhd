using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VisitManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatedByToVisits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Visits",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "system@visitmanagement.com");

            // Update seed data to include CreatedBy values
            migrationBuilder.UpdateData(
                table: "Visits",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedBy",
                value: "admin@visitmanagement.com");

            migrationBuilder.UpdateData(
                table: "Visits",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedBy",
                value: "admin@visitmanagement.com");

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

            migrationBuilder.UpdateData(
                table: "VisitUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 31, 12, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "VisitUsers",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 31, 12, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "VisitUsers",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 31, 12, 0, 0, 0, DateTimeKind.Local));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Visits");

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2025, 12, 30, 4, 45, 24, 16, DateTimeKind.Local).AddTicks(9423), new DateTime(2025, 12, 30, 4, 45, 24, 16, DateTimeKind.Local).AddTicks(9424) });

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2025, 12, 30, 4, 45, 24, 16, DateTimeKind.Local).AddTicks(9427), new DateTime(2025, 12, 30, 4, 45, 24, 16, DateTimeKind.Local).AddTicks(9428) });

            migrationBuilder.UpdateData(
                table: "VisitUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 30, 4, 45, 24, 16, DateTimeKind.Local).AddTicks(9177));

            migrationBuilder.UpdateData(
                table: "VisitUsers",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 30, 4, 45, 24, 16, DateTimeKind.Local).AddTicks(9180));

            migrationBuilder.UpdateData(
                table: "VisitUsers",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 30, 4, 45, 24, 16, DateTimeKind.Local).AddTicks(9183));

            migrationBuilder.UpdateData(
                table: "Visits",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 30, 4, 45, 24, 16, DateTimeKind.Local).AddTicks(9376));

            migrationBuilder.UpdateData(
                table: "Visits",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 30, 4, 45, 24, 16, DateTimeKind.Local).AddTicks(9382));
        }
    }
}

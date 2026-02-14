using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VisitManagement.Migrations
{
    /// <inheritdoc />
    public partial class RenameTaskStatusEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2026, 2, 11, 14, 29, 41, 675, DateTimeKind.Local).AddTicks(4980), new DateTime(2026, 2, 11, 14, 29, 41, 675, DateTimeKind.Local).AddTicks(4980) });

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2026, 2, 11, 14, 29, 41, 675, DateTimeKind.Local).AddTicks(4984), new DateTime(2026, 2, 11, 14, 29, 41, 675, DateTimeKind.Local).AddTicks(4985) });

            migrationBuilder.UpdateData(
                table: "Stakeholders",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 11, 14, 29, 41, 675, DateTimeKind.Local).AddTicks(5023));

            migrationBuilder.UpdateData(
                table: "Stakeholders",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 11, 14, 29, 41, 675, DateTimeKind.Local).AddTicks(5026));

            migrationBuilder.UpdateData(
                table: "Stakeholders",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 11, 14, 29, 41, 675, DateTimeKind.Local).AddTicks(5028));

            migrationBuilder.UpdateData(
                table: "Stakeholders",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 11, 14, 29, 41, 675, DateTimeKind.Local).AddTicks(5030));

            migrationBuilder.UpdateData(
                table: "Stakeholders",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 11, 14, 29, 41, 675, DateTimeKind.Local).AddTicks(5033));

            migrationBuilder.UpdateData(
                table: "VisitUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 11, 14, 29, 41, 675, DateTimeKind.Local).AddTicks(4698));

            migrationBuilder.UpdateData(
                table: "VisitUsers",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 11, 14, 29, 41, 675, DateTimeKind.Local).AddTicks(4701));

            migrationBuilder.UpdateData(
                table: "VisitUsers",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 11, 14, 29, 41, 675, DateTimeKind.Local).AddTicks(4704));

            migrationBuilder.UpdateData(
                table: "Visits",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 11, 14, 29, 41, 675, DateTimeKind.Local).AddTicks(4933));

            migrationBuilder.UpdateData(
                table: "Visits",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 11, 14, 29, 41, 675, DateTimeKind.Local).AddTicks(4939));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2026, 2, 11, 14, 25, 12, 882, DateTimeKind.Local).AddTicks(4584), new DateTime(2026, 2, 11, 14, 25, 12, 882, DateTimeKind.Local).AddTicks(4585) });

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2026, 2, 11, 14, 25, 12, 882, DateTimeKind.Local).AddTicks(4588), new DateTime(2026, 2, 11, 14, 25, 12, 882, DateTimeKind.Local).AddTicks(4588) });

            migrationBuilder.UpdateData(
                table: "Stakeholders",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 11, 14, 25, 12, 882, DateTimeKind.Local).AddTicks(4635));

            migrationBuilder.UpdateData(
                table: "Stakeholders",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 11, 14, 25, 12, 882, DateTimeKind.Local).AddTicks(4638));

            migrationBuilder.UpdateData(
                table: "Stakeholders",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 11, 14, 25, 12, 882, DateTimeKind.Local).AddTicks(4640));

            migrationBuilder.UpdateData(
                table: "Stakeholders",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 11, 14, 25, 12, 882, DateTimeKind.Local).AddTicks(4643));

            migrationBuilder.UpdateData(
                table: "Stakeholders",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 11, 14, 25, 12, 882, DateTimeKind.Local).AddTicks(4645));

            migrationBuilder.UpdateData(
                table: "VisitUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 11, 14, 25, 12, 882, DateTimeKind.Local).AddTicks(4262));

            migrationBuilder.UpdateData(
                table: "VisitUsers",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 11, 14, 25, 12, 882, DateTimeKind.Local).AddTicks(4265));

            migrationBuilder.UpdateData(
                table: "VisitUsers",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 11, 14, 25, 12, 882, DateTimeKind.Local).AddTicks(4268));

            migrationBuilder.UpdateData(
                table: "Visits",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 11, 14, 25, 12, 882, DateTimeKind.Local).AddTicks(4531));

            migrationBuilder.UpdateData(
                table: "Visits",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 11, 14, 25, 12, 882, DateTimeKind.Local).AddTicks(4537));
        }
    }
}

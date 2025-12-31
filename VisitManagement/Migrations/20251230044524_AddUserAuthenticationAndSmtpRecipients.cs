using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VisitManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddUserAuthenticationAndSmtpRecipients : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AuthType",
                table: "VisitUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LdapUserId",
                table: "VisitUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DefaultCcRecipients",
                table: "SmtpSettings",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DefaultToRecipients",
                table: "SmtpSettings",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

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
                columns: new[] { "AuthType", "CreatedDate", "LdapUserId" },
                values: new object[] { 0, new DateTime(2025, 12, 30, 4, 45, 24, 16, DateTimeKind.Local).AddTicks(9177), null });

            migrationBuilder.UpdateData(
                table: "VisitUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AuthType", "CreatedDate", "LdapUserId" },
                values: new object[] { 0, new DateTime(2025, 12, 30, 4, 45, 24, 16, DateTimeKind.Local).AddTicks(9180), null });

            migrationBuilder.UpdateData(
                table: "VisitUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AuthType", "CreatedDate", "LdapUserId" },
                values: new object[] { 0, new DateTime(2025, 12, 30, 4, 45, 24, 16, DateTimeKind.Local).AddTicks(9183), null });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthType",
                table: "VisitUsers");

            migrationBuilder.DropColumn(
                name: "LdapUserId",
                table: "VisitUsers");

            migrationBuilder.DropColumn(
                name: "DefaultCcRecipients",
                table: "SmtpSettings");

            migrationBuilder.DropColumn(
                name: "DefaultToRecipients",
                table: "SmtpSettings");

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2025, 12, 29, 17, 23, 18, 16, DateTimeKind.Local).AddTicks(5927), new DateTime(2025, 12, 29, 17, 23, 18, 16, DateTimeKind.Local).AddTicks(5928) });

            migrationBuilder.UpdateData(
                table: "EmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2025, 12, 29, 17, 23, 18, 16, DateTimeKind.Local).AddTicks(5931), new DateTime(2025, 12, 29, 17, 23, 18, 16, DateTimeKind.Local).AddTicks(5931) });

            migrationBuilder.UpdateData(
                table: "VisitUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 29, 17, 23, 18, 16, DateTimeKind.Local).AddTicks(5716));

            migrationBuilder.UpdateData(
                table: "VisitUsers",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 29, 17, 23, 18, 16, DateTimeKind.Local).AddTicks(5719));

            migrationBuilder.UpdateData(
                table: "VisitUsers",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 29, 17, 23, 18, 16, DateTimeKind.Local).AddTicks(5721));

            migrationBuilder.UpdateData(
                table: "Visits",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 29, 17, 23, 18, 16, DateTimeKind.Local).AddTicks(5884));

            migrationBuilder.UpdateData(
                table: "Visits",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 29, 17, 23, 18, 16, DateTimeKind.Local).AddTicks(5890));
        }
    }
}

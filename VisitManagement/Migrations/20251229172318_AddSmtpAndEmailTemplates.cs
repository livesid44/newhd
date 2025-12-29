using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VisitManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddSmtpAndEmailTemplates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Visits",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "EmailTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TemplateType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SmtpSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Server = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Port = table.Column<int>(type: "int", nullable: false),
                    FromEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FromName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnableSsl = table.Column<bool>(type: "bit", nullable: false),
                    EnableNotifications = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmtpSettings", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "EmailTemplates",
                columns: new[] { "Id", "Body", "CreatedDate", "IsActive", "ModifiedDate", "Name", "Subject", "TemplateType" },
                values: new object[,]
                {
                    { 1, "<html><body>\n<h2>New Visit Has Been Created</h2>\n<p>A new visit has been scheduled with the following details:</p>\n<ul>\n<li><strong>Account Name:</strong> {AccountName}</li>\n<li><strong>Visit Date:</strong> {VisitDate}</li>\n<li><strong>Location:</strong> {Location}</li>\n<li><strong>Status:</strong> {VisitStatus}</li>\n<li><strong>Sales SPOC:</strong> {SalesSpoc}</li>\n<li><strong>Visit Type:</strong> {VisitType}</li>\n<li><strong>Visitors:</strong> {VisitorsName}</li>\n</ul>\n<p>Please review the visit details in the system.</p>\n<p>Best regards,<br/>Visit Management System</p>\n</body></html>", new DateTime(2025, 12, 29, 17, 23, 18, 16, DateTimeKind.Local).AddTicks(5927), true, new DateTime(2025, 12, 29, 17, 23, 18, 16, DateTimeKind.Local).AddTicks(5928), "Visit Created Notification", "New Visit Created - {AccountName}", "VisitCreated" },
                    { 2, "<html><body>\n<h2>Visit Has Been Updated</h2>\n<p>The visit details have been modified:</p>\n<ul>\n<li><strong>Account Name:</strong> {AccountName}</li>\n<li><strong>Visit Date:</strong> {VisitDate}</li>\n<li><strong>Location:</strong> {Location}</li>\n<li><strong>Status:</strong> {VisitStatus}</li>\n<li><strong>Sales SPOC:</strong> {SalesSpoc}</li>\n<li><strong>Visit Type:</strong> {VisitType}</li>\n<li><strong>Visitors:</strong> {VisitorsName}</li>\n</ul>\n<p>Please check the updated information in the system.</p>\n<p>Best regards,<br/>Visit Management System</p>\n</body></html>", new DateTime(2025, 12, 29, 17, 23, 18, 16, DateTimeKind.Local).AddTicks(5931), true, new DateTime(2025, 12, 29, 17, 23, 18, 16, DateTimeKind.Local).AddTicks(5931), "Visit Updated Notification", "Visit Updated - {AccountName}", "VisitUpdated" }
                });

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
                columns: new[] { "CreatedBy", "CreatedDate" },
                values: new object[] { "admin@visitmanagement.com", new DateTime(2025, 12, 29, 17, 23, 18, 16, DateTimeKind.Local).AddTicks(5884) });

            migrationBuilder.UpdateData(
                table: "Visits",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedBy", "CreatedDate" },
                values: new object[] { "admin@visitmanagement.com", new DateTime(2025, 12, 29, 17, 23, 18, 16, DateTimeKind.Local).AddTicks(5890) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailTemplates");

            migrationBuilder.DropTable(
                name: "SmtpSettings");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Visits");

            migrationBuilder.UpdateData(
                table: "VisitUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 29, 12, 33, 24, 452, DateTimeKind.Local).AddTicks(9952));

            migrationBuilder.UpdateData(
                table: "VisitUsers",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 29, 12, 33, 24, 452, DateTimeKind.Local).AddTicks(9955));

            migrationBuilder.UpdateData(
                table: "VisitUsers",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 29, 12, 33, 24, 452, DateTimeKind.Local).AddTicks(9957));

            migrationBuilder.UpdateData(
                table: "Visits",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 29, 12, 33, 24, 453, DateTimeKind.Local).AddTicks(123));

            migrationBuilder.UpdateData(
                table: "Visits",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 29, 12, 33, 24, 453, DateTimeKind.Local).AddTicks(129));
        }
    }
}

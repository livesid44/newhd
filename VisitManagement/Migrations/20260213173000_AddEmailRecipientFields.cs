using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VisitManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailRecipientFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ToRecipients",
                table: "EmailTemplates",
                type: "TEXT",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CcRecipients",
                table: "EmailTemplates",
                type: "TEXT",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BccRecipients",
                table: "EmailTemplates",
                type: "TEXT",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DefaultBccRecipients",
                table: "SmtpSettings",
                type: "TEXT",
                maxLength: 500,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ToRecipients",
                table: "EmailTemplates");

            migrationBuilder.DropColumn(
                name: "CcRecipients",
                table: "EmailTemplates");

            migrationBuilder.DropColumn(
                name: "BccRecipients",
                table: "EmailTemplates");

            migrationBuilder.DropColumn(
                name: "DefaultBccRecipients",
                table: "SmtpSettings");
        }
    }
}

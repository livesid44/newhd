using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VisitManagement.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Visits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerialNumber = table.Column<int>(type: "int", nullable: false),
                    TypeOfVisit = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Vertical = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SalesSpoc = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AccountName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DebitingProjectId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OpportunityDetails = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    OpportunityType = table.Column<int>(type: "int", nullable: false),
                    ServiceScope = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    SalesStage = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TcvMnUsd = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    VisitStatus = table.Column<int>(type: "int", nullable: false),
                    VisitType = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IntimationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Site = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    VisitorsName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    NumberOfAttendees = table.Column<int>(type: "int", nullable: false),
                    LevelOfVisitors = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    VisitDuration = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    VisitLead = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    KeyMessages = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visits", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedDate", "Email", "FullName", "IsActive", "PhoneNumber", "Role" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 12, 29, 11, 5, 37, 753, DateTimeKind.Local).AddTicks(9473), "john.doe@example.com", "John Doe", true, "+1-555-0100", "Administrator" },
                    { 2, new DateTime(2025, 12, 29, 11, 5, 37, 753, DateTimeKind.Local).AddTicks(9476), "jane.smith@example.com", "Jane Smith", true, "+1-555-0101", "Sales Manager" },
                    { 3, new DateTime(2025, 12, 29, 11, 5, 37, 753, DateTimeKind.Local).AddTicks(9478), "bob.johnson@example.com", "Bob Johnson", true, "+1-555-0102", "Team Lead" }
                });

            migrationBuilder.InsertData(
                table: "Visits",
                columns: new[] { "Id", "AccountName", "CreatedDate", "DebitingProjectId", "IntimationDate", "KeyMessages", "LevelOfVisitors", "Location", "ModifiedDate", "NumberOfAttendees", "OpportunityDetails", "OpportunityType", "Remarks", "SalesSpoc", "SalesStage", "SerialNumber", "ServiceScope", "Site", "TcvMnUsd", "TypeOfVisit", "Vertical", "VisitDate", "VisitDuration", "VisitLead", "VisitStatus", "VisitType", "VisitorsName" },
                values: new object[,]
                {
                    { 1, "Acme Corporation", new DateTime(2025, 12, 29, 11, 5, 37, 753, DateTimeKind.Local).AddTicks(9616), "PROJ-001", new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Demonstrate cloud capabilities and cost savings", "C-Level", "New York", null, 5, "New cloud infrastructure project", 0, "Important strategic meeting", "John Doe", "Proposal", 1, "Cloud migration and consulting", "Acme HQ", 1.5m, "Client Meeting", "Technology", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "1 Day", "Capability", 0, "On-site", "Mark Williams, Sarah Davis" },
                    { 2, "Global Bank Ltd", new DateTime(2025, 12, 29, 11, 5, 37, 753, DateTimeKind.Local).AddTicks(9622), "PROJ-002", new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Showcase platform scalability and security features", "VP Level", "London", null, 8, "Digital transformation initiative", 1, "Follow-up demo session", "Jane Smith", "Negotiation", 2, "Digital banking platform development", "Global Bank Office", 3.2m, "Technical Demo", "Finance", new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "4 Hours", "Sales", 1, "Virtual", "Peter Brown, Lisa Anderson" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Visits");
        }
    }
}

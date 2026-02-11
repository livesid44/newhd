using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VisitManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddVisitCategoryAndRelatedModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "Visits",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Checklists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VisitId = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    ChecklistType = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ItemName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    CompletedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CompletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checklists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Checklists_Visits_VisitId",
                        column: x => x.VisitId,
                        principalTable: "Visits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stakeholders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Team = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Site = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stakeholders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VisitId = table.Column<int>(type: "int", nullable: false),
                    TaskName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    AssignedToTeam = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AssignedToPerson = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CompletionNotes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskAssignments_Visits_VisitId",
                        column: x => x.VisitId,
                        principalTable: "Visits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.InsertData(
                table: "Stakeholders",
                columns: new[] { "Id", "CreatedDate", "Email", "FullName", "IsActive", "Location", "ModifiedDate", "PhoneNumber", "Role", "Site", "Team" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 2, 11, 14, 25, 12, 882, DateTimeKind.Local).AddTicks(4635), "ce.lead@techmahindra.com", "Client Experience Team Lead", true, "Global", null, "+1-555-0200", "Team Lead", null, "Client Experience" },
                    { 2, new DateTime(2026, 2, 11, 14, 25, 12, 882, DateTimeKind.Local).AddTicks(4638), "marketing.manager@techmahindra.com", "Marketing Manager", true, "Global", null, "+1-555-0201", "Manager", null, "Marketing" },
                    { 3, new DateTime(2026, 2, 11, 14, 25, 12, 882, DateTimeKind.Local).AddTicks(4640), "creative.lead@techmahindra.com", "Creative Hub Lead", true, "Global", null, "+1-555-0202", "Lead", null, "Creative Hub" },
                    { 4, new DateTime(2026, 2, 11, 14, 25, 12, 882, DateTimeKind.Local).AddTicks(4643), "cs.manager@techmahindra.com", "Customer Success Manager", true, "Global", null, "+1-555-0203", "Manager", null, "Customer Success" },
                    { 5, new DateTime(2026, 2, 11, 14, 25, 12, 882, DateTimeKind.Local).AddTicks(4645), "tim.coordinator@techmahindra.com", "TIM Coordinator", true, "Global", null, "+1-555-0204", "Coordinator", null, "TIM" }
                });

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
                columns: new[] { "Category", "CreatedDate" },
                values: new object[] { null, new DateTime(2026, 2, 11, 14, 25, 12, 882, DateTimeKind.Local).AddTicks(4531) });

            migrationBuilder.UpdateData(
                table: "Visits",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Category", "CreatedDate" },
                values: new object[] { null, new DateTime(2026, 2, 11, 14, 25, 12, 882, DateTimeKind.Local).AddTicks(4537) });

            migrationBuilder.CreateIndex(
                name: "IX_Checklists_VisitId",
                table: "Checklists",
                column: "VisitId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskAssignments_VisitId",
                table: "TaskAssignments",
                column: "VisitId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Checklists");

            migrationBuilder.DropTable(
                name: "Stakeholders");

            migrationBuilder.DropTable(
                name: "TaskAssignments");

            migrationBuilder.DropColumn(
                name: "Category",
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

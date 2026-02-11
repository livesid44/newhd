using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VisitManagement.Data;
using VisitManagement.Models;
using System.Text;

namespace VisitManagement.Controllers
{
    [Authorize]
    public class ReportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ExportToExcel(DateTime? startDate, DateTime? endDate)
        {
            var userEmail = User.Identity?.Name;
            var isAdmin = User.IsInRole("Admin");

            // Query visits based on role and date filter
            var visitsQuery = _context.Visits.AsQueryable();

            if (!isAdmin)
            {
                visitsQuery = visitsQuery.Where(v => v.CreatedBy == userEmail);
            }

            if (startDate.HasValue)
            {
                visitsQuery = visitsQuery.Where(v => v.VisitDate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                visitsQuery = visitsQuery.Where(v => v.VisitDate <= endDate.Value);
            }

            var visits = await visitsQuery.OrderBy(v => v.VisitDate).ToListAsync();

            // Generate CSV content
            var csv = new StringBuilder();
            
            // Add header row
            csv.AppendLine("Visit Date,Type Of Visit,Opportunity Type,Sales Stage,Client Name,Visit Category,Geo,Location,Location CS SPOC,Sales SPOC,Vertical,Vertical Head,Account Owner,Horizontal,Horizontal Head,Clients Country of Origin,Debiting Project ID,TCV MN USD,Name and No. Attendees - Clients End,Duration of Visit,Additional Information,Repository");

            // Add data rows
            foreach (var visit in visits)
            {
                csv.AppendLine($"{visit.VisitDate:dd/MM/yyyy},{EscapeCsv(visit.TypeOfVisit)},{EscapeCsv(visit.OpportunityType.ToString())},{EscapeCsv(visit.SalesStage)},{EscapeCsv(visit.AccountName)},{EscapeCsv(visit.Category?.ToString())},{EscapeCsv(visit.Geo)},{EscapeCsv(visit.Location)},{EscapeCsv(visit.LocationCsSpoc)},{EscapeCsv(visit.SalesSpoc)},{EscapeCsv(visit.Vertical)},{EscapeCsv(visit.VerticalHead)},{EscapeCsv(visit.AccountOwner)},{EscapeCsv(visit.Horizontal)},{EscapeCsv(visit.HorizontalHead)},{EscapeCsv(visit.ClientsCountryOfOrigin)},{EscapeCsv(visit.DebitingProjectId)},{visit.TcvMnUsd},{EscapeCsv(visit.VisitorsName)},{EscapeCsv(visit.VisitDuration)},{EscapeCsv(visit.AdditionalInformation)},{EscapeCsv(visit.Repository)}");
            }

            var bytes = Encoding.UTF8.GetBytes(csv.ToString());
            var fileName = $"VisitReport_{DateTime.Now:yyyyMMdd_HHmmss}.csv";

            return File(bytes, "text/csv", fileName);
        }

        private string EscapeCsv(string? value)
        {
            if (string.IsNullOrEmpty(value))
                return "";

            // Escape quotes and wrap in quotes if contains comma, quote, or newline
            if (value.Contains(",") || value.Contains("\"") || value.Contains("\n") || value.Contains("\r"))
            {
                return "\"" + value.Replace("\"", "\"\"") + "\"";
            }

            return value;
        }
    }
}

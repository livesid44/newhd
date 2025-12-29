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
            csv.AppendLine("S.NO,Type Of Visit,Vertical,Sales SPOC,Account Name,Debiting Project ID,Opportunity Details,Opportunity Type,Service Scope,Sales Stage,TCV MN USD,Visit Status,Visit Type,Visit Date,Date of Intimation,Location,Site,Visitors Name,No. Attendees,Level of Visitors,Duration of Visit,Remarks,Visit Lead,Key Messages,Created By");

            // Add data rows
            foreach (var visit in visits)
            {
                csv.AppendLine($"{visit.SerialNumber},{EscapeCsv(visit.TypeOfVisit)},{EscapeCsv(visit.Vertical)},{EscapeCsv(visit.SalesSpoc)},{EscapeCsv(visit.AccountName)},{EscapeCsv(visit.DebitingProjectId)},{EscapeCsv(visit.OpportunityDetails)},{EscapeCsv(visit.OpportunityType.ToString())},{EscapeCsv(visit.ServiceScope)},{EscapeCsv(visit.SalesStage)},{visit.TcvMnUsd},{EscapeCsv(visit.VisitStatus.ToString())},{EscapeCsv(visit.VisitType)},{visit.VisitDate:dd/MM/yyyy},{visit.IntimationDate:dd/MM/yyyy},{EscapeCsv(visit.Location)},{EscapeCsv(visit.Site)},{EscapeCsv(visit.VisitorsName)},{visit.NumberOfAttendees},{EscapeCsv(visit.LevelOfVisitors)},{EscapeCsv(visit.VisitDuration)},{EscapeCsv(visit.Remarks)},{EscapeCsv(visit.VisitLead)},{EscapeCsv(visit.KeyMessages)},{EscapeCsv(visit.CreatedBy)}");
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

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VisitManagement.Data;
using VisitManagement.Models;

namespace VisitManagement.Controllers
{
    [Authorize]
    public class ChecklistsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChecklistsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Checklists by Visit
        public async Task<IActionResult> Index(int? visitId)
        {
            if (!visitId.HasValue)
            {
                return RedirectToAction("Index", "Visits");
            }

            var visit = await _context.Visits.FindAsync(visitId.Value);
            if (visit == null)
            {
                return NotFound();
            }

            ViewBag.Visit = visit;

            var checklists = await _context.Checklists
                .Where(c => c.VisitId == visitId.Value)
                .OrderBy(c => c.ChecklistType)
                .ThenBy(c => c.DisplayOrder)
                .ToListAsync();

            // If no checklists exist and visit has a category, create default checklists
            if (!checklists.Any() && visit.Category.HasValue)
            {
                await CreateDefaultChecklists(visitId.Value, visit.Category.Value);
                checklists = await _context.Checklists
                    .Where(c => c.VisitId == visitId.Value)
                    .OrderBy(c => c.ChecklistType)
                    .ThenBy(c => c.DisplayOrder)
                    .ToListAsync();
            }

            return View(checklists);
        }

        // POST: Checklists/ToggleComplete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleComplete(int id, int visitId)
        {
            var checklist = await _context.Checklists.FindAsync(id);
            if (checklist == null)
            {
                return NotFound();
            }

            checklist.IsCompleted = !checklist.IsCompleted;
            checklist.ModifiedDate = DateTime.Now;

            if (checklist.IsCompleted)
            {
                checklist.CompletedBy = User.Identity?.Name;
                checklist.CompletedDate = DateTime.Now;
            }
            else
            {
                checklist.CompletedBy = null;
                checklist.CompletedDate = null;
            }

            _context.Update(checklist);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index), new { visitId });
        }

        // POST: Checklists/UpdateRemarks/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateRemarks(int id, string remarks, int visitId)
        {
            var checklist = await _context.Checklists.FindAsync(id);
            if (checklist == null)
            {
                return NotFound();
            }

            checklist.Remarks = remarks;
            checklist.ModifiedDate = DateTime.Now;

            _context.Update(checklist);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index), new { visitId });
        }

        private async Task CreateDefaultChecklists(int visitId, VisitCategory category)
        {
            var checklists = new List<Checklist>();

            // Common checklists for all categories
            var commonItems = new List<(string type, string item, int order)>
            {
                ("Pre-Visit", "Confirm visit date and time", 1),
                ("Pre-Visit", "Send calendar invite to all attendees", 2),
                ("Pre-Visit", "Prepare visit agenda", 3),
                ("Pre-Visit", "Arrange meeting room/venue", 4),
                ("During Visit", "Welcome visitors", 1),
                ("During Visit", "Present key messages", 2),
                ("During Visit", "Facility tour (if applicable)", 3),
                ("Post-Visit", "Send thank you email", 1),
                ("Post-Visit", "Share presentation materials", 2),
                ("Post-Visit", "Collect feedback", 3),
            };

            foreach (var (type, item, order) in commonItems)
            {
                checklists.Add(new Checklist
                {
                    VisitId = visitId,
                    Category = category,
                    ChecklistType = type,
                    ItemName = item,
                    DisplayOrder = order,
                    CreatedDate = DateTime.Now
                });
            }

            // Category-specific checklists
            if (category == VisitCategory.Platinum)
            {
                checklists.AddRange(new[]
                {
                    new Checklist { VisitId = visitId, Category = category, ChecklistType = "Platinum Specific", ItemName = "Arrange luxury accommodation (5-star)", DisplayOrder = 1, CreatedDate = DateTime.Now },
                    new Checklist { VisitId = visitId, Category = category, ChecklistType = "Platinum Specific", ItemName = "Arrange private dining", DisplayOrder = 2, CreatedDate = DateTime.Now },
                    new Checklist { VisitId = visitId, Category = category, ChecklistType = "Platinum Specific", ItemName = "Business class/First class travel arrangements", DisplayOrder = 3, CreatedDate = DateTime.Now },
                    new Checklist { VisitId = visitId, Category = category, ChecklistType = "Platinum Specific", ItemName = "Executive gifts preparation", DisplayOrder = 4, CreatedDate = DateTime.Now },
                    new Checklist { VisitId = visitId, Category = category, ChecklistType = "Platinum Specific", ItemName = "Senior leadership engagement", DisplayOrder = 5, CreatedDate = DateTime.Now },
                });
            }
            else if (category == VisitCategory.Gold)
            {
                checklists.AddRange(new[]
                {
                    new Checklist { VisitId = visitId, Category = category, ChecklistType = "Gold Specific", ItemName = "Arrange premium accommodation (4-star)", DisplayOrder = 1, CreatedDate = DateTime.Now },
                    new Checklist { VisitId = visitId, Category = category, ChecklistType = "Gold Specific", ItemName = "Standard dining arrangements", DisplayOrder = 2, CreatedDate = DateTime.Now },
                    new Checklist { VisitId = visitId, Category = category, ChecklistType = "Gold Specific", ItemName = "Business class travel", DisplayOrder = 3, CreatedDate = DateTime.Now },
                    new Checklist { VisitId = visitId, Category = category, ChecklistType = "Gold Specific", ItemName = "Corporate gifts", DisplayOrder = 4, CreatedDate = DateTime.Now },
                });
            }
            else if (category == VisitCategory.Silver)
            {
                checklists.AddRange(new[]
                {
                    new Checklist { VisitId = visitId, Category = category, ChecklistType = "Silver Specific", ItemName = "Arrange standard accommodation (3-star)", DisplayOrder = 1, CreatedDate = DateTime.Now },
                    new Checklist { VisitId = visitId, Category = category, ChecklistType = "Silver Specific", ItemName = "Basic refreshments", DisplayOrder = 2, CreatedDate = DateTime.Now },
                    new Checklist { VisitId = visitId, Category = category, ChecklistType = "Silver Specific", ItemName = "Economy/Business class travel", DisplayOrder = 3, CreatedDate = DateTime.Now },
                });
            }

            // Marketing and Creative checklists
            checklists.AddRange(new[]
            {
                new Checklist { VisitId = visitId, Category = category, ChecklistType = "Marketing", ItemName = "Prepare company presentation", DisplayOrder = 1, CreatedDate = DateTime.Now },
                new Checklist { VisitId = visitId, Category = category, ChecklistType = "Marketing", ItemName = "Create customized brochures", DisplayOrder = 2, CreatedDate = DateTime.Now },
                new Checklist { VisitId = visitId, Category = category, ChecklistType = "Marketing", ItemName = "Prepare case studies", DisplayOrder = 3, CreatedDate = DateTime.Now },
                new Checklist { VisitId = visitId, Category = category, ChecklistType = "Creative", ItemName = "Design visit materials", DisplayOrder = 1, CreatedDate = DateTime.Now },
                new Checklist { VisitId = visitId, Category = category, ChecklistType = "Creative", ItemName = "Photography/videography arrangements", DisplayOrder = 2, CreatedDate = DateTime.Now },
            });

            _context.Checklists.AddRange(checklists);
            await _context.SaveChangesAsync();
        }
    }
}

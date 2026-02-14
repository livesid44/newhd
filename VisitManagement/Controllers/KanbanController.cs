using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VisitManagement.Data;
using VisitManagement.Models;

namespace VisitManagement.Controllers
{
    [Authorize]
    public class KanbanController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KanbanController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Kanban/Board?visitId=1
        public async Task<IActionResult> Board(int? visitId)
        {
            if (visitId == null)
            {
                return NotFound();
            }

            var visit = await _context.Visits.FindAsync(visitId);
            if (visit == null)
            {
                return NotFound();
            }

            // Check authorization
            if (!User.IsInRole("Admin") && visit.CreatedBy != User.Identity?.Name)
            {
                return Forbid();
            }

            ViewData["VisitId"] = visitId;
            ViewData["VisitDetails"] = visit;

            // Get all tasks for this visit, grouped by status
            var tasks = await _context.TaskAssignments
                .Include(t => t.Visit)
                .Where(t => t.VisitId == visitId)
                .OrderBy(t => t.Priority)
                .ThenBy(t => t.DueDate)
                .ToListAsync();

            return View(tasks);
        }

        // POST: Kanban/UpdateStatus (AJAX)
        [HttpPost]
        public async Task<IActionResult> UpdateStatus([FromBody] UpdateStatusRequest request)
        {
            var task = await _context.TaskAssignments.FindAsync(request.TaskId);
            if (task == null)
            {
                return NotFound();
            }

            // Parse the new status
            if (!Enum.TryParse<TaskAssignmentStatus>(request.NewStatus, out var newStatus))
            {
                return BadRequest("Invalid status");
            }

            task.Status = newStatus;
            task.ModifiedDate = DateTime.Now;

            if (newStatus == TaskAssignmentStatus.Completed)
            {
                task.CompletedDate = DateTime.Now;
            }

            await _context.SaveChangesAsync();

            return Ok(new { success = true, taskId = task.Id, newStatus = newStatus.ToString() });
        }

        public class UpdateStatusRequest
        {
            public int TaskId { get; set; }
            public string NewStatus { get; set; } = string.Empty;
        }
    }
}

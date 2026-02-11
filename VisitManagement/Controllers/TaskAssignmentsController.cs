using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VisitManagement.Data;
using VisitManagement.Models;

namespace VisitManagement.Controllers
{
    [Authorize]
    public class TaskAssignmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TaskAssignmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TaskAssignments
        public async Task<IActionResult> Index(string filterTeam, string filterStatus)
        {
            ViewData["CurrentFilterTeam"] = filterTeam;
            ViewData["CurrentFilterStatus"] = filterStatus;

            var tasks = _context.TaskAssignments.Include(t => t.Visit).AsQueryable();

            if (!string.IsNullOrEmpty(filterTeam))
            {
                tasks = tasks.Where(t => t.AssignedToTeam == filterTeam);
            }

            if (!string.IsNullOrEmpty(filterStatus))
            {
                if (Enum.TryParse<TaskAssignmentStatus>(filterStatus, out var status))
                {
                    tasks = tasks.Where(t => t.Status == status);
                }
            }

            // Non-admin users see only tasks for their visits or assigned to them
            if (!User.IsInRole("Admin"))
            {
                var userEmail = User.Identity?.Name;
                tasks = tasks.Where(t => t.Visit!.CreatedBy == userEmail || t.AssignedToPerson == userEmail);
            }

            return View(await tasks.OrderBy(t => t.DueDate).ToListAsync());
        }

        // GET: TaskAssignments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.TaskAssignments
                .Include(t => t.Visit)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // GET: TaskAssignments/Create
        public IActionResult Create(int? visitId)
        {
            ViewBag.Visits = _context.Visits.OrderByDescending(v => v.CreatedDate).ToList();
            var task = new TaskAssignment();
            if (visitId.HasValue)
            {
                task.VisitId = visitId.Value;
            }
            return View(task);
        }

        // POST: TaskAssignments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VisitId,TaskName,AssignedToTeam,AssignedToPerson,DueDate,Priority,Status,Description")] TaskAssignment task)
        {
            if (ModelState.IsValid)
            {
                task.CreatedDate = DateTime.Now;
                task.CreatedBy = User.Identity?.Name ?? "Unknown";
                _context.Add(task);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Visits = _context.Visits.OrderByDescending(v => v.CreatedDate).ToList();
            return View(task);
        }

        // GET: TaskAssignments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.TaskAssignments.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            ViewBag.Visits = _context.Visits.OrderByDescending(v => v.CreatedDate).ToList();
            return View(task);
        }

        // POST: TaskAssignments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VisitId,TaskName,AssignedToTeam,AssignedToPerson,DueDate,Priority,Status,Description,CompletionNotes,CreatedDate,CreatedBy")] TaskAssignment task)
        {
            if (id != task.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    task.ModifiedDate = DateTime.Now;
                    if (task.Status == TaskAssignmentStatus.Completed && !task.CompletedDate.HasValue)
                    {
                        task.CompletedDate = DateTime.Now;
                    }
                    _context.Update(task);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskExists(task.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Visits = _context.Visits.OrderByDescending(v => v.CreatedDate).ToList();
            return View(task);
        }

        // GET: TaskAssignments/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.TaskAssignments
                .Include(t => t.Visit)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // POST: TaskAssignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await _context.TaskAssignments.FindAsync(id);
            if (task != null)
            {
                _context.TaskAssignments.Remove(task);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskExists(int id)
        {
            return _context.TaskAssignments.Any(e => e.Id == id);
        }
    }
}

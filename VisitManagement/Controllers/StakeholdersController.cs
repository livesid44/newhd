using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VisitManagement.Data;
using VisitManagement.Models;

namespace VisitManagement.Controllers
{
    [Authorize]
    public class StakeholdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StakeholdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Stakeholders
        public async Task<IActionResult> Index(string searchString, string filterTeam, string filterLocation)
        {
            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentFilterTeam"] = filterTeam;
            ViewData["CurrentFilterLocation"] = filterLocation;

            var stakeholders = _context.Stakeholders.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                stakeholders = stakeholders.Where(s => s.FullName.Contains(searchString) 
                    || s.Email.Contains(searchString) 
                    || s.Role.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(filterTeam))
            {
                stakeholders = stakeholders.Where(s => s.Team == filterTeam);
            }

            if (!string.IsNullOrEmpty(filterLocation))
            {
                stakeholders = stakeholders.Where(s => s.Location == filterLocation);
            }

            return View(await stakeholders.Where(s => s.IsActive).OrderBy(s => s.Team).ThenBy(s => s.FullName).ToListAsync());
        }

        // GET: Stakeholders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stakeholder = await _context.Stakeholders.FirstOrDefaultAsync(m => m.Id == id);
            if (stakeholder == null)
            {
                return NotFound();
            }

            return View(stakeholder);
        }

        // GET: Stakeholders/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Stakeholders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("FullName,Email,PhoneNumber,Team,Role,Location,Site,IsActive")] Stakeholder stakeholder)
        {
            if (ModelState.IsValid)
            {
                stakeholder.CreatedDate = DateTime.Now;
                _context.Add(stakeholder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stakeholder);
        }

        // GET: Stakeholders/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stakeholder = await _context.Stakeholders.FindAsync(id);
            if (stakeholder == null)
            {
                return NotFound();
            }
            return View(stakeholder);
        }

        // POST: Stakeholders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,Email,PhoneNumber,Team,Role,Location,Site,IsActive,CreatedDate")] Stakeholder stakeholder)
        {
            if (id != stakeholder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    stakeholder.ModifiedDate = DateTime.Now;
                    _context.Update(stakeholder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StakeholderExists(stakeholder.Id))
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
            return View(stakeholder);
        }

        // GET: Stakeholders/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stakeholder = await _context.Stakeholders.FirstOrDefaultAsync(m => m.Id == id);
            if (stakeholder == null)
            {
                return NotFound();
            }

            return View(stakeholder);
        }

        // POST: Stakeholders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stakeholder = await _context.Stakeholders.FindAsync(id);
            if (stakeholder != null)
            {
                stakeholder.IsActive = false;
                stakeholder.ModifiedDate = DateTime.Now;
                _context.Update(stakeholder);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StakeholderExists(int id)
        {
            return _context.Stakeholders.Any(e => e.Id == id);
        }
    }
}

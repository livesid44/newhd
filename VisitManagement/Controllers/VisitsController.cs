using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VisitManagement.Data;
using VisitManagement.Models;

namespace VisitManagement.Controllers
{
    [Authorize]
    public class VisitsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VisitsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Visits
        public async Task<IActionResult> Index(string searchString, string sortOrder)
        {
            ViewData["CurrentFilter"] = searchString;
            ViewData["AccountNameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "account_desc" : "";
            ViewData["VisitDateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["StatusSortParm"] = sortOrder == "Status" ? "status_desc" : "Status";

            var visits = from v in _context.Visits
                        select v;

            if (!String.IsNullOrEmpty(searchString))
            {
                visits = visits.Where(v => v.AccountName.Contains(searchString)
                                       || v.Location.Contains(searchString)
                                       || v.SalesSpoc.Contains(searchString)
                                       || v.TypeOfVisit.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "account_desc":
                    visits = visits.OrderByDescending(v => v.AccountName);
                    break;
                case "Date":
                    visits = visits.OrderBy(v => v.VisitDate);
                    break;
                case "date_desc":
                    visits = visits.OrderByDescending(v => v.VisitDate);
                    break;
                case "Status":
                    visits = visits.OrderBy(v => v.VisitStatus);
                    break;
                case "status_desc":
                    visits = visits.OrderByDescending(v => v.VisitStatus);
                    break;
                default:
                    visits = visits.OrderBy(v => v.AccountName);
                    break;
            }

            return View(await visits.AsNoTracking().ToListAsync());
        }

        // GET: Visits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visit = await _context.Visits
                .FirstOrDefaultAsync(m => m.Id == id);
            if (visit == null)
            {
                return NotFound();
            }

            return View(visit);
        }

        // GET: Visits/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Visits/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SerialNumber,TypeOfVisit,Vertical,SalesSpoc,AccountName,DebitingProjectId,OpportunityDetails,OpportunityType,ServiceScope,SalesStage,TcvMnUsd,VisitStatus,VisitType,VisitDate,IntimationDate,Location,Site,VisitorsName,NumberOfAttendees,LevelOfVisitors,VisitDuration,Remarks,VisitLead,KeyMessages")] Visit visit)
        {
            if (ModelState.IsValid)
            {
                visit.CreatedDate = DateTime.Now;
                _context.Add(visit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(visit);
        }

        // GET: Visits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visit = await _context.Visits.FindAsync(id);
            if (visit == null)
            {
                return NotFound();
            }
            return View(visit);
        }

        // POST: Visits/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SerialNumber,TypeOfVisit,Vertical,SalesSpoc,AccountName,DebitingProjectId,OpportunityDetails,OpportunityType,ServiceScope,SalesStage,TcvMnUsd,VisitStatus,VisitType,VisitDate,IntimationDate,Location,Site,VisitorsName,NumberOfAttendees,LevelOfVisitors,VisitDuration,Remarks,VisitLead,KeyMessages,CreatedDate")] Visit visit)
        {
            if (id != visit.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    visit.ModifiedDate = DateTime.Now;
                    _context.Update(visit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VisitExists(visit.Id))
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
            return View(visit);
        }

        // GET: Visits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visit = await _context.Visits
                .FirstOrDefaultAsync(m => m.Id == id);
            if (visit == null)
            {
                return NotFound();
            }

            return View(visit);
        }

        // POST: Visits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var visit = await _context.Visits.FindAsync(id);
            if (visit != null)
            {
                _context.Visits.Remove(visit);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VisitExists(int id)
        {
            return _context.Visits.Any(e => e.Id == id);
        }
    }
}

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

            // Role-based filtering: Users see only their own visits, Admins see all
            if (!User.IsInRole("Admin"))
            {
                var userEmail = User.Identity?.Name;
                visits = visits.Where(v => v.CreatedBy == userEmail);
            }

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
            PopulateDropdownData();
            return View();
        }

        // POST: Visits/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VisitDate,TypeOfVisit,OpportunityType,SalesStage,AccountName,Category,Geo,Location,LocationCsSpoc,SalesSpoc,Vertical,VerticalHead,AccountOwner,Horizontal,HorizontalHead,ClientsCountryOfOrigin,DebitingProjectId,TcvMnUsd,VisitorsName,VisitDuration,AdditionalInformation,Repository,VisitStatus,VisitType,IntimationDate,Site,NumberOfAttendees,LevelOfVisitors,Remarks,VisitLead,KeyMessages,OpportunityDetails,ServiceScope")] Visit visit)
        {
            if (ModelState.IsValid)
            {
                visit.CreatedDate = DateTime.Now;
                visit.CreatedBy = User.Identity?.Name ?? "Unknown";
                
                // Automatically determine visit category if not set
                if (!visit.Category.HasValue)
                {
                    visit.Category = DetermineVisitCategory(visit);
                }
                
                _context.Add(visit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateDropdownData();
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

            // Non-admin users can only edit their own visits
            if (!User.IsInRole("Admin") && visit.CreatedBy != User.Identity?.Name)
            {
                return Forbid();
            }

            PopulateDropdownData();
            return View(visit);
        }

        // POST: Visits/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VisitDate,TypeOfVisit,OpportunityType,SalesStage,AccountName,Category,Geo,Location,LocationCsSpoc,SalesSpoc,Vertical,VerticalHead,AccountOwner,Horizontal,HorizontalHead,ClientsCountryOfOrigin,DebitingProjectId,TcvMnUsd,VisitorsName,VisitDuration,AdditionalInformation,Repository,VisitStatus,VisitType,IntimationDate,Site,NumberOfAttendees,LevelOfVisitors,Remarks,VisitLead,KeyMessages,OpportunityDetails,ServiceScope,CreatedDate,CreatedBy")] Visit visit)
        {
            if (id != visit.Id)
            {
                return NotFound();
            }

            // Non-admin users can only edit their own visits
            if (!User.IsInRole("Admin") && visit.CreatedBy != User.Identity?.Name)
            {
                return Forbid();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    visit.ModifiedDate = DateTime.Now;
                    
                    // Re-evaluate category if not set and TCV or visitor level exists
                    if (!visit.Category.HasValue)
                    {
                        visit.Category = DetermineVisitCategory(visit);
                    }
                    
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
            PopulateDropdownData();
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

            // Non-admin users can only delete their own visits
            if (!User.IsInRole("Admin") && visit.CreatedBy != User.Identity?.Name)
            {
                return Forbid();
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
                // Non-admin users can only delete their own visits
                if (!User.IsInRole("Admin") && visit.CreatedBy != User.Identity?.Name)
                {
                    return Forbid();
                }

                _context.Visits.Remove(visit);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VisitExists(int id)
        {
            return _context.Visits.Any(e => e.Id == id);
        }

        private void PopulateDropdownData()
        {
            ViewBag.TypeOfVisitList = new List<string>
            {
                "Prospect Visit",
                "Operations Visit",
                "RnR",
                "Ramp",
                "Trainings",
                "Others"
            };

            ViewBag.VerticalList = new List<string>
            {
                "Technology",
                "Finance",
                "Healthcare",
                "Manufacturing",
                "Retail",
                "Telecom",
                "Others"
            };

            ViewBag.SalesStageList = new List<string>
            {
                "Prospect",
                "Qualification",
                "Proposal",
                "Negotiation",
                "Closed Won",
                "Closed Lost"
            };

            ViewBag.VisitTypeList = new List<string>
            {
                "On-site",
                "Virtual",
                "Hybrid"
            };

            ViewBag.LocationList = new List<string>
            {
                "Pune",
                "Mumbai",
                "Bangalore",
                "Hyderabad",
                "Chennai",
                "Delhi NCR",
                "Kolkata",
                "New York",
                "London",
                "Singapore",
                "Dubai",
                "Others"
            };

            ViewBag.SiteList = new List<string>
            {
                "Hinjewadi",
                "Magarpatta",
                "Whitefield",
                "HITEC City",
                "OMR",
                "Gurgaon",
                "Noida",
                "Others"
            };

            ViewBag.LevelOfVisitorsList = new List<string>
            {
                "C-Level/CXO",
                "VP Level",
                "Director Level",
                "Manager Level",
                "Team Lead",
                "Individual Contributor",
                "Others"
            };

            ViewBag.VisitLeadList = new List<string>
            {
                "Capability",
                "Sales",
                "Marketing",
                "Delivery",
                "Others"
            };
        }

        private VisitCategory DetermineVisitCategory(Visit visit)
        {
            // Automatic categorization based on TCV and visitor level
            var tcvInMillion = visit.TcvMnUsd;
            var visitorLevel = visit.LevelOfVisitors?.ToLower() ?? "";

            // Platinum: CXO involvement + >$20M opportunity OR C-Level visitors + >$15M
            if ((visitorLevel.Contains("c-level") || visitorLevel.Contains("cxo") || visitorLevel.Contains("ceo") || 
                 visitorLevel.Contains("cfo") || visitorLevel.Contains("cto")) && tcvInMillion >= 15)
            {
                return VisitCategory.Platinum;
            }

            // Gold: VP/Senior Leaders + $10-20M OR high-value opportunities
            if ((visitorLevel.Contains("vp") || visitorLevel.Contains("vice president") || 
                 visitorLevel.Contains("senior") || visitorLevel.Contains("director")) && tcvInMillion >= 10)
            {
                return VisitCategory.Gold;
            }

            // Silver: All others
            return VisitCategory.Silver;
        }
    }
}

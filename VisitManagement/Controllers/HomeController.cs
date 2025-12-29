using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VisitManagement.Data;
using VisitManagement.Models;
using VisitManagement.ViewModels;

namespace VisitManagement.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var currentUserEmail = User.Identity?.Name;
        var isAdmin = User.IsInRole("Admin");

        // Get visits based on role
        var visits = isAdmin
            ? await _context.Visits.ToListAsync()
            : await _context.Visits.Where(v => v.CreatedBy == currentUserEmail).ToListAsync();

        // Calculate statistics
        var totalVisits = visits.Count;
        var confirmedVisits = visits.Count(v => v.VisitStatus == VisitStatus.Confirmed);
        var tentativeVisits = visits.Count(v => v.VisitStatus == VisitStatus.Tentative);
        
        // Date-wise statistics (Last 30 days)
        var last30Days = DateTime.Now.AddDays(-30);
        var visitsLast30Days = visits.Where(v => v.VisitDate >= last30Days).ToList();
        
        // Group by date
        var dateWiseStats = visitsLast30Days
            .GroupBy(v => v.VisitDate.Date)
            .OrderBy(g => g.Key)
            .Select(g => new DateWiseVisitCount
            {
                Date = g.Key,
                Count = g.Count()
            })
            .ToList();

        // User-wise statistics (for admin)
        var userWiseStats = isAdmin
            ? visits
                .GroupBy(v => v.CreatedBy)
                .Select(g => new UserWiseVisitCount
                {
                    UserEmail = g.Key,
                    Count = g.Count()
                })
                .OrderByDescending(u => u.Count)
                .Take(10)
                .ToList()
            : new List<UserWiseVisitCount>();

        // Recent visits
        var recentVisits = visits
            .OrderByDescending(v => v.CreatedDate)
            .Take(5)
            .ToList();

        // Upcoming visits
        var upcomingVisits = visits
            .Where(v => v.VisitDate >= DateTime.Now)
            .OrderBy(v => v.VisitDate)
            .Take(5)
            .ToList();

        var dashboardViewModel = new DashboardViewModel
        {
            TotalVisits = totalVisits,
            ConfirmedVisits = confirmedVisits,
            TentativeVisits = tentativeVisits,
            DateWiseStats = dateWiseStats,
            UserWiseStats = userWiseStats,
            RecentVisits = recentVisits,
            UpcomingVisits = upcomingVisits,
            IsAdmin = isAdmin
        };

        return View(dashboardViewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

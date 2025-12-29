using VisitManagement.Models;

namespace VisitManagement.ViewModels
{
    public class DashboardViewModel
    {
        public int TotalVisits { get; set; }
        public int ConfirmedVisits { get; set; }
        public int TentativeVisits { get; set; }
        public List<DateWiseVisitCount> DateWiseStats { get; set; } = new();
        public List<UserWiseVisitCount> UserWiseStats { get; set; } = new();
        public List<Visit> RecentVisits { get; set; } = new();
        public List<Visit> UpcomingVisits { get; set; } = new();
        public bool IsAdmin { get; set; }
    }

    public class DateWiseVisitCount
    {
        public DateTime Date { get; set; }
        public int Count { get; set; }
    }

    public class UserWiseVisitCount
    {
        public string UserEmail { get; set; } = string.Empty;
        public int Count { get; set; }
    }
}

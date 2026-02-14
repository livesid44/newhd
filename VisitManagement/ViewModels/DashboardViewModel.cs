using VisitManagement.Models;

namespace VisitManagement.ViewModels
{
    public class DashboardViewModel
    {
        public int TotalVisits { get; set; }
        public int ConfirmedVisits { get; set; }
        public int TentativeVisits { get; set; }
        
        // Month-over-Month statistics
        public int CurrentMonthVisits { get; set; }
        public int PreviousMonthVisits { get; set; }
        public int MonthOverMonthChange { get; set; }
        
        // Visit type segregation
        public int ProspectVisits { get; set; }
        public int OperationsVisits { get; set; }
        
        public List<DateWiseVisitCount> DateWiseStats { get; set; } = new();
        public List<UserWiseVisitCount> UserWiseStats { get; set; } = new();
        public List<LocationWiseCount> LocationWiseStats { get; set; } = new();
        public List<SalesSpocWiseCount> SalesSpocWiseStats { get; set; } = new();
        public List<VerticalWiseCount> VerticalWiseStats { get; set; } = new();
        
        public List<Visit> RecentVisits { get; set; } = new();
        public List<Visit> UpcomingVisits { get; set; } = new();
        public List<Visit> UpcomingProspectVisits { get; set; } = new();
        public List<Visit> UpcomingOperationsVisits { get; set; } = new();
        
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
    
    public class LocationWiseCount
    {
        public string Location { get; set; } = string.Empty;
        public int Count { get; set; }
    }
    
    public class SalesSpocWiseCount
    {
        public string SalesSpoc { get; set; } = string.Empty;
        public int Count { get; set; }
    }
    
    public class VerticalWiseCount
    {
        public string Vertical { get; set; } = string.Empty;
        public int Count { get; set; }
    }
}

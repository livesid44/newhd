using System.ComponentModel.DataAnnotations;

namespace VisitManagement.Models
{
    public enum VisitCategory
    {
        [Display(Name = "Platinum (CXO level, >$20M opportunity, Luxury amenities, C-Suite engagement)")]
        Platinum,
        
        [Display(Name = "Gold (VP/AVP/Director level, $10-20M opportunity, Premium amenities)")]
        Gold,
        
        [Display(Name = "Silver (Non-CXO, <$10M opportunity, Standard amenities)")]
        Silver
    }
}

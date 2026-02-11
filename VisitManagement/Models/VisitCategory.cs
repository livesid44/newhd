using System.ComponentModel.DataAnnotations;

namespace VisitManagement.Models
{
    public enum VisitCategory
    {
        [Display(Name = "Platinum")]
        Platinum,
        
        [Display(Name = "Gold")]
        Gold,
        
        [Display(Name = "Silver")]
        Silver
    }
}

using System.ComponentModel.DataAnnotations;

namespace VisitManagement.Models
{
    public enum EmailTemplateType
    {
        [Display(Name = "Visit Created")]
        VisitCreated,
        
        [Display(Name = "Visit Updated")]
        VisitUpdated,
        
        [Display(Name = "Visit Confirmed")]
        VisitConfirmed,
        
        [Display(Name = "Visit Reminder")]
        VisitReminder,
        
        [Display(Name = "Visit Completed")]
        VisitCompleted,
        
        [Display(Name = "Task Assigned")]
        TaskAssigned,
        
        [Display(Name = "Task Due Soon")]
        TaskDueSoon
    }
}

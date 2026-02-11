using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VisitManagement.Models
{
    public class TaskAssignment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Visit ID")]
        public int VisitId { get; set; }

        [ForeignKey("VisitId")]
        public Visit? Visit { get; set; }

        [Required]
        [Display(Name = "Task Name")]
        [MaxLength(500)]
        public string TaskName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Assigned To Team")]
        [MaxLength(200)]
        public string AssignedToTeam { get; set; } = string.Empty;

        [Display(Name = "Assigned To Person")]
        [MaxLength(200)]
        public string? AssignedToPerson { get; set; }

        [Required]
        [Display(Name = "Due Date")]
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }

        [Required]
        [Display(Name = "Priority")]
        public TaskPriority Priority { get; set; }

        [Required]
        [Display(Name = "Status")]
        public TaskAssignmentStatus Status { get; set; }

        [Display(Name = "Description")]
        [MaxLength(1000)]
        public string? Description { get; set; }

        [Display(Name = "Completion Notes")]
        [MaxLength(1000)]
        public string? CompletionNotes { get; set; }

        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Display(Name = "Modified Date")]
        public DateTime? ModifiedDate { get; set; }

        [Display(Name = "Completed Date")]
        public DateTime? CompletedDate { get; set; }

        [Required]
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; } = string.Empty;
    }

    public enum TaskPriority
    {
        Low,
        Medium,
        High,
        Critical
    }

    public enum TaskAssignmentStatus
    {
        NotStarted,
        InProgress,
        Completed,
        Blocked,
        Cancelled
    }
}

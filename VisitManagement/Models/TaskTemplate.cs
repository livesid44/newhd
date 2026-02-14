using System.ComponentModel.DataAnnotations;

namespace VisitManagement.Models
{
    public class TaskTemplate
    {
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Name { get; set; } = string.Empty;

        [StringLength(2000)]
        public string? Description { get; set; }

        [Required]
        [StringLength(100)]
        public string Category { get; set; } = string.Empty; // Platinum CS, Gold CS, Silver CS, Marketing, TIM

        [StringLength(100)]
        public string? AssignedToTeam { get; set; }

        public TaskPriority Priority { get; set; } = TaskPriority.Medium;

        public int EstimatedDays { get; set; } = 1;

        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}

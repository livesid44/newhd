using System.ComponentModel.DataAnnotations;

namespace VisitManagement.Models
{
    public class TaskComment
    {
        public int Id { get; set; }

        [Required]
        public int TaskAssignmentId { get; set; }

        public TaskAssignment? TaskAssignment { get; set; }

        [Required]
        [StringLength(2000)]
        public string Comment { get; set; } = string.Empty;

        [StringLength(256)]
        public string CreatedBy { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}

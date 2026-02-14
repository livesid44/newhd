using System.ComponentModel.DataAnnotations;

namespace VisitManagement.Models
{
    public class TaskDocument
    {
        public int Id { get; set; }

        [Required]
        public int TaskAssignmentId { get; set; }

        public TaskAssignment? TaskAssignment { get; set; }

        [Required]
        [StringLength(500)]
        public string FileName { get; set; } = string.Empty;

        [Required]
        [StringLength(1000)]
        public string FilePath { get; set; } = string.Empty;

        public long FileSize { get; set; }

        [StringLength(256)]
        public string UploadedBy { get; set; } = string.Empty;

        public DateTime UploadedDate { get; set; } = DateTime.UtcNow;
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VisitManagement.Models
{
    public class Checklist
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Visit ID")]
        public int VisitId { get; set; }

        [ForeignKey("VisitId")]
        public Visit? Visit { get; set; }

        [Required]
        [Display(Name = "Category")]
        public VisitCategory Category { get; set; }

        [Required]
        [Display(Name = "Checklist Type")]
        [MaxLength(200)]
        public string ChecklistType { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Item Name")]
        [MaxLength(500)]
        public string ItemName { get; set; } = string.Empty;

        [Display(Name = "Description")]
        [MaxLength(1000)]
        public string? Description { get; set; }

        [Required]
        [Display(Name = "Is Completed")]
        public bool IsCompleted { get; set; } = false;

        [Display(Name = "Completed By")]
        [MaxLength(200)]
        public string? CompletedBy { get; set; }

        [Display(Name = "Completed Date")]
        public DateTime? CompletedDate { get; set; }

        [Display(Name = "Remarks")]
        [MaxLength(500)]
        public string? Remarks { get; set; }

        [Required]
        [Display(Name = "Display Order")]
        public int DisplayOrder { get; set; }

        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Display(Name = "Modified Date")]
        public DateTime? ModifiedDate { get; set; }
    }
}

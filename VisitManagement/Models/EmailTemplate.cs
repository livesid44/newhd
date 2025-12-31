using System.ComponentModel.DataAnnotations;

namespace VisitManagement.Models
{
    public class EmailTemplate
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Template Name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Template Type")]
        public string TemplateType { get; set; } = string.Empty; // "VisitCreated", "VisitUpdated"

        [Required]
        [Display(Name = "Subject")]
        public string Subject { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Body")]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; } = string.Empty;

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; } = true;

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
    }
}

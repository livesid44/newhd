using System.ComponentModel.DataAnnotations;

namespace VisitManagement.Models
{
    public class Stakeholder
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        [MaxLength(200)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Email")]
        [MaxLength(200)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Phone Number")]
        [MaxLength(50)]
        public string? PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Team/Department")]
        [MaxLength(200)]
        public string Team { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Role")]
        [MaxLength(200)]
        public string Role { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Location")]
        [MaxLength(200)]
        public string Location { get; set; } = string.Empty;

        [Display(Name = "Site")]
        [MaxLength(200)]
        public string? Site { get; set; }

        [Required]
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; } = true;

        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Display(Name = "Modified Date")]
        public DateTime? ModifiedDate { get; set; }
    }
}

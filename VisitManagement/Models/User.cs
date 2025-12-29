using System.ComponentModel.DataAnnotations;

namespace VisitManagement.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        [MaxLength(200)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(200)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Role { get; set; } = string.Empty;

        [Phone]
        [Display(Name = "Phone Number")]
        [MaxLength(20)]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; } = true;
    }
}

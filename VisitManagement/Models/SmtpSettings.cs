using System.ComponentModel.DataAnnotations;

namespace VisitManagement.Models
{
    public class SmtpSettings
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "SMTP Server")]
        public string Server { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Port")]
        [Range(1, 65535)]
        public int Port { get; set; } = 587;

        [Required]
        [Display(Name = "From Email")]
        [EmailAddress]
        public string FromEmail { get; set; } = string.Empty;

        [Required]
        [Display(Name = "From Name")]
        public string FromName { get; set; } = string.Empty;

        [Display(Name = "Username")]
        public string? Username { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Display(Name = "Default To Recipients")]
        [MaxLength(500)]
        public string? DefaultToRecipients { get; set; }

        [Display(Name = "Default CC Recipients")]
        [MaxLength(500)]
        public string? DefaultCcRecipients { get; set; }

        [Display(Name = "Default BCC Recipients")]
        [MaxLength(500)]
        public string? DefaultBccRecipients { get; set; }

        [Display(Name = "Enable SSL")]
        public bool EnableSsl { get; set; } = true;

        [Display(Name = "Enable Email Notifications")]
        public bool EnableNotifications { get; set; } = true;

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
    }
}

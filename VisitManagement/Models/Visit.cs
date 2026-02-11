using System.ComponentModel.DataAnnotations;

namespace VisitManagement.Models
{
    public class Visit
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Visit Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime VisitDate { get; set; }

        [Required]
        [Display(Name = "Type Of Visit")]
        [MaxLength(200)]
        public string TypeOfVisit { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Opportunity Type")]
        public OpportunityType OpportunityType { get; set; }

        [Required]
        [Display(Name = "Sales Stage")]
        [MaxLength(200)]
        public string SalesStage { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Client Name")]
        [MaxLength(200)]
        public string AccountName { get; set; } = string.Empty;

        [Display(Name = "Visit Category")]
        public VisitCategory? Category { get; set; }

        [MaxLength(200)]
        public string? Geo { get; set; }

        [Required]
        [MaxLength(200)]
        public string Location { get; set; } = string.Empty;

        [Display(Name = "Location CS SPOC")]
        [MaxLength(200)]
        public string? LocationCsSpoc { get; set; }

        [Required]
        [Display(Name = "Sales SPOC")]
        [MaxLength(200)]
        public string SalesSpoc { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string Vertical { get; set; } = string.Empty;

        [Display(Name = "Vertical Head")]
        [MaxLength(200)]
        public string? VerticalHead { get; set; }

        [Display(Name = "Account Owner")]
        [MaxLength(200)]
        public string? AccountOwner { get; set; }

        [MaxLength(200)]
        public string? Horizontal { get; set; }

        [Display(Name = "Horizontal Head")]
        [MaxLength(200)]
        public string? HorizontalHead { get; set; }

        [Display(Name = "Clients Country of Origin")]
        [MaxLength(200)]
        public string? ClientsCountryOfOrigin { get; set; }

        [Required]
        [Display(Name = "Debiting Project ID")]
        [MaxLength(100)]
        public string DebitingProjectId { get; set; } = string.Empty;

        [Required]
        [Display(Name = "TCV MN USD")]
        [Range(0, double.MaxValue)]
        public decimal TcvMnUsd { get; set; }

        [Required]
        [Display(Name = "Name and No. Attendees - Clients End")]
        [MaxLength(500)]
        public string VisitorsName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Duration of Visit (Days/Hours)")]
        [MaxLength(100)]
        public string VisitDuration { get; set; } = string.Empty;

        [Display(Name = "Additional Information")]
        [MaxLength(1000)]
        public string? AdditionalInformation { get; set; }

        [Display(Name = "Repository")]
        [MaxLength(500)]
        public string? Repository { get; set; }

        // Legacy/System fields
        [Required]
        [Display(Name = "Visit Status")]
        public VisitStatus VisitStatus { get; set; }

        [Required]
        [Display(Name = "Visit Type")]
        [MaxLength(200)]
        public string VisitType { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Date of Intimation to Client Visit Team")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime IntimationDate { get; set; }

        [Required]
        [MaxLength(200)]
        public string Site { get; set; } = string.Empty;

        [Required]
        [Display(Name = "No. Attendees from Client's End")]
        [Range(0, int.MaxValue)]
        public int NumberOfAttendees { get; set; }

        [Required]
        [Display(Name = "Level of Visitors")]
        [MaxLength(200)]
        public string LevelOfVisitors { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Remarks { get; set; }

        [Required]
        [Display(Name = "Visit Lead")]
        [MaxLength(200)]
        public string VisitLead { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Key Messages")]
        [MaxLength(1000)]
        public string KeyMessages { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Opportunity Details")]
        [MaxLength(500)]
        public string OpportunityDetails { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Service Scope")]
        [MaxLength(500)]
        public string ServiceScope { get; set; } = string.Empty;

        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Display(Name = "Modified Date")]
        public DateTime? ModifiedDate { get; set; }

        [Required]
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; } = string.Empty;
    }

    public enum OpportunityType
    {
        NN,
        EN
    }

    public enum VisitStatus
    {
        Confirmed,
        Tentative
    }
}

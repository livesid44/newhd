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
        [Display(Name = "Opportunity Type (NN/EN)")]
        public OpportunityType OpportunityType { get; set; }

        [Required]
        [Display(Name = "Sales Stage")]
        [MaxLength(200)]
        public string SalesStage { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Client name")]
        [MaxLength(200)]
        public string AccountName { get; set; } = string.Empty;

        [Display(Name = "Visit Category")]
        public VisitCategory? Category { get; set; }

        [MaxLength(200)]
        public string? Geo { get; set; }

        [Required]
        [MaxLength(200)]
        public string Location { get; set; } = string.Empty;

        [Display(Name = "Location CS spoc")]
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

        [Display(Name = "Account owner")]
        [MaxLength(200)]
        public string? AccountOwner { get; set; }

        [MaxLength(200)]
        public string? Horizontal { get; set; }

        [Display(Name = "Horizontal head")]
        [MaxLength(200)]
        public string? HorizontalHead { get; set; }

        [Display(Name = "Clients Country of origin")]
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

        [Display(Name = "Name and No. Attendees - clients end")]
        [MaxLength(500)]
        public string? NameAndNoOfAttendees { get; set; }

        [Display(Name = "Duration of Visit (Days/Hours)")]
        [MaxLength(100)]
        public string? VisitDuration { get; set; }

        [Display(Name = "Additional Information")]
        [MaxLength(1000)]
        public string? AdditionalInformation { get; set; }

        [MaxLength(500)]
        public string? Repository { get; set; }

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

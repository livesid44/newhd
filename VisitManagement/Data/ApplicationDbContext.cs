using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VisitManagement.Models;

namespace VisitManagement.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Visit> Visits { get; set; }
        public DbSet<User> VisitUsers { get; set; }
        public DbSet<SmtpSettings> SmtpSettings { get; set; }
        public DbSet<EmailTemplate> EmailTemplates { get; set; }
        public DbSet<TaskAssignment> TaskAssignments { get; set; }
        public DbSet<Checklist> Checklists { get; set; }
        public DbSet<Stakeholder> Stakeholders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure decimal precision for TcvMnUsd
            modelBuilder.Entity<Visit>()
                .Property(v => v.TcvMnUsd)
                .HasPrecision(18, 2);

            // Seed data for Users
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FullName = "John Doe",
                    Email = "john.doe@example.com",
                    Role = "Administrator",
                    PhoneNumber = "+1-555-0100",
                    CreatedDate = DateTime.Now,
                    IsActive = true
                },
                new User
                {
                    Id = 2,
                    FullName = "Jane Smith",
                    Email = "jane.smith@example.com",
                    Role = "Sales Manager",
                    PhoneNumber = "+1-555-0101",
                    CreatedDate = DateTime.Now,
                    IsActive = true
                },
                new User
                {
                    Id = 3,
                    FullName = "Bob Johnson",
                    Email = "bob.johnson@example.com",
                    Role = "Team Lead",
                    PhoneNumber = "+1-555-0102",
                    CreatedDate = DateTime.Now,
                    IsActive = true
                }
            );

            // Seed data for Visits
            modelBuilder.Entity<Visit>().HasData(
                new Visit
                {
                    Id = 1,
                    TypeOfVisit = "Client Meeting",
                    Vertical = "Technology",
                    SalesSpoc = "John Doe",
                    AccountName = "Acme Corporation",
                    DebitingProjectId = "PROJ-001",
                    OpportunityDetails = "New cloud infrastructure project",
                    OpportunityType = OpportunityType.NN,
                    ServiceScope = "Cloud migration and consulting",
                    SalesStage = "Proposal",
                    TcvMnUsd = 1.5m,
                    VisitStatus = VisitStatus.Confirmed,
                    VisitType = "On-site",
                    VisitDate = new DateTime(2024, 1, 15),
                    IntimationDate = new DateTime(2024, 1, 8),
                    Location = "New York",
                    Site = "Acme HQ",
                    VisitorsName = "Mark Williams, Sarah Davis",
                    NumberOfAttendees = 5,
                    LevelOfVisitors = "C-Level",
                    VisitDuration = "1 Day",
                    Remarks = "Important strategic meeting",
                    VisitLead = "Capability",
                    KeyMessages = "Demonstrate cloud capabilities and cost savings",
                    CreatedDate = DateTime.Now,
                    CreatedBy = "admin@visitmanagement.com"
                },
                new Visit
                {
                    Id = 2,
                    TypeOfVisit = "Technical Demo",
                    Vertical = "Finance",
                    SalesSpoc = "Jane Smith",
                    AccountName = "Global Bank Ltd",
                    DebitingProjectId = "PROJ-002",
                    OpportunityDetails = "Digital transformation initiative",
                    OpportunityType = OpportunityType.EN,
                    ServiceScope = "Digital banking platform development",
                    SalesStage = "Negotiation",
                    TcvMnUsd = 3.2m,
                    VisitStatus = VisitStatus.Tentative,
                    VisitType = "Virtual",
                    VisitDate = new DateTime(2024, 2, 20),
                    IntimationDate = new DateTime(2024, 2, 10),
                    Location = "London",
                    Site = "Global Bank Office",
                    VisitorsName = "Peter Brown, Lisa Anderson",
                    NumberOfAttendees = 8,
                    LevelOfVisitors = "VP Level",
                    VisitDuration = "4 Hours",
                    Remarks = "Follow-up demo session",
                    VisitLead = "Sales",
                    KeyMessages = "Showcase platform scalability and security features",
                    CreatedDate = DateTime.Now,
                    CreatedBy = "admin@visitmanagement.com"
                }
            );

            // Seed data for Email Templates
            modelBuilder.Entity<EmailTemplate>().HasData(
                new EmailTemplate
                {
                    Id = 1,
                    Name = "Visit Created Notification",
                    TemplateType = "VisitCreated",
                    Subject = "New Visit Created - {AccountName}",
                    Body = @"<html><body>
<h2>New Visit Has Been Created</h2>
<p>A new visit has been scheduled with the following details:</p>
<ul>
<li><strong>Account Name:</strong> {AccountName}</li>
<li><strong>Visit Date:</strong> {VisitDate}</li>
<li><strong>Location:</strong> {Location}</li>
<li><strong>Status:</strong> {VisitStatus}</li>
<li><strong>Sales SPOC:</strong> {SalesSpoc}</li>
<li><strong>Visit Type:</strong> {VisitType}</li>
<li><strong>Visitors:</strong> {VisitorsName}</li>
</ul>
<p>Please review the visit details in the system.</p>
<p>Best regards,<br/>Visit Management System</p>
</body></html>",
                    IsActive = true,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                },
                new EmailTemplate
                {
                    Id = 2,
                    Name = "Visit Updated Notification",
                    TemplateType = "VisitUpdated",
                    Subject = "Visit Updated - {AccountName}",
                    Body = @"<html><body>
<h2>Visit Has Been Updated</h2>
<p>The visit details have been modified:</p>
<ul>
<li><strong>Account Name:</strong> {AccountName}</li>
<li><strong>Visit Date:</strong> {VisitDate}</li>
<li><strong>Location:</strong> {Location}</li>
<li><strong>Status:</strong> {VisitStatus}</li>
<li><strong>Sales SPOC:</strong> {SalesSpoc}</li>
<li><strong>Visit Type:</strong> {VisitType}</li>
<li><strong>Visitors:</strong> {VisitorsName}</li>
</ul>
<p>Please check the updated information in the system.</p>
<p>Best regards,<br/>Visit Management System</p>
</body></html>",
                    IsActive = true,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                }
            );

            // Seed data for Stakeholders
            modelBuilder.Entity<Stakeholder>().HasData(
                new Stakeholder
                {
                    Id = 1,
                    FullName = "Client Experience Team Lead",
                    Email = "ce.lead@techmahindra.com",
                    PhoneNumber = "+1-555-0200",
                    Team = "Client Experience",
                    Role = "Team Lead",
                    Location = "Global",
                    IsActive = true,
                    CreatedDate = DateTime.Now
                },
                new Stakeholder
                {
                    Id = 2,
                    FullName = "Marketing Manager",
                    Email = "marketing.manager@techmahindra.com",
                    PhoneNumber = "+1-555-0201",
                    Team = "Marketing",
                    Role = "Manager",
                    Location = "Global",
                    IsActive = true,
                    CreatedDate = DateTime.Now
                },
                new Stakeholder
                {
                    Id = 3,
                    FullName = "Creative Hub Lead",
                    Email = "creative.lead@techmahindra.com",
                    PhoneNumber = "+1-555-0202",
                    Team = "Creative Hub",
                    Role = "Lead",
                    Location = "Global",
                    IsActive = true,
                    CreatedDate = DateTime.Now
                },
                new Stakeholder
                {
                    Id = 4,
                    FullName = "Customer Success Manager",
                    Email = "cs.manager@techmahindra.com",
                    PhoneNumber = "+1-555-0203",
                    Team = "Customer Success",
                    Role = "Manager",
                    Location = "Global",
                    IsActive = true,
                    CreatedDate = DateTime.Now
                },
                new Stakeholder
                {
                    Id = 5,
                    FullName = "TIM Coordinator",
                    Email = "tim.coordinator@techmahindra.com",
                    PhoneNumber = "+1-555-0204",
                    Team = "TIM",
                    Role = "Coordinator",
                    Location = "Global",
                    IsActive = true,
                    CreatedDate = DateTime.Now
                },
                // Location CS SPOC stakeholders
                new Stakeholder
                {
                    Id = 6,
                    FullName = "Rajesh Kumar - Pune CS SPOC",
                    Email = "rajesh.kumar@techmahindra.com",
                    PhoneNumber = "+91-98765-43210",
                    Team = "Customer Success",
                    Role = "Location CS SPOC",
                    Location = "Pune",
                    IsActive = true,
                    CreatedDate = DateTime.Now
                },
                new Stakeholder
                {
                    Id = 7,
                    FullName = "Priya Sharma - Mumbai CS SPOC",
                    Email = "priya.sharma@techmahindra.com",
                    PhoneNumber = "+91-98765-43211",
                    Team = "Customer Success",
                    Role = "Location CS SPOC",
                    Location = "Mumbai",
                    IsActive = true,
                    CreatedDate = DateTime.Now
                },
                new Stakeholder
                {
                    Id = 8,
                    FullName = "Amit Patel - Bangalore CS SPOC",
                    Email = "amit.patel@techmahindra.com",
                    PhoneNumber = "+91-98765-43212",
                    Team = "Customer Success",
                    Role = "Location CS SPOC",
                    Location = "Bangalore",
                    IsActive = true,
                    CreatedDate = DateTime.Now
                },
                // Sales SPOC stakeholders
                new Stakeholder
                {
                    Id = 9,
                    FullName = "John Anderson - Enterprise Sales",
                    Email = "john.anderson@techmahindra.com",
                    PhoneNumber = "+1-555-1001",
                    Team = "Sales",
                    Role = "Sales SPOC",
                    Location = "North America",
                    IsActive = true,
                    CreatedDate = DateTime.Now
                },
                new Stakeholder
                {
                    Id = 10,
                    FullName = "Sarah Williams - BFSI Sales",
                    Email = "sarah.williams@techmahindra.com",
                    PhoneNumber = "+1-555-1002",
                    Team = "Sales",
                    Role = "Sales SPOC",
                    Location = "Global",
                    IsActive = true,
                    CreatedDate = DateTime.Now
                },
                new Stakeholder
                {
                    Id = 11,
                    FullName = "Michael Chen - APAC Sales",
                    Email = "michael.chen@techmahindra.com",
                    PhoneNumber = "+65-9876-5432",
                    Team = "Sales",
                    Role = "Sales SPOC",
                    Location = "Singapore",
                    IsActive = true,
                    CreatedDate = DateTime.Now
                },
                // Vertical Head stakeholders
                new Stakeholder
                {
                    Id = 12,
                    FullName = "David Thompson - Technology Vertical Head",
                    Email = "david.thompson@techmahindra.com",
                    PhoneNumber = "+1-555-2001",
                    Team = "Vertical Leadership",
                    Role = "Vertical Head",
                    Location = "Global",
                    IsActive = true,
                    CreatedDate = DateTime.Now
                },
                new Stakeholder
                {
                    Id = 13,
                    FullName = "Jennifer Lee - Finance Vertical Head",
                    Email = "jennifer.lee@techmahindra.com",
                    PhoneNumber = "+1-555-2002",
                    Team = "Vertical Leadership",
                    Role = "Vertical Head",
                    Location = "Global",
                    IsActive = true,
                    CreatedDate = DateTime.Now
                },
                new Stakeholder
                {
                    Id = 14,
                    FullName = "Robert Martinez - Healthcare Vertical Head",
                    Email = "robert.martinez@techmahindra.com",
                    PhoneNumber = "+1-555-2003",
                    Team = "Vertical Leadership",
                    Role = "Vertical Head",
                    Location = "Global",
                    IsActive = true,
                    CreatedDate = DateTime.Now
                },
                // Account Owner stakeholders
                new Stakeholder
                {
                    Id = 15,
                    FullName = "Lisa Johnson - Strategic Accounts",
                    Email = "lisa.johnson@techmahindra.com",
                    PhoneNumber = "+1-555-3001",
                    Team = "Account Management",
                    Role = "Account Owner",
                    Location = "North America",
                    IsActive = true,
                    CreatedDate = DateTime.Now
                },
                new Stakeholder
                {
                    Id = 16,
                    FullName = "Mark Davis - Enterprise Accounts",
                    Email = "mark.davis@techmahindra.com",
                    PhoneNumber = "+44-20-7123-4567",
                    Team = "Account Management",
                    Role = "Account Owner",
                    Location = "London",
                    IsActive = true,
                    CreatedDate = DateTime.Now
                },
                new Stakeholder
                {
                    Id = 17,
                    FullName = "Anita Desai - Key Accounts",
                    Email = "anita.desai@techmahindra.com",
                    PhoneNumber = "+91-98765-43220",
                    Team = "Account Management",
                    Role = "Account Owner",
                    Location = "India",
                    IsActive = true,
                    CreatedDate = DateTime.Now
                }
            );
        }
    }
}

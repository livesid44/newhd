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
        public DbSet<TaskTemplate> TaskTemplates { get; set; }
        public DbSet<TaskComment> TaskComments { get; set; }
        public DbSet<TaskDocument> TaskDocuments { get; set; }

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

            // Seed data for Email Templates
            modelBuilder.Entity<EmailTemplate>().HasData(
                new EmailTemplate
                {
                    Id = 1,
                    Name = "Visit Created Notification",
                    TemplateType = "VisitCreated",
                    Subject = "New Visit Scheduled - {AccountName}",
                    Body = "<div style=\"font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 20px; background-color: #f5f5f5;\"><div style=\"background-color: #e31837; padding: 20px; text-align: center;\"><h1 style=\"color: white; margin: 0;\">Tech Mahindra</h1><h2 style=\"color: white; margin: 10px 0 0 0; font-size: 18px;\">Visit Management System</h2></div><div style=\"background-color: white; padding: 30px; margin-top: 0;\"><h2 style=\"color: #e31837; border-bottom: 2px solid #e31837; padding-bottom: 10px;\">New Visit Scheduled</h2><p style=\"font-size: 16px; line-height: 1.6;\">A new visit has been created in the system.</p><table style=\"width: 100%; border-collapse: collapse; margin: 20px 0;\"><tr><td style=\"padding: 10px; border-bottom: 1px solid #ddd; font-weight: bold; width: 40%;\">Client Name:</td><td style=\"padding: 10px; border-bottom: 1px solid #ddd;\">{AccountName}</td></tr><tr><td style=\"padding: 10px; border-bottom: 1px solid #ddd; font-weight: bold;\">Visit Date:</td><td style=\"padding: 10px; border-bottom: 1px solid #ddd;\">{VisitDate}</td></tr><tr><td style=\"padding: 10px; border-bottom: 1px solid #ddd; font-weight: bold;\">Location:</td><td style=\"padding: 10px; border-bottom: 1px solid #ddd;\">{Location}</td></tr><tr><td style=\"padding: 10px; border-bottom: 1px solid #ddd; font-weight: bold;\">Visit Category:</td><td style=\"padding: 10px; border-bottom: 1px solid #ddd;\">{Category}</td></tr><tr><td style=\"padding: 10px; border-bottom: 1px solid #ddd; font-weight: bold;\">Sales SPOC:</td><td style=\"padding: 10px; border-bottom: 1px solid #ddd;\">{SalesSpoc}</td></tr><tr><td style=\"padding: 10px; border-bottom: 1px solid #ddd; font-weight: bold;\">Opportunity Type:</td><td style=\"padding: 10px; border-bottom: 1px solid #ddd;\">{OpportunityType}</td></tr><tr><td style=\"padding: 10px; border-bottom: 1px solid #ddd; font-weight: bold;\">Expected Attendees:</td><td style=\"padding: 10px; border-bottom: 1px solid #ddd;\">{NameAndAttendees}</td></tr></table><div style=\"background-color: #f8f9fa; padding: 15px; border-left: 4px solid #e31837; margin: 20px 0;\"><p style=\"margin: 0; font-size: 14px;\"><strong>Next Steps:</strong></p><ul style=\"margin: 10px 0 0 0; padding-left: 20px;\"><li>Review visit details in the system</li><li>Coordinate with relevant teams</li><li>Prepare necessary materials and presentations</li></ul></div><p style=\"margin-top: 30px; font-size: 14px; color: #666;\">To view full details and manage this visit, please log in to the Visit Management System.</p></div><div style=\"background-color: #333; color: white; padding: 20px; text-align: center; margin-top: 0;\"><p style=\"margin: 0; font-size: 12px;\">&copy; 2024 Tech Mahindra. All rights reserved.</p><p style=\"margin: 10px 0 0 0; font-size: 11px;\">This is an automated notification. Please do not reply to this email.</p></div></div>",
                    IsActive = true,
                    CreatedDate = DateTime.Now
                },
                new EmailTemplate
                {
                    Id = 2,
                    Name = "Visit Updated Notification",
                    TemplateType = "VisitUpdated",
                    Subject = "Visit Details Updated - {AccountName}",
                    Body = "<div style=\"font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 20px; background-color: #f5f5f5;\"><div style=\"background-color: #ffc107; padding: 20px; text-align: center;\"><h1 style=\"color: #333; margin: 0;\">Tech Mahindra</h1><h2 style=\"color: #333; margin: 10px 0 0 0; font-size: 18px;\">Visit Management System</h2></div><div style=\"background-color: white; padding: 30px; margin-top: 0;\"><h2 style=\"color: #ffc107; border-bottom: 2px solid #ffc107; padding-bottom: 10px;\">Visit Details Updated</h2><p style=\"font-size: 16px; line-height: 1.6;\">A visit has been updated with new information.</p><table style=\"width: 100%; border-collapse: collapse; margin: 20px 0;\"><tr><td style=\"padding: 10px; border-bottom: 1px solid #ddd; font-weight: bold; width: 40%;\">Client Name:</td><td style=\"padding: 10px; border-bottom: 1px solid #ddd;\">{AccountName}</td></tr><tr><td style=\"padding: 10px; border-bottom: 1px solid #ddd; font-weight: bold;\">Visit Date:</td><td style=\"padding: 10px; border-bottom: 1px solid #ddd;\">{VisitDate}</td></tr><tr><td style=\"padding: 10px; border-bottom: 1px solid #ddd; font-weight: bold;\">Location:</td><td style=\"padding: 10px; border-bottom: 1px solid #ddd;\">{Location}</td></tr><tr><td style=\"padding: 10px; border-bottom: 1px solid #ddd; font-weight: bold;\">Visit Category:</td><td style=\"padding: 10px; border-bottom: 1px solid #ddd;\">{Category}</td></tr><tr><td style=\"padding: 10px; border-bottom: 1px solid #ddd; font-weight: bold;\">Sales SPOC:</td><td style=\"padding: 10px; border-bottom: 1px solid #ddd;\">{SalesSpoc}</td></tr></table><div style=\"background-color: #fff3cd; padding: 15px; border-left: 4px solid #ffc107; margin: 20px 0;\"><p style=\"margin: 0; font-size: 14px;\"><strong>Action Required:</strong></p><p style=\"margin: 10px 0 0 0;\">Please review the updated details and adjust your preparations accordingly.</p></div><p style=\"margin-top: 30px; font-size: 14px; color: #666;\">Log in to the Visit Management System to see all changes.</p></div><div style=\"background-color: #333; color: white; padding: 20px; text-align: center; margin-top: 0;\"><p style=\"margin: 0; font-size: 12px;\">&copy; 2024 Tech Mahindra. All rights reserved.</p><p style=\"margin: 10px 0 0 0; font-size: 11px;\">This is an automated notification. Please do not reply to this email.</p></div></div>",
                    IsActive = true,
                    CreatedDate = DateTime.Now
                },
                new EmailTemplate
                {
                    Id = 3,
                    Name = "Visit Confirmed Notification",
                    TemplateType = "VisitConfirmed",
                    Subject = "Visit Confirmed - {AccountName}",
                    Body = "<div style=\"font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 20px; background-color: #f5f5f5;\"><div style=\"background-color: #28a745; padding: 20px; text-align: center;\"><h1 style=\"color: white; margin: 0;\">Tech Mahindra</h1><h2 style=\"color: white; margin: 10px 0 0 0; font-size: 18px;\">Visit Management System</h2></div><div style=\"background-color: white; padding: 30px; margin-top: 0;\"><h2 style=\"color: #28a745; border-bottom: 2px solid #28a745; padding-bottom: 10px;\">✓ Visit Confirmed</h2><p style=\"font-size: 16px; line-height: 1.6;\">Great news! The following visit has been confirmed.</p><table style=\"width: 100%; border-collapse: collapse; margin: 20px 0;\"><tr><td style=\"padding: 10px; border-bottom: 1px solid #ddd; font-weight: bold; width: 40%;\">Client Name:</td><td style=\"padding: 10px; border-bottom: 1px solid #ddd;\">{AccountName}</td></tr><tr><td style=\"padding: 10px; border-bottom: 1px solid #ddd; font-weight: bold;\">Visit Date:</td><td style=\"padding: 10px; border-bottom: 1px solid #ddd;\">{VisitDate}</td></tr><tr><td style=\"padding: 10px; border-bottom: 1px solid #ddd; font-weight: bold;\">Location:</td><td style=\"padding: 10px; border-bottom: 1px solid #ddd;\">{Location}</td></tr><tr><td style=\"padding: 10px; border-bottom: 1px solid #ddd; font-weight: bold;\">Visit Category:</td><td style=\"padding: 10px; border-bottom: 1px solid #ddd;\">{Category}</td></tr><tr><td style=\"padding: 10px; border-bottom: 1px solid #ddd; font-weight: bold;\">Expected Attendees:</td><td style=\"padding: 10px; border-bottom: 1px solid #ddd;\">{NameAndAttendees}</td></tr></table><div style=\"background-color: #d4edda; padding: 15px; border-left: 4px solid #28a745; margin: 20px 0;\"><p style=\"margin: 0; font-size: 14px;\"><strong>Preparation Checklist:</strong></p><ul style=\"margin: 10px 0 0 0; padding-left: 20px;\"><li>Venue arrangements confirmed</li><li>Agenda finalized and shared</li><li>Presentations prepared</li><li>Team members briefed</li><li>Materials and gifts ready</li></ul></div><p style=\"margin-top: 30px; font-size: 14px; color: #666;\">All stakeholders have been notified. Please complete the checklist in the system.</p></div><div style=\"background-color: #333; color: white; padding: 20px; text-align: center; margin-top: 0;\"><p style=\"margin: 0; font-size: 12px;\">&copy; 2024 Tech Mahindra. All rights reserved.</p><p style=\"margin: 10px 0 0 0; font-size: 11px;\">This is an automated notification. Please do not reply to this email.</p></div></div>",
                    IsActive = true,
                    CreatedDate = DateTime.Now
                },
                new EmailTemplate
                {
                    Id = 4,
                    Name = "Visit Reminder",
                    TemplateType = "VisitReminder",
                    Subject = "Reminder: Visit Tomorrow - {AccountName}",
                    Body = "<div style=\"font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 20px; background-color: #f5f5f5;\"><div style=\"background-color: #17a2b8; padding: 20px; text-align: center;\"><h1 style=\"color: white; margin: 0;\">Tech Mahindra</h1><h2 style=\"color: white; margin: 10px 0 0 0; font-size: 18px;\">Visit Management System</h2></div><div style=\"background-color: white; padding: 30px; margin-top: 0;\"><h2 style=\"color: #17a2b8; border-bottom: 2px solid #17a2b8; padding-bottom: 10px;\">⏰ Visit Reminder - Tomorrow</h2><p style=\"font-size: 16px; line-height: 1.6;\">This is a reminder that you have an upcoming visit scheduled for tomorrow.</p><table style=\"width: 100%; border-collapse: collapse; margin: 20px 0;\"><tr><td style=\"padding: 10px; border-bottom: 1px solid #ddd; font-weight: bold; width: 40%;\">Client Name:</td><td style=\"padding: 10px; border-bottom: 1px solid #ddd;\">{AccountName}</td></tr><tr><td style=\"padding: 10px; border-bottom: 1px solid #ddd; font-weight: bold;\">Visit Date:</td><td style=\"padding: 10px; border-bottom: 1px solid #ddd;\">{VisitDate}</td></tr><tr><td style=\"padding: 10px; border-bottom: 1px solid #ddd; font-weight: bold;\">Location:</td><td style=\"padding: 10px; border-bottom: 1px solid #ddd;\">{Location}</td></tr><tr><td style=\"padding: 10px; border-bottom: 1px solid #ddd; font-weight: bold;\">Visit Category:</td><td style=\"padding: 10px; border-bottom: 1px solid #ddd;\">{Category}</td></tr><tr><td style=\"padding: 10px; border-bottom: 1px solid #ddd; font-weight: bold;\">Sales SPOC:</td><td style=\"padding: 10px; border-bottom: 1px solid #ddd;\">{SalesSpoc}</td></tr></table><div style=\"background-color: #d1ecf1; padding: 15px; border-left: 4px solid #17a2b8; margin: 20px 0;\"><p style=\"margin: 0; font-size: 14px;\"><strong>Last Minute Checklist:</strong></p><ul style=\"margin: 10px 0 0 0; padding-left: 20px;\"><li>Verify all arrangements one final time</li><li>Confirm attendee list</li><li>Review presentation materials</li><li>Check venue readiness</li><li>Prepare backup plans</li></ul></div><p style=\"margin-top: 30px; font-size: 14px; color: #666;\">Log in to the system to review all checklist items and update status.</p></div><div style=\"background-color: #333; color: white; padding: 20px; text-align: center; margin-top: 0;\"><p style=\"margin: 0; font-size: 12px;\">&copy; 2024 Tech Mahindra. All rights reserved.</p><p style=\"margin: 10px 0 0 0; font-size: 11px;\">This is an automated notification. Please do not reply to this email.</p></div></div>",
                    IsActive = true,
                    CreatedDate = DateTime.Now
                },
                new EmailTemplate
                {
                    Id = 5,
                    Name = "Visit Completed Notification",
                    TemplateType = "VisitCompleted",
                    Subject = "Visit Completed - Action Required - {AccountName}",
                    Body = "<div style=\"font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 20px; background-color: #f5f5f5;\"><div style=\"background-color: #6c757d; padding: 20px; text-align: center;\"><h1 style=\"color: white; margin: 0;\">Tech Mahindra</h1><h2 style=\"color: white; margin: 10px 0 0 0; font-size: 18px;\">Visit Management System</h2></div><div style=\"background-color: white; padding: 30px; margin-top: 0;\"><h2 style=\"color: #6c757d; border-bottom: 2px solid #6c757d; padding-bottom: 10px;\">Visit Completed - Action Required</h2><p style=\"font-size: 16px; line-height: 1.6;\">The following visit has been marked as completed. Please complete the post-visit activities.</p><table style=\"width: 100%; border-collapse: collapse; margin: 20px 0;\"><tr><td style=\"padding: 10px; border-bottom: 1px solid #ddd; font-weight: bold; width: 40%;\">Client Name:</td><td style=\"padding: 10px; border-bottom: 1px solid #ddd;\">{AccountName}</td></tr><tr><td style=\"padding: 10px; border-bottom: 1px solid #ddd; font-weight: bold;\">Visit Date:</td><td style=\"padding: 10px; border-bottom: 1px solid #ddd;\">{VisitDate}</td></tr><tr><td style=\"padding: 10px; border-bottom: 1px solid #ddd; font-weight: bold;\">Location:</td><td style=\"padding: 10px; border-bottom: 1px solid #ddd;\">{Location}</td></tr><tr><td style=\"padding: 10px; border-bottom: 1px solid #ddd; font-weight: bold;\">Visit Category:</td><td style=\"padding: 10px; border-bottom: 1px solid #ddd;\">{Category}</td></tr></table><div style=\"background-color: #e2e3e5; padding: 15px; border-left: 4px solid #6c757d; margin: 20px 0;\"><p style=\"margin: 0; font-size: 14px;\"><strong>Post-Visit Activities:</strong></p><ul style=\"margin: 10px 0 0 0; padding-left: 20px;\"><li>Send thank you emails to attendees</li><li>Share presentation materials and recordings</li><li>Upload visit photos and documents to repository</li><li>Collect and document feedback</li><li>Update CRM with visit outcomes</li><li>Schedule follow-up meetings if needed</li></ul></div><p style=\"margin-top: 30px; font-size: 14px; color: #666;\">Please complete all post-visit checklist items in the system within 48 hours.</p></div><div style=\"background-color: #333; color: white; padding: 20px; text-align: center; margin-top: 0;\"><p style=\"margin: 0; font-size: 12px;\">&copy; 2024 Tech Mahindra. All rights reserved.</p><p style=\"margin: 10px 0 0 0; font-size: 11px;\">This is an automated notification. Please do not reply to this email.</p></div></div>",
                    IsActive = true,
                    CreatedDate = DateTime.Now
                },
                new EmailTemplate
                {
                    Id = 6,
                    Name = "Task Assigned Notification",
                    TemplateType = "TaskAssigned",
                    Subject = "New Task Assigned - {TaskName}",
                    Body = "<div style=\"font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 20px; background-color: #f5f5f5;\"><div style=\"background-color: #e31837; padding: 20px; text-align: center;\"><h1 style=\"color: white; margin: 0;\">Tech Mahindra</h1><h2 style=\"color: white; margin: 10px 0 0 0; font-size: 18px;\">Visit Management System</h2></div><div style=\"background-color: white; padding: 30px; margin-top: 0;\"><h2 style=\"color: #e31837; border-bottom: 2px solid #e31837; padding-bottom: 10px;\">New Task Assigned</h2><p style=\"font-size: 16px; line-height: 1.6;\">A new task has been assigned to your team for an upcoming visit.</p><table style=\"width: 100%; border-collapse: collapse; margin: 20px 0;\"><tr><td style=\"padding: 10px; border-bottom: 1px solid #ddd; font-weight: bold; width: 40%;\">Task Name:</td><td style=\"padding: 10px; border-bottom: 1px solid #ddd;\">{TaskName}</td></tr><tr><td style=\"padding: 10px; border-bottom: 1px solid #ddd; font-weight: bold;\">Assigned To:</td><td style=\"padding: 10px; border-bottom: 1px solid #ddd;\">{AssignedTeam}</td></tr><tr><td style=\"padding: 10px; border-bottom: 1px solid #ddd; font-weight: bold;\">Due Date:</td><td style=\"padding: 10px; border-bottom: 1px solid #ddd;\">{DueDate}</td></tr><tr><td style=\"padding: 10px; border-bottom: 1px solid #ddd; font-weight: bold;\">Priority:</td><td style=\"padding: 10px; border-bottom: 1px solid #ddd;\">{Priority}</td></tr><tr><td style=\"padding: 10px; border-bottom: 1px solid #ddd; font-weight: bold;\">Related Visit:</td><td style=\"padding: 10px; border-bottom: 1px solid #ddd;\">{AccountName} - {VisitDate}</td></tr></table><div style=\"background-color: #f8f9fa; padding: 15px; border-left: 4px solid #e31837; margin: 20px 0;\"><p style=\"margin: 0; font-size: 14px;\"><strong>Task Description:</strong></p><p style=\"margin: 10px 0 0 0;\">{TaskDescription}</p></div><p style=\"margin-top: 30px; font-size: 14px; color: #666;\">Log in to the Task Assignments section to view details and update status.</p></div><div style=\"background-color: #333; color: white; padding: 20px; text-align: center; margin-top: 0;\"><p style=\"margin: 0; font-size: 12px;\">&copy; 2024 Tech Mahindra. All rights reserved.</p><p style=\"margin: 10px 0 0 0; font-size: 11px;\">This is an automated notification. Please do not reply to this email.</p></div></div>",
                    IsActive = true,
                    CreatedDate = DateTime.Now
                },
                new EmailTemplate
                {
                    Id = 7,
                    Name = "Task Due Soon Notification",
                    TemplateType = "TaskDueSoon",
                    Subject = "Urgent: Task Due Soon - {TaskName}",
                    Body = "<div style=\"font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 20px; background-color: #f5f5f5;\"><div style=\"background-color: #dc3545; padding: 20px; text-align: center;\"><h1 style=\"color: white; margin: 0;\">Tech Mahindra</h1><h2 style=\"color: white; margin: 10px 0 0 0; font-size: 18px;\">Visit Management System</h2></div><div style=\"background-color: white; padding: 30px; margin-top: 0;\"><h2 style=\"color: #dc3545; border-bottom: 2px solid #dc3545; padding-bottom: 10px;\">⚠️ Task Due Soon - Urgent</h2><p style=\"font-size: 16px; line-height: 1.6;\">The following task is due soon and requires your immediate attention.</p><table style=\"width: 100%; border-collapse: collapse; margin: 20px 0;\"><tr><td style=\"padding: 10px; border-bottom: 1px solid #ddd; font-weight: bold; width: 40%;\">Task Name:</td><td style=\"padding: 10px; border-bottom: 1px solid #ddd;\">{TaskName}</td></tr><tr><td style=\"padding: 10px; border-bottom: 1px solid #ddd; font-weight: bold;\">Assigned To:</td><td style=\"padding: 10px; border-bottom: 1px solid #ddd;\">{AssignedTeam}</td></tr><tr><td style=\"padding: 10px; border-bottom: 1px solid #ddd; font-weight: bold;\">Due Date:</td><td style=\"padding: 10px; border-bottom: 1px solid #ddd; color: #dc3545; font-weight: bold;\">{DueDate}</td></tr><tr><td style=\"padding: 10px; border-bottom: 1px solid #ddd; font-weight: bold;\">Current Status:</td><td style=\"padding: 10px; border-bottom: 1px solid #ddd;\">{TaskStatus}</td></tr><tr><td style=\"padding: 10px; border-bottom: 1px solid #ddd; font-weight: bold;\">Related Visit:</td><td style=\"padding: 10px; border-bottom: 1px solid #ddd;\">{AccountName} - {VisitDate}</td></tr></table><div style=\"background-color: #f8d7da; padding: 15px; border-left: 4px solid #dc3545; margin: 20px 0;\"><p style=\"margin: 0; font-size: 14px;\"><strong>⚠️ Immediate Action Required</strong></p><p style=\"margin: 10px 0 0 0;\">This task is approaching its deadline. Please prioritize completion and update the status in the system.</p></div><p style=\"margin-top: 30px; font-size: 14px; color: #666;\">If you need assistance or extension, please contact your team lead immediately.</p></div><div style=\"background-color: #333; color: white; padding: 20px; text-align: center; margin-top: 0;\"><p style=\"margin: 0; font-size: 12px;\">&copy; 2024 Tech Mahindra. All rights reserved.</p><p style=\"margin: 10px 0 0 0; font-size: 11px;\">This is an automated notification. Please do not reply to this email.</p></div></div>",
                    IsActive = true,
                    CreatedDate = DateTime.Now
                }
            );

            // Seed data for Task Templates
            modelBuilder.Entity<TaskTemplate>().HasData(
                // Platinum CS Checklist
                new TaskTemplate { Id = 1, Name = "Confirm executive accommodation (5-star hotel)", Description = "Book and confirm luxury accommodation for CXO visitors", Category = "Platinum CS", AssignedToTeam = "Client Experience", Priority = TaskPriority.Critical, EstimatedDays = 7, DisplayOrder = 1, IsActive = true, CreatedDate = DateTime.UtcNow },
                new TaskTemplate { Id = 2, Name = "Arrange private dining experience", Description = "Book private dining venue with curated menu", Category = "Platinum CS", AssignedToTeam = "Client Experience", Priority = TaskPriority.High, EstimatedDays = 5, DisplayOrder = 2, IsActive = true, CreatedDate = DateTime.UtcNow },
                new TaskTemplate { Id = 3, Name = "Business/First class travel arrangements", Description = "Book premium air travel for visitors", Category = "Platinum CS", AssignedToTeam = "Client Experience", Priority = TaskPriority.Critical, EstimatedDays = 7, DisplayOrder = 3, IsActive = true, CreatedDate = DateTime.UtcNow },
                new TaskTemplate { Id = 4, Name = "Prepare executive welcome kit", Description = "Curate personalized welcome materials and gifts", Category = "Platinum CS", AssignedToTeam = "Client Experience", Priority = TaskPriority.Medium, EstimatedDays = 5, DisplayOrder = 4, IsActive = true, CreatedDate = DateTime.UtcNow },
                new TaskTemplate { Id = 5, Name = "Senior leadership engagement coordination", Description = "Schedule meetings with C-Suite members", Category = "Platinum CS", AssignedToTeam = "Client Experience", Priority = TaskPriority.Critical, EstimatedDays = 10, DisplayOrder = 5, IsActive = true, CreatedDate = DateTime.UtcNow },
                new TaskTemplate { Id = 6, Name = "Arrange facility tour with senior leaders", Description = "Organize comprehensive facility tour", Category = "Platinum CS", AssignedToTeam = "Operations", Priority = TaskPriority.High, EstimatedDays = 3, DisplayOrder = 6, IsActive = true, CreatedDate = DateTime.UtcNow },
                new TaskTemplate { Id = 7, Name = "Executive presentation materials", Description = "Prepare high-level strategic presentations", Category = "Platinum CS", AssignedToTeam = "Marketing", Priority = TaskPriority.High, EstimatedDays = 5, DisplayOrder = 7, IsActive = true, CreatedDate = DateTime.UtcNow },

                // Gold CS Checklist
                new TaskTemplate { Id = 8, Name = "Confirm premium accommodation (4-star hotel)", Description = "Book quality accommodation for VP/Director visitors", Category = "Gold CS", AssignedToTeam = "Client Experience", Priority = TaskPriority.High, EstimatedDays = 5, DisplayOrder = 1, IsActive = true, CreatedDate = DateTime.UtcNow },
                new TaskTemplate { Id = 9, Name = "Arrange standard dining arrangements", Description = "Book dining venue for team dinner", Category = "Gold CS", AssignedToTeam = "Client Experience", Priority = TaskPriority.Medium, EstimatedDays = 3, DisplayOrder = 2, IsActive = true, CreatedDate = DateTime.UtcNow },
                new TaskTemplate { Id = 10, Name = "Business class travel arrangements", Description = "Book business class tickets", Category = "Gold CS", AssignedToTeam = "Client Experience", Priority = TaskPriority.High, EstimatedDays = 5, DisplayOrder = 3, IsActive = true, CreatedDate = DateTime.UtcNow },
                new TaskTemplate { Id = 11, Name = "Prepare corporate welcome kit", Description = "Prepare standard welcome materials", Category = "Gold CS", AssignedToTeam = "Client Experience", Priority = TaskPriority.Medium, EstimatedDays = 3, DisplayOrder = 4, IsActive = true, CreatedDate = DateTime.UtcNow },
                new TaskTemplate { Id = 12, Name = "Senior management engagement", Description = "Schedule meetings with senior leaders", Category = "Gold CS", AssignedToTeam = "Client Experience", Priority = TaskPriority.High, EstimatedDays = 7, DisplayOrder = 5, IsActive = true, CreatedDate = DateTime.UtcNow },
                new TaskTemplate { Id = 13, Name = "Facility tour arrangement", Description = "Organize standard facility tour", Category = "Gold CS", AssignedToTeam = "Operations", Priority = TaskPriority.Medium, EstimatedDays = 2, DisplayOrder = 6, IsActive = true, CreatedDate = DateTime.UtcNow },

                // Silver CS Checklist
                new TaskTemplate { Id = 14, Name = "Confirm standard accommodation (3-star hotel)", Description = "Book standard accommodation", Category = "Silver CS", AssignedToTeam = "Client Experience", Priority = TaskPriority.Medium, EstimatedDays = 3, DisplayOrder = 1, IsActive = true, CreatedDate = DateTime.UtcNow },
                new TaskTemplate { Id = 15, Name = "Arrange basic refreshments", Description = "Organize refreshments and lunch", Category = "Silver CS", AssignedToTeam = "Client Experience", Priority = TaskPriority.Low, EstimatedDays = 1, DisplayOrder = 2, IsActive = true, CreatedDate = DateTime.UtcNow },
                new TaskTemplate { Id = 16, Name = "Economy travel arrangements", Description = "Book economy class tickets", Category = "Silver CS", AssignedToTeam = "Client Experience", Priority = TaskPriority.Medium, EstimatedDays = 3, DisplayOrder = 3, IsActive = true, CreatedDate = DateTime.UtcNow },
                new TaskTemplate { Id = 17, Name = "Prepare standard welcome materials", Description = "Prepare basic welcome folder", Category = "Silver CS", AssignedToTeam = "Client Experience", Priority = TaskPriority.Low, EstimatedDays = 2, DisplayOrder = 4, IsActive = true, CreatedDate = DateTime.UtcNow },
                new TaskTemplate { Id = 18, Name = "Management engagement", Description = "Schedule meetings with team leads", Category = "Silver CS", AssignedToTeam = "Client Experience", Priority = TaskPriority.Medium, EstimatedDays = 5, DisplayOrder = 5, IsActive = true, CreatedDate = DateTime.UtcNow },

                // Marketing Team Checklist
                new TaskTemplate { Id = 19, Name = "Prepare marketing presentations", Description = "Create comprehensive marketing deck", Category = "Marketing", AssignedToTeam = "Marketing", Priority = TaskPriority.High, EstimatedDays = 5, DisplayOrder = 1, IsActive = true, CreatedDate = DateTime.UtcNow },
                new TaskTemplate { Id = 20, Name = "Compile case studies", Description = "Gather relevant case studies and success stories", Category = "Marketing", AssignedToTeam = "Marketing", Priority = TaskPriority.Medium, EstimatedDays = 3, DisplayOrder = 2, IsActive = true, CreatedDate = DateTime.UtcNow },
                new TaskTemplate { Id = 21, Name = "Prepare company brochures", Description = "Print and prepare branded materials", Category = "Marketing", AssignedToTeam = "Marketing", Priority = TaskPriority.Medium, EstimatedDays = 3, DisplayOrder = 3, IsActive = true, CreatedDate = DateTime.UtcNow },
                new TaskTemplate { Id = 22, Name = "Create video presentations", Description = "Prepare video demos and presentations", Category = "Marketing", AssignedToTeam = "Marketing", Priority = TaskPriority.Medium, EstimatedDays = 5, DisplayOrder = 4, IsActive = true, CreatedDate = DateTime.UtcNow },
                new TaskTemplate { Id = 23, Name = "Arrange photography/videography", Description = "Coordinate photo/video coverage of visit", Category = "Marketing", AssignedToTeam = "Creative Hub", Priority = TaskPriority.Low, EstimatedDays = 2, DisplayOrder = 5, IsActive = true, CreatedDate = DateTime.UtcNow },

                // TIM Team Checklist
                new TaskTemplate { Id = 24, Name = "Setup conference room AV equipment", Description = "Test and configure audio-visual systems", Category = "TIM", AssignedToTeam = "TIM", Priority = TaskPriority.High, EstimatedDays = 1, DisplayOrder = 1, IsActive = true, CreatedDate = DateTime.UtcNow },
                new TaskTemplate { Id = 25, Name = "Prepare demo environments", Description = "Setup technical demo platforms and environments", Category = "TIM", AssignedToTeam = "TIM", Priority = TaskPriority.Critical, EstimatedDays = 5, DisplayOrder = 2, IsActive = true, CreatedDate = DateTime.UtcNow },
                new TaskTemplate { Id = 26, Name = "Ensure network connectivity", Description = "Verify and test network access for visitors", Category = "TIM", AssignedToTeam = "TIM", Priority = TaskPriority.High, EstimatedDays = 2, DisplayOrder = 3, IsActive = true, CreatedDate = DateTime.UtcNow },
                new TaskTemplate { Id = 27, Name = "Configure visitor Wi-Fi access", Description = "Setup secure guest Wi-Fi credentials", Category = "TIM", AssignedToTeam = "TIM", Priority = TaskPriority.Medium, EstimatedDays = 1, DisplayOrder = 4, IsActive = true, CreatedDate = DateTime.UtcNow },
                new TaskTemplate { Id = 28, Name = "Backup technical support", Description = "Arrange on-site technical support during visit", Category = "TIM", AssignedToTeam = "TIM", Priority = TaskPriority.High, EstimatedDays = 1, DisplayOrder = 5, IsActive = true, CreatedDate = DateTime.UtcNow },
                new TaskTemplate { Id = 29, Name = "Test presentation equipment", Description = "Test projectors, screens, and video conferencing", Category = "TIM", AssignedToTeam = "TIM", Priority = TaskPriority.Medium, EstimatedDays = 1, DisplayOrder = 6, IsActive = true, CreatedDate = DateTime.UtcNow }
            );
        }
    }
}

using Microsoft.EntityFrameworkCore;
using VisitManagement.Models;

namespace VisitManagement.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Visit> Visits { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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
                    SerialNumber = 1,
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
                    CreatedDate = DateTime.Now
                },
                new Visit
                {
                    Id = 2,
                    SerialNumber = 2,
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
                    CreatedDate = DateTime.Now
                }
            );
        }
    }
}

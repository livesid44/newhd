using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using VisitManagement.Controllers;
using VisitManagement.Data;
using VisitManagement.Models;
using VisitManagement.Services;
using Xunit;

namespace VisitManagement.Tests
{
    public class VisitsControllerTests
    {
        private ApplicationDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new ApplicationDbContext(options);
            return context;
        }

        private IEmailService GetMockEmailService()
        {
            var mockEmailService = new Mock<IEmailService>();
            
            // Setup SendVisitNotificationAsync to return success
            mockEmailService
                .Setup(x => x.SendVisitNotificationAsync(It.IsAny<Visit>(), It.IsAny<EmailTemplateType>()))
                .ReturnsAsync(true);
            
            return mockEmailService.Object;
        }

        [Fact]
        public async Task Index_ReturnsViewResult_WithListOfVisits()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var emailService = GetMockEmailService();
            var controller = new VisitsController(context, emailService);

            var visit = new Visit
            {
                Id = 1,
                TypeOfVisit = "Test Visit",
                Vertical = "Test Vertical",
                SalesSpoc = "Test SPOC",
                AccountName = "Test Account",
                DebitingProjectId = "PROJ-001",
                OpportunityDetails = "Test Opportunity",
                OpportunityType = OpportunityType.NN,
                ServiceScope = "Test Scope",
                SalesStage = "Test Stage",
                TcvMnUsd = 1.5m,
                VisitStatus = VisitStatus.Confirmed,
                VisitType = "On-site",
                VisitDate = DateTime.Now,
                IntimationDate = DateTime.Now,
                Location = "Test Location",
                Site = "Test Site",
                VisitorsName = "Test Visitor",
                NumberOfAttendees = 5,
                LevelOfVisitors = "C-Level",
                VisitDuration = "1 Day",
                VisitLead = "Capability",
                KeyMessages = "Test Messages"
            };

            context.Visits.Add(visit);
            await context.SaveChangesAsync();

            // Act
            var result = await controller.Index(null, null);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Visit>>(viewResult.Model);
            Assert.Single(model);
        }

        [Fact]
        public async Task Create_Post_ValidModel_RedirectsToIndex()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var emailService = GetMockEmailService();
            var controller = new VisitsController(context, emailService);

            var visit = new Visit
            {
                TypeOfVisit = "Test Visit",
                Vertical = "Test Vertical",
                SalesSpoc = "Test SPOC",
                AccountName = "Test Account",
                DebitingProjectId = "PROJ-001",
                OpportunityDetails = "Test Opportunity",
                OpportunityType = OpportunityType.NN,
                ServiceScope = "Test Scope",
                SalesStage = "Test Stage",
                TcvMnUsd = 1.5m,
                VisitStatus = VisitStatus.Confirmed,
                VisitType = "On-site",
                VisitDate = DateTime.Now,
                IntimationDate = DateTime.Now,
                Location = "Test Location",
                Site = "Test Site",
                VisitorsName = "Test Visitor",
                NumberOfAttendees = 5,
                LevelOfVisitors = "C-Level",
                VisitDuration = "1 Day",
                VisitLead = "Capability",
                KeyMessages = "Test Messages"
            };

            // Act
            var result = await controller.Create(visit);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Single(context.Visits);
        }

        [Fact]
        public async Task Details_ReturnsNotFound_WhenIdIsNull()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var emailService = GetMockEmailService();
            var controller = new VisitsController(context, emailService);

            // Act
            var result = await controller.Details(null);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_ReturnsViewResult_WithVisit()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var emailService = GetMockEmailService();
            var controller = new VisitsController(context, emailService);

            var visit = new Visit
            {
                Id = 1,
                TypeOfVisit = "Test Visit",
                Vertical = "Test Vertical",
                SalesSpoc = "Test SPOC",
                AccountName = "Test Account",
                DebitingProjectId = "PROJ-001",
                OpportunityDetails = "Test Opportunity",
                OpportunityType = OpportunityType.NN,
                ServiceScope = "Test Scope",
                SalesStage = "Test Stage",
                TcvMnUsd = 1.5m,
                VisitStatus = VisitStatus.Confirmed,
                VisitType = "On-site",
                VisitDate = DateTime.Now,
                IntimationDate = DateTime.Now,
                Location = "Test Location",
                Site = "Test Site",
                VisitorsName = "Test Visitor",
                NumberOfAttendees = 5,
                LevelOfVisitors = "C-Level",
                VisitDuration = "1 Day",
                VisitLead = "Capability",
                KeyMessages = "Test Messages"
            };

            context.Visits.Add(visit);
            await context.SaveChangesAsync();

            // Act
            var result = await controller.Details(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Visit>(viewResult.Model);
            Assert.Equal("Test Account", model.AccountName);
        }

        [Fact]
        public async Task Delete_Post_RemovesVisit_AndRedirectsToIndex()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var emailService = GetMockEmailService();
            var controller = new VisitsController(context, emailService);

            var visit = new Visit
            {
                Id = 1,
                TypeOfVisit = "Test Visit",
                Vertical = "Test Vertical",
                SalesSpoc = "Test SPOC",
                AccountName = "Test Account",
                DebitingProjectId = "PROJ-001",
                OpportunityDetails = "Test Opportunity",
                OpportunityType = OpportunityType.NN,
                ServiceScope = "Test Scope",
                SalesStage = "Test Stage",
                TcvMnUsd = 1.5m,
                VisitStatus = VisitStatus.Confirmed,
                VisitType = "On-site",
                VisitDate = DateTime.Now,
                IntimationDate = DateTime.Now,
                Location = "Test Location",
                Site = "Test Site",
                VisitorsName = "Test Visitor",
                NumberOfAttendees = 5,
                LevelOfVisitors = "C-Level",
                VisitDuration = "1 Day",
                VisitLead = "Capability",
                KeyMessages = "Test Messages"
            };

            context.Visits.Add(visit);
            await context.SaveChangesAsync();

            // Act
            var result = await controller.DeleteConfirmed(1);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Empty(context.Visits);
        }
    }
}

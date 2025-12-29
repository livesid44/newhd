using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VisitManagement.Controllers;
using VisitManagement.Data;
using VisitManagement.Models;
using Xunit;

namespace VisitManagement.Tests
{
    public class UsersControllerTests
    {
        private ApplicationDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new ApplicationDbContext(options);
            return context;
        }

        [Fact]
        public async Task Index_ReturnsViewResult_WithListOfUsers()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = new UsersController(context);

            var user = new User
            {
                Id = 1,
                FullName = "Test User",
                Email = "test@example.com",
                Role = "Administrator",
                PhoneNumber = "+1-555-0100",
                IsActive = true
            };

            context.Users.Add(user);
            await context.SaveChangesAsync();

            // Act
            var result = await controller.Index(null);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<User>>(viewResult.Model);
            Assert.Single(model);
        }

        [Fact]
        public async Task Create_Post_ValidModel_RedirectsToIndex()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = new UsersController(context);

            var user = new User
            {
                FullName = "New User",
                Email = "newuser@example.com",
                Role = "Team Lead",
                PhoneNumber = "+1-555-0200",
                IsActive = true
            };

            // Act
            var result = await controller.Create(user);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Single(context.Users);
        }

        [Fact]
        public async Task Details_ReturnsNotFound_WhenIdIsNull()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = new UsersController(context);

            // Act
            var result = await controller.Details(null);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_ReturnsViewResult_WithUser()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = new UsersController(context);

            var user = new User
            {
                Id = 1,
                FullName = "Test User",
                Email = "test@example.com",
                Role = "Administrator",
                PhoneNumber = "+1-555-0100",
                IsActive = true
            };

            context.Users.Add(user);
            await context.SaveChangesAsync();

            // Act
            var result = await controller.Details(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<User>(viewResult.Model);
            Assert.Equal("Test User", model.FullName);
        }

        [Fact]
        public async Task Delete_Post_RemovesUser_AndRedirectsToIndex()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = new UsersController(context);

            var user = new User
            {
                Id = 1,
                FullName = "Test User",
                Email = "test@example.com",
                Role = "Administrator",
                PhoneNumber = "+1-555-0100",
                IsActive = true
            };

            context.Users.Add(user);
            await context.SaveChangesAsync();

            // Act
            var result = await controller.DeleteConfirmed(1);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Empty(context.Users);
        }

        [Fact]
        public async Task Edit_Post_UpdatesUser_AndRedirectsToIndex()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = new UsersController(context);

            var user = new User
            {
                Id = 1,
                FullName = "Test User",
                Email = "test@example.com",
                Role = "Administrator",
                PhoneNumber = "+1-555-0100",
                IsActive = true,
                CreatedDate = DateTime.Now
            };

            context.Users.Add(user);
            await context.SaveChangesAsync();

            // Modify user
            user.FullName = "Updated User";
            user.Role = "Manager";

            // Act
            var result = await controller.Edit(1, user);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);

            var updatedUser = await context.Users.FindAsync(1);
            Assert.Equal("Updated User", updatedUser.FullName);
            Assert.Equal("Manager", updatedUser.Role);
        }
    }
}

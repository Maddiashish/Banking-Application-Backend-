// Test case 1: Customer point exists in database and can be deleted
using Banking1.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Banking1.Data;
using Banking1.Models;

namespace Banking1.Tests
{
    public class CustomerPointTestCases
    {
        private ApplicationDbContext _context;
        private CustomerMastersController _controller;

        public CustomerPointTestCases()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new ApplicationDbContext(options);
            _controller = new CustomerMastersController(_context);
        }
        [Fact]
        public async Task DeleteCustomerPoint_CustomerPointExists_RemovesFromDatabase()
        {
            // Arrange
            int id = 1;
            var dbContext = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "DeleteCustomerPoint_Db")
                .Options;
            using (var context = new ApplicationDbContext(dbContext))
            {
                context.CustomerPoint.Add(new CustomerPoint { Id = id });
                await context.SaveChangesAsync();
            }
            using (var context = new ApplicationDbContext(dbContext))
            {
                var controller = new CustomerPointsController(context);

                // Act
                var result = await controller.DeleteCustomerPoint(id);

                // Assert
                Assert.IsType<NoContentResult>(result);
                Assert.False(context.CustomerPoint.Any(cp => cp.Id == id));
            }
        }

        // Test case 2: Customer point does not exist in database and returns 404 Not Found
        [Fact]
        public async Task DeleteCustomerPoint_CustomerPointDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            int id = 1;
            var dbContext = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "DeleteCustomerPoint_Db")
                .Options;
            using (var context = new ApplicationDbContext(dbContext))
            {
                var controller = new CustomerPointsController(context);

                // Act
                var result = await controller.DeleteCustomerPoint(id);

                // Assert
                Assert.IsType<NotFoundResult>(result);
            }
        }

        // Test case 3: DbContext.CustomerPoint is null and returns NotFound
        [Fact]
        public async Task DeleteCustomerPoint_CustomerPointIsNull_ReturnsNotFound()
        {
            // Arrange
            int id = 1;
            var dbContext = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "DeleteCustomerPoint_Db")
                .Options;
            using (var context = new ApplicationDbContext(dbContext))
            {
                context.CustomerPoint = null;
                var controller = new CustomerPointsController(context);

                // Act
                var result = await controller.DeleteCustomerPoint(id);

                // Assert
                Assert.IsType<NotFoundResult>(result);
            }
        }
    }
}
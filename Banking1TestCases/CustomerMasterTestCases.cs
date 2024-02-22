using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Banking1.Controllers;
using Banking1.Data;
using Banking1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Banking1.Tests
{
    public class CustomerMastersControllerTests 
    {
        private ApplicationDbContext _context;
        private CustomerMastersController _controller;

        public CustomerMastersControllerTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new ApplicationDbContext(options);
            _controller = new CustomerMastersController(_context);
        }

        //[Fact]
        //public async Task GetCustomerMaster_ReturnsNotFound_WhenNoCustomersExist()
        //{
        //    // Arrange
        //    _context.CustomerMaster.RemoveRange(_context.CustomerMaster);
        //    await _context.SaveChangesAsync();

        //    // Act
        //    var result = await _controller.GetCustomerMaster();

        //    // Assert
        //    Assert.IsType<NotFoundResult>(result.Result);
        //}

        //[Fact]
        //public async Task GetCustomerMaster_ReturnsListOfCustomers_WhenCustomersExist()
        //{
        //    // Arrange
        //    _context.CustomerMaster.Add(new CustomerMaster { Id = 1, Name = "John Doe",Email="123a",Age=10,Mobileno=123456789 });
        //    //_context.CustomerMaster.Add(new CustomerMaster { Id = 2, Name = "Jane Doe", });
        //    await _context.SaveChangesAsync();

        //    // Act
        //    var result = await _controller.GetCustomerMaster();

            // Assert
        //    var customers = Assert.IsType<List<CustomerMaster>>(result.Value);
        //    Assert.Equal(2, customers.Count);
        //}

        [Fact]
        public async Task GetCustomerMaster_ReturnsNotFound_WhenCustomerIdDoesNotExist()
        {
            // Arrange
            var id = 1;
            _context.CustomerMaster.RemoveRange(_context.CustomerMaster);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.GetCustomerMaster(id);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        //[Fact]
        //public async Task GetCustomerMaster_ReturnsCustomer_WhenCustomerIdExists()
        //{
        //    // Arrange
        //    var id = 1;
        //    _context.CustomerMaster.Add(new CustomerMaster { Id = id, Name = "John Doe" });
        //    await _context.SaveChangesAsync();

        //    // Act
        //    var result = await _controller.GetCustomerMaster(id);

        //    // Assert
        //    var customer = Assert.IsType<CustomerMaster>(result.Value);
        //    Assert.Equal(id, customer.Id);
        //}

        [Fact]
        public async Task PutCustomerMaster_ReturnsBadRequest_WhenIdsDoNotMatch()
        {
            // Arrange
            var id = 1;
            var customer = new CustomerMaster { Id = 2, Name = "John Doe" };

            // Act
            var result = await _controller.PutCustomerMaster(id, customer);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task PutCustomerMaster_ReturnsNotFound_WhenCustomerIdDoesNotExist()
        {
            // Arrange
            var id = 1;
            var customer = new CustomerMaster { Id = id, Name = "John Doe" };
            _context.CustomerMaster.RemoveRange(_context.CustomerMaster);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.PutCustomerMaster(id, customer);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using CloudCustomers.API.Controllers;
using CloudCustomers.API.Models;
using CloudCustomers.API.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CloudCustomers.UnitTests.Systems.Controllers;

public class TestUsersController
{
    [Fact]
    public async Task Get_OnSuccess_ReturnsStatusCode200()
    {
        // Arrange
        var mockUsersService = new Mock<IUsersService>();
        mockUsersService
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(new List<User>());

        var sut = new UsersController(mockUsersService.Object);
        
        // Act
        var result = (OkObjectResult)await sut.Get();
        
        // Assert
        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task Get_OnSuccess_InvokeUserServiceExactyOnce()
    {
        var mockUsersService = new Mock<IUsersService>();
        
        mockUsersService
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(new List<User>());
        
        var sut = new UsersController(mockUsersService.Object);
        var result = (ObjectResult)await sut.Get();
        mockUsersService.Verify(service => service.GetAllUsers(), Times.Once);
    }
}
using Xunit;
using Moq;
using MicroserviceTester.Areas.UserService.Services;
using MicroserviceTester.Areas.UserService.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using MicroserviceTester.Areas.UserService.Models;
using FluentAssertions;

namespace MicroserviceTester.Tests.ExceptionHandlingTests
{
    public class UsersControllerExceptionTests
    {
        private readonly Mock<IUserService> _mockUserService;
        private readonly Mock<ILogger<UsersController>> _mockLogger;
        private readonly UsersController _controller;

        public UsersControllerExceptionTests()
        {
            _mockUserService = new Mock<IUserService>();
            _mockLogger = new Mock<ILogger<UsersController>>();
            _controller = new UsersController(_mockUserService.Object, _mockLogger.Object);
        }

        [Fact]
        public void GetUser_When_Service_Throws_Exception_Should_Return_Status500()
        {
            // Arrange
            int userId = 1;
            _mockUserService.Setup(s => s.GetUser(userId)).Throws(new System.Exception("Database error"));

            // Act
            var result = _controller.GetUser(userId) as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(500);
            result.Value.Should().Be("An unexpected error occurred.");
        }
    }
}

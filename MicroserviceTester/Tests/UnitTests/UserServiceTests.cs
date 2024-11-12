using MicroserviceTester.Areas.UserService.Models;
using MicroserviceTester.Areas.UserService.Services;
using Xunit;
namespace MicroserviceTester.Tests.UnitTests
{
    public class UserServiceTests
    {
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _userService = new UserService();
        }

        [Fact]
        public void AddUser_Should_Add_User()
        {
            // Arrange
            var user = new User { Id = 1, Username = "TestUser" };

            // Act
            _userService.AddUser(user);

            // Assert
            var result = _userService.GetUser(1);
            Assert.NotNull(result);
            Assert.Equal("TestUser", result.Username);
        }

        [Fact]
        public void GetUser_Should_Return_Null_If_User_Does_Not_Exist()
        {
            // Act
            var result = _userService.GetUser(999);

            // Assert
            Assert.Null(result);
        }
    }
}

using MicroserviceTester.Areas.UserService.Services;
using Xunit;

namespace MicroserviceTester.Tests.ContractTests
{
    public class UserServiceContractTests
    {
        [Fact]
        public void UserService_Should_Implement_IUserService()
        {
            // Arrange
            var userService = new UserService();

            // Act & Assert
            Assert.IsAssignableFrom<IUserService>(userService);
        }
    }
}

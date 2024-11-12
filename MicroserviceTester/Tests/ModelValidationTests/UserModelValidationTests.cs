using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using MicroserviceTester.Areas.UserService.Models;

namespace MicroserviceTester.Tests.ModelValidationTests
{
    public class UserModelValidationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public UserModelValidationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CreateUser_With_Missing_Username_Should_Return_BadRequest()
        {
            // Arrange
            var user = new User { Id = 700 };
            var userJson = JsonConvert.SerializeObject(user);
            var userContent = new StringContent(userJson, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/Users", userContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var errorMessage = await response.Content.ReadAsStringAsync();
            errorMessage.Should().Contain("The Username field is required.");
        }

        [Fact]
        public async Task CreateUser_With_Duplicate_Id_Should_Return_Conflict()
        {
            // Arrange
            var user1 = new User { Id = 800, Username = "DuplicateUser" };
            var user2 = new User { Id = 800, Username = "AnotherUser" };
            var userJson1 = JsonConvert.SerializeObject(user1);
            var userContent1 = new StringContent(userJson1, Encoding.UTF8, "application/json");
            var userJson2 = JsonConvert.SerializeObject(user2);
            var userContent2 = new StringContent(userJson2, Encoding.UTF8, "application/json");

            // Act
            var response1 = await _client.PostAsync("/api/Users", userContent1);
            var response2 = await _client.PostAsync("/api/Users", userContent2);

            // Assert
            response1.StatusCode.Should().Be(HttpStatusCode.Created);
            response2.StatusCode.Should().Be(HttpStatusCode.Conflict);
            var errorMessage = await response2.Content.ReadAsStringAsync();
            errorMessage.Should().Contain("User with this ID already exists.");
        }
    }
}

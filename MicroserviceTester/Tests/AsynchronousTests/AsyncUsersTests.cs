using Xunit;
using MicroserviceTester.Areas.UserService.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace MicroserviceTester.Tests.AsynchronousTests
{
    public class AsyncUsersTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public AsyncUsersTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CreateUser_Async_Should_Return_Created()
        {
            // Arrange
            var user = new User { Id = 600, Username = "AsyncUser" };
            var userJson = JsonConvert.SerializeObject(user);
            var userContent = new StringContent(userJson, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/Users", userContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task DeleteUser_Async_Should_Return_NoContent()
        {
            // Arrange
            var user = new User { Id = 601, Username = "AsyncDeleteUser" };
            var userJson = JsonConvert.SerializeObject(user);
            var userContent = new StringContent(userJson, Encoding.UTF8, "application/json");
            await _client.PostAsync("/api/Users", userContent);

            // Act
            var deleteResponse = await _client.DeleteAsync($"/api/Users/{user.Id}");

            // Assert
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}

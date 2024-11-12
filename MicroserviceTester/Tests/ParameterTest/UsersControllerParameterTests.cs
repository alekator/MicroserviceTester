using Xunit;
using MicroserviceTester.Areas.UserService.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace MicroserviceTester.Tests.ParameterTest
{
    public class UsersControllerParameterTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public UsersControllerParameterTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Theory]
        [InlineData(0, "ValidUser")]
        [InlineData(-1, "NegativeIDUser")]
        [InlineData(1000, "LargeIDUser")]
        public async Task CreateUser_With_Various_IDs_Should_Handle_Correctly(int id, string username)
        {
            // Arrange
            var user = new User { Id = id, Username = username };
            var userJson = JsonConvert.SerializeObject(user);
            var userContent = new StringContent(userJson, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/Users", userContent);

            // Assert
            if (id <= 0)
            {
                response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            }
            else
            {
                response.StatusCode.Should().Be(HttpStatusCode.Created);
            }
        }
    }
}

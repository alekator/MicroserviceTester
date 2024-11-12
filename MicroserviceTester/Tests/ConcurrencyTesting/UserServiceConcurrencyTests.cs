using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using MicroserviceTester.Areas.UserService.Models;
using System.Threading;

namespace MicroserviceTester.Tests.ConcurrencyTesting
{
    public class UserServiceConcurrencyTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public UserServiceConcurrencyTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Concurrent_User_Creation_Should_Handle_Correctly()
        {
            // Arrange
            var user = new User { Id = 900, Username = "ConcurrentUser" };
            var userJson = JsonConvert.SerializeObject(user);

            int successCount = 0;
            int conflictCount = 0;

            // Act
            var tasks = new Task[10];
            for (int i = 0; i < 10; i++)
            {
                tasks[i] = Task.Run(async () =>
                {
                    var userContent = new StringContent(userJson, Encoding.UTF8, "application/json");
                    var response = await _client.PostAsync("/api/Users", userContent);
                    if (response.StatusCode == HttpStatusCode.Created)
                        Interlocked.Increment(ref successCount);
                    else if (response.StatusCode == HttpStatusCode.Conflict)
                        Interlocked.Increment(ref conflictCount);
                });
            }

            await Task.WhenAll(tasks);

            // Assert
            successCount.Should().Be(1);
            conflictCount.Should().Be(9);
        }
    }
}

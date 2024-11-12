using Xunit;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using MicroserviceTester.Areas.OrderService.Models;
using MicroserviceTester.Areas.ProductService.Models;
using MicroserviceTester.Areas.UserService.Models;

namespace MicroserviceTester.Tests.EndToEndTests
{
    public class ApplicationEndToEndTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public ApplicationEndToEndTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task FullScenario_Should_Work_Correctly()
        {
            // Arrange
            var user = new User { Id = 1, Username = "John" };
            var product = new Product { Id = 1, Name = "Laptop" };
            var order = new Order { Id = 1, UserId = 1, ProductId = 1 };

            // Act
            // Создание пользователя
            var userResponse = await _client.PostAsJsonAsync("/api/Users", user);
            Assert.Equal(HttpStatusCode.Created, userResponse.StatusCode);

            // Создание продукта
            var productResponse = await _client.PostAsJsonAsync("/api/Products", product);
            Assert.Equal(HttpStatusCode.Created, productResponse.StatusCode);

            // Создание заказа
            var orderResponse = await _client.PostAsJsonAsync("/api/Orders", order);
            if (orderResponse.StatusCode != HttpStatusCode.Created)
            {
                var errorContent = await orderResponse.Content.ReadAsStringAsync();
                Assert.Equal(HttpStatusCode.Created, orderResponse.StatusCode);

            }
            Assert.Equal(HttpStatusCode.Created, orderResponse.StatusCode);

            // Получение заказа
            var getOrderResponse = await _client.GetAsync($"/api/Orders/{order.Id}");
            Assert.Equal(HttpStatusCode.OK, getOrderResponse.StatusCode);
        }
    }
}

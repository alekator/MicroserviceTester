using MicroserviceTester.Areas.OrderService.Models;
using MicroserviceTester.Areas.ProductService.Models;
using MicroserviceTester.Areas.UserService.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using Xunit;

namespace MicroserviceTester.Tests.IntegrationTests
{
    public class OrdersControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public OrdersControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CreateOrder_Should_Return_Created_And_Order_Should_Be_Retrievable()
        {
            // Arrange
            var user = new User { Id = 1, Username = "OrderUser" };
            var product = new Product { Id = 1, Name = "OrderProduct" };
            var order = new Order { Id = 1, UserId = 1, ProductId = 1 };

            // Создание пользователя
            var userJson = JsonConvert.SerializeObject(user);
            var userContent = new StringContent(userJson, Encoding.UTF8, "application/json");
            var userResponse = await _client.PostAsync("/api/Users", userContent);
            Assert.Equal(HttpStatusCode.Created, userResponse.StatusCode);

            // Создание продукта
            var productJson = JsonConvert.SerializeObject(product);
            var productContent = new StringContent(productJson, Encoding.UTF8, "application/json");
            var productResponse = await _client.PostAsync("/api/Products", productContent);
            Assert.Equal(HttpStatusCode.Created, productResponse.StatusCode);

            // Создание заказа
            var orderJson = JsonConvert.SerializeObject(order);
            var orderContent = new StringContent(orderJson, Encoding.UTF8, "application/json");
            var orderResponse = await _client.PostAsync("/api/Orders", orderContent);
            Assert.Equal(HttpStatusCode.Created, orderResponse.StatusCode);

            // Получение заказа
            var getOrderResponse = await _client.GetAsync("/api/Orders/1");
            Assert.Equal(HttpStatusCode.OK, getOrderResponse.StatusCode);

            var responseString = await getOrderResponse.Content.ReadAsStringAsync();
            var retrievedOrder = JsonConvert.DeserializeObject<Order>(responseString);
            Assert.NotNull(retrievedOrder);
            Assert.Equal(1, retrievedOrder.UserId);
            Assert.Equal(1, retrievedOrder.ProductId);
        }

        [Fact]
        public async Task CreateOrder_With_Nonexistent_User_Should_Return_BadRequest()
        {
            // Arrange
            var product = new Product { Id = 2, Name = "Product2" };
            var order = new Order { Id = 2, UserId = 999, ProductId = 2 };

            // Создание продукта
            var productJson = JsonConvert.SerializeObject(product);
            var productContent = new StringContent(productJson, Encoding.UTF8, "application/json");
            var productResponse = await _client.PostAsync("/api/Products", productContent);
            Assert.Equal(HttpStatusCode.Created, productResponse.StatusCode);

            // Создание заказа с несуществующим пользователем
            var orderJson = JsonConvert.SerializeObject(order);
            var orderContent = new StringContent(orderJson, Encoding.UTF8, "application/json");
            var orderResponse = await _client.PostAsync("/api/Orders", orderContent);
            Assert.Equal(HttpStatusCode.BadRequest, orderResponse.StatusCode);
        }

        [Fact]
        public async Task CreateOrder_With_Nonexistent_Product_Should_Return_BadRequest()
        {
            // Arrange
            var user = new User { Id = 3, Username = "User3" };
            var order = new Order { Id = 3, UserId = 3, ProductId = 999 };

            // Создание пользователя
            var userJson = JsonConvert.SerializeObject(user);
            var userContent = new StringContent(userJson, Encoding.UTF8, "application/json");
            var userResponse = await _client.PostAsync("/api/Users", userContent);
            Assert.Equal(HttpStatusCode.Created, userResponse.StatusCode);

            // Создание заказа с несуществующим продуктом
            var orderJson = JsonConvert.SerializeObject(order);
            var orderContent = new StringContent(orderJson, Encoding.UTF8, "application/json");
            var orderResponse = await _client.PostAsync("/api/Orders", orderContent);
            Assert.Equal(HttpStatusCode.BadRequest, orderResponse.StatusCode);
        }
    }
}

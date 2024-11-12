using MicroserviceTester.Areas.OrderService.Models;
using MicroserviceTester.Areas.ProductService.Models;
using MicroserviceTester.Areas.UserService.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using Xunit;

namespace MicroserviceTester.Tests.EndToEndTests
{
    public class ExtendedApplicationEndToEndTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public ExtendedApplicationEndToEndTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CreateUser_CreateProduct_CreateOrder_VerifyOrder()
        {
            // Arrange
            var user = new User { Id = 10, Username = "E2EUser" };
            var product = new Product { Id = 10, Name = "E2EProduct" };
            var order = new Order { Id = 10, UserId = 10, ProductId = 10 };

            // Act & Assert

            var userJson = JsonConvert.SerializeObject(user);
            var userContent = new StringContent(userJson, Encoding.UTF8, "application/json");
            var userResponse = await _client.PostAsync("/api/Users", userContent);
            Assert.Equal(HttpStatusCode.Created, userResponse.StatusCode);

            var productJson = JsonConvert.SerializeObject(product);
            var productContent = new StringContent(productJson, Encoding.UTF8, "application/json");
            var productResponse = await _client.PostAsync("/api/Products", productContent);
            Assert.Equal(HttpStatusCode.Created, productResponse.StatusCode);

            var orderJson = JsonConvert.SerializeObject(order);
            var orderContent = new StringContent(orderJson, Encoding.UTF8, "application/json");
            var orderResponse = await _client.PostAsync("/api/Orders", orderContent);
            Assert.Equal(HttpStatusCode.Created, orderResponse.StatusCode);

            var getOrderResponse = await _client.GetAsync($"/api/Orders/{order.Id}");
            Assert.Equal(HttpStatusCode.OK, getOrderResponse.StatusCode);
            var orderResponseString = await getOrderResponse.Content.ReadAsStringAsync();
            var retrievedOrder = JsonConvert.DeserializeObject<Order>(orderResponseString);
            Assert.NotNull(retrievedOrder);
            Assert.Equal(order.Id, retrievedOrder.Id);
            Assert.Equal(order.UserId, retrievedOrder.UserId);
            Assert.Equal(order.ProductId, retrievedOrder.ProductId);

            var getOrdersResponse = await _client.GetAsync("/api/Orders");
            Assert.Equal(HttpStatusCode.OK, getOrdersResponse.StatusCode);
            var ordersListString = await getOrdersResponse.Content.ReadAsStringAsync();
            var ordersList = JsonConvert.DeserializeObject<List<Order>>(ordersListString);
            Assert.Contains(ordersList, o => o.Id == order.Id);
        }

        [Fact]
        public async Task DeleteUser_Should_Not_Affect_Existing_Orders()
        {
            var user = new User { Id = 20, Username = "DeleteUser" };
            var product = new Product { Id = 20, Name = "DeleteProduct" };
            var order = new Order { Id = 20, UserId = 20, ProductId = 20 };

            var userJson = JsonConvert.SerializeObject(user);
            var userContent = new StringContent(userJson, Encoding.UTF8, "application/json");
            var userResponse = await _client.PostAsync("/api/Users", userContent);
            Assert.Equal(HttpStatusCode.Created, userResponse.StatusCode);

            var productJson = JsonConvert.SerializeObject(product);
            var productContent = new StringContent(productJson, Encoding.UTF8, "application/json");
            var productResponse = await _client.PostAsync("/api/Products", productContent);
            Assert.Equal(HttpStatusCode.Created, productResponse.StatusCode);

            var orderJson = JsonConvert.SerializeObject(order);
            var orderContent = new StringContent(orderJson, Encoding.UTF8, "application/json");
            var orderResponse = await _client.PostAsync("/api/Orders", orderContent);
            Assert.Equal(HttpStatusCode.Created, orderResponse.StatusCode);

            var deleteUserResponse = await _client.DeleteAsync($"/api/Users/{user.Id}");
            Assert.Equal(HttpStatusCode.NoContent, deleteUserResponse.StatusCode);

            var getOrderResponse = await _client.GetAsync($"/api/Orders/{order.Id}");
            Assert.Equal(HttpStatusCode.OK, getOrderResponse.StatusCode);
        }
    }
}

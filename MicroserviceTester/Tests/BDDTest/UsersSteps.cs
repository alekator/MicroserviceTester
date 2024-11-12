using TechTalk.SpecFlow;
using Xunit;
using System.Net;
using Newtonsoft.Json;
using System.Text;
using FluentAssertions;
using MicroserviceTester.Areas.OrderService.Models;
using MicroserviceTester.Areas.ProductService.Models;
using MicroserviceTester.Areas.UserService.Models;
using MicroserviceTester.Areas.OrderService.Services;
using MicroserviceTester.Areas.ProductService.Services;
using MicroserviceTester.Areas.UserService.Services;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace MicroserviceTester.Tests.BDDTest
{
    [Binding]
    public class UsersSteps : IDisposable
    {
        private readonly HttpClient _client;
        private readonly IUserService _userService;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private HttpResponseMessage _response;
        private Order _retrievedOrder;

        public UsersSteps(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
            _userService = factory.Services.GetService(typeof(IUserService)) as IUserService;
            _productService = factory.Services.GetService(typeof(IProductService)) as IProductService;
            _orderService = factory.Services.GetService(typeof(IOrderService)) as IOrderService;
        }

        [Given(@"a user with ID (\d+) exists")]
        public async Task GivenAUserWithIDExists(int id)
        {
            var user = new User { Id = id, Username = "BDDUser" };
            var userJson = JsonConvert.SerializeObject(user);
            var content = new StringContent(userJson, Encoding.UTF8, "application/json");
            _response = await _client.PostAsync("/api/Users", content);
            _response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Given(@"a product with ID (\d+) exists")]
        public async Task GivenAProductWithIDExists(int id)
        {
            var product = new Product { Id = id, Name = "BDDProduct" };
            var productJson = JsonConvert.SerializeObject(product);
            var content = new StringContent(productJson, Encoding.UTF8, "application/json");
            _response = await _client.PostAsync("/api/Products", content);
            _response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Given(@"an order with ID (\d+) exists linking user (\d+) and product (\d+)")]
        public async Task GivenAnOrderWithIDExistsLinkingUserAndProduct(int orderId, int userId, int productId)
        {
            var order = new Order { Id = orderId, UserId = userId, ProductId = productId };
            var orderJson = JsonConvert.SerializeObject(order);
            var content = new StringContent(orderJson, Encoding.UTF8, "application/json");
            _response = await _client.PostAsync("/api/Orders", content);
            _response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [When(@"I delete the user with ID (\d+)")]
        public async Task WhenIDeleteTheUserWithID(int id)
        {
            _response = await _client.DeleteAsync($"/api/Users/{id}");
        }

        [Then(@"the order with ID (\d+) should still exist")]
        public async Task ThenTheOrderWithIDShouldStillExist(int orderId)
        {
            var getResponse = await _client.GetAsync($"/api/Orders/{orderId}");
            getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var orderJson = await getResponse.Content.ReadAsStringAsync();
            _retrievedOrder = JsonConvert.DeserializeObject<Order>(orderJson);
            _retrievedOrder.Should().NotBeNull();
            _retrievedOrder.Id.Should().Be(orderId);
        }

        public void Dispose()
        {
            _userService?.ClearAllUsers();
            _productService?.ClearAllProducts();
            _orderService?.ClearAllOrders();
        }
    }
}

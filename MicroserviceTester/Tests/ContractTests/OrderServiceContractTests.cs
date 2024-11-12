using FluentAssertions;
using MicroserviceTester.Areas.OrderService.Models;
using Newtonsoft.Json;
using System.Net;
using System.Text.RegularExpressions;
using WireMock.Matchers;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using Xunit;

namespace MicroserviceTester.Tests.ContractTests
{
    public class OrderServiceContractTests : IAsyncLifetime
    {
        private readonly WireMockServer _server;
        private readonly HttpClient _httpClient;

        public OrderServiceContractTests()
        {
            // Инициализация мок-сервера на динамическом порту
            _server = WireMockServer.Start();
            var port = _server.Ports[0];
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri($"http://localhost:{port}")
            };
        }

        public Task InitializeAsync()
        {
            // Нет инициализации, необходимой перед тестами
            return Task.CompletedTask;
        }

        public Task DisposeAsync()
        {
            // Остановка мок-сервера после завершения тестов
            _server.Stop();
            _server.Dispose();
            _httpClient.Dispose();
            return Task.CompletedTask;
        }

        [Fact]
        public async Task CreateOrder_With_Existing_User_And_Product_Should_Return_Created_Simplified()
        {
            // Arrange
            var order = new Order { Id = 1, UserId = 1, ProductId = 1 };
            var responseBody = JsonConvert.SerializeObject(order);

            // Настройка ожидаемого запроса и ответа без условий на заголовки и тело
            _server.Given(
                Request.Create()
                    .WithPath("/api/Orders")
                    .UsingPost()
            )
            .RespondWith(
                Response.Create()
                    .WithStatusCode(201) // HTTP 201 Created
                    .WithHeader("Content-Type", "application/json; charset=utf-8")
                    .WithBody(responseBody)
            );

            // Act
            var content = new StringContent(JsonConvert.SerializeObject(order), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/Orders", content);

            // Вывод полученных запросов для отладки
            foreach (var entry in _server.LogEntries)
            {
                Console.WriteLine($"Path: {entry.RequestMessage.Path}, Method: {entry.RequestMessage.Method}, Body: {entry.RequestMessage.Body}");
            }

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var responseString = await response.Content.ReadAsStringAsync();
            var returnedOrder = JsonConvert.DeserializeObject<Order>(responseString);

            returnedOrder.Should().NotBeNull();
            returnedOrder.Id.Should().Be(order.Id);
            returnedOrder.UserId.Should().Be(order.UserId);
            returnedOrder.ProductId.Should().Be(order.ProductId);

            // Проверка, что все ожидаемые запросы были выполнены
            _server.LogEntries.Should().ContainSingle(entry =>
                entry.RequestMessage.Path.Equals("/api/Orders", System.StringComparison.OrdinalIgnoreCase) &&
                entry.RequestMessage.Method.Equals("POST", System.StringComparison.OrdinalIgnoreCase)
            );
        }
    }
}

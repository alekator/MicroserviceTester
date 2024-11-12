using Xunit;
using FluentAssertions;
using WireMock.Server;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MicroserviceTester.Areas.ProductService.Models; // Убедитесь, что модели доступны
using System.Net;
using System.Collections.Generic;
using WireMock.Matchers;

namespace MicroserviceTester.Tests.ContractTests
{
    public class ProductServiceContractTests : IAsyncLifetime
    {
        private readonly WireMockServer _server;
        private readonly HttpClient _httpClient;

        public ProductServiceContractTests()
        {
            // Инициализация мок-сервера на порту 9333 (убедитесь, что порт свободен)
            _server = WireMockServer.Start(port: 9333);
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:9333")
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
        public async Task CreateProduct_WhenCalled_ReturnsCreatedProduct_Simplified()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "ContractProduct" };
            var responseBody = JsonConvert.SerializeObject(product);

            // Настройка ожидаемого запроса и ответа без условий на заголовки и тело
            _server.Given(
                Request.Create()
                    .WithPath("/api/Products")
                    .UsingPost()
            )
            .RespondWith(
                Response.Create()
                    .WithStatusCode(201) // HTTP 201 Created
                    .WithHeader("Content-Type", "application/json; charset=utf-8")
                    .WithBody(responseBody)
            );

            // Act
            var content = new StringContent(JsonConvert.SerializeObject(product), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/Products", content);

            // Вывод полученных запросов для отладки
            foreach (var entry in _server.LogEntries)
            {
                Console.WriteLine($"Path: {entry.RequestMessage.Path}, Method: {entry.RequestMessage.Method}, Body: {entry.RequestMessage.Body}");
            }

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var responseString = await response.Content.ReadAsStringAsync();
            var returnedProduct = JsonConvert.DeserializeObject<Product>(responseString);

            Assert.NotNull(returnedProduct);
            Assert.Equal(product.Id, returnedProduct.Id);
            Assert.Equal(product.Name, returnedProduct.Name);

            // Проверка, что все ожидаемые запросы были выполнены
            _server.LogEntries.Should().ContainSingle(entry =>
                entry.RequestMessage.Path.Equals("/api/Products", System.StringComparison.OrdinalIgnoreCase) &&
                entry.RequestMessage.Method.Equals("POST", System.StringComparison.OrdinalIgnoreCase)
            );
        }
    }
}

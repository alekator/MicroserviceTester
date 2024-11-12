using MicroserviceTester.Areas.ProductService.Models;
using MicroserviceTester.Areas.ProductService.Services;
using Xunit;

namespace MicroserviceTester.Tests.UnitTests
{
    public class ProductServiceTests
    {
        private readonly ProductService _productService;

        public ProductServiceTests()
        {
            _productService = new ProductService();
        }

        [Fact]
        public void AddProduct_Should_Add_Product_To_List()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "TestProduct" };

            // Act
            _productService.AddProduct(product);

            // Assert
            var retrievedProduct = _productService.GetProduct(1);
            Assert.NotNull(retrievedProduct);
            Assert.Equal("TestProduct", retrievedProduct.Name);
        }

        [Fact]
        public void GetProduct_Should_Return_Null_If_Product_Does_Not_Exist()
        {
            // Act
            var product = _productService.GetProduct(999);

            // Assert
            Assert.Null(product);
        }

        [Fact]
        public void ProductExists_Should_Return_True_If_Product_Exists()
        {
            // Arrange
            var product = new Product { Id = 2, Name = "ExistingProduct" };
            _productService.AddProduct(product);

            // Act
            var exists = _productService.ProductExists(2);

            // Assert
            Assert.True(exists);
        }

        [Fact]
        public void ProductExists_Should_Return_False_If_Product_Does_Not_Exist()
        {
            // Act
            var exists = _productService.ProductExists(3);

            // Assert
            Assert.False(exists);
        }
    }
}

using MicroserviceTester.Areas.OrderService.Models;
using MicroserviceTester.Areas.OrderService.Services;
using MicroserviceTester.Areas.ProductService.Services;
using MicroserviceTester.Areas.UserService.Services;
using Moq;
using Xunit;

namespace MicroserviceTester.Tests.UnitTests
{
    public class OrderServiceTests
    {
        private readonly OrderService _orderService;
        private readonly Mock<IUserService> _userServiceMock;
        private readonly Mock<IProductService> _productServiceMock;

        public OrderServiceTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _productServiceMock = new Mock<IProductService>();
            _orderService = new OrderService(_userServiceMock.Object, _productServiceMock.Object);
        }

        [Fact]
        public void AddOrder_Should_Add_Order_To_List()
        {
            // Arrange
            var order = new Order { Id = 1, UserId = 1, ProductId = 1 };
            _userServiceMock.Setup(us => us.UserExists(1)).Returns(true);
            _productServiceMock.Setup(ps => ps.ProductExists(1)).Returns(true);

            // Act
            _orderService.AddOrder(order);

            // Assert
            var retrievedOrder = _orderService.GetOrder(1);
            Assert.NotNull(retrievedOrder);
            Assert.Equal(1, retrievedOrder.UserId);
            Assert.Equal(1, retrievedOrder.ProductId);
        }

        [Fact]
        public void GetOrder_Should_Return_Null_If_Order_Does_Not_Exist()
        {
            // Act
            var order = _orderService.GetOrder(999);

            // Assert
            Assert.Null(order);
        }

        [Fact]
        public void OrderExists_Should_Return_True_If_Order_Exists()
        {
            // Arrange
            var order = new Order { Id = 2, UserId = 2, ProductId = 2 };
            _userServiceMock.Setup(us => us.UserExists(2)).Returns(true);
            _productServiceMock.Setup(ps => ps.ProductExists(2)).Returns(true);
            _orderService.AddOrder(order);

            // Act
            var exists = _orderService.OrderExists(2);

            // Assert
            Assert.True(exists);
        }

        [Fact]
        public void OrderExists_Should_Return_False_If_Order_Does_Not_Exist()
        {
            // Act
            var exists = _orderService.OrderExists(3);

            // Assert
            Assert.False(exists);
        }
    }
}

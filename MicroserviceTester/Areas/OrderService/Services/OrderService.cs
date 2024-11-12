using MicroserviceTester.Areas.OrderService.Models;
using MicroserviceTester.Areas.ProductService.Services;
using MicroserviceTester.Areas.UserService.Services;
using System.Collections.Generic;
using System.Linq;

namespace MicroserviceTester.Areas.OrderService.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUserService _userService;
        private readonly IProductService _productService;
        private readonly List<Order> _orders = new();

        public OrderService(IUserService userService, IProductService productService)
        {
            _userService = userService;
            _productService = productService;
        }

        public void AddOrder(Order order)
        {
            if (_userService.UserExists(order.UserId) && _productService.ProductExists(order.ProductId))
            {
                _orders.Add(order);
            }
            else
            {
                throw new InvalidOperationException("User or Product does not exist.");
            }
        }

        public Order? GetOrder(int id)
        {
            return _orders.FirstOrDefault(o => o.Id == id);
        }

        public bool OrderExists(int id)
        {
            return _orders.Any(o => o.Id == id);
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _orders;
        }
    }
}

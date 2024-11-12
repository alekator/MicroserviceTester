using MicroserviceTester.Areas.OrderService.Models;
using MicroserviceTester.Areas.OrderService.Services;
using MicroserviceTester.Areas.ProductService.Services;
using MicroserviceTester.Areas.UserService.Services;
using Microsoft.AspNetCore.Mvc;

namespace MicroserviceTester.Areas.OrderService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        private readonly IProductService _productService;

        public OrdersController(
            IOrderService orderService,
            IUserService userService,
            IProductService productService)
        {
            _orderService = orderService;
            _userService = userService;
            _productService = productService;
        }

        // GET: api/Orders
        [HttpGet]
        public IActionResult GetOrders()
        {
            var orders = _orderService.GetAllOrders();
            return Ok(orders);
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public IActionResult GetOrder(int id)
        {
            var order = _orderService.GetOrder(id);
            return order != null ? Ok(order) : NotFound();
        }

        // POST: api/Orders
        [HttpPost]
        public IActionResult CreateOrder([FromBody] Order order)
        {
            if (!_userService.UserExists(order.UserId))
            {
                return BadRequest("User does not exist.");
            }

            if (!_productService.ProductExists(order.ProductId))
            {
                return BadRequest("Product does not exist.");
            }

            _orderService.AddOrder(order);
            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
        }
    }
}

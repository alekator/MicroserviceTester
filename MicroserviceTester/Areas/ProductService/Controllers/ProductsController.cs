using MicroserviceTester.Areas.ProductService.Models;
using MicroserviceTester.Areas.ProductService.Services;
using Microsoft.AspNetCore.Mvc;

namespace MicroserviceTester.Areas.ProductService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/Products
        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _productService.GetAllProducts();
            return Ok(products);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = _productService.GetProduct(id);
            return product != null ? Ok(product) : NotFound();
        }

        // POST: api/Products
        [HttpPost]
        public IActionResult CreateProduct([FromBody] Product product)
        {
            if (_productService.ProductExists(product.Id))
            {
                return Conflict("Product with this ID already exists.");
            }

            _productService.AddProduct(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }
    }
}

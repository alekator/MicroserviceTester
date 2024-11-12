using MicroserviceTester.Areas.ProductService.Models;
using System.Collections.Generic;
using System.Linq;

namespace MicroserviceTester.Areas.ProductService.Services
{
    public class ProductService : IProductService
    {
        private readonly List<Product> _products = new();

        public void AddProduct(Product product)
        {
            _products.Add(product);
        }

        public Product? GetProduct(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        public bool ProductExists(int id)
        {
            return _products.Any(p => p.Id == id);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _products;
        }
    }
}

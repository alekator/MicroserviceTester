using MicroserviceTester.Areas.ProductService.Models;
using System.Collections.Generic;

namespace MicroserviceTester.Areas.ProductService.Services
{
    public interface IProductService
    {
        void AddProduct(Product product);
        Product? GetProduct(int id);
        bool ProductExists(int id);
        void ClearAllProducts();
        IEnumerable<Product> GetAllProducts();
    }
}

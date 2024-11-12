using MicroserviceTester.Areas.OrderService.Models;
using System.Collections.Generic;

namespace MicroserviceTester.Areas.OrderService.Services
{
    public interface IOrderService
    {
        void AddOrder(Order order);
        Order? GetOrder(int id);
        bool OrderExists(int id);
        void ClearAllOrders();
        IEnumerable<Order> GetAllOrders();
    }
}

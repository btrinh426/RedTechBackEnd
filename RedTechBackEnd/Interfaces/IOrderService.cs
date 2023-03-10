using Microsoft.AspNetCore.Mvc;
using RedTechBackEnd.Models;

namespace RedTechBackEnd.Interfaces
{
    public interface IOrderService
    {
        public Task<ActionResult<IEnumerable<Order>>> GetOrders();

        public Task<ActionResult<Order>> GetOrder(Guid id);

        public void PutOrder(Guid id, Order order);

        public Task<int> PostOrder(Order order);

        public Task DeleteOrder(Guid id);
    }
}

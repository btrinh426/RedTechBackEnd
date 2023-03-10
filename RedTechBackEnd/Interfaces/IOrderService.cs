using Microsoft.AspNetCore.Mvc;
using RedTechBackEnd.Models;

namespace RedTechBackEnd.Interfaces
{
    public interface IOrderService
    {

        ICollection<Order> GetOrders();

        Order GetOrder(Guid id);
        bool OrderExists(Guid id);

        bool UpdateOrder(Order order);

        bool CreateOrder(Order order);

        bool DeleteOrder(Order order);
    }
}

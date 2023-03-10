using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using RedTechBackEnd.Dto;
using RedTechBackEnd.Interfaces;
using RedTechBackEnd.Models;

namespace RedTechBackEnd.Services
{
    public class OrderService : IOrderService
    {
        private readonly RedTechDbContext _context;
        public OrderService(RedTechDbContext context)
        {
            _context = context;
        }

        public ICollection<Order> GetOrders()
        {
            return _context.Orders.OrderBy(o => o.Id).ToList();
        }

        public Order GetOrder(Guid id)
        {
            return _context.Orders.Where(o => o.Id == id).FirstOrDefault();
        }

        public bool OrderExists(Guid id)
        {
            return _context.Orders.Any(o => o.Id == id);
        }
        
        public async Task<IEnumerable<Order>> Search(string id)
        {
            IQueryable<Order> query = _context.Orders;

            if (!string.IsNullOrEmpty(id))
            {
                query = query.Where(o => o.Id.ToString() == id);
            }
            return await query.ToListAsync();
        }

        public bool UpdateOrder(Order order)
        {
            _context.Update(order);
            return Save();
        }

        public bool DeleteOrder(Order order)
        {
            _context.Remove(order);
            return Save();
        }

        public bool CreateOrder(Order order)
        {
            _context.Orders.Add(order);

            return Save();
        }

        private bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}

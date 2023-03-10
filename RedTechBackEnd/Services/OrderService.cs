using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
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

        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<ActionResult<Order>> GetOrder(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                throw new Exception("not found");
            }

            return order;
        }

        public void PutOrder(Guid id, Order order)
        {
            _context.Entry(order).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    throw NotFound();
                }
                else
                {
                    throw;
                }
            }

        }

        public async Task DeleteOrder(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);

            if(order == null)
            {
                throw NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

        }

        public async Task<int> PostOrder(Order order)
        {
            _context.Orders.Add(order);
            return await _context.SaveChangesAsync();
        }

        private Exception NotFound()
        {
            throw new NotImplementedException();
        }

        private Task<IActionResult> NoContent()
        {
            throw new NotImplementedException();
        }


        private bool OrderExists(Guid id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }


    }
}

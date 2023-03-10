using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RedTechBackEnd.Interfaces;
using RedTechBackEnd.Models;

namespace RedTechBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderService _service = null;

        public OrderController(IOrderService service)
        {
            _service = service;
        }


        // GET: api/Order
        [AllowAnonymous]
        [HttpGet]
        public Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return _service.GetOrders();

        }

        // GET: api/Order/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public Task<ActionResult<Order>> GetOrders(Guid id)
        {

            return _service.GetOrder(id);
        }

        // PUT: api/Order/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [AllowAnonymous]
        [HttpPut("{id}")]
        public StatusCodeResult PutOrder(Guid id, Order order)
        {
            StatusCodeResult response = null;

            if (id != order.Id)
            {
                throw BadRequest();
            }

            try
            {
                _service.PutOrder(id, order);
                response = NoContent();
            }
            catch
            {
                throw;
            }

            return response;

        }

        // POST: api/Order
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [AllowAnonymous]
        [HttpPost]
        public ActionResult<Order> PostOrder(Order order)
        {
            ObjectResult result = null;
            try
            {
                _service.PostOrder(order);
                ActionResult response = CreatedAtAction("GetOrder", new { id = order.Id }, order);
                result = StatusCode(201, response);
            }
            catch
            {
                throw;
            }
            return result;
        }

        // DELETE: api/Order/5
        [AllowAnonymous]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {

            await _service.DeleteOrder(id);

            return NoContent();
        }

        private Exception BadRequest()
        {
            throw new NotImplementedException();
        }

    }
}

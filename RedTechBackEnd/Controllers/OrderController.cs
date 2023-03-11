using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using RedTechBackEnd.Dto;
using RedTechBackEnd.Interfaces;
using RedTechBackEnd.Models;

namespace RedTechBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service = null;
        private readonly IMapper _mapper;

        public OrderController(IOrderService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
            
        }


        // GET: api/Order
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Order>))]
        public IActionResult GetOrders()
        {
                
            var orders = _service.GetOrders();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(orders);

        }

        //GET: api/Order/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Order))]
        [ProducesResponseType(400)]
        public IActionResult GetOrder(Guid id)
        {
            if (!_service.OrderExists(id))
                return NotFound();

            var order = _mapper.Map<Order>(_service.GetOrder(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(order);
        }


        // PUT: api/Order/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [AllowAnonymous]
        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateOrder(Guid id, [FromBody] OrderDto updatedOrder)
        {
            if (updatedOrder == null)
                return BadRequest(ModelState);

            if (id != updatedOrder.Id)
                return BadRequest("Id does not match");

            if (!_service.OrderExists(id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var orderMap = _mapper.Map<Order>(updatedOrder);

            if (!_service.UpdateOrder(orderMap))
            {
                ModelState.AddModelError("", "Something went wrong updating order");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        // POST: api/Order
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateOrder(Order order)
        {
            if(order == null)
                return BadRequest(ModelState);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var orderMap = _mapper.Map<Order>(order);

            if(!_service.CreateOrder(order))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully created");
        }

        // DELETE: api/Order/5
        [AllowAnonymous]
        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteOrder(Guid id)
        {

            if (!_service.OrderExists(id))
            {
                return NotFound();
            }

            var orderToDelete = _service.GetOrder(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_service.DeleteOrder(orderToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting order");
            }

            return NoContent();
        }

        //[AllowAnonymous]
        //[HttpDelete("{id}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        //public IActionResult DeleteOrders(IEnumerable<Guid> listIds)
        //{
        //    var delList = _service.DeleteOrders(listIds);
        //    if(delList == null)
        //    {
        //        return BadRequest(404);
        //    }

        //    return RedirectToAction("Index");
        //}

        //// DELETE: multiple Ids
        //[AllowAnonymous]
        //[HttpDelete("{id}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(404)]
        //public IActionResult DeleteOrders(List<Guid> ids)
        //{
        //    List<Guid> orderIds = new List<Guid>();
            
        //    foreach (var id in ids)
        //    {
        //        orderIds.Add(id);
        //    }

        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    if (!_service.DeleteOrder(orderToDelete))
        //    {
        //        ModelState.AddModelError("", "Something went wrong deleting order");
        //    }

        //    return NoContent();
        //}



        [HttpGet("(search)")]
        public async Task<ActionResult<IEnumerable<Order>>> Search(string id)
        {
            try
            {
                var result = await _service.Search(id);

                if (result.Any())
                {
                    return Ok(result);
                }

                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
            }
        }

    }
}

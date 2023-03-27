using Marlin.sqlite.Data;
using Marlin.sqlite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Marlin.sqlite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderStatusHistoryController : ControllerBase
    {
        private readonly DataContext _context;

        public OrderStatusHistoryController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]

        public async Task<ActionResult<List<OrderStatusHistory>>> AddOrder(OrderStatusHistory order)
        {
            _context.OrderStatusHistory.Add(order);
            await _context.SaveChangesAsync();


            return Ok(await _context.OrderStatusHistory.ToListAsync());
        }


        [HttpGet]
        public async Task<ActionResult<List<OrderStatusHistory>>> GetOrders()
        {
            return Ok(await _context.OrderStatusHistory.ToListAsync());
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<List<OrderStatusHistory>>> GetOrder(int id)
        {
            var order = await _context.OrderStatusHistory.FindAsync(id);
            if (order == null)
            {
                return BadRequest("Order not found.");
            }
            return Ok(order);
        }
    }
}

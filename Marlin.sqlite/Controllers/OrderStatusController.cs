using Marlin.sqlite.Data;
using Marlin.sqlite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Marlin.sqlite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderStatusController : ControllerBase
    {
        private readonly DataContext _context;

        public OrderStatusController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]

        public async Task<ActionResult<List<OrderStatus>>> AddOrder(OrderStatus order)
        {
            _context.OrderStatus.Add(order);
            await _context.SaveChangesAsync();


            return Ok(await _context.OrderStatus.ToListAsync());
        }


        [HttpGet]
        public async Task<ActionResult<List<OrderStatus>>> GetOrders()
        {
            return Ok(await _context.OrderStatus.ToListAsync());
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<List<OrderStatus>>> GetOrder(int id)
        {
            var order = await _context.OrderStatus.FindAsync(id);
            if (order == null)
            {
                return BadRequest("Order not found.");
            }
            return Ok(order);
        }
    }
}
